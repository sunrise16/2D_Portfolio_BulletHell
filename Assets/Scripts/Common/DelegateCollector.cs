#region USING
using UnityEngine;
using System.Collections.Generic;
using MEC;
#endregion

public delegate void DelegateCommon();
public delegate void DelegateFlag(int iFlag);
public delegate IEnumerator<float> DelegateMove(float fDelayTime = 0.0f);
public delegate IEnumerator<float> DelegateCounterPattern(GameObject pEnemyObject, GameObject pPlayerObject);
public delegate IEnumerator<float> DelegateUICommon(UI_Main pUIMain);
public delegate IEnumerator<float> DelegateUIMove(UI_Main pUIMain, float fMoveTime, float fMoveSpeedX, float fMoveSpeedY, float fMoveAccelerationSpeedX = 0.0f, float fMoveAccelerationSpeedY = 0.0f, float fDelayTime = 0.0f);
public delegate IEnumerator<float> DelegateUIScale(UI_Main pUIMain, Vector2 vScaleSpeed, Vector2 vStartScale, Vector2 vTargetScale, float fDelayTime = 0.0f);
public delegate IEnumerator<float> DelegateUIAlpha(UI_Main pUIMain, float fAlphaSpeed, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fTargetAlpha = 1.0f);
public delegate IEnumerator<float> DelegateUIAlphaFlash(UI_Main pUIMain, int iFlashCount = 8, float fFlashSpeed = 0.075f, float fAlphaMin = 0.25f, float fAlphaMax = 0.75f);
public delegate IEnumerator<float> DelegateUIAlphaPingpong(UI_Main pUIMain, float fAlphaSpeed, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f);
public delegate IEnumerator<float> DelegateUIAlphaPingpongText(UI_Main pUIMain, float fAlphaSpeed, string szChangeText, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f);
public delegate IEnumerator<float> DelegateUIAlphaPingpongImage(UI_Main pUIMain, float fAlphaSpeed, int iChangeSprite, int iPingpongCount = 0, float fDelayTime = 0.0f, float fStartAlpha = 0.0f, float fAlphaMin = 0.0f, float fAlphaMax = 1.0f);