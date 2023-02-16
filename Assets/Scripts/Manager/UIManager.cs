#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using MEC;
#endregion

public class UIManager : UnitySingleton<UIManager>
{
    #region VARIABLE
    public Dictionary<string, GameObject> pUIDictionary;
    [HideInInspector] public GameObject pUICanvas;
    [HideInInspector] public GameObject pBGCanvas;
    [HideInInspector] public GameObject pEventSystem;
    public VideoClip[] pUIVideoArray;
    public Texture[] pUITextureArray;
    public Sprite[] pUISpriteArray;

    private bool bInit = false;
    #endregion

    #region UNITY LIFE CYCLE
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        pUICanvas = GameObject.Find("UICANVAS");
        pBGCanvas = GameObject.Find("BGCANVAS");
        pEventSystem = GameObject.Find("EVENTSYSTEM");

        if (bInit.Equals(true))
        {
            return;
        }

        pUIDictionary = new Dictionary<string, GameObject>();
        pUIVideoArray = Resources.LoadAll<VideoClip>(GlobalData.szVideoPath);
        pUITextureArray = Resources.LoadAll<Texture>(GlobalData.szTexturePath);
        pUISpriteArray = Resources.LoadAll<Sprite>(GlobalData.szUISpritePath);
        bInit = true;
    }
    public UI_Main GetUIMain(string szUIName)
    {
        return pUIDictionary[szUIName].GetComponent<UI_Main>();
    }
    public void SetDelegate(UI_Main pUIMain)
    {
        pUIMain.pDelegateUIMove = DelegateUIMove;
        pUIMain.pDelegateUIScale = DelegateUIScale;
        pUIMain.pDelegateUIAlpha = DelegateUIAlpha;
        pUIMain.pDelegateUIAlphaFlash = DelegateUIAlphaFlash;
        pUIMain.pDelegateUIAlphaPingpong = DelegateUIAlphaPingpong;
        pUIMain.pDelegateUIAlphaPingpongText = DelegateUIAlphaPingpongText;
        pUIMain.pDelegateUIAlphaPingpongImage = DelegateUIAlphaPingpongImage;
    }
    public GameObject CreateUI(Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szUITag, EUIType enUIType, EUICanvasType enUICanvasType, float[] fData)
    {
        switch (enUIType)
        {
            case EUIType.Type_Text:
                GameObject pUIText = GameObject.Instantiate(Resources.Load(GlobalData.szUITextPrefabPath)) as GameObject;
                Text pText = pUIText.GetComponent<Text>();

                pText.rectTransform.anchoredPosition = new Vector2(vPosition.x, vPosition.y);
                pText.rectTransform.rotation = Quaternion.Euler(vRotation);
                pText.rectTransform.localScale = vScale;
                pText.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
                pText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                pText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                pText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                pText.rectTransform.parent = enUICanvasType.Equals(EUICanvasType.Type_UICanvas) ? pUICanvas.transform : pBGCanvas.transform;

                pText.text = "Sample";

                pUIText.GetComponent<UI_Main>().Init(pUIText, pText.rectTransform, vPosition, vRotation, vScale, szUITag, enUIType, fData);
                pUIText.name = szUITag;
                pUIText.SetActive(false);
                pUIDictionary.Add(szUITag, pUIText);
                return pUIText;
            case EUIType.Type_Image:
                GameObject pUIImage = GameObject.Instantiate(Resources.Load(GlobalData.szUIImagePrefabPath)) as GameObject;
                Image pImage = pUIImage.GetComponent<Image>();

                pImage.rectTransform.anchoredPosition = new Vector2(vPosition.x, vPosition.y);
                pImage.rectTransform.rotation = Quaternion.Euler(vRotation);
                pImage.rectTransform.localScale = vScale;
                pImage.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
                pImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                pImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                pImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                pImage.rectTransform.parent = enUICanvasType.Equals(EUICanvasType.Type_UICanvas) ? pUICanvas.transform : pBGCanvas.transform;

                pImage.color = Color.clear;
                pImage.material = null;

                pUIImage.GetComponent<UI_Main>().Init(pUIImage, pImage.rectTransform, vPosition, vRotation, vScale, szUITag, enUIType, fData);
                pUIImage.name = szUITag;
                pUIImage.SetActive(false);
                pUIDictionary.Add(szUITag, pUIImage);
                return pUIImage;
            case EUIType.Type_RawImage:
                GameObject pUIRawImage = GameObject.Instantiate(Resources.Load(GlobalData.szUIRawImagePrefabPath)) as GameObject;
                RawImage pRawImage = pUIRawImage.GetComponent<RawImage>();

                pRawImage.rectTransform.anchoredPosition = new Vector2(vPosition.x, vPosition.y);
                pRawImage.rectTransform.rotation = Quaternion.Euler(vRotation);
                pRawImage.rectTransform.localScale = vScale;
                pRawImage.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
                pRawImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                pRawImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                pRawImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                pRawImage.rectTransform.parent = enUICanvasType.Equals(EUICanvasType.Type_UICanvas) ? pUICanvas.transform : pBGCanvas.transform;

                pRawImage.color = Color.clear;
                pRawImage.material = null;

                pUIRawImage.GetComponent<UI_Main>().Init(pUIRawImage, pRawImage.rectTransform, vPosition, vRotation, vScale, szUITag, enUIType, fData);
                pUIRawImage.name = szUITag;
                pUIRawImage.SetActive(false);
                pUIDictionary.Add(szUITag, pUIRawImage);
                return pUIRawImage;
            case EUIType.Type_Button:
                break;
            case EUIType.Type_Panel:
                GameObject pUIPanel = GameObject.Instantiate(Resources.Load(GlobalData.szUIPanelPrefabPath)) as GameObject;
                Image pPanelImage = pUIPanel.GetComponent<Image>();

                pPanelImage.rectTransform.anchoredPosition = new Vector2(vPosition.x, vPosition.y);
                pPanelImage.rectTransform.rotation = Quaternion.Euler(vRotation);
                pPanelImage.rectTransform.localScale = vScale;
                pPanelImage.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
                pPanelImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                pPanelImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                pPanelImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                pPanelImage.rectTransform.parent = enUICanvasType.Equals(EUICanvasType.Type_UICanvas) ? pUICanvas.transform : pBGCanvas.transform;

                pPanelImage.color = Color.clear;
                pPanelImage.material = null;

                pUIPanel.GetComponent<UI_Main>().Init(pUIPanel, pPanelImage.rectTransform, vPosition, vRotation, vScale, szUITag, enUIType, fData);
                pUIPanel.name = szUITag;
                pUIPanel.SetActive(false);
                pUIDictionary.Add(szUITag, pUIPanel);
                return pUIPanel;
            case EUIType.Type_Scrollbar:
                break;
            case EUIType.Type_Slider:
                break;
            case EUIType.Type_Toggle:
                break;
            default:
                break;
        }
        return null;
    }
    public UI_Base GetUIBase(string szUIName)
    {
        return pUIDictionary[szUIName].GetComponent<UI_Main>().GetUIBase();
    }
    public void RemoveUI(string szUIName)
    {
        if (pUIDictionary[szUIName] != null)
        {
            GameObject.Destroy(pUIDictionary[szUIName]);
            pUIDictionary.Remove(szUIName);
        }
    }
    #endregion

    #region DELEGATE METHOD
    public IEnumerator<float> DelegateUIMove(UI_Main pUIMain, float fMoveTime, float fMoveSpeedX, float fMoveSpeedY, float fMoveAccelerationSpeedX = 0.0f, float fMoveAccelerationSpeedY = 0.0f, float fDelayTime = 0.0f)
    {
        if (pUIMain == null || (fMoveTime <= 0.0f || (fMoveSpeedX.Equals(0.0f) && fMoveSpeedY.Equals(0.0f) && fMoveAccelerationSpeedX.Equals(0.0f) && fMoveAccelerationSpeedY.Equals(0.0f)))) yield break;

        float fTime = 0.0f;
        UI_Base pUIBase = pUIMain.GetUIBase();
        RectTransform pRectTransform = pUIBase.GetGameObject().GetComponent<RectTransform>();

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (fTime <= fMoveTime)
        {
            pRectTransform.anchoredPosition = new Vector2(pRectTransform.anchoredPosition.x + fMoveSpeedX, pRectTransform.anchoredPosition.y + fMoveSpeedY);
            fMoveSpeedX += fMoveAccelerationSpeedX;
            fMoveSpeedY += fMoveAccelerationSpeedY;
            fTime += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIScale(UI_Main pUIMain, Vector2 vScaleSpeed, Vector2 vStartScale, Vector2 vTargetScale, float fDelayTime = 0.0f)
    {
        if (pUIMain == null) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        RectTransform pRectTransform = pUIBase.GetGameObject().GetComponent<RectTransform>();
        pRectTransform.localScale = new Vector2(vStartScale.x, vStartScale.y);
        bool bDone = false;

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (bDone.Equals(false))
        {
            pRectTransform.localScale = new Vector2(pRectTransform.localScale.x + vScaleSpeed.x, pRectTransform.localScale.x + vScaleSpeed.y);
            if ((vScaleSpeed.x > 0.0f && pRectTransform.localScale.x > vTargetScale.x) || (vScaleSpeed.x < 0.0f && pRectTransform.localScale.x < vTargetScale.x))
            {
                pRectTransform.localScale = new Vector2(vTargetScale.x, pRectTransform.localScale.y);
            }
            if ((vScaleSpeed.y > 0.0f && pRectTransform.localScale.y > vTargetScale.y) || (vScaleSpeed.y < 0.0f && pRectTransform.localScale.y < vTargetScale.y))
            {
                pRectTransform.localScale = new Vector2(pRectTransform.localScale.x, vTargetScale.y);
            }
            if (pRectTransform.localScale.x.Equals(vTargetScale.x) && pRectTransform.localScale.y.Equals(vTargetScale.y))
            {
                bDone = true;
            }
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIAlpha(UI_Main pUIMain, float fAlphaSpeed, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fTargetAlpha = 1.0f)
    {
        if (pUIMain == null || fAlphaSpeed.Equals(0.0f)) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        DelegateCommon pDel = null;
        float fAlpha = fStartAlpha;
        bool bDone = false;

        switch (pUIBase.GetUIType())
        {
            case EUIType.Type_Image:
            case EUIType.Type_Panel:
            case EUIType.Type_Button:
            case EUIType.Type_Scrollbar:
                Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha);
                pDel = () => { pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha); };
                break;
            case EUIType.Type_RawImage:
                RawImage pRawImage = pUIBase.GetGameObject().GetComponent<RawImage>();
                pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha);
                pDel = () => { pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha); };
                break;
            case EUIType.Type_Text:
                Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha);
                pDel = () => { pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha); };
                break;
            default:
                break;
        }

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (bDone.Equals(false))
        {
            fAlpha += fAlphaSpeed;
            if ((fAlphaSpeed > 0.0f && fAlpha > fTargetAlpha) || (fAlphaSpeed < 0.0f && fAlpha < fTargetAlpha))
            {
                fAlpha = fTargetAlpha;
                bDone = true;
            }
            pDel();
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIAlphaFlash(UI_Main pUIMain, int iFlashCount = 8, float fFlashSpeed = 0.075f, float fAlphaMin = 0.25f, float fAlphaMax = 0.75f)
    {
        if (pUIMain == null) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        DelegateCommon pDel = null;
        int iCount = 0;

        switch (pUIBase.GetUIType())
        {
            case EUIType.Type_Image:
            case EUIType.Type_Panel:
            case EUIType.Type_Button:
            case EUIType.Type_Scrollbar:
                Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                pDel = () => { pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, (iCount % 2).Equals(0) ? fAlphaMin : fAlphaMax); };
                break;
            case EUIType.Type_RawImage:
                RawImage pRawImage = pUIBase.GetGameObject().GetComponent<RawImage>();
                pDel = () => { pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, (iCount % 2).Equals(0) ? fAlphaMin : fAlphaMax); };
                break;
            case EUIType.Type_Text:
                Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                pDel = () => { pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, (iCount % 2).Equals(0) ? fAlphaMin : fAlphaMax); };
                break;
            default:
                break;
        }

        while (iCount <= iFlashCount)
        {
            iCount++;
            pDel();
            yield return Timing.WaitForSeconds(fFlashSpeed);
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIAlphaPingpong(UI_Main pUIMain, float fAlphaSpeed, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        if (pUIMain == null || fAlphaSpeed.Equals(0.0f)) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        DelegateCommon pDel = null;
        int iCount = 0;
        float fAlpha = fStartAlpha;

        switch (pUIBase.GetUIType())
        {
            case EUIType.Type_Image:
            case EUIType.Type_Panel:
            case EUIType.Type_Button:
            case EUIType.Type_Scrollbar:
                Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha);
                pDel = () => { pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha); };
                break;
            case EUIType.Type_RawImage:
                RawImage pRawImage = pUIBase.GetGameObject().GetComponent<RawImage>();
                pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha);
                pDel = () => { pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha); };
                break;
            case EUIType.Type_Text:
                Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha);
                pDel = () => { pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha); };
                break;
            default:
                break;
        }

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (true)
        {
            fAlpha += fAlphaSpeed;
            if (fAlphaSpeed > 0.0f && fAlpha > fAlphaMax)
            {
                fAlpha = fAlphaMax;
                fAlphaSpeed *= -1;
                iCount++;
            }
            else if (fAlphaSpeed < 0.0f && fAlpha < fAlphaMin)
            {
                fAlpha = fAlphaMin;
                fAlphaSpeed *= -1;
                iCount++;
            }
            pDel();

            if (!iPingpongCount.Equals(0) && iCount.Equals(iPingpongCount))
            {
                break;
            }
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIAlphaPingpongText(UI_Main pUIMain, float fAlphaSpeed, string szChangeText, int iPingpongCount = 2, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        if (pUIMain == null || fAlphaSpeed.Equals(0.0f)) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        DelegateCommon pDel = null;
        int iCount = 0;
        float fAlpha = fStartAlpha;

        switch (pUIBase.GetUIType())
        {
            case EUIType.Type_Image:
            case EUIType.Type_Panel:
            case EUIType.Type_Button:
            case EUIType.Type_Scrollbar:
                Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha);
                pDel = () => { pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha); };
                break;
            case EUIType.Type_RawImage:
                RawImage pRawImage = pUIBase.GetGameObject().GetComponent<RawImage>();
                pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha);
                pDel = () => { pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha); };
                break;
            case EUIType.Type_Text:
                Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha);
                pDel = () => { pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha); };
                break;
            default:
                break;
        }

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (true)
        {
            fAlpha += fAlphaSpeed;
            if (fAlphaSpeed > 0.0f && fAlpha > fAlphaMax)
            {
                fAlpha = fAlphaMax;
                fAlphaSpeed *= -1;
                iCount++;
                if (pUIBase.GetUIType().Equals(EUIType.Type_Text))
                {
                    Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                    pText.text = szChangeText;
                }
            }
            else if (fAlphaSpeed < 0.0f && fAlpha < fAlphaMin)
            {
                fAlpha = fAlphaMin;
                fAlphaSpeed *= -1;
                iCount++;
                if (pUIBase.GetUIType().Equals(EUIType.Type_Text))
                {
                    Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                    pText.text = szChangeText;
                }
            }
            pDel();

            if (!iPingpongCount.Equals(0) && iCount.Equals(iPingpongCount))
            {
                break;
            }
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    public IEnumerator<float> DelegateUIAlphaPingpongImage(UI_Main pUIMain, float fAlphaSpeed, int iChangeSprite, int iPingpongCount = 2, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        if (pUIMain == null || fAlphaSpeed.Equals(0.0f)) yield break;

        UI_Base pUIBase = pUIMain.GetUIBase();
        DelegateCommon pDel = null;
        int iCount = 0;
        float fAlpha = fStartAlpha;

        switch (pUIBase.GetUIType())
        {
            case EUIType.Type_Image:
            case EUIType.Type_Panel:
            case EUIType.Type_Button:
            case EUIType.Type_Scrollbar:
                Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha);
                pDel = () => { pImage.color = new Color(pImage.color.r, pImage.color.g, pImage.color.b, fAlpha); };
                break;
            case EUIType.Type_RawImage:
                RawImage pRawImage = pUIBase.GetGameObject().GetComponent<RawImage>();
                pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha);
                pDel = () => { pRawImage.color = new Color(pRawImage.color.r, pRawImage.color.g, pRawImage.color.b, fAlpha); };
                break;
            case EUIType.Type_Text:
                Text pText = pUIBase.GetGameObject().GetComponent<Text>();
                pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha);
                pDel = () => { pText.color = new Color(pText.color.r, pText.color.g, pText.color.b, fAlpha); };
                break;
            default:
                break;
        }

        if (!fDelayTime.Equals(0.0f))
        {
            yield return Timing.WaitForSeconds(fDelayTime);
        }
        while (true)
        {
            fAlpha += fAlphaSpeed;
            if (fAlphaSpeed > 0.0f && fAlpha > fAlphaMax)
            {
                fAlpha = fAlphaMax;
                fAlphaSpeed *= -1;
                iCount++;
                if (pUIBase.GetUIType().Equals(EUIType.Type_Image))
                {
                    Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                    pImage.sprite = pUISpriteArray[iChangeSprite];
                }
            }
            else if (fAlphaSpeed < 0.0f && fAlpha < fAlphaMin)
            {
                fAlpha = fAlphaMin;
                fAlphaSpeed *= -1;
                iCount++;
                if (pUIBase.GetUIType().Equals(EUIType.Type_Image))
                {
                    Image pImage = pUIBase.GetGameObject().GetComponent<Image>();
                    pImage.sprite = pUISpriteArray[iChangeSprite];
                }
            }
            pDel();

            if (!iPingpongCount.Equals(0) && iCount.Equals(iPingpongCount))
            {
                break;
            }
            yield return Timing.WaitForOneFrame;
        }
        yield break;
    }
    #endregion
}
