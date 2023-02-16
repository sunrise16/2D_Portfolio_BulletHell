#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
#endregion

public class PlayerSecondary
{
    #region VARIABLE
    private Player_Base pPlayerBase;
    private Vector3 vRefVector;
    private int iNumber;
    #endregion

    #region CONSTRUCTOR
    public PlayerSecondary(Player_Base pPlayerBase, int iNumber)
    {
        this.pPlayerBase = pPlayerBase;
        vRefVector = Vector3.one;
        this.iNumber = iNumber;
    }
    #endregion

    #region COMMON METHOD
    public void MoveSecondary(bool bKeyUp)
    {
        if (pPlayerBase.GetChildTransform(4).GetChild(iNumber).gameObject.activeSelf.Equals(false))
            return;

        if ((int)pPlayerBase.GetPlayerPower() == 1 || (int)pPlayerBase.GetPlayerPower() == 3)
        {
            if (((int)pPlayerBase.GetPlayerPower() == 1 && iNumber.Equals(0)) || ((int)pPlayerBase.GetPlayerPower() == 3 && iNumber.Equals(2)))
            {
                pPlayerBase.GetChildTransform(4).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(4).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(4).GetChild(bKeyUp.Equals(true) ? 15 : 14).position, ref vRefVector, 0.05f);
            }
            else
            {
                pPlayerBase.GetChildTransform(4).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(4).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(4).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
            }
        }
        else
        {
            pPlayerBase.GetChildTransform(4).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(4).GetChild(iNumber).position,
                pPlayerBase.GetChildTransform(4).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
        }
    }
    #endregion
}

public class Player_Main : MonoBehaviour
{
    #region VARIABLE
    private SpriteRenderer pPlayerSprite;
    private SpriteRenderer pHitPointSprite;
    private List<PlayerSecondary> pSecondaryList;
    private Player_Base pPlayerBase;
    private Timer pTimer;
    private Vector3 vTempPosition;
    private Vector2 vMargin;
    private float fPlayerAlpha;
    private float fHitPointAlpha;
    private float fTime = 0.0f;
    private bool bLock = false;
    private bool bUsingBomb = false;
    #endregion

    #region UNITY LIFE CYCLE
    void Update()
    {
        if (pPlayerBase.GetDeath().Equals(false) && bLock.Equals(false))
        {
            UseBomb();
        }
    }
    void FixedUpdate()
    {
        if (pPlayerBase.GetDeath().Equals(false) && bLock.Equals(false))
        {
            PlayerMove();
            ShotBullet();
            MoveInScreen();
        }
        ControlHitPoint();
        pPlayerSprite.color = new Color(1.0f, 1.0f, 1.0f, fPlayerAlpha);

        for (int i = 0; i < 4; i++)
        {
            pSecondaryList[i].MoveSecondary(pPlayerBase.GetSlowMode().Equals(true) ? true : false);
        }
    }
    #endregion

    #region GET METHOD
    public Player_Base GetPlayerBase() { return pPlayerBase; }
    #endregion

