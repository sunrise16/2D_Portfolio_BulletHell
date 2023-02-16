#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class SoundManager : UnitySingleton<SoundManager>
{
    #region VARIABLE
    [HideInInspector] public GameObject pBGM;
    [HideInInspector] public GameObject pSFX;
    [HideInInspector] public AudioClip[] pBGMClipArray;
    [HideInInspector] public AudioClip[] pSFXClipArray;

    private AudioSource pBGMSource;
    private List<AudioSource> pSFXSourceList;
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
        if (bInit.Equals(true))
        {
            return;
        }

        if (pBGM != null)
        {
            Destroy(pBGM);
            pBGM = null;
        }
        if (pSFX != null)
        {
            Destroy(pSFX);
            pSFX = null;
        }

        pBGM = new GameObject("BGM");
        pBGMSource = pBGM.AddComponent<AudioSource>();
        pBGMSource.playOnAwake = false;
        pBGMSource.volume = (float)GlobalData.iBGMVolume / 10.0f;
        pBGMSource.pitch = 1.0f;
        pBGM.transform.parent = transform;

        pSFXSourceList = new List<AudioSource>();

        pSFX = new GameObject("Sound Effect");
        pSFX.transform.parent = transform;

        pBGMClipArray = Resources.LoadAll<AudioClip>(GlobalData.szBGMClipPath);
        pSFXClipArray = Resources.LoadAll<AudioClip>(GlobalData.szSFXClipPath);

        for (int i = 0; i < pSFXClipArray.Length; i++)
        {
            GameObject pSoundObject = GameObject.Instantiate(Resources.Load(GlobalData.szSoundPrefabPath)) as GameObject;
            pSoundObject.name = pSFXClipArray[i].name;
            pSoundObject.transform.parent = pSFX.transform;
            pSoundObject.SetActive(true);

            AudioSource pAudioSource = pSoundObject.GetComponent<AudioSource>();
            pAudioSource.clip = pSFXClipArray[i];
            pAudioSource.volume = (float)GlobalData.iSFXVolume / 10.0f;
            pAudioSource.loop = false;
            pAudioSource.playOnAwake = false;
            pSFXSourceList.Add(pAudioSource);
        }
        bInit = true;
    }
    public void ChangeVolume(string szSoundType, string szCommand)
    {
        if (szSoundType.Equals("BGM"))
        {
            if (szCommand.Equals("Up"))
            {
                GlobalData.iBGMVolume++;
                if (GlobalData.iBGMVolume > 10) GlobalData.iBGMVolume = 0;
            }
            else if (szCommand.Equals("Down"))
            {
                GlobalData.iBGMVolume--;
                if (GlobalData.iBGMVolume < 0) GlobalData.iBGMVolume = 10;
            }
            pBGMSource.volume = (float)GlobalData.iBGMVolume / 10.0f;
        }
        else if (szSoundType.Equals("SE"))
        {
            if (szCommand.Equals("Up"))
            {
                GlobalData.iSFXVolume++;
                if (GlobalData.iSFXVolume > 10) GlobalData.iSFXVolume = 0;
            }
            else if (szCommand.Equals("Down"))
            {
                GlobalData.iSFXVolume--;
                if (GlobalData.iSFXVolume < 0) GlobalData.iSFXVolume = 10;
            }
            for (int i = 0; i < pSFXSourceList.Count; i++)
            {
                pSFXSourceList[i].volume = (float)GlobalData.iSFXVolume / 10.0f;
            }
        }
    }
    public void PlayBGM(EBGMType enBGMType, bool bLoop = false)
    {
        if (pBGMSource == null) return;

        float fVolume = (float)GlobalData.iBGMVolume / 10.0f;

        if (pBGMSource.isPlaying.Equals(true))
        {
            Timing.RunCoroutine(ChangeBGM(enBGMType, bLoop));
        }
        else
        {
            pBGMSource.clip = pBGMClipArray[Convert.ToInt32(enBGMType)];
            pBGMSource.volume = fVolume;
            pBGMSource.loop = bLoop;
            pBGMSource.Play();
        }
    }
    public void PlaySFX(ESFXType enSFXType, bool bLoop = false)
    {
        int iIndex = (int)enSFXType;

        if (pSFXSourceList[iIndex] == null) return;

        float fVolume = (float)GlobalData.iSFXVolume / 10.0f;

        if (pSFXSourceList[iIndex].isPlaying.Equals(true))
        {
            pSFXSourceList[iIndex].Stop();
        }
        pSFXSourceList[iIndex].volume = fVolume;
        pSFXSourceList[iIndex].loop = bLoop;
        pSFXSourceList[iIndex].Play();
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> StopBGM(bool bSmooth = false, float fVolumeSpeed = 0.02f)
    {
        if (pBGMSource == null) yield break;

        float fVolume = (float)GlobalData.iBGMVolume / 10.0f;

        if (bSmooth.Equals(true))
        {
            while (pBGMSource.volume > 0.0f)
            {
                pBGMSource.volume -= fVolumeSpeed;

                yield return Timing.WaitForOneFrame;
            }
        }
        pBGMSource.Stop();
        pBGMSource.volume = fVolume;

        yield break;
    }
    public IEnumerator<float> ChangeBGM(EBGMType enBGMType, bool bLoop = false, float fVolumeSpeed = 0.03f)
    {
        if (pBGMSource == null) yield break;

        float fVolume = (float)GlobalData.iBGMVolume / 10.0f;

        while (pBGMSource.volume > 0.0f)
        {
            pBGMSource.volume -= fVolumeSpeed;

            yield return Timing.WaitForOneFrame;
        }
        pBGMSource.Stop();
        pBGMSource.clip = pBGMClipArray[Convert.ToInt32(enBGMType)];
        pBGMSource.volume = fVolume;
        pBGMSource.loop = bLoop;
        pBGMSource.Play();

        yield break;
    }
    #endregion
}
