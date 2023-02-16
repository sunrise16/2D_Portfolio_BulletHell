#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class Timer
{
    #region VARIABLE
    private string szTimerTag;                  // 타이머 고유 이름
    private int iFlag;                          // 타이머 고유 넘버링
    private int iRepeatCount;                   // 타이머 현재 반복 횟수
    private int iRepeatLimit;                   // 타이머 반복 횟수 제한값 (값이 0이면 무한 반복)
    private float fTime;                        // 타이머 경과 시간값
    private float fLapsedTime;                  // 타이머 경과 시간 누적값
    private float fDelayTime;                   // 타이머 시작 전 딜레이값
    private float fTriggerTime;                 // 타이머 시간 목표값 (해당 시간에 도달하면 타이머 행동 처리)
    private bool bSwitch;                       // 타이머 ON, OFF 여부
    private bool bTrigger;                      // 타이머 행동 처리 여부
    private bool bPause;                        // 타이머 일시정지 여부
    #endregion

    #region CONSTRUCTOR
    public Timer(string szTimerTag, int iFlag = 0, int iRepeatLimit = 0, float fDelayTime = 0.0f, float fTriggerTime = 0.0f, bool bSwitch = false)
    {
        InitTimer(szTimerTag, iFlag, iRepeatLimit, fDelayTime, fTriggerTime, bSwitch);
    }
    #endregion

    #region GET METHOD
    public string GetTimerTag() { return szTimerTag; }
    public int GetFlag() { return iFlag; }
    public int GetRepeatCount() { return iRepeatCount; }
    public int GetRepeatLimit() { return iRepeatLimit; }
    public float GetTime() { return fTime; }
    public float GetDelayTime() { return fDelayTime; }
    public float GetLapsedTime() { return fLapsedTime; }
    public float GetTriggerTime() { return fTriggerTime; }
    public bool GetSwitch() { return bSwitch; }
    public bool GetTrigger() { return bTrigger; }
    public bool GetPause() { return bPause; }
    #endregion

    #region SET METHOD
    public void SetTimerTag(string szTimerTag) { this.szTimerTag = szTimerTag; }
    public void SetFlag(int iFlag) { this.iFlag = iFlag; }
    public void SetRepeatLimit(int iRepeatLimit) { this.iRepeatLimit = iRepeatLimit; }
    public void SetDelayTime(float fDelayTime) { this.fDelayTime = fDelayTime; }
    public void SetTriggerTime(float fTriggerTime) { this.fTriggerTime = fTriggerTime; }
    public void SetSwitch(bool bSwitch)
    {
        if (this.bSwitch.Equals(false) && bSwitch.Equals(true))
        {
            RunTimer();
        }
        else if (this.bSwitch.Equals(true) && bSwitch.Equals(false))
        {
            KillTimer();
        }
        this.bSwitch = bSwitch;
    }
    public void SetTrigger(bool bTrigger) { this.bTrigger = bTrigger; }
    public void SetPause(bool bPause)
    {
        if (this.bPause.Equals(false) && bPause.Equals(true))
        {
            PauseTimer();
        }
        else if (this.bPause.Equals(true) && bPause.Equals(false))
        {
            ResumeTimer();
        }
        this.bPause = bPause;
    }
    #endregion

    #region COMMON METHOD
    public void InitTimer()
    {
        szTimerTag = "";
        iFlag = 0;
        iRepeatCount = 0;
        iRepeatLimit = 0;
        fTime = 0.0f;
        fLapsedTime = 0.0f;
        fDelayTime = 0.0f;
        fTriggerTime = 0.0f;
        bSwitch = false;
        bTrigger = false;
        bPause = false;
    }
    public void InitTimer(string szTimerTag, int iFlag = 0, int iRepeatLimit = 0, float fDelayTime = 0.0f, float fTriggerTime = 0.0f, bool bSwitch = false)
    {
        this.szTimerTag = szTimerTag;
        this.iFlag = iFlag;
        iRepeatCount = 0;
        this.iRepeatLimit = iRepeatLimit > 0 ? iRepeatLimit : 0;
        fTime = 0.0f;
        fLapsedTime = 0.0f;
        this.fDelayTime = fDelayTime > 0.0f ? fDelayTime : 0.0f;
        this.fTriggerTime = fTriggerTime > 0.0f ? fTriggerTime : 0.0f;
        this.bSwitch = bSwitch;
        if (bSwitch.Equals(true))
        {
            RunTimer();
        }
        bTrigger = false;
        bPause = false;
    }
    public void RunTimer()
    {
        Timing.RunCoroutine(Run(), szTimerTag);
    }
    public void KillTimer()
    {
        Timing.KillCoroutines(szTimerTag);
    }
    public void PauseTimer()
    {
        Timing.PauseCoroutines(szTimerTag);
    }
    public void ResumeTimer()
    {
        Timing.ResumeCoroutines(szTimerTag);
    }
    public void ResetTimer()
    {
        SetTrigger(false);
        if (!fTime.Equals(0.0f)) fTime = 0.0f;
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> Run()
    {
        if (!fDelayTime.Equals(0.0f))
        {
            while (fTime < fDelayTime)
            {
                fTime += Time.deltaTime;
                fLapsedTime += Time.deltaTime;
                yield return Timing.WaitForOneFrame;
            }
        }

        fTime = 0.0f;
        while (!iRepeatCount.Equals(iRepeatLimit))
        {
            fTime += Time.deltaTime;
            fLapsedTime += Time.deltaTime;

            if (fTime >= fTriggerTime)
            {
                SetTrigger(true);
                iRepeatCount++;
                fTime = 0.0f;
                if (iRepeatCount.Equals(iRepeatLimit))
                {
                    break;
                }
            }
            yield return Timing.WaitForOneFrame;
        }
        SetSwitch(false);
        yield break;
    }
    #endregion
}