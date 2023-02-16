#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
#endregion

public class UI_Main : MonoBehaviour
{
    #region VARIABLE
    public DelegateCommon pDelegateCommon;
    public DelegateUICommon pDelegateUICommon;
    public DelegateUIMove pDelegateUIMove;
    public DelegateUIScale pDelegateUIScale;
    public DelegateUIAlpha pDelegateUIAlpha;
    public DelegateUIAlphaFlash pDelegateUIAlphaFlash;
    public DelegateUIAlphaPingpong pDelegateUIAlphaPingpong;
    public DelegateUIAlphaPingpongText pDelegateUIAlphaPingpongText;
    public DelegateUIAlphaPingpongImage pDelegateUIAlphaPingpongImage;

    private UI_Base pUIBase;
    private Timer pTimer;
    #endregion

    #region GET METHOD
    public UI_Base GetUIBase() { return (pUIBase != null) ? pUIBase : null; }
    public Timer GetTimer() { return (pTimer != null) ? pTimer : null; }
    #endregion

    #region COMMON METHOD
    public virtual void Init(GameObject pUIObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szUITag, EUIType enUIType, float[] fData)
    {
        if (pUIBase == null)
        {
            pUIBase = new UI_Base(pUIObject, pTransform, vPosition, vRotation, vScale, szUITag, enUIType, fData);
            pTimer = new Timer(szUITag);
        }
        else
        {
            // 혹시 재활용 코드 작성해야 할 경우에 작성할 것
        }
    }
    public void RunDelegateCommon()
    {
        pDelegateCommon();
    }
    public void RunDelegateUICommon(string szCoroutineTag)
    {
        Timing.RunCoroutine(pDelegateUICommon(this), szCoroutineTag);
    }
    public void RunDelegateUIMove(string szCoroutineTag, float fMoveTime, float fMoveSpeedX, float fMoveSpeedY, float fMoveAccelerationSpeedX = 0.0f, float fMoveAccelerationSpeedY = 0.0f, float fDelayTime = 0.0f)
    {
        Timing.RunCoroutine(pDelegateUIMove(this, fMoveTime, fMoveSpeedX, fMoveSpeedY, fMoveAccelerationSpeedX, fMoveAccelerationSpeedY, fDelayTime), szCoroutineTag);
    }
    public void RunDelegateUIScale(string szCoroutineTag, Vector2 vScaleSpeed, Vector2 vStartScale, Vector2 vTargetScale, float fDelayTime = 0.0f)
    {
        Timing.RunCoroutine(pDelegateUIScale(this, vScaleSpeed, vStartScale, vTargetScale, fDelayTime), szCoroutineTag);
    }
    public void RunDelegateUIAlpha(string szCoroutineTag, float fAlphaSpeed, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fTargetAlpha = 1.0f)
    {
        Timing.RunCoroutine(pDelegateUIAlpha(this, fAlphaSpeed, fDelayTime, fStartAlpha, fTargetAlpha), szCoroutineTag);
    }
    public void RunDelegateUIAlphaFlash(string szCoroutineTag, int iFlashCount = 8, float fFlashSpeed = 0.075f, float fAlphaMin = 0.25f, float fAlphaMax = 0.75f)
    {
        Timing.RunCoroutine(pDelegateUIAlphaFlash(this, iFlashCount, fFlashSpeed, fAlphaMin, fAlphaMax), szCoroutineTag);
    }
    public void RunDelegateUIAlphaPingpong(string szCoroutineTag, float fAlphaSpeed, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        Timing.RunCoroutine(pDelegateUIAlphaPingpong(this, fAlphaSpeed, iPingpongCount, fDelayTime, fStartAlpha, fAlphaMin, fAlphaMax), szCoroutineTag);
    }
    public void RunDelegateUIAlphaPingpongText(string szCoroutineTag, float fAlphaSpeed, string szChangeText, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        Timing.RunCoroutine(pDelegateUIAlphaPingpongText(this, fAlphaSpeed, szChangeText, iPingpongCount, fDelayTime, fStartAlpha, fAlphaMin, fAlphaMax), szCoroutineTag);
    }
    public void RunDelegateUIAlphaPingpongImage(string szCoroutineTag, float fAlphaSpeed, int iChangeSprite, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f)
    {
        Timing.RunCoroutine(pDelegateUIAlphaPingpongImage(this, fAlphaSpeed, iChangeSprite, iPingpongCount, fDelayTime, fStartAlpha, fAlphaMin, fAlphaMax), szCoroutineTag);
    }
    public void KillDelegateUI(string szCoroutineTag)
    {
        Timing.KillCoroutines(szCoroutineTag);
    }
    public void PauseDelegateUI(string szCoroutineTag)
    {
        Timing.PauseCoroutines(szCoroutineTag);
    }
    public void ResumeDelegateUI(string szCoroutineTag)
    {
        Timing.ResumeCoroutines(szCoroutineTag);
    }
    #endregion
}