    #region COMMON METHOD
    public void Init(EPlayerType enPlayerType, EPlayerWeaponType enPlayerWeaponType)
    {
        GameObject pPlayerObject = this.gameObject;
        Transform pTransform = pPlayerObject.GetComponent<Transform>();
        Vector3 vPosition = new Vector3(0.0f, -3.0f);
        Vector3 vRotation = Vector3.zero;
        Vector3 vScale = Vector3.one;

        if (pPlayerBase == null)
        {
            pPlayerBase = new Player_Base(pPlayerObject, pTransform, vPosition, vRotation, vScale, "Player", enPlayerType, enPlayerWeaponType);
            pTimer = new Timer("Player_Timer", 0, 0, 0.0f, 0.1f, true);
            pPlayerBase.SetCamera(Camera.main);
            pPlayerBase.SetAnimator(pTransform.GetChild(0).GetComponent<Animator>());
            pPlayerBase.SetSpriteRenderer(pTransform.GetChild(1).GetComponent<SpriteRenderer>());
            pPlayerBase.SetAudioSource(GetComponent<AudioSource>());
            pPlayerBase.SetRigidbody2D(GetComponent<Rigidbody2D>());
            pPlayerSprite = pPlayerBase.GetChildTransform(0).GetComponent<SpriteRenderer>();
            pHitPointSprite = pPlayerBase.GetChildTransform(1).GetComponent<SpriteRenderer>();

            pSecondaryList = new List<PlayerSecondary>();
            for (int i = 0; i < 4; i++)
            {
                pSecondaryList.Add(new PlayerSecondary(pPlayerBase, i));
            }
        }
        else
        {
            pPlayerBase.Init(pPlayerObject, pTransform, vPosition, vRotation, vScale, "Player", EObjectType.Type_Player);
            pTimer.InitTimer("Player_Timer", 0, 0, 0.0f, 0.1f, true);
        }
        pPlayerBase.SetSlowMode(false);
        pPlayerBase.SetDeath(false);
        pPlayerBase.SetInvincible(false);
        vMargin = new Vector2(0.03f, 0.03f);
        fPlayerAlpha = 1.0f;
        fHitPointAlpha = 0.0f;
    }
    public void PlayerMove()
    {
        pPlayerBase.SetSlowMode(Input.GetKey(KeyCode.LeftShift) ? true : false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pPlayerBase.SetPositionX(pPlayerBase.GetPositionX() - pPlayerBase.GetPlayerMoveSpeed());
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pPlayerBase.SetPositionX(pPlayerBase.GetPositionX() + pPlayerBase.GetPlayerMoveSpeed());
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pPlayerBase.SetPositionY(pPlayerBase.GetPositionY() + pPlayerBase.GetPlayerMoveSpeed());
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pPlayerBase.SetPositionY(pPlayerBase.GetPositionY() - pPlayerBase.GetPlayerMoveSpeed());
        }
    }
    public void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            fTime += Time.deltaTime;
            if (fTime >= 0.04f)
            {
                fTime = 0.0f;
                ExtractBullet(pPlayerBase.GetPlayerType(), pPlayerBase.GetPlayerWeaponType());
                SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_PlayerAttack);
            }
        }
        if (Input.GetKeyUp(KeyCode.Z)) fTime = 0.0f;
    }
    public void ExtractBullet(EPlayerType enPlayerType, EPlayerWeaponType enPlayerWeaponType)
    {
        // PRIMARY SHOT
        for (int i = 0; i < 2; i++)
        {
            GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                (pPlayerBase.GetChildTransform(4).GetChild(i + 4).position, Vector3.zero, Vector3.one, "Bullet_PlayerPrimary", EBulletType.Type_Capsule,
                EBulletShooterType.Type_Player, EPlayerBulletType.Type_APrimary, EEnemyBulletType.None, 1.0f, 10.0f);
            Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
            Bullet_Base pBulletBase = pBulletMain.GetBulletBase();

            pBulletMain.GetTimer().InitTimer();
            pBulletBase.SetBulletDamage(pPlayerBase.GetPlayerPrimaryDamage());
            pBulletBase.SetUniqueNumber(0);
            pBulletBase.SetBulletMoveSpeed(18.0f);
            pBulletBase.SetBulletRotateSpeed(0.0f);
            pBulletBase.SetBulletOption();
            pBulletBase.SetCondition(false);
            pBulletBase.SetCollisionDestroy(true);
            pBulletBase.SetColliderTrigger(true);
            pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSpriteArray[(int)EPlayerSpriteType.Type_Player_BulletPrimary];
            pBulletBase.GetSpriteRenderer().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            GameObject pBulletSprite = pBulletBase.GetChildGameObject(0);
            pBulletSprite.transform.localScale = new Vector2(0.5f, 0.5f);
            CapsuleCollider2D pCapsuleCollider = pBulletObject.GetComponent<CapsuleCollider2D>();
            pCapsuleCollider.size = new Vector2(0.25f, 0.5f);
        }

        // SECONDARY SHOT
        for (int i = 0; i < 4; i++)
        {
            GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                (pPlayerBase.GetChildTransform(4).GetChild(i).position, Vector3.zero, Vector3.one, "Bullet_PlayerSecondary", EBulletType.Type_Capsule,
                EBulletShooterType.Type_Player, EPlayerBulletType.Type_ASecondary, EEnemyBulletType.None, 1.0f, 10.0f);
            Bullet_Main pBulletMain = pBulletObject.GetComponent<Bullet_Main>();
            Bullet_Base pBulletBase = pBulletMain.GetBulletBase();

            pBulletMain.GetTimer().InitTimer();
            pBulletBase.SetBulletDamage(pPlayerBase.GetPlayerSecondaryDamage());
            pBulletBase.SetUniqueNumber(0);
            pBulletBase.SetBulletMoveSpeed(18.0f);
            pBulletBase.SetBulletRotateSpeed(0.0f);
            pBulletBase.SetBulletOption();
            pBulletBase.SetCondition(false);
            pBulletBase.SetCollisionDestroy(true);
            pBulletBase.SetColliderTrigger(true);
            pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSpriteArray[(int)EPlayerSpriteType.Type_Player_BulletSecondary];
            pBulletBase.GetSpriteRenderer().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            CapsuleCollider2D pCapsuleCollider = pBulletObject.GetComponent<CapsuleCollider2D>();
            pCapsuleCollider.size = new Vector2(0.2f, 0.75f);
        }
    }
    public void ControlHitPoint()
    {
        if (pPlayerBase.GetSlowMode().Equals(true))
        {
            fHitPointAlpha += 0.1f;
            if (fHitPointAlpha > 1.0f)
            {
                fHitPointAlpha = 1.0f;
            }
        }
        else
        {
            fHitPointAlpha -= 0.1f;
            if (fHitPointAlpha < 0.0f)
            {
                fHitPointAlpha = 0.0f;
            }
        }

        if (pPlayerBase.GetDeath().Equals(true))
        {
            fHitPointAlpha = 0.0f;
        }
        pHitPointSprite.color = new Color(1.0f, 1.0f, 1.0f, fHitPointAlpha);
    }
    public void UseBomb()
    {
        if (Input.GetKeyDown(KeyCode.X) && GameManager.Instance.iCurrentBomb > 0 && bUsingBomb.Equals(false))
        {
            bUsingBomb = true;
            SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_PlayerBomb);
            UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerBomb_{0}", GameManager.Instance.iCurrentBomb)].SetActive(false);
            GameManager.Instance.iCurrentBomb--;
            GameManager.Instance.iBombCount++;
            Timing.RunCoroutine(EffectManager.Instance.ExtractEffect(Vector3.zero, Vector3.zero, new Vector3(3.0f, 3.0f, 1.0f), EEffectType.Type_Player_Bomb, 3.0f));
            Timing.KillCoroutines("PlayerInvincible");
            Timing.RunCoroutine(PlayerInvincible(), "PlayerInvincible");
        }
    }
    public void MoveInScreen()
    {
        vTempPosition = pPlayerBase.GetCamera().WorldToViewportPoint(pPlayerBase.GetPosition());
        vTempPosition.x = Mathf.Clamp(vTempPosition.x, 0.0f + vMargin.x, 1.0f - vMargin.x);
        vTempPosition.y = Mathf.Clamp(vTempPosition.y, 0.0f + vMargin.y, 1.0f - vMargin.y);

        pPlayerBase.SetPosition(pPlayerBase.GetCamera().ViewportToWorldPoint(vTempPosition));
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> PlayerStart()
    {
        bLock = true;
        pPlayerBase.SetPosition(new Vector3(0.0f, -6.0f, 0.0f));

        yield return Timing.WaitForSeconds(1.0f);

        iTween.MoveTo(pPlayerBase.GetGameObject(), iTween.Hash("position", new Vector3(0.0f, -3.5f, 0.0f), "easetype", iTween.EaseType.linear, "time", 1.0f));

        yield return Timing.WaitForSeconds(1.0f);

        bLock = false; 

        yield break;
    }
    public IEnumerator<float> PlayerDeath()
    {
        bLock = true;
        pPlayerBase.SetDeath(true);
        pPlayerBase.SetSlowMode(false);
        GameManager.Instance.iMissCount++;

        fPlayerAlpha = 0.0f;
        pPlayerBase.GetRigidbody2D().velocity = Vector2.zero;
        pPlayerBase.GetChildGameObject(4).SetActive(false);
        SoundManager.Instance.PlaySFX(ESFXType.Type_SFX_PlayerDestroy);
        Timing.RunCoroutine(EffectManager.Instance.ExtractEffect(pPlayerBase.GetPosition(), Vector3.zero, Vector3.one, EEffectType.Type_Player_Destroy, 1.0f), "Player_DestroyEffect");

        yield return Timing.WaitForSeconds(1.0f);

        if (GameManager.Instance.iCurrentLife - 1 < 0)
        {
            // GAME OVER
            Timing.KillCoroutines();
            Timing.RunCoroutine(GameManager.Instance.pGameScene.GameOver());
            yield break;
        }
        else
        {
            UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerLife_{0}", GameManager.Instance.iCurrentLife)].SetActive(false);
            GameManager.Instance.iCurrentLife--;
        }

        if (GameManager.Instance.iCurrentBomb < 2)
        {
            for (int i = 1; i <= 2; i++)
            {
                UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerBomb_{0}", i)].SetActive(true);
            }
        }
        else if (GameManager.Instance.iCurrentBomb > 2)
        {
            for (int i = 6; i > 2; i--)
            {
                UIManager.Instance.pUIDictionary[string.Format("Game_UIPlayerBomb_{0}", i)].SetActive(false);
            }
        }
        GameManager.Instance.iCurrentBomb = 2;

        fPlayerAlpha = 0.8f;
        pPlayerBase.SetPosition(new Vector3(0.0f, -6.0f, 0.0f));
        pPlayerBase.GetChildGameObject(4).SetActive(true);
        iTween.MoveTo(pPlayerBase.GetGameObject(), iTween.Hash("position", new Vector3(0.0f, -3.5f, 0.0f), "easetype", iTween.EaseType.linear, "time", 1.0f));

        yield return Timing.WaitForSeconds(1.0f);

        bLock = false;
        pPlayerBase.SetDeath(false);
        Timing.KillCoroutines("PlayerInvincible");
        Timing.RunCoroutine(PlayerInvincible(), "PlayerInvincible");

        yield break;
    }
    public IEnumerator<float> PlayerInvincible()
    {
        int iCount = 0;
        Enemy_Main pEnemyMain = null;

        pPlayerBase.SetInvincible(true);

        while (iCount <= 96)
        {
            fPlayerAlpha = (iCount % 2).Equals(0) ? 0.8f : 0.3f;
            iCount++;

            if (iCount <= 32 && bUsingBomb.Equals(true))
            {
                for (int i = 0; i < EnemyManager.Instance.pEnemyParent.childCount; ++i)
                {
                    pEnemyMain = EnemyManager.Instance.pEnemyParent.GetChild(i).GetComponent<Enemy_Main>();
                    pEnemyMain.GetEnemyBase().SetEnemyCurrentHP(pEnemyMain.GetEnemyBase().GetEnemyCurrentHP() - 10.0f);
                    pEnemyMain.SetDamaged();
                    if (pEnemyMain.GetEnemyBase().GetEnemyCurrentHP() <= 0.0f)
                    {
                        pEnemyMain.DestroyEnemy();
                    }
                }
                GameManager.Instance.ClearBullet(true);
            }
            yield return Timing.WaitForSeconds(0.03f);
        }
        fPlayerAlpha = 1.0f;
        pPlayerBase.SetInvincible(false);
        bUsingBomb = false;

        yield break;
    }
    #endregion
}