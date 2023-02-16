#region USING
using System.Collections.Generic;
using UnityEngine;
#endregion

public class BulletPool : IPoolBase
{
    #region VARIABLE
    private List<GameObject> pBulletList;
    private Transform pBulletParent;
    private Transform pActiveBulletParent;
    #endregion

    #region CONSTRUCTOR
    public BulletPool(Transform pBulletParent, Transform pActiveBulletParent)
    {
        pBulletList = new List<GameObject>();
        this.pBulletParent = pBulletParent;
        this.pActiveBulletParent = pActiveBulletParent;
    }
    #endregion

    #region GET METHOD
    public Transform GetActiveBulletParent() { return pActiveBulletParent; }
    #endregion

    #region COMMON METHOD
    public int GetPoolCount()
    {
        return pBulletList.Count;
    }
    public GameObject AddPool(Vector3 vPosition, Vector3 vRotation, Vector3 vScale)
    {
        GameObject pBulletObject = GameObject.Instantiate(Resources.Load(GlobalData.szBulletPrefabPath)) as GameObject;
        Transform pTransform = pBulletObject.GetComponent<Transform>();
        pBulletObject.name = "Bullet";

        pBulletList.Add(pBulletObject);
        pTransform.position = vPosition;
        pTransform.rotation = Quaternion.Euler(vRotation);
        pTransform.localScale = vScale;
        pTransform.parent = pBulletParent;
        pBulletObject.SetActive(false);
        return pBulletObject;
    }
    public void ReturnPool(GameObject pBulletObject)
    {
        Transform pTransform = pBulletObject.GetComponent<Transform>();
        Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
        Bullet_Base pBulletBase = pBulletMain.GetBulletBase();
        BoxCollider2D pBoxCollider = pBulletObject.GetComponent<BoxCollider2D>();
        CapsuleCollider2D pCapsuleCollider = pBulletObject.GetComponent<CapsuleCollider2D>();
        CircleCollider2D pCircleCollider = pBulletObject.GetComponent<CircleCollider2D>();

        pBulletList.Add(pBulletObject);
        pTransform.parent = pBulletParent;
        pBoxCollider.size = pBoxCollider.offset = Vector2.zero;
        pBoxCollider.enabled = false;
        pCapsuleCollider.size = pCapsuleCollider.offset = Vector2.zero;
        pCapsuleCollider.enabled = false;
        pCircleCollider.radius = 0.0f;
        pCircleCollider.offset = Vector2.zero;
        pCircleCollider.enabled = false;
        if (!pBulletBase.GetBulletType().Equals(EBulletType.Type_Empty))
        {
            pBulletBase.GetSpriteRenderer().color = Color.white;
            pBulletBase.GetSpriteRenderer().sprite = null;
            pBulletBase.GetAnimator().runtimeAnimatorController = null;
        }
        pBulletBase.AllReset();
        // if (pTransform.childCount > 1)
        // {
        //     for (int i = 1; i < pTransform.childCount; i++)
        //     {
        //         if (pTransform.GetChild(i).GetComponent<Bullet_Main>())
        //         {
        //             BulletManager.Instance.GetBulletPool().ReturnPool(pTransform.GetChild(i).gameObject);
        //         }
        //         else if (pTransform.GetChild(i).GetComponent<Effect_Main>())
        //         {
        //             EffectManager.Instance.GetEffectPool().ReturnPool(pTransform.GetChild(i).gameObject);
        //         }
        //         else if (pTransform.GetChild(i).GetComponent<Enemy_Main>())
        //         {
        //             EnemyManager.Instance.GetEnemyPool().ReturnPool(pTransform.GetChild(i).gameObject);
        //         }
        //         i--;
        //     }
        // }
    }
    public GameObject ExtractBullet(Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag, EBulletType enBulletType,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        GameObject pBulletObject = pBulletList.Count.Equals(0) ? AddPool(vPosition, vRotation, vScale) : pBulletList[0];
        Transform pTransform = pBulletObject.GetComponent<Transform>();

        pTransform.parent = pActiveBulletParent;
        pBulletList.RemoveAt(0);

        switch (enBulletType)
        {
            case EBulletType.Type_Empty:
                return SettingEmptyBullet(ref pBulletObject, ref pTransform, vPosition, vRotation, vScale, szBulletTag, enBulletShooterType, enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
            case EBulletType.Type_Circle:
                return SettingCircleBullet(ref pBulletObject, ref pTransform, vPosition, vRotation, vScale, szBulletTag, enBulletShooterType, enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
            case EBulletType.Type_Capsule:
                return SettingCapsuleBullet(ref pBulletObject, ref pTransform, vPosition, vRotation, vScale, szBulletTag, enBulletShooterType, enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
            case EBulletType.Type_Box:
                return SettingBoxBullet(ref pBulletObject, ref pTransform, vPosition, vRotation, vScale, szBulletTag, enBulletShooterType, enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
            default:
                break;
        }
        return null;
    }
    public GameObject SettingEmptyBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
        pBulletMain.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EBulletType.Type_Empty,
            enBulletShooterType.Equals(EBulletShooterType.Type_Player) ? EBulletShooterType.Type_Player : EBulletShooterType.Type_Enemy,
            enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
        return pBulletObject;
    }
    public GameObject SettingCircleBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
        pBulletMain.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EBulletType.Type_Circle,
            enBulletShooterType.Equals(EBulletShooterType.Type_Player) ? EBulletShooterType.Type_Player : EBulletShooterType.Type_Enemy,
            enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
        Bullet_Base pBulletBase = pBulletMain.GetBulletBase();
        pBulletBase.GetCircleCollider().enabled = true;

        if (enBulletShooterType.Equals(EBulletShooterType.Type_Player))
        {
            switch (enPlayerBulletType)
            {
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                case EEnemyBulletType.Type_Circle_01:
                case EEnemyBulletType.Type_Circle_02:
                case EEnemyBulletType.Type_Circle_03:
                case EEnemyBulletType.Type_Circle_04:
                case EEnemyBulletType.Type_Circle_05:
                case EEnemyBulletType.Type_Circle_06:
                case EEnemyBulletType.Type_Circle_07:
                case EEnemyBulletType.Type_Circle_08:
                case EEnemyBulletType.Type_Circle_09:
                    pBulletBase.GetCircleCollider().radius = 0.07f;
                    break;
                default:
                    break;
            }
        }
        return pBulletObject;
    }
    public GameObject SettingCapsuleBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
        pBulletMain.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EBulletType.Type_Capsule,
            enBulletShooterType.Equals(EBulletShooterType.Type_Player) ? EBulletShooterType.Type_Player : EBulletShooterType.Type_Enemy,
            enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
        Bullet_Base pBulletBase = pBulletMain.GetBulletBase();
        pBulletBase.GetCapsuleCollider().enabled = true;

        if (enBulletShooterType.Equals(EBulletShooterType.Type_Player))
        {
            switch (enPlayerBulletType)
            {
                case EPlayerBulletType.Type_APrimary:
                    pBulletObject.transform.GetChild(0).localScale = new Vector2(0.5f, 0.5f);
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.25f, 0.5f);
                    break;
                case EPlayerBulletType.Type_ASecondary:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.2f, 0.75f);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                case EEnemyBulletType.Type_Glow_01:
                case EEnemyBulletType.Type_Glow_02:
                case EEnemyBulletType.Type_Glow_03:
                case EEnemyBulletType.Type_Glow_04:
                case EEnemyBulletType.Type_Glow_05:
                case EEnemyBulletType.Type_Glow_06:
                case EEnemyBulletType.Type_Glow_07:
                case EEnemyBulletType.Type_Glow_08:
                case EEnemyBulletType.Type_Glow_09:
                case EEnemyBulletType.Type_Glow_10:
                    pBulletObject.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.08f, 0.2f);
                    break;
                case EEnemyBulletType.Type_Glow_11:
                case EEnemyBulletType.Type_Glow_12:
                case EEnemyBulletType.Type_Glow_13:
                case EEnemyBulletType.Type_Glow_14:
                case EEnemyBulletType.Type_Glow_15:
                case EEnemyBulletType.Type_Glow_16:
                case EEnemyBulletType.Type_Glow_17:
                case EEnemyBulletType.Type_Glow_18:
                case EEnemyBulletType.Type_Glow_19:
                case EEnemyBulletType.Type_Glow_20:
                    pBulletObject.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.12f, 1.0f);
                    pBulletBase.GetCapsuleCollider().offset = new Vector2(0.0f, 0.15f);
                    break;
                case EEnemyBulletType.Type_SmallArrow_01:
                case EEnemyBulletType.Type_SmallArrow_02:
                case EEnemyBulletType.Type_SmallArrow_03:
                case EEnemyBulletType.Type_SmallArrow_04:
                case EEnemyBulletType.Type_SmallArrow_05:
                case EEnemyBulletType.Type_SmallArrow_06:
                case EEnemyBulletType.Type_SmallArrow_07:
                case EEnemyBulletType.Type_SmallArrow_08:
                case EEnemyBulletType.Type_SmallArrow_09:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.08f, 0.2f);
                    pBulletBase.GetCapsuleCollider().offset = new Vector2(0.0f, 0.02f);
                    break;
                case EEnemyBulletType.Type_MediumArrow_01:
                case EEnemyBulletType.Type_MediumArrow_02:
                case EEnemyBulletType.Type_MediumArrow_03:
                case EEnemyBulletType.Type_MediumArrow_04:
                case EEnemyBulletType.Type_MediumArrow_05:
                case EEnemyBulletType.Type_MediumArrow_06:
                case EEnemyBulletType.Type_MediumArrow_07:
                case EEnemyBulletType.Type_MediumArrow_08:
                case EEnemyBulletType.Type_MediumArrow_09:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.1f, 0.3f);
                    break;
                case EEnemyBulletType.Type_LargeArrow_01:
                case EEnemyBulletType.Type_LargeArrow_02:
                case EEnemyBulletType.Type_LargeArrow_03:
                case EEnemyBulletType.Type_LargeArrow_04:
                case EEnemyBulletType.Type_LargeArrow_05:
                case EEnemyBulletType.Type_LargeArrow_06:
                case EEnemyBulletType.Type_LargeArrow_07:
                case EEnemyBulletType.Type_LargeArrow_08:
                case EEnemyBulletType.Type_LargeArrow_09:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.1f, 0.5f);
                    break;
                default:
                    break;
            }
        }
        return pBulletObject;
    }
    public GameObject SettingBoxBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
        pBulletMain.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EBulletType.Type_Box,
            enBulletShooterType.Equals(EBulletShooterType.Type_Player) ? EBulletShooterType.Type_Player : EBulletShooterType.Type_Enemy,
            enPlayerBulletType, enEnemyBulletType, fAlpha, fDestroyPadding);
        Bullet_Base pBulletBase = pBulletMain.GetBulletBase();
        pBulletBase.GetBoxCollider().enabled = true;

        if (enBulletShooterType.Equals(EBulletShooterType.Type_Player))
        {
            switch (enPlayerBulletType)
            {
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                default:
                    break;
            }
        }
        return pBulletObject;
    }
    #endregion
}

public class BulletManager : Singleton<BulletManager>
{
    #region VARIABLE
    private BulletPool pBulletPool;
    #endregion

    #region GET METHOD
    public BulletPool GetBulletPool() { return pBulletPool; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        Transform pBulletParent = GameObject.Find("BULLETPOOL").GetComponent<Transform>();
        Transform pActiveBulletParent = GameObject.Find("ACTIVEBULLET").GetComponent<Transform>();
        pBulletPool = new BulletPool(pBulletParent, pActiveBulletParent);
    }
    #endregion
}