#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class Bullet_Main : MonoBehaviour
{
    #region VARIABLE
    private Bullet_Base pBulletBase;
    private Timer pTimer;
    private float fDistance;
    #endregion

    #region GET METHOD
    public Bullet_Base GetBulletBase() { return (pBulletBase != null) ? pBulletBase : null; }
    public Timer GetTimer() { return (pTimer != null) ? pTimer : null; }
    public float GetDistance() { return fDistance; }
    #endregion

    #region UNITY LIFE CYCLE
    void FixedUpdate()
    {
        if (pBulletBase == null)
        {
            return;
        }
    
        OutScreenCheck(pBulletBase.GetTransform(), pBulletBase.GetDestroyPadding());

        if (pBulletBase.GetBulletMoveSpeed() != 0.0f)
            BulletMove();
        if (pBulletBase.GetBulletMoveAccelerationSpeed() != 0.0f)
            BulletMoveAcceleration();

        if (pBulletBase.GetBulletRotateSpeed() != 0.0f)
            BulletRotate();
        if (pBulletBase.GetBulletRotateAccelerationSpeed() != 0.0f)
            BulletRotateAcceleration();

        RunTimer(pTimer);
    }
    void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pBulletBase.GetBulletShooterType().Equals(EBulletShooterType.Type_Player))
        {
            if (pCollider.CompareTag("ENEMY"))
            {
                Enemy_Main pEnemyMain = pCollider.gameObject.GetComponent<Enemy_Main>();
                pEnemyMain.SetDamaged();
                pEnemyMain.GetEnemyBase().SetEnemyCurrentHP(pEnemyMain.GetEnemyBase().GetEnemyCurrentHP() - pBulletBase.GetBulletDamage());
                
                if (pEnemyMain.GetEnemyBase().GetEnemyCurrentHP() <= 0.0f)
                {
                    pEnemyMain.DestroyEnemy();
                }
                else
                {
                    if (pEnemyMain.GetEnemyBase().GetEnemyCurrentHP() >= pEnemyMain.GetEnemyBase().GetEnemyMaxHP() * 0.2f)
                    {
                        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyDamage1);
                    }
                    else SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_EnemyDamage2);
                    GameManager.Instance.iTempScore += 10;
                }
                
                if (pBulletBase.GetCollisionDestroy().Equals(true))
                {
                    DestroyBullet();
                }
            }
            else return;
        }
        else if (pBulletBase.GetBulletShooterType().Equals(EBulletShooterType.Type_Enemy))
        {
            if (pCollider.name.Equals("HitPoint"))
            {
                Player_Main pPlayerMain = pCollider.transform.parent.GetComponent<Player_Main>();
                Player_Base pPlayerBase = pPlayerMain.GetPlayerBase();

                if (pBulletBase.GetCollisionDestroy().Equals(true) && pPlayerBase.GetDeath().Equals(false))
                {
                    Timing.RunCoroutine(EffectManager.Instance.ExtractEffect(pBulletBase.GetPosition(), Vector3.zero, new Vector3(2.0f, 2.0f, 1.0f), EEffectType.Type_Enemy_Destroy_03, 1.0f), "Bullet_DestroyEffect");
                    DestroyBullet();
                }
                if (pPlayerBase.GetDeath().Equals(false) && pPlayerBase.GetInvincible().Equals(false))
                {
                    Timing.RunCoroutine(pPlayerMain.PlayerDeath());
                }
            }
            else return;
        }
    }
    #endregion

    #region COMMON METHOD
    public void Init(GameObject pBulletObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag, EBulletType enBulletType,
        EBulletShooterType enBulletShooterType, EPlayerBulletType enPlayerBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fDestroyPadding)
    {
        // 초기 생성
        if (pBulletBase == null)
        {
            pBulletBase = new Bullet_Base(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag);
            pTimer = new Timer(szBulletTag);
            pBulletBase.SetSpriteRenderer(pBulletBase.GetTransform().GetChild(0).GetComponent<SpriteRenderer>());
            pBulletBase.SetAnimator(pBulletBase.GetTransform().GetChild(0).GetComponent<Animator>());
            pBulletBase.SetRigidbody(pBulletBase.GetGameObject().GetComponent<Rigidbody2D>());
            pBulletBase.SetBoxCollider(pBulletBase.GetGameObject().GetComponent<BoxCollider2D>());
            pBulletBase.SetCapsuleCollider(pBulletBase.GetGameObject().GetComponent<CapsuleCollider2D>());
            pBulletBase.SetCircleCollider(pBulletBase.GetGameObject().GetComponent<CircleCollider2D>());
        }
        // 오브젝트 풀에서 재활용 시 초기화
        else
        {
            pBulletBase.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EObjectType.Type_Bullet);
            pTimer.InitTimer(szBulletTag);
        }

        pBulletBase.SetBulletType(enBulletType);
        pBulletBase.SetBulletShooterType(enBulletShooterType);
        pBulletBase.SetPlayerBulletType(enPlayerBulletType);
        pBulletBase.SetEnemyBulletType(enEnemyBulletType);
        pBulletBase.GetSpriteRenderer().color = new Color(1, 1, 1, fAlpha);
        pBulletBase.GetSpriteRenderer().sortingOrder = enBulletShooterType.Equals(EBulletShooterType.Type_Player) ? 4 : 5;
        pBulletBase.SetBulletRotateValue(0);
        pBulletBase.SetDestroyPadding(fDestroyPadding);
        fDistance = 0.0f;
    }
    public void OutScreenCheck(Transform pTransform, float fDestroyPadding)
    {
        Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);

        if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y < 0 || vTempPosition.y > Screen.height)
        {
            if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y > Screen.height)
            {
                if (pBulletBase.GetBulletReflect().Equals(true))
                {
                    pBulletBase.SetBulletReflectCount(pBulletBase.GetBulletReflectCount() + 1);
                    if (pBulletBase.GetBulletReflectCount() >= pBulletBase.GetBulletReflectMaxCount())
                    {
                        pBulletBase.SetBulletReflect(false);
                        pBulletBase.SetBulletBottomReflect(false);
                    }

                    if (vTempPosition.x < 0 || vTempPosition.x > Screen.width)
                    {
                        pBulletBase.SetRotationZ(-pBulletBase.GetRotationZ());
                    }
                    else if (vTempPosition.y > Screen.height)
                    {
                        if (pBulletBase.GetRotationZ() < 0)
                        {
                            pBulletBase.SetRotationZ(-180 - pBulletBase.GetRotationZ());
                        }
                        else if (pBulletBase.GetRotationZ() > 0)
                        {
                            pBulletBase.SetRotationZ(180 - pBulletBase.GetRotationZ());
                        }
                        else
                        {
                            pBulletBase.SetRotationZ(0);
                        }
                    }
                }
            }
            else if (vTempPosition.y < 0)
            {
                if (pBulletBase.GetBulletBottomReflect().Equals(true))
                {
                    pBulletBase.SetBulletReflectCount(pBulletBase.GetBulletReflectCount() + 1);
                    if (pBulletBase.GetBulletReflectCount() >= pBulletBase.GetBulletReflectMaxCount())
                    {
                        pBulletBase.SetBulletReflect(false);
                        pBulletBase.SetBulletBottomReflect(false);
                    }
        
                    if (pBulletBase.GetRotationZ() < 0)
                    {
                        pBulletBase.SetRotationZ(-180 - pBulletBase.GetRotationZ());
                    }
                    else if (pBulletBase.GetRotationZ() > 0)
                    {
                        pBulletBase.SetRotationZ(180 - pBulletBase.GetRotationZ());
                    }
                    else
                    {
                        pBulletBase.SetRotationZ(0);
                    }
                }
            }

            // 각종 탄막 변화 스크립트 아래에 추가
        }

        if (vTempPosition.x < 0 - fDestroyPadding || vTempPosition.x > Screen.width + fDestroyPadding || vTempPosition.y < 0 - fDestroyPadding || vTempPosition.y > Screen.height + fDestroyPadding)
        {
            DestroyBullet();
        }
    }
    public void DestroyBullet()
    {
        pTimer.InitTimer();
        BulletManager.Instance.GetBulletPool().ReturnPool(pBulletBase.GetGameObject());
    }
    public void BulletMove()
    {
        pBulletBase.GetTransform().Translate(Vector2.up * pBulletBase.GetBulletMoveSpeed() * Time.deltaTime);
    }
    public void BulletMoveAcceleration()
    {
        if (pBulletBase.GetBulletMoveAccelerationSpeed() > 0.0f)
        {
            if (pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed() < pBulletBase.GetBulletMoveSpeedLimitMax())
            {
                pBulletBase.SetBulletMoveSpeed((pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed() > pBulletBase.GetBulletMoveSpeedLimitMax())
                    ? pBulletBase.GetBulletMoveSpeedLimitMax() : pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed());
            }
        }
        else if (pBulletBase.GetBulletMoveAccelerationSpeed() < 0.0f)
        {
            if (pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed() > pBulletBase.GetBulletMoveSpeedLimitMin())
            {
                pBulletBase.SetBulletMoveSpeed((pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed() < pBulletBase.GetBulletMoveSpeedLimitMin())
                    ? pBulletBase.GetBulletMoveSpeedLimitMin() : pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed());
            }
        }
    }
    public void BulletRotate()
    {
        pBulletBase.SetBulletRotateValue(pBulletBase.GetBulletRotateValue() + (pBulletBase.GetBulletRotateSpeed() * Time.deltaTime));
        if ((pBulletBase.GetBulletRotateSpeed() > 0.0f && (pBulletBase.GetBulletRotateValue() < pBulletBase.GetBulletRotateValueLimit())) ||
            (pBulletBase.GetBulletRotateSpeed() < 0.0f && (pBulletBase.GetBulletRotateValue() > pBulletBase.GetBulletRotateValueLimit())))
        {
            pBulletBase.GetTransform().Rotate(0.0f, 0.0f, pBulletBase.GetBulletRotateSpeed() * Time.deltaTime);
        }
    }
    public void BulletRotateAcceleration()
    {
        if (pBulletBase.GetBulletRotateAccelerationSpeed() > 0.0f)
        {
            if (pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed() < pBulletBase.GetBulletRotateSpeedLimitMax())
            {
                pBulletBase.SetBulletRotateSpeed((pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed() > pBulletBase.GetBulletRotateSpeedLimitMax())
                    ? pBulletBase.GetBulletRotateSpeedLimitMax() : pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed());
            }
        }
        else if (pBulletBase.GetBulletRotateAccelerationSpeed() < 0.0f)
        {
            if (pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed() > pBulletBase.GetBulletRotateSpeedLimitMin())
            {
                pBulletBase.SetBulletRotateSpeed((pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed() < pBulletBase.GetBulletRotateSpeedLimitMin())
                    ? pBulletBase.GetBulletRotateSpeedLimitMin() : pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed());
            }
        }
    }
    public void RunTimer(Timer pTimer)
    {
        if (pTimer.GetSwitch().Equals(true))
        {
            pTimer.Run();
        }
    }
    #endregion
}