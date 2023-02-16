#region USING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class UI_Base : Object_Base
{
    #region VARIABLE
    private EUIType enUIType;

    private float fUIMoveSpeedX;
    private float fUIMoveSpeedXLimit;
    private float fUIMoveAccelerationSpeedX;
    private float fUIMoveSpeedY;
    private float fUIMoveSpeedYLimit;
    private float fUIMoveAccelerationSpeedY;

    private float fUIRotateSpeed;
    private float fUIRotateSpeedLimit;
    private float fUIRotateAccelerationSpeed;
    
    private float fUIScaleSpeedX;
    private float fUIScaleSpeedXLimit;
    private float fUIScaleAccelerationSpeedX;
    private float fUIScaleSpeedY;
    private float fUIScaleSpeedYLimit;
    private float fUIScaleAccelerationSpeedY;
    private float fUIScaleSpeedZ;
    private float fUIScaleSpeedZLimit;
    private float fUIScaleAccelerationSpeedZ;

    private float fUIAlphaSpeed;
    private float fUIAlphaSpeedLimit;
    private float fUIAlphaAccelerationSpeed;
    #endregion

    #region CONSTRUCTOR
    public UI_Base(GameObject pUIObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, string szUITag, EUIType enUIType, float[] fData)
    {
        base.Init(pUIObject, pTransform, vPosition, vRotation, vScale, szUITag, EObjectType.Type_UI);

        this.enUIType = enUIType;

        fUIMoveSpeedX = fData[0];
        fUIMoveSpeedXLimit = fData[1];
        fUIMoveAccelerationSpeedX = fData[2];
        fUIMoveSpeedY = fData[3];
        fUIMoveSpeedYLimit = fData[4];
        fUIMoveAccelerationSpeedY = fData[5];

        fUIRotateSpeed = fData[6];
        fUIRotateSpeedLimit = fData[7];
        fUIRotateAccelerationSpeed = fData[8];

        fUIScaleSpeedX = fData[9];
        fUIScaleSpeedXLimit = fData[10];
        fUIScaleAccelerationSpeedX = fData[11];
        fUIScaleSpeedY = fData[12];
        fUIScaleSpeedYLimit = fData[13];
        fUIScaleAccelerationSpeedY = fData[14];
        fUIScaleSpeedZ = fData[15];
        fUIScaleSpeedZLimit = fData[16];
        fUIScaleAccelerationSpeedZ = fData[17];

        fUIAlphaSpeed = fData[18];
        fUIAlphaSpeedLimit = fData[19];
        fUIAlphaAccelerationSpeed = fData[20];
    }
    #endregion

    #region GET METHOD
    public EUIType GetUIType() { return enUIType; }
    public float GetUIMoveSpeedX() { return fUIMoveSpeedX; }
    public float GetUIMoveSpeedXLimit() { return fUIMoveSpeedXLimit; }
    public float GetUIMoveAccelerationSpeedX() { return fUIMoveAccelerationSpeedX; }
    public float GetUIMoveSpeedY() { return fUIMoveSpeedY; }
    public float GetUIMoveSpeedYLimit() { return fUIMoveSpeedYLimit; }
    public float GetUIMoveAccelerationSpeedY() { return fUIMoveAccelerationSpeedY; }
    public float GetUIRotateSpeed() { return fUIRotateSpeed; }
    public float GetUIRotateSpeedLimit() { return fUIRotateSpeedLimit; }
    public float GetUIRotateAccelerationSpeed() { return fUIRotateAccelerationSpeed; }
    public float GetUIScaleSpeedX() { return fUIScaleSpeedX; }
    public float GetUIScaleSpeedXLimit() { return fUIScaleSpeedXLimit; }
    public float GetUIScaleAccelerationSpeedX() { return fUIScaleAccelerationSpeedX; }
    public float GetUIScaleSpeedY() { return fUIScaleSpeedY; }
    public float GetUIScaleSpeedYLimit() { return fUIScaleSpeedYLimit; }
    public float GetUIScaleAccelerationSpeedY() { return fUIScaleAccelerationSpeedY; }
    public float GetUIScaleSpeedZ() { return fUIScaleSpeedZ; }
    public float GetUIScaleSpeedZLimit() { return fUIScaleSpeedZLimit; }
    public float GetUIScaleAccelerationSpeedZ() { return fUIScaleAccelerationSpeedZ; }
    public float GetUIAlphaSpeed() { return fUIAlphaSpeed; }
    public float GetUIAlphaSpeedLimit() { return fUIAlphaSpeedLimit; }
    public float GetUIAlphaAccelerationSpeed() { return fUIAlphaAccelerationSpeed; }
    #endregion

    #region SET METHOD
    public void SetUIType(EUIType enUIType) { this.enUIType = enUIType; }
    public void SetUIMoveSpeedX(float fUIMoveSpeedX) { this.fUIMoveSpeedX = fUIMoveSpeedX; }
    public void SetUIMoveSpeedXLimit(float fUIMoveSpeedXLimit) { this.fUIMoveSpeedXLimit = fUIMoveSpeedXLimit; }
    public void SetUIMoveAccelerationSpeedX(float fUIMoveAccelerationSpeedX) { this.fUIMoveAccelerationSpeedX = fUIMoveAccelerationSpeedX; }
    public void SetUIMoveSpeedY(float fUIMoveSpeedY) { this.fUIMoveSpeedY = fUIMoveSpeedY; }
    public void SetUIMoveSpeedYLimit(float fUIMoveSpeedYLimit) { this.fUIMoveSpeedYLimit = fUIMoveSpeedYLimit; }
    public void SetUIMoveAccelerationSpeedY(float fUIMoveAccelerationSpeedY) { this.fUIMoveAccelerationSpeedY = fUIMoveAccelerationSpeedY; }
    public void SetUIRotateSpeed(float fUIRotateSpeed) { this.fUIRotateSpeed = fUIRotateSpeed; }
    public void SetUIRotateSpeedLimit(float fUIRotateSpeedLimit) { this.fUIRotateSpeedLimit = fUIRotateSpeedLimit; }
    public void SetUIRotateAccelerationSpeed(float fUIRotateAccelerationSpeed) { this.fUIRotateAccelerationSpeed = fUIRotateAccelerationSpeed; }
    public void SetUIScaleSpeedX(float fUIScaleSpeedX) { this.fUIScaleSpeedX = fUIScaleSpeedX; }
    public void SetUIScaleSpeedXLimit(float fUIScaleSpeedXLimit) { this.fUIScaleSpeedXLimit = fUIScaleSpeedXLimit; }
    public void SetUIScaleAccelerationSpeedX(float fUIScaleAccelerationSpeedX) { this.fUIScaleAccelerationSpeedX = fUIScaleAccelerationSpeedX; }
    public void SetUIScaleSpeedY(float fUIScaleSpeedY) { this.fUIScaleSpeedY = fUIScaleSpeedY; }
    public void SetUIScaleSpeedYLimit(float fUIScaleSpeedYLimit) { this.fUIScaleSpeedYLimit = fUIScaleSpeedYLimit; }
    public void SetUIScaleAccelerationSpeedY(float fUIScaleAccelerationSpeedY) { this.fUIScaleAccelerationSpeedY = fUIScaleAccelerationSpeedY; }
    public void SetUIScaleSpeedZ(float fUIScaleSpeedZ) { this.fUIScaleSpeedZ = fUIScaleSpeedZ; }
    public void SetUIScaleSpeedZLimit(float fUIScaleSpeedZLimit) { this.fUIScaleSpeedZLimit = fUIScaleSpeedZLimit; }
    public void SetUIScaleAccelerationSpeedZ(float fUIScaleAccelerationSpeedZ) { this.fUIScaleAccelerationSpeedZ = fUIScaleAccelerationSpeedZ; }
    public void SetUIAlphaSpeed(float fUIAlphaSpeed) { this.fUIAlphaSpeed = fUIAlphaSpeed; }
    public void SetUIAlphaSpeedLimit(float fUIAlphaSpeedLimit) { this.fUIAlphaSpeedLimit = fUIAlphaSpeedLimit; }
    public void SetUIAlphaAccelerationSpeed(float fUIAlphaAccelerationSpeed) { this.fUIAlphaAccelerationSpeed = fUIAlphaAccelerationSpeed; }
    #endregion
}