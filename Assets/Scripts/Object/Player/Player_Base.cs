#region USING
using UnityEngine;
#endregion

public class Player_Base : Object_Base, IObjectBase
{
    #region VARIABLE
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private Camera pCamera;
    private AudioSource pAudioSource;
    private Rigidbody2D pRigidbody2D;
    private EPlayerType enPlayerType;
    private EPlayerWeaponType enPlayerWeaponType;
    private float fPlayerPower;
    private float fPlayerPrimaryDamage;
    private float fPlayerSecondaryDamage;
    private float fPlayerMoveSpeedFast;
    private float fPlayerMoveSpeedSlow;
    private bool bSlowMode;
    private bool bDeath;
    private bool bInvincible;
    #endregion

    #region CONSTRUCTOR
    public Player_Base(GameObject pPlayerObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szPlayerTag, EPlayerType enPlayerType, EPlayerWeaponType enPlayerWeaponType)
    {
        base.Init(pPlayerObject, pTransform, vPosition, vRotation, vScale, szPlayerTag, EObjectType.Type_Player);

        this.enPlayerType = enPlayerType;
        this.enPlayerWeaponType = enPlayerWeaponType;

        bSlowMode = false;
        bDeath = false;
        bInvincible = false;
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Camera GetCamera() { return pCamera; }
    public AudioSource GetAudioSource() { return pAudioSource; }
    public Rigidbody2D GetRigidbody2D() { return pRigidbody2D; }
    public EPlayerType GetPlayerType() { return enPlayerType; }
    public EPlayerWeaponType GetPlayerWeaponType() { return enPlayerWeaponType; }
    public float GetPlayerPower() { return fPlayerPower; }
    public float GetPlayerPrimaryDamage() { return fPlayerPrimaryDamage; }
    public float GetPlayerSecondaryDamage() { return fPlayerSecondaryDamage; }
    public float GetPlayerMoveSpeed() { return !bSlowMode ? fPlayerMoveSpeedFast : fPlayerMoveSpeedSlow; }
    public bool GetSlowMode() { return bSlowMode; }
    public bool GetDeath() { return bDeath; }
    public bool GetInvincible() { return bInvincible; }
    #endregion

    #region SET METHOD
    public void SetCamera(Camera pCamera) { this.pCamera = pCamera; }
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetAudioSource(AudioSource pAudioSource) { this.pAudioSource = pAudioSource; }
    public void SetRigidbody2D(Rigidbody2D pRigidbody2D) { this.pRigidbody2D = pRigidbody2D; }
    public void SetPlayerType(EPlayerType enPlayerType) { this.enPlayerType = enPlayerType; }
    public void SetPlayerWeaponType(EPlayerWeaponType enPlayerWeaponType) { this.enPlayerWeaponType = enPlayerWeaponType; }
    public void SetPlayerPower(float fPlayerPower) { this.fPlayerPower = fPlayerPower; }
    public void SetPlayerPrimaryDamage(float fPlayerPrimaryDamage) { this.fPlayerPrimaryDamage = fPlayerPrimaryDamage; }
    public void SetPlayerSecondaryDamage(float fPlayerSecondaryDamage) { this.fPlayerSecondaryDamage = fPlayerSecondaryDamage; }
    public void SetPlayerMoveSpeed(float fPlayerMoveSpeedFast, float fPlayerMoveSpeedSlow)
    {
        this.fPlayerMoveSpeedFast = fPlayerMoveSpeedFast;
        this.fPlayerMoveSpeedSlow = fPlayerMoveSpeedSlow;
    }
    public void SetSlowMode(bool bSlowMode) { this.bSlowMode = bSlowMode; }
    public void SetDeath(bool bDeath) { this.bDeath = bDeath; }
    public void SetInvincible(bool bInvincible) { this.bInvincible = bInvincible; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetPlayerType(EPlayerType.None);
        SetPlayerWeaponType(EPlayerWeaponType.None);
        SetPlayerPower(0.0f);
        SetPlayerPrimaryDamage(0.0f);
        SetPlayerSecondaryDamage(0.0f);
        SetPlayerMoveSpeed(0.0f, 0.0f);
        SetSlowMode(false);
        SetDeath(false);
        SetInvincible(false);

        base.AllReset();
    }
    #endregion
}