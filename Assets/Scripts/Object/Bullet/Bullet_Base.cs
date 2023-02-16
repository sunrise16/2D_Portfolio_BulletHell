#region USING
using UnityEngine;
#endregion

public class Bullet_Base : Object_Base, IObjectBase
{
    #region VARIABLE
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private Rigidbody2D pRigidbody;
    private BoxCollider2D pBoxCollider;
    private CapsuleCollider2D pCapsuleCollider;
    private CircleCollider2D pCircleCollider;

    private EBulletType enBulletType;
    private EBulletShooterType enBulletShooterType;
    private EPlayerBulletType enPlayerBulletType;
    private EEnemyBulletType enEnemyBulletType;

    private int iBulletSpriteIndex;
    private int iBulletReflectCount;
    private int iBulletReflectMaxCount;

    private float fBulletDamage;
    private float fBulletMoveSpeed;
    private float fBulletMoveAccelerationSpeed;
    private float fBulletMoveSpeedLimitMin;
    private float fBulletMoveSpeedLimitMax;
    private float fBulletRotateSpeed;
    private float fBulletRotateValue;
    private float fBulletRotateAccelerationSpeed;
    private float fBulletRotateSpeedLimitMin;
    private float fBulletRotateSpeedLimitMax;
    private float fBulletRotateValueLimit;
    private float fDestroyPadding;

    private bool bBulletReflect;
    private bool bBulletBottomReflect;
    private bool bBulletChange;
    private bool bBulletSplit;
    private bool bBulletAttach;
    private bool bCondition;
    private bool bCollisionDestroy;
    private bool bColliderTrigger;
    #endregion

