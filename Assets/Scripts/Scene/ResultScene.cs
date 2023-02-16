#region USING
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MEC;
#endregion

public class ResultScene : MonoBehaviour
{
    #region VARIABLE
    private GameObject pResultText_Help;
    private GameObject pRankingArrow;
    private Text pRankingPlayerName;
    private UI_Main pUIMain = null;
    private List<UserData> pUserDataList;
    private int iIndex = 0;
    private int iDifficulty = 0;
    private string szTempName = "";
    private bool bLock = true;
    private bool bEntryRanking = false;
    private bool bRankingPage = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        GameManager.Instance.Init(ESceneType.Type_ResultScene);
        UIManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();
    }
    void Start()
    {
        Timing.KillCoroutines();
        UIManager.Instance.pUIDictionary.Clear();
        GameManager.Instance.iTempScore = 0;

        pUserDataList = new List<UserData>();
        switch (GameManager.Instance.enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                iDifficulty = 6;
                break;
            case EGameDifficultyType.Type_Normal:
                iDifficulty = 8;
                break;
            case EGameDifficultyType.Type_Hard:
                iDifficulty = 7;
                break;
            default:
                break;
        }

        GameManager.Instance.enSceneType = ESceneType.Type_ResultScene;
        SoundManager.Instance.PlayBGM(EBGMType.Type_BGM23_StaffRoll, true);
        Timing.RunCoroutine(SetLoadingDone(1.5f));

        for (int i = 0; i < 10; ++i)
        {
            switch (GameManager.Instance.enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    if (GameManager.Instance.iScore > GlobalData.pEasyRanking[i].iTopScore && bEntryRanking.Equals(false)
                        && GlobalData.iStartingLife.Equals(3) && GlobalData.iStartingBomb.Equals(2))
                    {
                        bEntryRanking = true;
                        iIndex = i;
                        UserData pUserData = new UserData("", GameManager.Instance.iPhase, GameManager.Instance.iScore);
                        pUserDataList.Add(pUserData);
                        continue;
                    }
                    pUserDataList.Add(GlobalData.pEasyRanking[bEntryRanking.Equals(true) ? i - 1 : i]);
                    break;
                case EGameDifficultyType.Type_Normal:
                    if (GameManager.Instance.iScore > GlobalData.pNormalRanking[i].iTopScore && bEntryRanking.Equals(false)
                        && GlobalData.iStartingLife.Equals(3) && GlobalData.iStartingBomb.Equals(2))
                    {
                        bEntryRanking = true;
                        iIndex = i;
                        UserData pUserData = new UserData("", GameManager.Instance.iPhase, GameManager.Instance.iScore);
                        pUserDataList.Add(pUserData);
                        continue;
                    }
                    pUserDataList.Add(GlobalData.pNormalRanking[bEntryRanking.Equals(true) ? i - 1 : i]);
                    break;
                case EGameDifficultyType.Type_Hard:
                    if (GameManager.Instance.iScore > GlobalData.pHardRanking[i].iTopScore && bEntryRanking.Equals(false)
                        && GlobalData.iStartingLife.Equals(3) && GlobalData.iStartingBomb.Equals(2))
                    {
                        bEntryRanking = true;
                        iIndex = i;
                        UserData pUserData = new UserData("", GameManager.Instance.iPhase, GameManager.Instance.iScore);
                        pUserDataList.Add(pUserData);
                        continue;
                    }
                    pUserDataList.Add(GlobalData.pHardRanking[bEntryRanking.Equals(true) ? i - 1 : i]);
                    break;
                default:
                    break;
            }
        }

        GameObject pBackground = UIManager.Instance.CreateUI(new Vector2(0.0f, 0.0f), Vector3.zero, Vector3.one, "Result_UIBackground", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pBackground.SetActive(true);
        Image pImage = pBackground.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Background_Result];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pBackground.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIBackground_Alpha", 0.4f, 0.0f, 0.0f, 0.8f);

        GameObject pResultMainText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIMainText", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultMainText.SetActive(true);
        pImage = pResultMainText.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Result_MainText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pResultMainText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIMainText_Alpha", 0.4f, 0.0f, 0.0f, 1.0f);

        GameObject pResultDifficulty = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIDifficulty", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultDifficulty.SetActive(true);
        pImage = pResultDifficulty.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(328.0f, 180.0f);
        pImage.rectTransform.sizeDelta = new Vector2(300.0f, 77.25f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[iDifficulty];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pResultDifficulty.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIDifficulty_Alpha", 0.4f, 0.0f, 0.0f, 1.0f);

        GameObject pResultText_TotalScore = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_TotalScore", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_TotalScore.SetActive(true);
        Text pText = pResultText_TotalScore.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(328.0f, 90.0f);
        pText.rectTransform.sizeDelta = new Vector2(450.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = GameManager.Instance.iScore.ToString();
        pText.fontSize = 40;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_TotalScore.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_TotalScore_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        GameObject pResultText_TotalPhase = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_TotalPhase", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_TotalPhase.SetActive(true);
        pText = pResultText_TotalPhase.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(328.0f, 0.0f);
        pText.rectTransform.sizeDelta = new Vector2(450.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = GameManager.Instance.iPhase.ToString();
        pText.fontSize = 40;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_TotalPhase.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_TotalPhase_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        GameObject pResultText_MissCount = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_MissCount", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_MissCount.SetActive(true);
        pText = pResultText_MissCount.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(328.0f, -93.0f);
        pText.rectTransform.sizeDelta = new Vector2(450.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = GameManager.Instance.iMissCount.ToString();
        pText.fontSize = 40;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_MissCount.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_MissCount_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        GameObject pResultText_BombCount = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_BombCount", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_BombCount.SetActive(true);
        pText = pResultText_BombCount.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(328.0f, -188.0f);
        pText.rectTransform.sizeDelta = new Vector2(450.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = GameManager.Instance.iBombCount.ToString();
        pText.fontSize = 40;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_BombCount.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_BombCount_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        GameObject pResultText_DestroyedEnemy = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_DestroyedEnemy", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_DestroyedEnemy.SetActive(true);
        pText = pResultText_DestroyedEnemy.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(328.0f, -285.0f);
        pText.rectTransform.sizeDelta = new Vector2(450.0f, 60.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = GameManager.Instance.iDestroyCount.ToString();
        pText.fontSize = 40;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_DestroyedEnemy.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_DestroyedEnemy_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        pResultText_Help = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIText_Help", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pResultText_Help.SetActive(true);
        pText = pResultText_Help.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -455.0f);
        pText.rectTransform.sizeDelta = new Vector2(1024.0f, 108.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "(아무 키나 눌러 진행)";
        pText.fontSize = 32;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pResultText_Help.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIText_Help_Alpha", 0.25f, 0.5f, 0.0f, 1.0f);

        GameObject pPanel = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Result_UIPanel", EUIType.Type_Panel, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPanel.SetActive(true);
        pImage = pPanel.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        pUIMain = pPanel.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Result_UIPanel_Alpha", -0.4f, 0.0f, 1.0f, 0.0f);
    }
    void Update()
    {
        if (bLock.Equals(false))
        {
            if (bRankingPage.Equals(false) && Input.anyKeyDown)
            {
                bLock = true;
                SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                Timing.RunCoroutine(ToRanking());
            }
            else if (bRankingPage.Equals(true))
            {
                if (bEntryRanking.Equals(true))
                {
                    SetPlayerName();
                }
                if ((bEntryRanking.Equals(true) && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))) ||
                    (bEntryRanking.Equals(false) && Input.anyKeyDown))
                {
                    bLock = true;
                    szTempName = pRankingPlayerName.text;
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Bonus);
                    Timing.RunCoroutine(ToMainScene(true));
                }
            }
        }
    }
    #endregion

    #region COMMON METHOD
    public void SetPlayerName()
    {
        if (pRankingPlayerName.text.Length < 6)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                pRankingPlayerName.text += "A";
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                pRankingPlayerName.text += "B";
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                pRankingPlayerName.text += "C";
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                pRankingPlayerName.text += "D";
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                pRankingPlayerName.text += "E";
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                pRankingPlayerName.text += "F";
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                pRankingPlayerName.text += "G";
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                pRankingPlayerName.text += "H";
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                pRankingPlayerName.text += "I";
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                pRankingPlayerName.text += "J";
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                pRankingPlayerName.text += "K";
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                pRankingPlayerName.text += "L";
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                pRankingPlayerName.text += "M";
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                pRankingPlayerName.text += "N";
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                pRankingPlayerName.text += "O";
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                pRankingPlayerName.text += "P";
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                pRankingPlayerName.text += "Q";
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                pRankingPlayerName.text += "R";
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                pRankingPlayerName.text += "S";
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                pRankingPlayerName.text += "T";
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                pRankingPlayerName.text += "U";
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                pRankingPlayerName.text += "V";
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                pRankingPlayerName.text += "W";
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                pRankingPlayerName.text += "X";
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                pRankingPlayerName.text += "Y";
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                pRankingPlayerName.text += "Z";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                pRankingPlayerName.text += "1";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                pRankingPlayerName.text += "2";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                pRankingPlayerName.text += "3";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                pRankingPlayerName.text += "4";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                pRankingPlayerName.text += "5";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                pRankingPlayerName.text += "6";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                pRankingPlayerName.text += "7";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                pRankingPlayerName.text += "8";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                pRankingPlayerName.text += "9";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                pRankingPlayerName.text += "0";
            }
            else if (pRankingPlayerName.text.Length > 0 && Input.GetKeyDown(KeyCode.Backspace))
            {
                pRankingPlayerName.text = pRankingPlayerName.text.Remove(pRankingPlayerName.text.Length - 1);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                pRankingPlayerName.text = pRankingPlayerName.text.Remove(pRankingPlayerName.text.Length - 1);
            }
        }
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> SetLoadingDone(float fDelayTime)
    {
        yield return Timing.WaitForSeconds(fDelayTime);

        bLock = false;
        yield break;
    }
    public IEnumerator<float> ToMainScene(bool bSave = false)
    {
        SoundManager.Instance.StopBGM(true);
        UIManager.Instance.GetUIMain("Ranking_UIPanel").RunDelegateUIAlpha("Ranking_UIPanel_Alpha", 0.2f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(2.0f);

        if (bSave.Equals(true))
        {
            switch (GameManager.Instance.enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    for (int i = 0; i < 10; ++i)
                    {
                        if (i.Equals(iIndex))
                        {
                            pUserDataList[i] = new UserData(szTempName, pUserDataList[i].iTopPhase, pUserDataList[i].iTopScore);
                        }
                        GlobalData.pEasyRanking[i] = pUserDataList[i];
                    }
                    break;
                case EGameDifficultyType.Type_Normal:
                    for (int i = 0; i < 10; ++i)
                    {
                        if (i.Equals(iIndex))
                        {
                            pUserDataList[i] = new UserData(szTempName, pUserDataList[i].iTopPhase, pUserDataList[i].iTopScore);
                        }
                        GlobalData.pNormalRanking[i] = pUserDataList[i];
                    }
                    break;
                case EGameDifficultyType.Type_Hard:
                    for (int i = 0; i < 10; ++i)
                    {
                        if (i.Equals(iIndex))
                        {
                            pUserDataList[i] = new UserData(szTempName, pUserDataList[i].iTopPhase, pUserDataList[i].iTopScore);
                        }
                        GlobalData.pHardRanking[i] = pUserDataList[i];
                    }
                    break;
                default:
                    break;
            }
            GlobalData.RankingFileSave(GameManager.Instance.enGameDifficultyType);
        }
        UIManager.Instance.RemoveUI("Result_UIBackground");
        UIManager.Instance.RemoveUI("Result_UIText_Help");
        UIManager.Instance.RemoveUI("Ranking_UIDifficulty");
        UIManager.Instance.RemoveUI("Ranking_UIMainText");
        UIManager.Instance.RemoveUI("Ranking_UIPanel");
        if (bEntryRanking.Equals(true))
        {
            Timing.KillCoroutines("Ranking_UIArrow_AlphaPingpong");
            UIManager.Instance.RemoveUI("Ranking_UIArrow");
        }
        for (int i = 0; i < 10; ++i)
        {
            UIManager.Instance.RemoveUI(string.Format("Ranking_UIText_Rank{0}", i + 1));
            UIManager.Instance.RemoveUI(string.Format("Ranking_UIText_PlayerName{0}", i + 1));
            UIManager.Instance.RemoveUI(string.Format("Ranking_UIText_Phase{0}", i + 1));
            UIManager.Instance.RemoveUI(string.Format("Ranking_UIText_MaxScore{0}", i + 1));
        }

        SceneManager.LoadScene("MainScene");

        yield break;
    }
    public IEnumerator<float> ToRanking()
    {
        Text pText;

        UIManager.Instance.GetUIMain("Result_UIMainText").RunDelegateUIMove("Result_UIMainText_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIMainText").RunDelegateUIAlpha("Result_UIMainText_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIDifficulty").RunDelegateUIMove("Result_UIDifficulty_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIDifficulty").RunDelegateUIAlpha("Result_UIDifficulty_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_TotalScore").RunDelegateUIMove("Result_UIText_TotalScore_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_TotalScore").RunDelegateUIAlpha("Result_UIText_TotalScore_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_TotalPhase").RunDelegateUIMove("Result_UIText_TotalPhase_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_TotalPhase").RunDelegateUIAlpha("Result_UIText_TotalPhase_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_MissCount").RunDelegateUIMove("Result_UIText_MissCount_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_MissCount").RunDelegateUIAlpha("Result_UIText_MissCount_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_BombCount").RunDelegateUIMove("Result_UIText_BombCount_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_BombCount").RunDelegateUIAlpha("Result_UIText_BombCount_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_DestroyedEnemy").RunDelegateUIMove("Result_UIText_DestroyedEnemy_Move", 0.4f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_DestroyedEnemy").RunDelegateUIAlpha("Result_UIText_DestroyedEnemy_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        UIManager.Instance.GetUIMain("Result_UIText_Help").RunDelegateUIAlpha("Result_UIText_Help_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(1.0f);

        UIManager.Instance.RemoveUI("Result_UIMainText");
        UIManager.Instance.RemoveUI("Result_UIDifficulty");
        UIManager.Instance.RemoveUI("Result_UIText_TotalScore");
        UIManager.Instance.RemoveUI("Result_UIText_TotalPhase");
        UIManager.Instance.RemoveUI("Result_UIText_MissCount");
        UIManager.Instance.RemoveUI("Result_UIText_BombCount");
        UIManager.Instance.RemoveUI("Result_UIText_DestroyedEnemy");
        UIManager.Instance.RemoveUI("Result_UIPanel");
        Timing.RunCoroutine(SetLoadingDone(2.0f));

        GameObject pRankingDifficulty = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Ranking_UIDifficulty", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pRankingDifficulty.SetActive(true);
        Image pImage = pRankingDifficulty.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 427.0f);
        pImage.rectTransform.sizeDelta = new Vector2(400.0f, 103.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[iDifficulty];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pRankingDifficulty.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Ranking_UIDifficulty_Alpha", 0.15f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(0.15f);

        GameObject pRankingMainText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Ranking_UIMainText", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pRankingMainText.SetActive(true);
        pImage = pRankingMainText.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 325.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 100.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Result_RankingText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pRankingMainText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Ranking_UIMainText_Alpha", 0.15f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(0.15f);

        for (int i = 0; i < 10; ++i)
        {
            GameObject pRankingText_Rank = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Ranking_UIText_Rank{0}", i + 1),
                EUIType.Type_Text, EUICanvasType.Type_UICanvas, new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            pRankingText_Rank.SetActive(true);
            pText = pRankingText_Rank.GetComponent<Text>();
            pText.rectTransform.anchoredPosition = new Vector2(-402.0f, 240.0f - (65.0f * i));
            pText.rectTransform.sizeDelta = new Vector2(100.0f, 65.0f);
            pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
            pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
            pText.text = (i + 1).ToString();
            pText.fontSize = 35;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
            pUIMain = pRankingText_Rank.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Ranking_UIText_Rank{0}_Alpha", i + 1), 0.15f, 0.0f, 0.0f, 1.0f);

            GameObject pRankingText_PlayerName = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Ranking_UIText_PlayerName{0}", i + 1),
                EUIType.Type_Text, EUICanvasType.Type_UICanvas, new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            pRankingText_PlayerName.SetActive(true);
            pText = pRankingText_PlayerName.GetComponent<Text>();
            if (i.Equals(iIndex)) pRankingPlayerName = pText;
            pText.rectTransform.anchoredPosition = new Vector2(-165.0f, 240.0f - (65.0f * i));
            pText.rectTransform.sizeDelta = new Vector2(260.0f, 65.0f);
            pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
            pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
            pText.text = (bEntryRanking.Equals(true) && i.Equals(iIndex)) ? "" : pUserDataList[i].szPlayerName;
            pText.fontSize = 35;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
            pUIMain = pRankingText_PlayerName.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Ranking_UIText_PlayerName{0}_Alpha", i + 1), 0.15f, 0.0f, 0.0f, 1.0f);

            GameObject pRankingText_Phase = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Ranking_UIText_Phase{0}", i + 1),
                EUIType.Type_Text, EUICanvasType.Type_UICanvas, new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            pRankingText_Phase.SetActive(true);
            pText = pRankingText_Phase.GetComponent<Text>();
            pText.rectTransform.anchoredPosition = new Vector2(75.0f, 240.0f - (65.0f * i));
            pText.rectTransform.sizeDelta = new Vector2(150.0f, 65.0f);
            pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
            pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
            pText.text = pUserDataList[i].iTopPhase.ToString();
            pText.fontSize = 35;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
            pUIMain = pRankingText_Phase.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Ranking_UIText_Phase{0}_Alpha", i + 1), 0.15f, 0.0f, 0.0f, 1.0f);

            GameObject pRankingText_MaxScore = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, string.Format("Ranking_UIText_MaxScore{0}", i + 1),
                EUIType.Type_Text, EUICanvasType.Type_UICanvas, new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            pRankingText_MaxScore.SetActive(true);
            pText = pRankingText_MaxScore.GetComponent<Text>();
            pText.rectTransform.anchoredPosition = new Vector2(315.0f, 240.0f - (65.0f * i));
            pText.rectTransform.sizeDelta = new Vector2(320.0f, 65.0f);
            pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
            pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
            pText.text = pUserDataList[i].iTopScore.ToString();
            pText.fontSize = 35;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
            pUIMain = pRankingText_MaxScore.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlpha(string.Format("Ranking_UIText_MaxScore{0}_Alpha", i + 1), 0.15f, 0.0f, 0.0f, 1.0f);

            yield return Timing.WaitForSeconds(0.15f);
        }

        if (bEntryRanking.Equals(true))
        {
            pRankingArrow = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Ranking_UIArrow", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
                new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
            pRankingArrow.SetActive(true);
            pText = pRankingArrow.GetComponent<Text>();
            pText.rectTransform.anchoredPosition = new Vector2(-470.0f, 240.0f - (65.5f * iIndex));
            pText.rectTransform.sizeDelta = new Vector2(70.0f, 60.0f);
            pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
            pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
            pText.text = "▶";
            pText.fontSize = 40;
            pText.alignment = TextAnchor.MiddleCenter;
            pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
            pUIMain = pRankingArrow.GetComponent<UI_Main>();
            UIManager.Instance.SetDelegate(pUIMain);
            pUIMain.RunDelegateUIAlphaPingpong("Ranking_UIArrow_AlphaPingpong", 0.1f, 0, 0.0f, 1.0f, 0.0f, 1.0f);
        }
        GameObject pPanel = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Ranking_UIPanel", EUIType.Type_Panel, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pPanel.SetActive(true);
        pImage = pPanel.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        pUIMain = pPanel.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);

        pResultText_Help.GetComponent<Text>().fontSize = bEntryRanking.Equals(true) ? 26 : 32;
        pResultText_Help.GetComponent<Text>().text = bEntryRanking.Equals(true) ? string.Format("랭킹에 등록할 이름을 입력하세요. (최대 6글자, 알파벳 및 숫자만)\nEnter : 저장 후 메인으로") : "(아무 키나 눌러 메인으로)";
        UIManager.Instance.GetUIMain("Result_UIText_Help").RunDelegateUIAlpha("Result_UIText_Help_Alpha", 0.15f, 0.0f, 0.0f, 1.0f);
        bRankingPage = true;

        yield break;
    }
    #endregion
}