#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class EnemyManager : Singleton<EnemyManager>
{
    #region VARIABLE
    public Transform pEnemyParent;
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        pEnemyParent = GameObject.Find("ACTIVEENEMY").GetComponent<Transform>();
    }
    public GameObject ExtractEnemy(Vector3 vPosition, Vector3 vRotation, Vector3 vScale)
    {
        GameObject pEnemyObject = GameObject.Instantiate(Resources.Load(GlobalData.szEnemyPrefabPath)) as GameObject;
        Transform pTransform = pEnemyObject.GetComponent<Transform>();
        pEnemyObject.name = "Enemy";

        pTransform.position = vPosition;
        pTransform.rotation = Quaternion.Euler(vRotation);
        pTransform.localScale = vScale;
        pTransform.parent = pEnemyParent;
        pEnemyObject.SetActive(false);
        return pEnemyObject;
    }
    public GameObject ExtractEnemy(Vector3 vPosition, Vector3 vRotation, Vector3 vScale, EEnemySpriteType enEnemySpriteType, string szEnemyTag, float fEnemyHP, float fDestroyPadding, bool bCounter, bool bOutScreenShot)
    {
        GameObject pEnemyObject = ExtractEnemy(vPosition, vRotation, vScale);
        Transform pTransform = pEnemyObject.GetComponent<Transform>();
        Enemy_Main pEnemyMain = pEnemyObject.GetComponent<Enemy_Main>();

        pEnemyMain.Init(pEnemyObject, pTransform, vPosition, vRotation, vScale, enEnemySpriteType, szEnemyTag, fEnemyHP, fDestroyPadding, bCounter, bOutScreenShot);
        pTransform.parent = pEnemyParent;
        return pEnemyObject;
    }
    #endregion
}