#region USING
using UnityEngine;
#endregion

public class Enemy_Base : Object_Base, IObjectBase
{
    #region VARIABLE
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private CircleCollider2D pCircleCollider;
    private int iEnemySpriteIndex;
    private float fEnemyCurrentHP;
    private float fEnemyMaxHP;
    private float fEnemyMoveSpeedX;
    private float fEnemyMoveAccelerationSpeedX;
    private float fEnemyMoveAccelerationSpeedXMax;
    private float fEnemyMoveDecelerationSpeedX;
    private float fEnemyMoveDecelerationSpeedXMin;
    private float fEnemyMoveSpeedY;
    private float fEnemyMoveAccelerationSpeedY;
    private float fEnemyMoveAccelerationSpeedYMax;
    private float fEnemyMoveDecelerationSpeedY;
    private float fEnemyMoveDecelerationSpeedYMin;
    private float fDestroyPadding;
    private bool bCounter;
    private bool bOutScreenShot;
    #endregion

    #region CONSTRUCTOR
    public Enemy_Base(GameObject pEnemyObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szEnemyTag)
    {
        base.Init(pEnemyObject, pTransform, vPosition, vRotation, vScale, szEnemyTag, EObjectType.Type_Enemy);
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public CircleCollider2D GetCircleCollider() { return pCircleCollider; }
    public int GetEnemySpriteIndex() { return iEnemySpriteIndex; }
    public float GetEnemyCurrentHP() { return fEnemyCurrentHP; }
    public float GetEnemyMaxHP() { return fEnemyMaxHP; }
    public float GetEnemyMoveSpeedX() { return fEnemyMoveSpeedX; }
    public float GetEnemyMoveAccelerationSpeedX() { return fEnemyMoveAccelerationSpeedX; }
    public float GetEnemyMoveAccelerationSpeedXMax() { return fEnemyMoveAccelerationSpeedXMax; }
    public float GetEnemyMoveDecelerationSpeedX() { return fEnemyMoveDecelerationSpeedX; }
    public float GetEnemyMoveDecelerationSpeedXMin() { return fEnemyMoveDecelerationSpeedXMin; }
    public float GetEnemyMoveSpeedY() { return fEnemyMoveSpeedY; }
    public float GetEnemyMoveAccelerationSpeedY() { return fEnemyMoveAccelerationSpeedY; }
    public float GetEnemyMoveAccelerationSpeedYMax() { return fEnemyMoveAccelerationSpeedYMax; }
    public float GetEnemyMoveDecelerationSpeedY() { return fEnemyMoveDecelerationSpeedY; }
    public float GetEnemyMoveDecelerationSpeedYMin() { return fEnemyMoveDecelerationSpeedYMin; }
    public float GetDestroyPadding() { return fDestroyPadding; }
    public bool GetCounter() { return bCounter; }
    public bool GetOutScreenShot() { return bOutScreenShot; }
    #endregion

    #region SET METHOD
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetCircleCollider(CircleCollider2D pCircleCollider) { this.pCircleCollider = pCircleCollider; }
    public void SetEnemySpriteIndex(int iEnemySpriteIndex) { this.iEnemySpriteIndex = iEnemySpriteIndex; }
    public void SetEnemyCurrentHP(float fEnemyCurrentHP) { this.fEnemyCurrentHP = fEnemyCurrentHP; }
    public void SetEnemyMaxHP(float fEnemyMaxHP) { this.fEnemyMaxHP = fEnemyMaxHP; }
    public void SetEnemyMoveSpeedX(float fEnemyMoveSpeedX) { this.fEnemyMoveSpeedX = fEnemyMoveSpeedX; }
    public void SetEnemyMoveAccelerationSpeedX(float fEnemyMoveAccelerationSpeedX) { this.fEnemyMoveAccelerationSpeedX = fEnemyMoveAccelerationSpeedX; }
    public void SetEnemyMoveAccelerationSpeedXMax(float fEnemyMoveAccelerationSpeedXMax) { this.fEnemyMoveAccelerationSpeedXMax = fEnemyMoveAccelerationSpeedXMax; }
    public void SetEnemyMoveDecelerationSpeedX(float fEnemyMoveDecelerationSpeedX) { this.fEnemyMoveDecelerationSpeedX = fEnemyMoveDecelerationSpeedX; }
    public void SetEnemyMoveDecelerationSpeedXMin(float fEnemyMoveDecelerationSpeedXMin) { this.fEnemyMoveDecelerationSpeedXMin = fEnemyMoveDecelerationSpeedXMin; }
    public void SetEnemySpeedX(float fEnemyMoveSpeedX, float fEnemyMoveAccelerationSpeedX = 0.0f, float fEnemyMoveAccelerationSpeedXMax = 0.0f, float fEnemyMoveDecelerationSpeedX = 0.0f, float fEnemyMoveDecelerationSpeedXMin = 0.0f)
    {
        SetEnemyMoveSpeedX(fEnemyMoveSpeedX);
        SetEnemyMoveAccelerationSpeedX(fEnemyMoveAccelerationSpeedX);
        SetEnemyMoveAccelerationSpeedXMax(fEnemyMoveAccelerationSpeedXMax);
        SetEnemyMoveDecelerationSpeedX(fEnemyMoveDecelerationSpeedX);
        SetEnemyMoveDecelerationSpeedXMin(fEnemyMoveDecelerationSpeedXMin);
    }
    public void SetEnemyMoveSpeedY(float fEnemyMoveSpeedY) { this.fEnemyMoveSpeedY = fEnemyMoveSpeedY; }
    public void SetEnemyMoveAccelerationSpeedY(float fEnemyMoveAccelerationSpeedY) { this.fEnemyMoveAccelerationSpeedY = fEnemyMoveAccelerationSpeedY; }
    public void SetEnemyMoveAccelerationSpeedYMax(float fEnemyMoveAccelerationSpeedYMax) { this.fEnemyMoveAccelerationSpeedYMax = fEnemyMoveAccelerationSpeedYMax; }
    public void SetEnemyMoveDecelerationSpeedY(float fEnemyMoveDecelerationSpeedY) { this.fEnemyMoveDecelerationSpeedY = fEnemyMoveDecelerationSpeedY; }
    public void SetEnemyMoveDecelerationSpeedYMin(float fEnemyMoveDecelerationSpeedYMin) { this.fEnemyMoveDecelerationSpeedYMin = fEnemyMoveDecelerationSpeedYMin; }
    public void SetEnemySpeedY(float fEnemyMoveSpeedY, float fEnemyMoveAccelerationSpeedY = 0.0f, float fEnemyMoveAccelerationSpeedYMax = 0.0f, float fEnemyMoveDecelerationSpeedY = 0.0f, float fEnemyMoveDecelerationSpeedYMin = 0.0f)
    {
        SetEnemyMoveSpeedY(fEnemyMoveSpeedY);
        SetEnemyMoveAccelerationSpeedY(fEnemyMoveAccelerationSpeedY);
        SetEnemyMoveAccelerationSpeedYMax(fEnemyMoveAccelerationSpeedYMax);
        SetEnemyMoveDecelerationSpeedY(fEnemyMoveDecelerationSpeedY);
        SetEnemyMoveDecelerationSpeedYMin(fEnemyMoveDecelerationSpeedYMin);
    }
    public void SetDestroyPadding(float fDestroyPadding) { this.fDestroyPadding = fDestroyPadding; }
    public void SetCounter(bool bCounter) { this.bCounter = bCounter; }
    public void SetOutScreenShot(bool bOutScreenShot) { this.bOutScreenShot = bOutScreenShot; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetEnemySpriteIndex(0);
        SetEnemyCurrentHP(0.0f);
        SetEnemyMaxHP(0.0f);
        SetEnemySpeedX(0.0f);
        SetEnemySpeedY(0.0f);
        SetDestroyPadding(0.0f);
        SetCounter(false);
        SetOutScreenShot(false);

        base.AllReset();
    }
    #endregion
}