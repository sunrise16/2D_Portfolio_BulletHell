#region USING
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MEC;
#endregion

public class LoadingScene : MonoBehaviour
{
    #region VARIABLE
    private Image pImage = null;
    private UI_Main pUIMain = null;
    private float fTime = 0.0f;
    private bool bStart = false;
    private bool bOptionLoadingDone = false;
    private bool bEasyRankingLoadingDone = false;
    private bool bNormalRankingLoadingDone = false;
    private bool bHardRankingLoadingDone = false;
    private bool bEnd = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        GameManager.Instance.Init(ESceneType.Type_LoadingScene);
        UIManager.Instance.Init();
        EffectManager.Instance.Init();
        SoundManager.Instance.Init();
    }
    void Start()
    {
        Timing.KillCoroutines();
        UIManager.Instance.pUIDictionary.Clear();

        GameObject pBackground = UIManager.Instance.CreateUI(new Vector2(0.0f, 0.0f), Vector3.zero, Vector3.one, "Loading_UIBackground", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pBackground.SetActive(true);
        pImage = pBackground.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(1024.0f, 1024.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Background_Loading];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pBackground.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlpha("Loading_UIBackground_Alpha", 0.04f, 0.75f, 0.0f, 0.5f);

        GameObject pLoading = UIManager.Instance.CreateUI(new Vector2(0.0f, 0.0f), Vector3.zero, Vector3.one, "Loading_UIText", EUIType.Type_Image, EUICanvasType.Type_UICanvas,
            new float[21] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f });
        pLoading.SetActive(true);
        pImage = pLoading.GetComponent<Image>();
        pImage.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        pImage.rectTransform.sizeDelta = new Vector2(400.0f, 66.0f);
        pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Loading_NowLoading];
        pImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        pUIMain = pLoading.GetComponent<UI_Main>();
        UIManager.Instance.SetDelegate(pUIMain);
        pUIMain.RunDelegateUIAlphaPingpong("Loading_UIText_AlphaPingpong", 0.04f, 0, 1.0f, 0.0f, 0.0f, 1.0f);
    }

    void Update()
    {
        fTime += Time.deltaTime;

        if (bStart.Equals(false) && bEnd.Equals(false) && fTime >= 2.0f)
        {
            bStart = true;
            SettingSaveFile();
        }

        if (bStart.Equals(true) && bEnd.Equals(false) && bOptionLoadingDone.Equals(true) && bEasyRankingLoadingDone.Equals(true)
            && bNormalRankingLoadingDone.Equals(true) && bHardRankingLoadingDone.Equals(true) && Input.anyKeyDown)
        {
            Timing.RunCoroutine(LoadingDone());
            bEnd = true;
        }
    }
    #endregion

    #region COMMON METHOD
    public void SettingSaveFile()
    {
        // Directory.GetCurrentDirectory() = "D:/Unity Project/STG_Temp"
        if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Save"))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Save");
        }
        if (!File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Option.ljs") || !File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Easy.ljs")
            || !File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Normal.ljs") || !File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Hard.ljs"))
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Option.ljs") && GlobalData.OptionFileSave(true).Equals(true)) bOptionLoadingDone = true;
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Easy.ljs"))
            {
                if (GlobalData.RankingFileSave(EGameDifficultyType.Type_Easy, GlobalData.bFirstGame).Equals(true)) bEasyRankingLoadingDone = true;
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
            if (!File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Normal.ljs"))
            {
                if (GlobalData.RankingFileSave(EGameDifficultyType.Type_Normal, GlobalData.bFirstGame).Equals(true)) bNormalRankingLoadingDone = true;
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
            if (!File.Exists(Directory.GetCurrentDirectory() + "/Save/Save_Ranking_Hard.ljs"))
            {
                if (GlobalData.RankingFileSave(EGameDifficultyType.Type_Hard, GlobalData.bFirstGame).Equals(true)) bHardRankingLoadingDone = true;
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }

            if (bOptionLoadingDone.Equals(true) && bEasyRankingLoadingDone.Equals(true) && bNormalRankingLoadingDone.Equals(true) && bHardRankingLoadingDone.Equals(true))
            {
                UI_Main pUIMain = UIManager.Instance.GetUIMain("Loading_UIText");
                pUIMain.KillDelegateUI("Loading_UIText_AlphaPingpong");
                Image pImage = pUIMain.GetComponent<Image>();
                pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Loading_Done];
                pImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                pUIMain.pDelegateUIAlpha = UIManager.Instance.DelegateUIAlpha;
                pUIMain.pDelegateUIAlphaFlash = UIManager.Instance.DelegateUIAlphaFlash;
            }
        }
        else
        {
            if (GlobalData.FileLoad(GlobalData.bFirstGame).Equals(true))
            {
                bOptionLoadingDone = true;
                bEasyRankingLoadingDone = true;
                bNormalRankingLoadingDone = true;
                bHardRankingLoadingDone = true;

                UI_Main pUIMain = UIManager.Instance.GetUIMain("Loading_UIText");
                pUIMain.KillDelegateUI("Loading_UIText_AlphaPingpong");
                Image pImage = pUIMain.GetComponent<Image>();
                pImage.sprite = UIManager.Instance.pUISpriteArray[(int)EUISpriteType.Type_Loading_Done];
                pImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                pUIMain.pDelegateUIAlpha = UIManager.Instance.DelegateUIAlpha;
                pUIMain.pDelegateUIAlphaFlash = UIManager.Instance.DelegateUIAlphaFlash;
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
    #endregion

    #region IENUMRATOR
    public IEnumerator<float> LoadingDone()
    {
        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_Select);
        UI_Main pUIMain = UIManager.Instance.GetUIMain("Loading_UIBackground");
        pUIMain.RunDelegateUIAlpha("Loading_UIBackground_Alpha", -0.01f, 0.48f, pUIMain.GetComponent<Image>().color.a, 0.0f);
        pUIMain = UIManager.Instance.GetUIMain("Loading_UIText");
        pUIMain.RunDelegateUIAlpha("Loading_UIText_Alpha", -0.02f, 0.48f, 1.0f, 0.0f);
        pUIMain.RunDelegateUIAlphaFlash("Loading_UIText_AlphaFlash", 12, 0.04f, 0.4f, 1.0f);

        yield return Timing.WaitForSeconds(1.25f);

        Timing.KillCoroutines("Loading_UIBackground_Alpha");
        Timing.KillCoroutines("Loading_UIText_Alpha");
        Timing.KillCoroutines("Loading_UIText_AlphaFlash");

        UIManager.Instance.RemoveUI("Loading_UIBackground");
        UIManager.Instance.RemoveUI("Loading_UIText");

        SceneManager.LoadScene("MainScene");

        yield break;
    }
    #endregion
}