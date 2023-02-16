#region USING
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using MEC;
#endregion

public enum ECurrentScene
{
    None = -1,
    Type_Main,
    Type_Option,
    Type_Difficulty,
    Type_GameHelp,
    Max
}

public class MainScene : MonoBehaviour
{
    #region VARIABLE
    private UI_Main pUIMain = null;
    private ECurrentScene enCurrentScene = ECurrentScene.Type_Main;
    private int iMainKey = 2;
    private int iOptionKey = 1;
    private int iDifficultyKey = 1;
    private float fTime = 0.0f;
    private bool bLock = true;
    private bool bHelpPage1 = false;
    private bool bHelpPage2 = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        GameManager.Instance.Init(ESceneType.Type_MainScene);
        UIManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();
    }
    void Start()
    {
        Timing.KillCoroutines();
        UIManager.Instance.pUIDictionary.Clear();

        SoundManager.Instance.PlayBGM(EBGMType.Type_BGM01_Start, true);
        Timing.RunCoroutine(SetLoadingDone(1.0f));

        GameObject pMovie = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIMovie", EUIType.Type_RawImage, EUICanvasType.Type_BGCanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMovie.SetActive(true);
        RawImage pRawImage = pMovie.GetComponent<RawImage>();
        pRawImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pRawImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pRawImage.rectTransform.localScale = new Vector3(2.2f, 1.8f, 1.0f);
        pRawImage.texture = UIManager.Instance.pUITextureArray[11];
        pRawImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        VideoPlayer pVideoPlayer = pMovie.GetComponent<VideoPlayer>();
        pVideoPlayer.clip = UIManager.Instance.pUIVideoArray[1];
        pVideoPlayer.targetTexture = UIManager.Instance.pUITextureArray[11] as RenderTexture;
        pVideoPlayer.isLooping = true;
        pUIMain = pMovie.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIMovie_Alpha", 0.025f, 0.0f, 0.0f, 0.5f);

        GameObject pBackground = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIBackground", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pBackground.SetActive(true);
        Image pImage = pBackground.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Background_Main];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pBackground.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", 0.0125f, 0.0f, 0.0f, 0.25f);

        GameObject pMainTitle = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UITitle", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainTitle.SetActive(true);
        pImage = pMainTitle.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 145.0f);
        pImage.rectTransform.sizeDelta = new Vector2(540.0f, 290.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Main_TitleImage];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainTitle.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UITitle_Alpha", 0.025f, 0.5f, 0.0f, 1.0f);

        CreateTitleUI();
    }
    void Update()
    {
        fTime += Time.deltaTime;

        if (bLock.Equals(false))
        {
            if (enCurrentScene.Equals(ECurrentScene.Type_Main))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    iMainKey--;
                    if (iMainKey <= 0) iMainKey = 3;

                    switch (iMainKey)
                    {
                        case 1:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
                            pUIMain.RunDelegateUIScale("Main_UIStart_Scale", new Vector2(-0.4f, -0.4f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
                            pUIMain.RunDelegateUIScale("Main_UIOption_Scale", new Vector2(0.4f, 0.4f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임 옵션을 엽니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 2:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
                            pUIMain.RunDelegateUIScale("Main_UIExit_Scale", new Vector2(-0.4f, -0.4f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
                            pUIMain.RunDelegateUIScale("Main_UIStart_Scale", new Vector2(0.4f, 0.4f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임을 시작합니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 3:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
                            pUIMain.RunDelegateUIScale("Main_UIOption_Scale", new Vector2(-0.4f, -0.4f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
                            pUIMain.RunDelegateUIScale("Main_UIExit_Scale", new Vector2(0.4f, 0.4f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임을 종료합니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        default:
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    iMainKey++;
                    if (iMainKey >= 4) iMainKey = 1;

                    switch (iMainKey)
                    {
                        case 1:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
                            pUIMain.RunDelegateUIScale("Main_UIExit_Scale", new Vector2(-0.25f, -0.25f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", -0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
                            pUIMain.RunDelegateUIScale("Main_UIOption_Scale", new Vector2(0.25f, 0.25f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", 0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임 옵션을 엽니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 2:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
                            pUIMain.RunDelegateUIScale("Main_UIOption_Scale", new Vector2(-0.25f, -0.25f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", -0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
                            pUIMain.RunDelegateUIScale("Main_UIStart_Scale", new Vector2(0.25f, 0.25f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", 0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임을 시작합니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 3:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
                            pUIMain.RunDelegateUIScale("Main_UIStart_Scale", new Vector2(-0.25f, -0.25f), pUIMain.GetComponent<RectTransform>().localScale, Vector2.one);
                            pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", -0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
                            pUIMain.RunDelegateUIScale("Main_UIExit_Scale", new Vector2(0.25f, 0.25f), pUIMain.GetComponent<RectTransform>().localScale, new Vector2(1.5f, 1.5f));
                            pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", 0.1f, 0.0f, pUIMain.GetComponent<Image>().color.a, 1.0f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIHelpText1_AlphaPingpongText", -0.2f, "게임을 종료합니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        default:
                            break;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                    bLock = true;
                    switch (iMainKey)
                    {
                        case 1:
                            Timing.RunCoroutine(OpenOption());
                            break;
                        case 2:
                            Timing.RunCoroutine(StartGame());
                            break;
                        case 3:
                            Timing.RunCoroutine(ExitGame());
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (enCurrentScene.Equals(ECurrentScene.Type_Option))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    iOptionKey--;
                    if (iOptionKey <= 0) iOptionKey = 4;

                    switch (iOptionKey)
                    {
                        case 1:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 2:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 3:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 4:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        default:
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    iOptionKey++;
                    if (iOptionKey >= 5) iOptionKey = 1;

                    switch (iOptionKey)
                    {
                        case 1:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 2:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 3:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        case 4:
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", -0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 0.25f);
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", 0.2f, 0.0f, pUIMain.GetComponent<Text>().color.a, 1.0f);
                            break;
                        default:
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_ValueChange);

                    switch (iOptionKey)
                    {
                        case 1:
                            GlobalData.iStartingLife--;
                            if (GlobalData.iStartingLife < 2) GlobalData.iStartingLife = 6;
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText1_AlphaPingpongText", -0.3f, GlobalData.iStartingLife.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 2:
                            GlobalData.iStartingBomb--;
                            if (GlobalData.iStartingBomb < 2) GlobalData.iStartingBomb = 6;
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText2_AlphaPingpongText", -0.3f, GlobalData.iStartingBomb.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 3:
                            SoundManager.Instance.ChangeVolume("BGM", "Down");
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText3_AlphaPingpongText", -0.3f, GlobalData.iBGMVolume.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 4:
                            SoundManager.Instance.ChangeVolume("SE", "Down");
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText4_AlphaPingpongText", -0.3f, GlobalData.iSFXVolume.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        default:
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_ValueChange);

                    switch (iOptionKey)
                    {
                        case 1:
                            GlobalData.iStartingLife++;
                            if (GlobalData.iStartingLife > 6) GlobalData.iStartingLife = 2;
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText1_AlphaPingpongText", -0.3f, GlobalData.iStartingLife.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 2:
                            GlobalData.iStartingBomb++;
                            if (GlobalData.iStartingBomb > 6) GlobalData.iStartingBomb = 2;
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText2_AlphaPingpongText", -0.3f, GlobalData.iStartingBomb.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 3:
                            SoundManager.Instance.ChangeVolume("BGM", "Up");
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText3_AlphaPingpongText", -0.3f, GlobalData.iBGMVolume.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        case 4:
                            SoundManager.Instance.ChangeVolume("SE", "Up");
                            pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
                            pUIMain.RunDelegateUIAlphaPingpongText("Main_UIOptionValueText4_AlphaPingpongText", -0.3f, GlobalData.iSFXVolume.ToString(), 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                            break;
                        default:
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Cancel);
                    bLock = true;
                    Timing.RunCoroutine(ReturnTitleFromOption());
                }
            }
            else if (enCurrentScene.Equals(ECurrentScene.Type_Difficulty))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    iDifficultyKey--;
                    if (iDifficultyKey <= 0) iDifficultyKey = 3;

                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    bLock = true;
                    Timing.RunCoroutine(ChangeDifficulty(iDifficultyKey), "ChangeDifficulty");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    iDifficultyKey++;
                    if (iDifficultyKey >= 4) iDifficultyKey = 1;

                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_OptionChange);
                    bLock = true;
                    Timing.RunCoroutine(ChangeDifficulty(iDifficultyKey), "ChangeDifficulty");
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                    bLock = true;
                    if (GlobalData.bFirstGame.Equals(true))
                    {
                        Timing.RunCoroutine(ToGameHelpPage1());
                    }
                    else Timing.RunCoroutine(ToGameSceneFromDifficulty());
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Cancel);
                    bLock = true;
                    Timing.RunCoroutine(ReturnTitleFromDifficulty());
                }
            }
            else if (enCurrentScene.Equals(ECurrentScene.Type_GameHelp))
            {
                if (Input.anyKeyDown.Equals(true))
                {
                    bLock = true;
                    if (bHelpPage1.Equals(true))
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_ValueChange);
                        Timing.RunCoroutine(ToGameHelpPage2());
                    }
                    else if (bHelpPage2.Equals(true))
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
                        Timing.RunCoroutine(ToGameSceneFromGameHelp());
                    }
                }
            }
        }
    }
    #endregion

    #region COMMON METHOD
    public void CreateTitleUI()
    {
        GameObject pMainStart = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIStart", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainStart.SetActive(true);
        Image pImage = pMainStart.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(40.0f, -400.0f);
        pImage.rectTransform.sizeDelta = new Vector2(300.0f, 70.0f);
        pImage.rectTransform.localScale = new Vector2(1.5f, 1.5f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Main_StartText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainStart.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", 0.025f, 0.5f, 0.0f, 1.0f);

        GameObject pMainOption = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOption", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOption.SetActive(true);
        pImage = pMainOption.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(-300.0f, -400.0f);
        pImage.rectTransform.sizeDelta = new Vector2(300.0f, 70.0f);
        pImage.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Main_OptionText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainOption.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", 0.04825f, 0.5f, 0.0f, 0.25f);

        GameObject pMainExit = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIExit", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainExit.SetActive(true);
        pImage = pMainExit.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(340.0f, -400.0f);
        pImage.rectTransform.sizeDelta = new Vector2(300.0f, 70.0f);
        pImage.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Main_ExitText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainExit.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", 0.04825f, 0.5f, 0.0f, 0.25f);

        GameObject pMainHelpText1 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIHelpText1", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainHelpText1.SetActive(true);
        Text pText = pMainHelpText1.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -250.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 50.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "게임을 시작합니다.";
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainHelpText1.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText1_Alpha", 0.025f, 0.5f, 0.0f, 1.0f);

        GameObject pMainHelpText2 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIHelpText2", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainHelpText2.SetActive(true);
        pText = pMainHelpText2.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -290.0f);
        pText.rectTransform.sizeDelta = new Vector2(700.0f, 50.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "'← →' : 옵션 선택  /  'Z' : 결정";
        pText.fontSize = 18;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainHelpText2.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText2_Alpha", 0.025f, 0.5f, 0.0f, 1.0f);
    }
    public void KillTitleUI()
    {
        Timing.KillCoroutines("Main_UIStart_Move");
        Timing.KillCoroutines("Main_UIStart_Alpha");
        Timing.KillCoroutines("Main_UIOption_Move");
        Timing.KillCoroutines("Main_UIOption_Alpha");
        Timing.KillCoroutines("Main_UIExit_Move");
        Timing.KillCoroutines("Main_UIExit_Alpha");
        Timing.KillCoroutines("Main_UIHelpText1_Move");
        Timing.KillCoroutines("Main_UIHelpText1_Alpha");
        Timing.KillCoroutines("Main_UIHelpText2_Move");
        Timing.KillCoroutines("Main_UIHelpText2_Alpha");

        UIManager.Instance.RemoveUI("Main_UIStart");
        UIManager.Instance.RemoveUI("Main_UIOption");
        UIManager.Instance.RemoveUI("Main_UIExit");
        UIManager.Instance.RemoveUI("Main_UIHelpText1");
        UIManager.Instance.RemoveUI("Main_UIHelpText2");
    }
    public void CreateOptionUI()
    {
        GameObject pMainOptionText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionText", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOptionText.SetActive(true);
        Image pImage = pMainOptionText.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(-100.0f, -200.0f);
        pImage.rectTransform.sizeDelta = new Vector2(435.0f, 350.0f);
        pImage.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Option_Image];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainOptionText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionText_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pMainOptionValueText1 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionValueText1", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOptionValueText1.SetActive(true);
        Text pText = pMainOptionValueText1.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(240.0f, -78.0f);
        pText.rectTransform.sizeDelta = new Vector2(100.0f, 80.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GlobalData.iStartingLife.ToString();
        pText.fontSize = 70;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainOptionValueText1.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pMainOptionValueText2 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionValueText2", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOptionValueText2.SetActive(true);
        pText = pMainOptionValueText2.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(240.0f, -160.0f);
        pText.rectTransform.sizeDelta = new Vector2(100.0f, 80.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GlobalData.iStartingBomb.ToString();
        pText.fontSize = 70;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainOptionValueText2.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", 0.0125f, 0.0f, 0.0f, 0.25f);

        GameObject pMainOptionValueText3 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionValueText3", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOptionValueText3.SetActive(true);
        pText = pMainOptionValueText3.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(240.0f, -246.0f);
        pText.rectTransform.sizeDelta = new Vector2(100.0f, 80.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GlobalData.iBGMVolume.ToString();
        pText.fontSize = 70;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainOptionValueText3.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", 0.0125f, 0.0f, 0.0f, 0.25f);

        GameObject pMainOptionValueText4 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionValueText4", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainOptionValueText4.SetActive(true);
        pText = pMainOptionValueText4.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(240.0f, -332.0f);
        pText.rectTransform.sizeDelta = new Vector2(100.0f, 80.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = GlobalData.iSFXVolume.ToString();
        pText.fontSize = 70;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pMainOptionValueText4.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", 0.0125f, 0.0f, 0.0f, 0.25f);

        GameObject pOptionHelpText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIOptionHelpText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pOptionHelpText.SetActive(true);
        pText = pOptionHelpText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -450.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 30.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "'↑ ↓' : 옵션 선택  /  '← →' : 값 변경  /  'X' : 타이틀로";
        pText.fontSize = 18;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pOptionHelpText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionHelpText_Alpha", 0.025f, 0.0f, 0.0f, 1.0f);
    }
    public void KillOptionUI()
    {
        Timing.KillCoroutines("Main_UIOptionText_Move");
        Timing.KillCoroutines("Main_UIOptionText_Alpha");
        Timing.KillCoroutines("Main_UIOptionValueText1_Move");
        Timing.KillCoroutines("Main_UIOptionValueText1_Alpha");
        Timing.KillCoroutines("Main_UIOptionValueText2_Move");
        Timing.KillCoroutines("Main_UIOptionValueText2_Alpha");
        Timing.KillCoroutines("Main_UIOptionValueText3_Move");
        Timing.KillCoroutines("Main_UIOptionValueText3_Alpha");
        Timing.KillCoroutines("Main_UIOptionValueText4_Move");
        Timing.KillCoroutines("Main_UIOptionValueText4_Alpha");
        Timing.KillCoroutines("Main_UIOptionHelpText_Move");
        Timing.KillCoroutines("Main_UIOptionHelpText_Alpha");

        UIManager.Instance.RemoveUI("Main_UIOptionText");
        UIManager.Instance.RemoveUI("Main_UIOptionValueText1");
        UIManager.Instance.RemoveUI("Main_UIOptionValueText2");
        UIManager.Instance.RemoveUI("Main_UIOptionValueText3");
        UIManager.Instance.RemoveUI("Main_UIOptionValueText4");
        UIManager.Instance.RemoveUI("Main_UIOptionHelpText");
    }
    public void CreateDifficultyUI()
    {
        GameObject pDifficultyIllust = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIDifficulty_Illust", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pDifficultyIllust.SetActive(true);
        Image pImage = pDifficultyIllust.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 100.0f);
        pImage.rectTransform.sizeDelta = new Vector2(750.0f, 375.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_GameDifficulty_EasyIllust];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pDifficultyIllust.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_Illust_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pDifficultyText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIDifficulty_Text", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pDifficultyText.SetActive(true);
        pImage = pDifficultyText.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, -200.0f);
        pImage.rectTransform.sizeDelta = new Vector2(400.0f, 103.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_GameDifficulty_EasyText];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pDifficultyText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_Text_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pDifficultyTitleText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIDifficulty_TitleText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pDifficultyTitleText.SetActive(true);
        Text pText = pDifficultyTitleText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 400.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 100.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.text = "SELECT DIFFICULTY";
        pText.fontSize = 70;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.5f, 0.25f, 0.25f, 0.0f);
        pUIMain = pDifficultyTitleText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_TitleText_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pDifficultyDiscriptionText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIDifficulty_DiscriptionText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pDifficultyDiscriptionText.SetActive(true);
        pText = pDifficultyDiscriptionText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -280.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 50.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "슈팅게임을 처음 하는 사람이 입문할 수 있는 난이도입니다.";
        pText.fontSize = 30;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pDifficultyDiscriptionText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_DiscriptionText_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);

        GameObject pDifficultyHelpText = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIDifficulty_HelpText", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pDifficultyHelpText.SetActive(true);
        pText = pDifficultyHelpText.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, -450.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 50.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = "'← →' : 난이도 변경  /  'Z' : 게임 시작  /  'X' : 타이틀로";
        pText.fontSize = 24;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pDifficultyHelpText.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_HelpText_Alpha", 0.05f, 0.0f, 0.0f, 1.0f);
    }
    public void MoveDifficultyUI()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_Illust");
        pUIMain.RunDelegateUIMove("Main_UIDifficulty_Illust_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_Illust_Alpha", -0.15f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_Text");
        pUIMain.RunDelegateUIMove("Main_UIDifficulty_Text_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_Text_Alpha", -0.15f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_TitleText");
        pUIMain.RunDelegateUIMove("Main_UIDifficulty_TitleText_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_TitleText_Alpha", -0.15f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_DiscriptionText");
        pUIMain.RunDelegateUIMove("Main_UIDifficulty_DiscriptionText_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_DiscriptionText_Alpha", -0.15f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_HelpText");
        pUIMain.RunDelegateUIMove("Main_UIDifficulty_HelpText_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIDifficulty_HelpText_Alpha", -0.15f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
    }
    public void KillDifficultyUI()
    {
        Timing.KillCoroutines("Main_UIDifficulty_Illust_Move");
        Timing.KillCoroutines("Main_UIDifficulty_Illust_Alpha");
        Timing.KillCoroutines("Main_UIDifficulty_Text_Move");
        Timing.KillCoroutines("Main_UIDifficulty_Text_Alpha");
        Timing.KillCoroutines("Main_UIDifficulty_TitleText_Move");
        Timing.KillCoroutines("Main_UIDifficulty_TitleText_Alpha");
        Timing.KillCoroutines("Main_UIDifficulty_DiscriptionText_Move");
        Timing.KillCoroutines("Main_UIDifficulty_DiscriptionText_Alpha");
        Timing.KillCoroutines("Main_UIDifficulty_HelpText_Move");
        Timing.KillCoroutines("Main_UIDifficulty_HelpText_Alpha");

        UIManager.Instance.RemoveUI("Main_UIDifficulty_Illust");
        UIManager.Instance.RemoveUI("Main_UIDifficulty_Text");
        UIManager.Instance.RemoveUI("Main_UIDifficulty_TitleText");
        UIManager.Instance.RemoveUI("Main_UIDifficulty_DiscriptionText");
        UIManager.Instance.RemoveUI("Main_UIDifficulty_HelpText");
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> SetLoadingDone(float fDelayTime)
    {
        yield return Timing.WaitForSeconds(fDelayTime);

        bLock = false;
        yield break;
    }
    public IEnumerator<float> OpenOption()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
        pUIMain.RunDelegateUIAlphaFlash("Main_UIOption_AlphaFlash", 12, 0.03f, 0.4f, 1.0f);

        yield return Timing.WaitForSeconds(0.36f);

        Timing.KillCoroutines("Main_UIOption_AlphaFlash");
        pUIMain.RunDelegateUIMove("Main_UIOption_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
        pUIMain.RunDelegateUIMove("Main_UIStart_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
        pUIMain.RunDelegateUIMove("Main_UIExit_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
        pUIMain.RunDelegateUIMove("Main_UIHelpText1_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText1_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText2");
        pUIMain.RunDelegateUIMove("Main_UIHelpText2_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText2_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);

        yield return Timing.WaitForSeconds(0.5f);

        enCurrentScene = ECurrentScene.Type_Option;
        iOptionKey = 1;
        KillTitleUI();
        CreateOptionUI();

        yield return Timing.WaitForSeconds(0.3f);

        bLock = false;
        yield break;
    }
    public IEnumerator<float> StartGame()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
        pUIMain.RunDelegateUIAlphaFlash("Main_UIStart_AlphaFlash", 12, 0.03f, 0.4f, 1.0f);

        yield return Timing.WaitForSeconds(0.36f);

        pUIMain.RunDelegateUIMove("Main_UIStart_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
        pUIMain.RunDelegateUIMove("Main_UIOption_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
        pUIMain.RunDelegateUIMove("Main_UIExit_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
        pUIMain.RunDelegateUIMove("Main_UIHelpText1_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText1_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText2");
        pUIMain.RunDelegateUIMove("Main_UIHelpText2_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText2_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UITitle");
        pUIMain.RunDelegateUIMove("Main_UITitle_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UITitle_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);

        yield return Timing.WaitForSeconds(0.5f);

        enCurrentScene = ECurrentScene.Type_Difficulty;
        iDifficultyKey = 1;

        UIManager.Instance.RemoveUI("Main_UITitle");
        KillTitleUI();
        CreateDifficultyUI();

        yield return Timing.WaitForSeconds(0.3f);

        bLock = false;
        yield break;
    }
    public IEnumerator<float> ChangeDifficulty(int iDifficultyKey)
    {
        int iSprite1 = 0;
        int iSprite2 = 0;
        switch (iDifficultyKey)
        {
            case 1:
                iSprite1 = (int)EUISpriteType.Type_GameDifficulty_EasyIllust;
                iSprite2 = (int)EUISpriteType.Type_GameDifficulty_EasyText;
                pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_DiscriptionText");
                pUIMain.RunDelegateUIAlphaPingpongText("Main_UIDifficulty_DiscriptionText_AlphaPingpongText", -0.2f, "처음 슈팅게임에 입문하는 사람을 위해", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                break;
            case 2:
                iSprite1 = (int)EUISpriteType.Type_GameDifficulty_NormalIllust;
                iSprite2 = (int)EUISpriteType.Type_GameDifficulty_NormalText;
                pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_DiscriptionText");
                pUIMain.RunDelegateUIAlphaPingpongText("Main_UIDifficulty_DiscriptionText_AlphaPingpongText", -0.2f, "슈팅게임에 익숙한 사람에게 추천하는 난이도입니다.", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                break;
            case 3:
                iSprite1 = (int)EUISpriteType.Type_GameDifficulty_HardIllust;
                iSprite2 = (int)EUISpriteType.Type_GameDifficulty_HardText;
                pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_DiscriptionText");
                pUIMain.RunDelegateUIAlphaPingpongText("Main_UIDifficulty_DiscriptionText_AlphaPingpongText", -0.2f, "슈팅게임 정복자라면 이 난이도에 도전하세요!", 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f, 1.0f);
                break;
            default:
                break;
        }

        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_Illust");
        pUIMain.RunDelegateUIAlphaPingpongImage("Main_UIDifficulty_Illust_AlphaPingpongImage", -0.2f, iSprite1, 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f, 1.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIDifficulty_Text");
        pUIMain.RunDelegateUIAlphaPingpongImage("Main_UIDifficulty_Illust_AlphaPingpongImage", -0.2f, iSprite2, 2, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(0.3f);

        bLock = false;
        yield break;
    }
    public IEnumerator<float> ToGameHelpPage1()
    {
        MoveDifficultyUI();

        yield return Timing.WaitForSeconds(0.75f);

        enCurrentScene = ECurrentScene.Type_GameHelp;
        bHelpPage1 = true;
        GameObject pGameHelpText1 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIGameHelpText1", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameHelpText1.SetActive(true);
        Text pText = pGameHelpText1.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 1000.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = string.Format("처음 게임을 시작하셨군요!\n게임 방법에 대해 간략하게 설명하겠습니다.\n\n적이 쏘는 탄막을 피하며 적을 격추하고\n최대한 많은 점수를 획득하면 됩니다.\n\n스테이지는 각각 하나의 페이즈로 이루어져 있으며, 페이즈는 무한으로 진행됩니다.\n진행할수록 난이도가 어려워지니 조심하세요!\n\n\n(아무 키를 눌러 진행)");
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameHelpText1.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIGameHelpText1_Alpha", 0.1f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(0.75f);

        bLock = false;

        yield break;
    }
    public IEnumerator<float> ToGameHelpPage2()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIGameHelpText1");
        pUIMain.RunDelegateUIAlpha("Main_UIGameHelpText1_Alpha", -0.1f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(0.75f);

        Timing.KillCoroutines("Main_UIGameHelpText1_Alpha");
        UIManager.Instance.RemoveUI("Main_UIGameHelpText1");
        bHelpPage1 = false;
        bHelpPage2 = true;

        GameObject pGameHelpText2 = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UIGameHelpText2", EUIType.Type_Text, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pGameHelpText2.SetActive(true);
        Text pText = pGameHelpText2.GetComponent<Text>();
        pText.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pText.rectTransform.sizeDelta = new Vector2(1000.0f, 1000.0f);
        pText.rectTransform.localScale = new Vector2(1.0f, 1.0f);
        pText.font = GameManager.Instance.pFontArray[(int)EUITextFontType.Type_NotoSansKRBlack];
        pText.text = string.Format("- 조작키 -\n\n←↑↓→ : 이동\nZ : 공격\nX : 폭탄 사용\nShift : 저속 이동 (누르고 있을 경우)\n\n- 점수 체계 -\n\n적에게 공격 적중  :  10 점\n적 격추  :  (100 X 현재 페이즈) 점\n폭탄으로 소거한 탄막  :  개당 (10 X 현재 잔기 갯수) 점\n페이즈 증가 시  :  (1000 X (현재 잔기 갯수 + 현재 폭탄 갯수)) 점\n\n- 보너스 -\n\n난이도별로 10 / 20 / 30 페이즈 진행 후 잔기와 폭탄이 1개씩 추가로 지급됩니다.\n6개를 초과한 잔기와 폭탄은 50000 점으로 변환됩니다.\n\n※ 주의! 옵션에서 초기 잔기와 폭탄 갯수를 임의로 조절했을 경우\n게임 종료 시 점수가 랭킹에 저장되지 않습니다.\n\n\n(아무 키나 눌러 게임 시작)");
        pText.fontSize = 28;
        pText.alignment = TextAnchor.MiddleCenter;
        pText.color = new Color(0.8f, 0.8f, 0.8f, 0.0f);
        pUIMain = pGameHelpText2.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UIGameHelpText2_Alpha", 0.1f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(0.75f);

        bLock = false;

        yield break;
    }
    public IEnumerator<float> ToGameSceneFromDifficulty()
    {
        Timing.RunCoroutine(SoundManager.Instance.StopBGM(true));
        MoveDifficultyUI();
        pUIMain = UIManager.Instance.GetUIMain("Main_UIBackground");
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIMovie");
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(0.75f);

        KillDifficultyUI();
        Timing.RunCoroutine(ToGameScene());

        yield break;
    }
    public IEnumerator<float> ToGameSceneFromGameHelp()
    {
        Timing.RunCoroutine(SoundManager.Instance.StopBGM(true));
        pUIMain = UIManager.Instance.GetUIMain("Main_UIGameHelpText2");
        pUIMain.RunDelegateUIAlpha("Main_UIGameHelpText2_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIBackground");
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIMovie");
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", -0.2f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(0.75f);

        Timing.KillCoroutines("Main_UIGameHelpText2_Alpha");
        UIManager.Instance.RemoveUI("Main_UIGameHelpText2");
        Timing.RunCoroutine(ToGameScene());

        yield break;
    }
    public IEnumerator<float> ToGameScene()
    {
        Timing.KillCoroutines("Main_UIBackground_Alpha");
        Timing.KillCoroutines("Main_UIMovie_Alpha");
        UIManager.Instance.RemoveUI("Main_UIBackground");
        UIManager.Instance.RemoveUI("Main_UIMovie");

        GameObject pLoading = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UILoadingText", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pLoading.SetActive(true);
        Image pImage = pLoading.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(400.0f, 66.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Loading_NowLoading];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pLoading.GetComponent<UI_Main>();
        pUIMain.pDelegateUIAlphaPingpong = UIManager.Instance.DelegateUIAlphaPingpong;
        pUIMain.RunDelegateUIAlphaPingpong("Main_UILoadingText_AlphaPingpong", 0.05f, 0, 0.0f, 0.0f, 0.0f, 1.0f);

        yield return Timing.WaitForSeconds(2.25f);

        pUIMain.KillDelegateUI("Main_UILoadingText_AlphaPingpong");
        bLock = false;
        if (GlobalData.bFirstGame.Equals(true)) GlobalData.bFirstGame = false;
        GlobalData.OptionFileSave();
        GameManager.Instance.enGameDifficultyType = (EGameDifficultyType)(iDifficultyKey - 1);

        UIManager.Instance.RemoveUI("Main_UILoadingText");

        SceneManager.LoadScene("GameScene");

        yield break;
    }
    public IEnumerator<float> ExitGame()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIExit");
        pUIMain.RunDelegateUIAlphaFlash("Main_UIExit_AlphaFlash", 12, 0.03f, 0.4f, 1.0f);

        yield return Timing.WaitForSeconds(0.36f);

        pUIMain.RunDelegateUIAlpha("Main_UIExit_Alpha", -0.02f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIBackground");
        pUIMain.RunDelegateUIAlpha("Main_UIBackground_Alpha", -0.02f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UITitle");
        pUIMain.RunDelegateUIAlpha("Main_UITitle_Alpha", -0.02f, 0.0f, 1.0f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIStart");
        pUIMain.RunDelegateUIAlpha("Main_UIStart_Alpha", -0.005f, 0.0f, 0.25f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOption");
        pUIMain.RunDelegateUIAlpha("Main_UIOption_Alpha", -0.005f, 0.0f, 0.25f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText1");
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText1_Alpha", -0.005f, 0.0f, 0.25f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIHelpText2");
        pUIMain.RunDelegateUIAlpha("Main_UIHelpText2_Alpha", -0.005f, 0.0f, 0.25f, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIMovie");
        pUIMain.RunDelegateUIAlpha("Main_UIMovie_Alpha", -0.02f, 0.0f, 1.0f, 0.0f);

        yield return Timing.WaitForSeconds(1.0f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public IEnumerator<float> ReturnTitleFromOption()
    {
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionText");
        pUIMain.RunDelegateUIMove("Main_UIOptionText_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionText_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText1");
        pUIMain.RunDelegateUIMove("Main_UIOptionValueText1_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText1_Alpha", iOptionKey.Equals(1) ? -0.1f : -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText2");
        pUIMain.RunDelegateUIMove("Main_UIOptionValueText2_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText2_Alpha", iOptionKey.Equals(2) ? -0.1f : -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText3");
        pUIMain.RunDelegateUIMove("Main_UIOptionValueText3_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText3_Alpha", iOptionKey.Equals(3) ? -0.1f : -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionValueText4");
        pUIMain.RunDelegateUIMove("Main_UIOptionValueText4_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionValueText4_Alpha", iOptionKey.Equals(4) ? -0.1f : -0.025f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Main_UIOptionHelpText");
        pUIMain.RunDelegateUIMove("Main_UIOptionHelpText_Move", 0.2f, -1.0f, 0.0f, -0.1f, 0.0f, 0.0f);
        pUIMain.RunDelegateUIAlpha("Main_UIOptionHelpText_Alpha", -0.1f, 0.0f, pUIMain.GetUIBase().GetGameObject().GetComponent<Text>().color.a, 0.0f);

        yield return Timing.WaitForSeconds(0.5f);

        GlobalData.OptionFileSave();

        enCurrentScene = ECurrentScene.Type_Main;
        iMainKey = 2;
        iOptionKey = 1;
        KillOptionUI();
        CreateTitleUI();
        Timing.RunCoroutine(SetLoadingDone(1.0f));
        yield break;
    }
    public IEnumerator<float> ReturnTitleFromDifficulty()
    {
        MoveDifficultyUI();

        yield return Timing.WaitForSeconds(0.5f);

        enCurrentScene = ECurrentScene.Type_Main;
        iMainKey = 2;
        iDifficultyKey = 1;
        KillDifficultyUI();

        GameObject pMainTitle = UIManager.Instance.CreateUI(Vector3.zero, Vector3.zero, Vector3.one, "Main_UITitle", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pMainTitle.SetActive(true);
        Image pImage = pMainTitle.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 145.0f);
        pImage.rectTransform.sizeDelta = new Vector2(540.0f, 290.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Main_TitleImage];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pMainTitle.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Main_UITitle_Alpha", 0.025f, 0.5f, 0.0f, 1.0f);

        CreateTitleUI();
        Timing.RunCoroutine(SetLoadingDone(1.0f));
        yield break;
    }
    #endregion
}