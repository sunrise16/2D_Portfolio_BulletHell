#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class EffectManager : UnitySingleton<EffectManager>
{
    #region VARIABLE
    [HideInInspector] public Transform pEffectParent;
    public GameObject[] pEffectArray;

    private bool bInit = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        pEffectParent = GameObject.Find("EFFECT").GetComponent<Transform>();

        if (bInit.Equals(true))
        {
            return;
        }

        pEffectArray = Resources.LoadAll<GameObject>(GlobalData.szEffectPrefabPath);
        bInit = true;
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> ExtractEffect(Vector3 vPosition, Vector3 vRotation, Vector3 vScale, EEffectType enEffectType, float fActiveTime)
    {
        GameObject pEffectObject = GameObject.Instantiate(pEffectArray[(int)enEffectType]);
        Transform pTransform = pEffectObject.GetComponent<Transform>();

        pTransform.position = vPosition;
        pTransform.rotation = Quaternion.Euler(vRotation);
        pTransform.localScale = vScale;
        pTransform.parent = pEffectParent;
        pEffectObject.SetActive(true);

        yield return Timing.WaitForSeconds(fActiveTime);

        Destroy(pEffectObject);

        yield break;
    }
    #endregion
}