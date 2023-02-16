#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public partial class GameManager : UnitySingleton<GameManager> // a.k.a PatternManager
{
    #region VARIABLE
    private CoroutineHandle pTempHandle;
    #endregion

    #region SHOOT PATTERN METHOD
    #region PHASE 1-10
    public IEnumerator<float> Phase1_10Pattern1(GameObject pGameObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        Vector3 vTempPosition;
        Vector3 vRotation;
        float fAngle;
        float fTempAngle;

        yield return Timing.WaitForSeconds(1.5f);

        while (true)
        {
            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fAngle = UnityEngine.Random.Range(-8.0f, 8.0f);

                    for (int j = 0; j < 32; j++)
                    {
                        fTempAngle = fAngle + (11.25f * j);
                        vTempPosition = Utility.GetBulletCreatingPosition(new Vector2(pGameObject.transform.position.x, pGameObject.transform.position.y - 0.1f));
                        vRotation = new Vector3(0.0f, 0.0f, fTempAngle - 90.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (vTempPosition, vRotation, Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy,
                            EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    yield return Timing.WaitForSeconds(2.8f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    fAngle = UnityEngine.Random.Range(-8.0f, 8.0f);

                    for (int j = 0; j < 72; j++)
                    {
                        fTempAngle = fAngle + ((j < 48).Equals(true) ? (7.5f * j) : (15.0f * (j - 48)));
                        vTempPosition = Utility.GetBulletCreatingPosition(new Vector2(pGameObject.transform.position.x, pGameObject.transform.position.y - 0.1f));
                        vRotation = new Vector3(0.0f, 0.0f, fTempAngle - 90.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (vTempPosition, vRotation, Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy,
                            EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(((j < 48).Equals(true) ? 3.5f : 2.25f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    yield return Timing.WaitForSeconds(2.2f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    fAngle = UnityEngine.Random.Range(-8.0f, 8.0f);

                    for (int j = 0; j < 80; j++)
                    {
                        fTempAngle = fAngle + (9.0f * (j % 40));
                        vTempPosition = Utility.GetBulletCreatingPosition(new Vector2(pGameObject.transform.position.x, pGameObject.transform.position.y - 0.1f));
                        vRotation = new Vector3(0.0f, 0.0f, fTempAngle - 90.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (vTempPosition, vRotation, Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy,
                            EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(((j < 40).Equals(true) ? 4.0f : 2.5f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    yield return Timing.WaitForSeconds(1.6f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase1_10Pattern2(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(1.0f);

        switch (enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                for (int i = 0; i < 12; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);

                    pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule,
                        EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 10.0f);
                    pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                    pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletBase.SetBulletMoveSpeed(3.0f + (0.1f * (iPhase - 1)));
                    pBulletBase.SetBulletRotateSpeed();
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];

                    yield return Timing.WaitForSeconds(0.8f);
                }
                break;
            case EGameDifficultyType.Type_Normal:
                for (int i = 0; i < 12; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int j = 0; j < 3; ++j)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (-15.0f + (15.0f * j));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule,
                            EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.5f + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];
                    }
                    yield return Timing.WaitForSeconds(0.7f);
                }
                break;
            case EGameDifficultyType.Type_Hard:
                for (int i = 0; i < 12; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int j = 0; j < 6; ++j)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (-15.0f + (15.0f * (j % 3)));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet", EBulletType.Type_Capsule,
                            EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(((j < 3) ? 5.0f : 3.5f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];
                    }
                    yield return Timing.WaitForSeconds(0.8f);
                }
                break;
            default:
                break;
        }
        Timing.RunCoroutine(pGameObject.GetComponent<Enemy_Main>().pDelegateMove(0.0f));
        yield break;
    }
    public IEnumerator<float> Phase1_10Pattern3(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(0.5f);

        while (true)
        {
            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    for (int i = 0; i < 3; ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (i >= 1 ? UnityEngine.Random.Range(-20.0f, 20.0f) : 0.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), i.Equals(0) ? new Vector3(2.0f, 2.0f, 1.0f) : Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, i.Equals(0) ? EEnemyBulletType.Type_Circle_02 : EEnemyBulletType.Type_Circle_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(2.0f, 3.25f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[i.Equals(0) ? (int)EEnemyBulletType.Type_Circle_02 : (int)EEnemyBulletType.Type_Circle_01];
                    }
                    yield return Timing.WaitForSeconds(1.5f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    for (int i = 0; i < 8; ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (i >= 1 ? UnityEngine.Random.Range(-20.0f, 20.0f) : 0.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), i.Equals(0) ? new Vector3(2.0f, 2.0f, 1.0f) : Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, i.Equals(0) ? EEnemyBulletType.Type_Circle_02 : EEnemyBulletType.Type_Circle_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.0f, 4.5f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[i.Equals(0) ? (int)EEnemyBulletType.Type_Circle_02 : (int)EEnemyBulletType.Type_Circle_01];
                    }
                    yield return Timing.WaitForSeconds(1.5f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    for (int i = 0; i < 14; ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (i >= 1 ? UnityEngine.Random.Range(-20.0f, 20.0f) : 0.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), i.Equals(0) ? new Vector3(2.0f, 2.0f, 1.0f) : Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, i.Equals(0) ? EEnemyBulletType.Type_Circle_02 : EEnemyBulletType.Type_Circle_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(4.0f, 6.0f) + (0.1f * (iPhase - 1)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[i.Equals(0) ? (int)EEnemyBulletType.Type_Circle_02 : (int)EEnemyBulletType.Type_Circle_01];
                    }
                    yield return Timing.WaitForSeconds(1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase1_10Pattern4(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(1.0f);

        while (true)
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    for (int i = 0; i < 2; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 20; ++j)
                        {
                            fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (18.0f * (j % 20)) + (9.0f * i);

                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                                EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed(3.0f - (0.3f * i));
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption();
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];

                            if (j.Equals(19)) yield return Timing.WaitForSeconds(0.5f);
                        }
                    }
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 6 + (3 * ((iPhase - 1) / 2)); ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (-20.0f + (20.0f * (i % 3)));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(3.0f, 3.0f), "Enemy_Bullet",
                            EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_06, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(5.0f - (0.5f * (i / 3)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_06];
                    }
                    yield return Timing.WaitForSeconds(1.75f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    for (int i = 0; i < 2; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 40; ++j)
                        {
                            fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (9.0f * (j % 40)) + (4.5f * i);

                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                                EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed(4.0f + (0.4f * i));
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption();
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];

                            if (j.Equals(39)) yield return Timing.WaitForSeconds(0.5f);
                        }
                    }
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 15 + (5 * ((iPhase - 1) / 2)); ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (-20.0f + (10.0f * (i % 5)));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(3.0f, 3.0f), "Enemy_Bullet",
                            EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_06, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(5.5f - (0.5f * (i / 5)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_06];
                    }
                    yield return Timing.WaitForSeconds(1.75f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    for (int i = 0; i < 2; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 80; ++j)
                        {
                            fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (9.0f * (j % 40)) + (4.5f * i);

                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                                EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed((j < 40) ? 4.5f : 5.5f + (1.5f * i));
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption();
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];

                            if (j.Equals(79)) yield return Timing.WaitForSeconds(0.5f);
                        }
                    }
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 21 + (7 * ((iPhase - 1) / 2)); ++i)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (-30.0f + (10.0f * (i % 7)));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(3.0f, 3.0f), "Enemy_Bullet",
                            EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_06, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(6.0f - (0.8f * (i / 7)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_06];
                    }
                    yield return Timing.WaitForSeconds(1.75f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase1_10Pattern5(GameObject pGameObject, GameObject pPlayerObject, int iUniqueNumber, int iRepeatCount)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
        float fTempAngle = fAngle;
        float fSubAngle = 0.0f;

        yield return Timing.WaitForSeconds(1.0f);

        switch (enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                for (int i = 0; i < 25; ++i)
                {
                    fTempAngle = fAngle + 180.0f;
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);

                    for (int j = 0; j < 2; ++j)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j % 2).Equals(0) ? fAngle : fTempAngle) - 90.0f), new Vector3(0.5f, 0.5f),
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_11, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(5.0f, -0.0125f, 2.5f + (0.1f * iPhase), 0.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_11];
                    }
                    fAngle += (iUniqueNumber % 2).Equals(0) ? 22.5f : -22.5f;

                    if (i < 24) yield return Timing.WaitForSeconds(0.25f);
                }
                break;
            case EGameDifficultyType.Type_Normal:
                for (int i = 0; i < 45; ++i)
                {
                    fTempAngle = fAngle + 180.0f;
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);

                    for (int j = 0; j < 2; ++j)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j % 2).Equals(0) ? fAngle : fTempAngle) - 90.0f), new Vector3(0.5f, 0.5f),
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_11, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(5.0f, -0.0125f, 2.5f + (0.1f * iPhase), 0.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_11];
                    }
                    fAngle += (iUniqueNumber % 2).Equals(0) ? 9.0f : -9.0f;

                    if (i < 44) yield return Timing.WaitForSeconds(0.1f);
                }
                break;
            case EGameDifficultyType.Type_Hard:
                for (int i = 0; i < 60; ++i)
                {
                    fTempAngle = fAngle + 180.0f;
                    if ((i % 2).Equals(0)) SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);

                    for (int j = 0; j < 2; ++j)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j % 2).Equals(0) ? fAngle : fTempAngle) - 90.0f), new Vector3(0.5f, 0.5f),
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_11, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(6.0f, -0.015f, 3.0f, 0.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_11];
                    }
                    if ((i % 8).Equals(0))
                    {
                        fSubAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + UnityEngine.Random.Range(-30.0f, 30.0f);

                        GameObject pSubBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fSubAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_MediumArrow_01, 1.0f, 15.0f);
                        Bullet_Main pSubBulletMain = pSubBulletObject.GetComponent<Bullet_Main>();
                        Bullet_Base pSubBulletBase = pSubBulletMain.GetBulletBase();

                        pSubBulletBase.SetUniqueNumber(0);
                        pSubBulletBase.SetBulletMoveSpeed(5.0f, -0.01f, 3.0f, 0.0f);
                        pSubBulletBase.SetBulletRotateSpeed();
                        pSubBulletBase.SetBulletOption();
                        pSubBulletBase.SetCondition(false);
                        pSubBulletBase.SetCollisionDestroy(true);
                        pSubBulletBase.SetColliderTrigger(true);
                        pSubBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_MediumArrow_01];
                    }
                    fAngle += (iUniqueNumber % 2).Equals(0) ? 7.2f : -7.2f;

                    if (i < 59) yield return Timing.WaitForSeconds(0.08f);
                }
                break;
            default:
                break;
        }
        yield return Timing.WaitForSeconds((float)iRepeatCount);

        Timing.RunCoroutine(pGameObject.GetComponent<Enemy_Main>().pDelegateMove(0.5f));

        yield break;
    }
    public IEnumerator<float> Phase1_10CounterPattern1(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);

        switch (enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack3);
                pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(0.5f, 0.5f), "Enemy_Bullet",
                    EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_16, 1.0f, 20.0f);
                pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.SetUniqueNumber(0);
                pBulletBase.SetBulletMoveSpeed(4.0f);
                pBulletBase.SetBulletRotateSpeed();
                pBulletBase.SetBulletOption();
                pBulletBase.SetCondition(false);
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(true);
                pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_16];
                break;
            case EGameDifficultyType.Type_Normal:
                for (int i = 0; i < 5; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack3);
                    pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (-22.5f + (11.25f * i))) - 90.0f), new Vector3(0.5f, 0.5f), "Enemy_Bullet",
                        EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_16, 1.0f, 20.0f);
                    pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                    pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletBase.SetBulletMoveSpeed(5.0f);
                    pBulletBase.SetBulletRotateSpeed();
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_16];
                }
                break;
            case EGameDifficultyType.Type_Hard:
                for (int i = 0; i < 9; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack3);
                    pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (-45.0f + (11.25f * i))) - 90.0f), new Vector3(0.5f, 0.5f), "Enemy_Bullet",
                        EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_16, 1.0f, 20.0f);
                    pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                    pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletBase.SetBulletMoveSpeed(6.0f);
                    pBulletBase.SetBulletRotateSpeed();
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_16];
                }
                break;
            default:
                break;
        }
        yield break;
    }
    #endregion
    #region PHASE 11-20
    public IEnumerator<float> Phase11_20Pattern1(GameObject pGameObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;
        float fBulletSpeed;

        yield return Timing.WaitForSeconds(1.5f);

        while (true)
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 5 + (((iPhase - 1) % 10) / 4); ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.0f, 6.0f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    yield return Timing.WaitForSeconds(0.1f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 6 + (((iPhase - 1) % 10) / 4); ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.5f, 6.5f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    for (int i = 0; i < 2 + (((iPhase - 1) % 10) / 4); ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);
                        fBulletSpeed = UnityEngine.Random.Range(1.5f, 3.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(0.5f, 0.5f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_11, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(fBulletSpeed, 0.025f, 0.0f, fBulletSpeed + 4.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_11];
                    }
                    yield return Timing.WaitForSeconds(0.1f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 8 + (((iPhase - 1) % 10) / 4); ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_01, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.5f, 7.5f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_01];
                    }
                    for (int i = 0; i < 2 + (((iPhase - 1) % 10) / 4); ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);
                        fBulletSpeed = UnityEngine.Random.Range(1.5f, 3.0f);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(0.5f, 0.5f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_11, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(fBulletSpeed, 0.025f, 0.0f, fBulletSpeed + 5.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_11];
                    }
                    for (int i = 0; i < 2; ++i)
                    {
                        fAngle = Utility.GetRandomAngle(pGameObject);

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_05, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.5f, 4.5f));
                        pBulletBase.SetBulletRotateSpeed(i.Equals(0) ? 60.0f : -60.0f, 0.0f, 0.0f, 0.0f, i.Equals(0) ? 60.0f : -60.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_05];
                    }
                    yield return Timing.WaitForSeconds(0.1f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase11_20Pattern2(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(0.5f);

        switch (enGameDifficultyType)
        {
            case EGameDifficultyType.Type_Easy:
                for (int i = 0; i < 4; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int j = 0; j < 3; ++j)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (j.Equals(0) ? 0.0f : UnityEngine.Random.Range(-60.0f, 60.0f));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(1.25f, 1.25f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_SmallArrow_04, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.0f, 4.5f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_SmallArrow_04];
                    }
                    yield return Timing.WaitForSeconds(0.5f);
                }
                break;
            case EGameDifficultyType.Type_Normal:
                for (int i = 0; i < 4; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int j = 0; j < 4; ++j)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (j.Equals(0) ? 0.0f : UnityEngine.Random.Range(-45.0f, 45.0f));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(1.25f, 1.25f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_SmallArrow_05, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.5f, 5.5f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_SmallArrow_05];
                    }
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);

                    pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(1.25f, 1.25f), "Enemy_Bullet",
                        EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_LargeArrow_05, 1.0f, 20.0f);
                    pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                    pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletBase.SetBulletMoveSpeed(6.0f);
                    pBulletBase.SetBulletRotateSpeed();
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_LargeArrow_05];

                    yield return Timing.WaitForSeconds(0.5f);
                }
                break;
            case EGameDifficultyType.Type_Hard:
                for (int i = 0; i < 4; ++i)
                {
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int j = 0; j < 5; ++j)
                    {
                        fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject) + (j.Equals(0) ? 0.0f : UnityEngine.Random.Range(-30.0f, 30.0f));

                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(1.25f, 1.25f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_SmallArrow_08, 1.0f, 10.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(UnityEngine.Random.Range(3.5f, 5.5f));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_SmallArrow_08];
                    }
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    for (int j = 0; j < 3; ++j)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle + (-22.5f + (22.5f * j)) - 90.0f), new Vector3(1.25f, 1.25f), "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_LargeArrow_08, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(5.0f);
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_LargeArrow_08];
                    }

                    yield return Timing.WaitForSeconds(0.5f);
                }
                break;
            default:
                break;
        }
        yield break;
    }
    public IEnumerator<float> Phase11_20Pattern3(GameObject pGameObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;
        bool bDirectionChange = false;

        yield return Timing.WaitForSeconds(0.5f);

        while (true)
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    for (int i = 0; i < 8; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 4; ++j)
                        {
                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j < 2).Equals(true) ? fAngle - 90.0f : fAngle + 90.0f) + (bDirectionChange.Equals(false) ? (22.5f * i) : (-22.5f * i))),
                                new Vector3(1.25f, 1.25f), "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None,
                                (j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed((j % 2).Equals(0) ? 4.0f : 3.0f);
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption(true);
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)((j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04)];
                        }
                        yield return Timing.WaitForSeconds(0.15f);
                    }
                    bDirectionChange = bDirectionChange.Equals(false) ? true : false;
                    yield return Timing.WaitForSeconds(2.5f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    for (int i = 0; i < 16; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 4; ++j)
                        {
                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j < 2).Equals(true) ? fAngle - 90.0f : fAngle + 90.0f) + (bDirectionChange.Equals(false) ? (11.25f * i) : (-11.25f * i))),
                                new Vector3(1.25f, 1.25f), "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None,
                                (j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed((j % 2).Equals(0) ? 4.5f : 3.25f);
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption(true);
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)((j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04)];
                        }
                        yield return Timing.WaitForSeconds(0.075f);
                    }
                    bDirectionChange = bDirectionChange.Equals(false) ? true : false;
                    yield return Timing.WaitForSeconds(2.0f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    for (int i = 0; i < 24; ++i)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                        for (int j = 0; j < 4; ++j)
                        {
                            pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pGameObject.transform.position, new Vector3(0.0f, 0.0f, ((j < 2).Equals(true) ? fAngle - 90.0f : fAngle + 90.0f) + (bDirectionChange.Equals(false) ? (7.5f * i) : (-7.5f * i))),
                                new Vector3(1.25f, 1.25f), "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None,
                                (j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04, 1.0f, 10.0f);
                            pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                            pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetUniqueNumber(0);
                            pBulletBase.SetBulletMoveSpeed((j % 2).Equals(0) ? 5.0f : 3.5f);
                            pBulletBase.SetBulletRotateSpeed();
                            pBulletBase.SetBulletOption(true);
                            pBulletBase.SetCondition(false);
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)((j % 2).Equals(1) ? EEnemyBulletType.Type_MediumArrow_02 : EEnemyBulletType.Type_MediumArrow_04)];
                        }
                        yield return Timing.WaitForSeconds(0.05f);
                    }
                    bDirectionChange = bDirectionChange.Equals(false) ? true : false;
                    yield return Timing.WaitForSeconds(2.0f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase11_20Pattern4(GameObject pGameObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(0.25f);

        while (true)
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 12; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (30.0f * i)) - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(2.5f + (0.1f * ((iPhase - 1) % 10)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];
                    }
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    for (int i = 0; i < 3; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, fAngle - 90.0f), new Vector3(2.0f, 2.0f), "Enemy_Bullet",
                            EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_07, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (1.0f * i));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_07];
                    }
                    yield return Timing.WaitForSeconds(1.8f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 18; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (20.0f * i)) - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (0.1f * ((iPhase - 1) % 10)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];
                    }
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    for (int i = 0; i < 9; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (-15.0f + (15.0f * (i % 3))) - 90.0f)), new Vector3(2.0f, 2.0f),
                            "Enemy_Bullet", EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_07, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(2.5f + (1.5f * (i / 3)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_07];
                    }
                    yield return Timing.WaitForSeconds(1.6f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 48; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24))) - 90.0f), Vector3.one, "Enemy_Bullet",
                            EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_02, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed((3.0f + (0.1f * ((iPhase - 1) % 10))) + (1.5f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_02];
                    }
                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    for (int i = 0; i < 15; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (-30.0f + (15.0f * (i % 5))) - 90.0f)), new Vector3(2.0f, 2.0f),
                            "Enemy_Bullet", EBulletType.Type_Circle, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Circle_07, 1.0f, 20.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(4.0f + (0.5f * (i / 5)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Circle_07];
                    }
                    yield return Timing.WaitForSeconds(1.4f);
                    break;
                default:
                    break;
            }
        }
    }
    public IEnumerator<float> Phase11_20Pattern5(GameObject pGameObject, GameObject pPlayerObject)
    {
        GameObject pBulletObject;
        Bullet_Main pBulletMain;
        Bullet_Base pBulletBase;
        float fAngle;

        yield return Timing.WaitForSeconds(1.5f);

        while (true)
        {
            switch (enGameDifficultyType)
            {
                case EGameDifficultyType.Type_Easy:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 48; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) + (7.5f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_08, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(2.5f + (1.0f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(45.0f, 0.0f, 0.0f, 0.0f, 90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_08];
                    }
                    yield return Timing.WaitForSeconds(1.25f);

                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 48; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) - (7.5f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_09, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(2.5f + (1.0f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(-45.0f, 0.0f, 0.0f, 0.0f, -90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_09];
                    }
                    yield return Timing.WaitForSeconds(1.25f);

                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 108; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (10.0f * (i % 36)))), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (1.0f * (i / 36)) + (0.1f * ((iPhase - 1) % 10)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];
                    }
                    yield return Timing.WaitForSeconds(2.0f);
                    break;
                case EGameDifficultyType.Type_Normal:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 72; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) + (3.75f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_08, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (0.75f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(45.0f, 0.0f, 0.0f, 0.0f, 90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_08];
                    }
                    yield return Timing.WaitForSeconds(1.0f);

                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 72; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) - (3.75f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_09, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (0.75f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(-45.0f, 0.0f, 0.0f, 0.0f, -90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_09];
                    }
                    yield return Timing.WaitForSeconds(1.0f);

                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 144; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (10.0f * (i % 36)))), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.0f + (0.75f * (i / 36)) + (0.1f * ((iPhase - 1) % 10)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];
                    }
                    yield return Timing.WaitForSeconds(2.0f);
                    break;
                case EGameDifficultyType.Type_Hard:
                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 120; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) + (3.0f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_08, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.25f + (0.5f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(45.0f, 0.0f, 0.0f, 0.0f, 90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_08];
                    }
                    yield return Timing.WaitForSeconds(1.0f);

                    fAngle = UnityEngine.Random.Range(-180.0f, 180.0f);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack1);
                    for (int i = 0; i < 120; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (15.0f * (i % 24) - (3.0f * (i / 24))) - 90.0f)), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_09, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.25f + (0.5f * (i / 24)));
                        pBulletBase.SetBulletRotateSpeed(-45.0f, 0.0f, 0.0f, 0.0f, -90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_09];
                    }
                    yield return Timing.WaitForSeconds(1.0f);

                    fAngle = Utility.GetAimedAngle(pGameObject, pPlayerObject);
                    SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyAttack2);
                    for (int i = 0; i < 180; ++i)
                    {
                        pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pGameObject.transform.position, new Vector3(0.0f, 0.0f, (fAngle + (10.0f * (i % 36)))), Vector3.one,
                            "Enemy_Bullet", EBulletType.Type_Capsule, EBulletShooterType.Type_Enemy, EPlayerBulletType.None, EEnemyBulletType.Type_Glow_10, 1.0f, 15.0f);
                        pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
                        pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletBase.SetBulletMoveSpeed(3.5f + (0.75f * (i / 36)) + (0.1f * ((iPhase - 1) % 10)));
                        pBulletBase.SetBulletRotateSpeed();
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletSpriteArray[(int)EEnemyBulletType.Type_Glow_10];
                    }
                    yield return Timing.WaitForSeconds(1.75f);
                    break;
                default:
                    break;
            }
            Timing.RunCoroutine(pGameObject.GetComponent<Enemy_Main>().pDelegateMove(0.0f));

            yield return Timing.WaitForSeconds(1.0f);
        }
    }
    #endregion
    #endregion
}