    #region CONSTRUCTOR
    public Bullet_Base(GameObject pBulletObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szBulletTag)
    {
        base.Init(pBulletObject, pTransform, vPosition, vRotation, vScale, szBulletTag, EObjectType.Type_Bullet);
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Rigidbody2D GetRigidbody() { return pRigidbody; }
    public BoxCollider2D GetBoxCollider() { return pBoxCollider; }
    public CapsuleCollider2D GetCapsuleCollider() { return pCapsuleCollider; }
    public CircleCollider2D GetCircleCollider() { return pCircleCollider; }
    public EBulletType GetBulletType() { return enBulletType; }
    public EBulletShooterType GetBulletShooterType() { return enBulletShooterType; }
    public EPlayerBulletType GetPlayerBulletType() { return enPlayerBulletType; }
    public EEnemyBulletType GetEnemyBulletType() { return enEnemyBulletType; }
    public int GetBulletSpriteIndex() { return iBulletSpriteIndex; }
    public int GetBulletReflectCount() { return iBulletReflectCount; }
    public int GetBulletReflectMaxCount() { return iBulletReflectMaxCount; }
    public float GetBulletDamage() { return fBulletDamage; }
    public float GetBulletMoveSpeed() { return fBulletMoveSpeed; }
    public float GetBulletMoveAccelerationSpeed() { return fBulletMoveAccelerationSpeed; }
    public float GetBulletMoveSpeedLimitMin() { return fBulletMoveSpeedLimitMin; }
    public float GetBulletMoveSpeedLimitMax() { return fBulletMoveSpeedLimitMax; }
    public float GetBulletRotateSpeed() { return fBulletRotateSpeed; }
    public float GetBulletRotateValue() { return fBulletRotateValue; }
    public float GetBulletRotateAccelerationSpeed() { return fBulletRotateAccelerationSpeed; }
    public float GetBulletRotateSpeedLimitMin() { return fBulletRotateSpeedLimitMin; }
    public float GetBulletRotateSpeedLimitMax() { return fBulletRotateSpeedLimitMax; }
    public float GetBulletRotateValueLimit() { return fBulletRotateValueLimit; }
    public float GetDestroyPadding() { return fDestroyPadding; }
    public bool GetBulletReflect() { return bBulletReflect; }
    public bool GetBulletBottomReflect() { return bBulletBottomReflect; }
    public bool GetBulletChange() { return bBulletChange; }
    public bool GetBulletSplit() { return bBulletSplit; }
    public bool GetBulletAttach() { return bBulletAttach; }
    public bool GetCondition() { return bCondition; }
    public bool GetCollisionDestroy() { return bCollisionDestroy; }
    public bool GetColliderTrigger() { return bColliderTrigger; }
    #endregion

    #region SET METHOD
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetRigidbody(Rigidbody2D pRigidbody) { this.pRigidbody = pRigidbody; }
    public void SetBoxCollider(BoxCollider2D pBoxCollider) { this.pBoxCollider = pBoxCollider; }
    public void SetCapsuleCollider(CapsuleCollider2D pCapsuleCollider) { this.pCapsuleCollider = pCapsuleCollider; }
    public void SetCircleCollider(CircleCollider2D pCircleCollider) { this.pCircleCollider = pCircleCollider; }
    public void SetBulletType(EBulletType enBulletType) { this.enBulletType = enBulletType; }
    public void SetBulletShooterType(EBulletShooterType enBulletShooterType) { this.enBulletShooterType = enBulletShooterType; }
    public void SetPlayerBulletType(EPlayerBulletType enPlayerBulletType) { this.enPlayerBulletType = enPlayerBulletType; }
    public void SetEnemyBulletType(EEnemyBulletType enEnemyBulletType) { this.enEnemyBulletType = enEnemyBulletType; }
    public void SetBulletSpriteIndex(int iBulletSpriteIndex) { this.iBulletSpriteIndex = iBulletSpriteIndex; }
    public void SetBulletReflectCount(int iBulletReflectCount) { this.iBulletReflectCount = iBulletReflectCount; }
    public void SetBulletReflectMaxCount(int iBulletReflectMaxCount) { this.iBulletReflectMaxCount = iBulletReflectMaxCount; }
    public void SetBulletDamage(float fBulletDamage) { this.fBulletDamage = fBulletDamage; }
    public void SetBulletMoveSpeed(float fBulletMoveSpeed) { this.fBulletMoveSpeed = fBulletMoveSpeed; }
    public void SetBulletMoveAccelerationSpeed(float fBulletMoveAccelerationSpeed) { this.fBulletMoveAccelerationSpeed = fBulletMoveAccelerationSpeed; }
    public void SetBulletMoveSpeedLimitMin(float fBulletMoveSpeedLimitMin) { this.fBulletMoveSpeedLimitMin = fBulletMoveSpeedLimitMin; }
    public void SetBulletMoveSpeedLimitMax(float fBulletMoveSpeedLimitMax) { this.fBulletMoveSpeedLimitMax = fBulletMoveSpeedLimitMax; }
    public void SetBulletMoveSpeed(float fBulletMoveSpeed = 0.0f, float fBulletMoveAccelerationSpeed = 0.0f, float fBulletMoveSpeedLimitMin = 0.0f, float fBulletMoveSpeedLimitMax = 0.0f)
    {
        SetBulletMoveSpeed(fBulletMoveSpeed);
        SetBulletMoveAccelerationSpeed(fBulletMoveAccelerationSpeed);
        SetBulletMoveSpeedLimitMin(fBulletMoveSpeedLimitMin);
        SetBulletMoveSpeedLimitMax(fBulletMoveSpeedLimitMax);
    }
    public void SetBulletRotateSpeed(float fBulletRotateSpeed) { this.fBulletRotateSpeed = fBulletRotateSpeed; }
    public void SetBulletRotateValue(float fBulletRotateValue) { this.fBulletRotateValue = fBulletRotateValue; }
    public void SetBulletRotateAccelerationSpeed(float fBulletRotateAccelerationSpeed) { this.fBulletRotateAccelerationSpeed = fBulletRotateAccelerationSpeed; }
    public void SetBulletRotateSpeedLimitMin(float fBulletRotateSpeedLimitMin) { this.fBulletRotateSpeedLimitMin = fBulletRotateSpeedLimitMin; }
    public void SetBulletRotateSpeedLimitMax(float fBulletRotateSpeedLimitMax) { this.fBulletRotateSpeedLimitMax = fBulletRotateSpeedLimitMax; }
    public void SetBulletRotateValueLimit(float fBulletRotateValueLimit) { this.fBulletRotateValueLimit = fBulletRotateValueLimit; }
    public void SetBulletRotateSpeed(float fBulletRotateSpeed = 0.0f, float fBulletRotateAccelerationSpeed = 0.0f, float fBulletRotateSpeedLimitMin = 0.0f, float fBulletRotateSpeedLimitMax = 0.0f, float fBulletRotateValueLimit = 0.0f)
    {
        SetBulletRotateSpeed(fBulletRotateSpeed);
        SetBulletRotateAccelerationSpeed(fBulletRotateAccelerationSpeed);
        SetBulletRotateSpeedLimitMin(fBulletRotateSpeedLimitMin);
        SetBulletRotateSpeedLimitMax(fBulletRotateSpeedLimitMax);
        SetBulletRotateValueLimit(fBulletRotateValueLimit);
    }
    public void SetDestroyPadding(float fDestroyPadding) { this.fDestroyPadding = fDestroyPadding; }
    public void SetBulletReflect(bool bBulletReflect) { this.bBulletReflect = bBulletReflect; }
    public void SetBulletBottomReflect(bool bBulletBottomReflect) { this.bBulletBottomReflect = bBulletBottomReflect; }
    public void SetBulletChange(bool bBulletChange) { this.bBulletChange = bBulletChange; }
    public void SetBulletSplit(bool bBulletSplit) { this.bBulletSplit = bBulletSplit; }
    public void SetBulletAttach(bool bBulletAttach) { this.bBulletAttach = bBulletAttach; }
    public void SetBulletOption(bool bBulletReflect = false, bool bBulletBottomReflect = false, bool bBulletChange = false, bool bBulletSplit = false, bool bBulletAttach = false)
    {
        SetBulletReflect(bBulletReflect);
        SetBulletBottomReflect(bBulletBottomReflect);
        SetBulletChange(bBulletChange);
        SetBulletSplit(bBulletSplit);
        SetBulletAttach(bBulletAttach);
    }
    public void SetCondition(bool bCondition) { this.bCondition = bCondition; }
    public void SetCollisionDestroy(bool bCollisionDestroy) { this.bCollisionDestroy = bCollisionDestroy; }
    public void SetColliderTrigger(bool bColliderTrigger) { this.bColliderTrigger = bColliderTrigger; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetBulletType(EBulletType.None);
        SetBulletShooterType(EBulletShooterType.None);
        SetPlayerBulletType(EPlayerBulletType.None);
        SetEnemyBulletType(EEnemyBulletType.None);

        SetBulletSpriteIndex(0);
        SetBulletReflectCount(0);
        SetBulletReflectMaxCount(0);

        SetBulletDamage(0.0f);
        SetBulletMoveSpeed();
        SetBulletRotateSpeed();
        SetBulletRotateValue(0);
        SetDestroyPadding(0.0f);
        SetBulletOption();
        SetCondition(false);
        SetCollisionDestroy(false);
        SetColliderTrigger(false);

        base.AllReset();
    }
    #endregion
}
