#region USING
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using MEC;
#endregion

public class GameScene : MonoBehaviour
{
    #region VARIABLE
    [HideInInspector] public Text pGameScoreText = null;
    [HideInInspector] public Text pGameMaxScoreText = null;
    [HideInInspector] public GameObject pMovie = null;
    [HideInInspector] public GameObject pPhaseTimeText = null;
    [HideInInspector] public GameObject pPhaseHelpText = null;
    [HideInInspector] public GameObject pPanel = null;

    private Image pImage = null;
    private UI_Main pUIMain = null;
    private GameObject pPhase = null;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        GameManager.Instance.Init(ESceneType.Type_GameScene);
        UIManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();

        BulletManager.Instance.Init();
        EnemyManager.Instance.Init();
        // ItemManager.Instance.Init();
    }
    void Start()
    {
        Timing.KillCoroutines();
        UIManager.Instance.pUIDictionary.Clear();

        GameManager.Instance.iCurrentLife = GlobalData.iStartingLife;
        GameManager.Instance.iCurrentBomb = GlobalData.iStartingBomb;
        GameManager.Instance.iScore = 0;
        GameManager.Instance.iTempScore = 0;
        GameManager.Instance.iMissCount = 0;
        GameManager.Instance.iBombCount = 0;
        GameManager.Instance.iDestroyCount = 0;
        GameManager.Instance.iPhase = 0;
        GameManager.Instance.iWaitingCount = 0;
        GameManager.Instance.fElapsedTime = 0.0f;
        GameManager.Instance.fPhaseTime = 0.0f;
        GameManager.Instance.bRunPhase = false;
        GameManager.Instance.bCheckEndTiming = false;
        GameManager.Instance.bWarningTime = false;
        GameManager.Instance.bAllKill = false;
        GameManager.Instance.bWaiting = false;
        GameManager.Instance.bGameOver = false;
        GameManager.Instance.enSceneType = ESceneType.Type_GameScene;
        GameManager.Instance.pGameScene = this;
        // For GameScene Testing
        if (GameManager.Instance.enGameDifficultyType.Equals(EGameDifficultyType.None)) GameManager.Instance.enGameDifficultyType = EGameDifficultyType.Type_Hard;

        switch (GameManager.Instance.enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                GameManager.Instance.iMaxScore = GlobalData.pEasyRanking[0].iTopScore;
                break;
            case EGameDifficultyType.Type_Normal:
                GameManager.Instance.iMaxScore = GlobalData.pNormalRanking[0].iTopScore;
                break;
            case EGameDifficultyType.Type_Hard:
                GameManager.Instance.iMaxScore = GlobalData.pHardRanking[0].iTopScore;
                break;
            default:
                break;
        }

        GameManager.Instance.SetPlayerInfo();
        SoundManager.Instance.PlayBGM(EBGMType.Type_BGM02_Phase1_10, true);
        Timing.RunCoroutine(Waiting(2.5f));

        pMovie = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIMovie", EUIType.Type_RawImage, EUICanvasType.Type_BGCanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMovie.SetActive(true);
        RawImage pRawImage = pMovie.GetComponent<RawImage>();
        pRawImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pRawImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pRawImage.rectTransform.localScale = new Vector3(2.2f, 1.8f, 1.0f);
        pRawImage.texture = UIManager.Instance.pUITextureArray[11];
        pRawImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        VideoPlayer pVideoPlayer = pMovie.GetComponent<VideoPlayer>();
        pVideoPlayer.clip = UIManager.Instance.pUIVideoArray[0];
        pVideoPlayer.targetTexture = UIManager.Instance.pUITextureArray[11] as RenderTexture;
        pVideoPlayer.isLooping = true;
        pUIMain = pMovie.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIMovie_Alpha", 0.125f, 0.0f, 0.0f, 0.5f);

        GameObject pGameScoreText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIGameScoreText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameScoreText.SetActive(true);
        Text pText = pGameScoreText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(-300.0f, 490.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = "SCORE";
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameScoreText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIGameScoreText_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pGameScore = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIGameScore", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameScore.SetActive(true);
        pText = pGameScore.GetComponent<Text>();
        this.pGameScoreText = pText;
        pText.rectTransform.anchoredPosition = new Vector2(-300.0f, 460.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GameManager.Instance.iScore.ToString();
        pText.fontSize = 24;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameScore.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIGameScore_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pGameMaxScoreText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIGameMaxScoreText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameMaxScoreText.SetActive(true);
        pText = pGameMaxScoreText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(300.0f, 490.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = "MAX SCORE";
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameMaxScoreText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIGameMaxScoreText_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pGameMaxScore = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIGameMaxScore", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameMaxScore.SetActive(true);
        pText = pGameMaxScore.GetComponent<Text>();
        this.pGameMaxScoreText = pText;
        pText.rectTransform.anchoredPosition = new Vector2(300.0f, 460.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GameManager.Instance.iMaxScore.ToString();
        pText.fontSize = 24;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameMaxScore.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIGameMaxScore_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pPhaseText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIPhaseText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPhaseText.SetActive(true);
        pText = pPhaseText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 485.0f);
        pText.rectTransform.sizeDelta = new Vector2(200.0f, 40.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = "PHASE";
        pText.fontSize = 32;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pPhaseText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIPhaseText_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        pPhase = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIPhase", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPhase.SetActive(true);
        pText = pPhase.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 455.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GameManager.Instance.iPhase.ToString();
        pText.fontSize = 30;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pPhase.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIPhase_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pPhaseTimeText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIPhaseTimeText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPhaseTimeText.SetActive(true);
        pText = pPhaseTimeText.GetComponent<Text>();
        this.pPhaseTimeText = pPhaseTimeText;
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -485.0f);
        pText.rectTransform.sizeDelta = new Vector2(200.0f, 40.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = "0";
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pPhaseTimeText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIPhaseTimeText_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        GameObject pPhaseHelpText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIPhaseHelpText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pText = pPhaseHelpText.GetComponent<Text>();
        this.pPhaseHelpText = pPhaseHelpText;
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 425.0f);
        pText.rectTransform.sizeDelta = new Vector2(800.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "페이즈를 10번 클리어했습니다! 패턴이 변화합니다.";
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pPhaseHelpText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);

        for (int i = 1; i <= 6; ++i)
        {
            GameObject pPlayerLife = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Game_UIPlayerLife_{0}", i), EUIType.Type_Image, EUICanvasType.Type_UICanvas,
                new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            if (i > GlobalData.iStartingLife) pPlayerLife.SetActive(false);
            else pPlayerLife.SetActive(true);
            pImage = pPlayerLife.GetComponent<Image>();
            pImage.rectTransform.anchoredPosition = new Vector2(-494.0f + (40.0f * (i - 1)), -480.0f);
            pImage.rectTransform.sizeDelta = new Vector2(32.0f, 48.0f);
            pImage.sprite = GameManager.Instance.pPlayerSpriteArray[(int)EPlayerSpriteType.Type_Player_Life];
            pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            pUIMain = pPlayerLife.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Game_UIPlayerLife_{0}_Alpha", i), 0.25f, 0.5f, 0.0f, 1.0f);
        }
        for (int i = 1; i <= 6; ++i)
        {
            GameObject pPlayerBomb = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Game_UIPlayerBomb_{0}", i), EUIType.Type_Image, EUICanvasType.Type_UICanvas,
                new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            if (i > GlobalData.iStartingBomb) pPlayerBomb.SetActive(false);
            else pPlayerBomb.SetActive(true);
            pImage = pPlayerBomb.GetComponent<Image>();
            pImage.rectTransform.anchoredPosition = new Vector2(494.0f - (40.0f * (i - 1)), -480.0f);
            pImage.rectTransform.sizeDelta = new Vector2(32.0f, 48.0f);
            pImage.sprite = GameManager.Instance.pPlayerSpriteArray[(int)EPlayerSpriteType.Type_Player_Bomb];
            pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            pUIMain = pPlayerBomb.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Game_UIPlayerBomb_{0}_Alpha", i), 0.25f, 0.5f, 0.0f, 1.0f);
        }

        pPanel = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Game_UIPanel", EUIType.Type_Panel, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPanel.SetActive(true);
        pImage = pPanel.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        pUIMain = pPanel.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Game_UIPanel_Alpha", -0.2f, 0.5f, 1.0f, 0.0f);
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> Waiting(float fDelayTime)
    {
        yield return Timing.WaitForSeconds(fDelayTime);

        Timing.RunCoroutine(ChangePhase());

        yield break;
    }
    public IEnumerator<float> ChangePhase()
    {
        GameManager.Instance.ClearBullet();
        GameManager.Instance.iPhase++;
        pPhase.GetComponent<Text>().text = GameManager.Instance.iPhase.ToString();
        pPhase.GetComponent<UI_Main>().RunDelegateUIScale("Game_UIPhase_Scale", new Vector2(-0.2f, -0.2f), new Vector2(pPhase.transform.localScale.x * 2.5f, pPhase.transform.localScale.y * 2.5f), Vector2.one);

        if (GameManager.Instance.iPhase.Equals(1))
        {
            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_PhaseUp);

            yield return Timing.WaitForSeconds(1.5f);
        }
        else if (GameManager.Instance.iPhase > 1)
        {
            GameManager.Instance.iTempScore += (1000 * (GameManager.Instance.iCurrentLife + GameManager.Instance.iCurrentBomb));
            if ((GameManager.Instance.iPhase % 10).Equals(1))
            {
                Timing.RunCoroutine(ChangeTextScale(), "Game_PhaseChange");
                // Timing.RunCoroutine(ChangeBackgroundColor(), "Game_BackgroundColorChange");
                Timing.RunCoroutine(SoundManager.Instance.ChangeBGM((EBGMType)((int)EBGMType.Type_BGM02_Phase1_10 + (GameManager.Instance.iPhase / 10)), true), "Game_BGMChange");

                switch (GameManager.Instance.enGameDifficultyType)
                {
                    case EGameDifficultyType.Type_Easy:
                        if ((GameManager.Instance.iPhase % 10).Equals(1))
                        {
                            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Bonus);
                            GameManager.Instance.PlayerBonus();
                        }
                        else SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                        break;
                    case EGameDifficultyType.Type_Normal:
                        if ((GameManager.Instance.iPhase % 20).Equals(1))
                        {
                            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Bonus);
                            GameManager.Instance.PlayerBonus();
                        }
                        else SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                        break;
                    case EGameDifficultyType.Type_Hard:
                        if ((GameManager.Instance.iPhase % 30).Equals(1))
                        {
                            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Bonus);
                            GameManager.Instance.PlayerBonus();
                        }
                        else SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                        break;
                    default:
                        break;
                }
                yield return Timing.WaitForSeconds(3.0f);
            }
            else
            {
                SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_PhaseUp);
                yield return Timing.WaitForSeconds(1.5f);
            }
        }

        GameManager.Instance.PhaseActive(GameManager.Instance.iPhase);

        yield break;
    }
    public IEnumerator<float> ChangeTextScale()
    {
        pPhaseHelpText.SetActive(true);
        pPhaseHelpText.GetComponent<UI_Main>().RunDelegateUIAlpha("Game_UIPhaseHelpText_Alpha", 0.25f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(2.25f);

        pPhaseHelpText.GetComponent<UI_Main>().RunDelegateUIAlpha("Game_UIPhaseHelpText_Alpha", -0.25f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(0.75f);

        pPhaseHelpText.SetActive(false);
        yield break;
    }
    public IEnumerator<float> GameOver()
    {
        GameManager.Instance.bGameOver = true;
        Timing.RunCoroutine(SoundManager.Instance.StopBGM(true, 0.008f));
        pPanel.GetComponent<UI_Main>().RunDelegateUIAlpha("Game_UIPanel_Alpha", 0.03f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(3.5f);

        UIManager.Instance.RemoveUI("Game_UIMovie");
        UIManager.Instance.RemoveUI("Game_UIGameScoreText");
        UIManager.Instance.RemoveUI("Game_UIGameScore");
        UIManager.Instance.RemoveUI("Game_UIGameMaxScoreText");
        UIManager.Instance.RemoveUI("Game_UIGameMaxScore");
        UIManager.Instance.RemoveUI("Game_UIPhaseText");
        UIManager.Instance.RemoveUI("Game_UIPhase");
        UIManager.Instance.RemoveUI("Game_UIPhaseTimeText");
        UIManager.Instance.RemoveUI("Game_UIPhaseHelpText");
        UIManager.Instance.RemoveUI("Game_UIPanel");
        for (int i = 1; i <= 6; ++i)
        {
            UIManager.Instance.RemoveUI(string.Format("Game_UIPlayerLife_{0}", i));
            UIManager.Instance.RemoveUI(string.Format("Game_UIPlayerBomb_{0}", i));
        }

        SceneManager.LoadScene("ResultScene");

        yield break;
    }
    public IEnumerator<float> ChangeBackgroundColor()
    {
        Color pColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.5f);
        RawImage pRawImage = pMovie.GetComponent<RawImage>();

        float R = pRawImage.color.r;
        float G = pRawImage.color.g;
        float B = pRawImage.color.b;

        while (true)
        {
            if (R > pColor.r)
            {
                R -= 0.001f;
            }
            else R += 0.001f;
            if (G > pColor.g)
            {
                G -= 0.001f;
            }
            else G += 0.001f;
            if (B > pColor.b)
            {
                B -= 0.001f;
            }
            else B += 0.001f;
            pRawImage.color = new Color(R, G, B, 0.5f);

            if (R.Equals(pColor.r) && G.Equals(pColor.g) && B.Equals(pColor.b)) break;
        }
        yield break;
    }
    #endregion
}
