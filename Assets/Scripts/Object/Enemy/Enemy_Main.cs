#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class Enemy_Main : MonoBehaviour
{
	#region VARIABLE
	public List<CoroutineHandle> pPatternList;
	public DelegateMove pDelegateMove;
	public DelegateMove pDelegateAutoMove;
	public DelegateCommon pDelegateDestroy;
	public DelegateCounterPattern pDelegateCounterPattern;
	public Vector3 vDirection;
	public int iFireCount;
	public bool bDamaged = false;
	public bool bLookAt = false;

	private Enemy_Base pEnemyBase;
	private Vector3 vTempPosition;
	private float fTime = 0.0f;
	private float fColorTime = 0.0f;
	private bool bScreenOut;
	private bool bColorInversion;
	#endregion

	#region GET METHOD
	public Enemy_Base GetEnemyBase() { return pEnemyBase; }
    #endregion

    #region UNITY LIFE CYCLE
    void FixedUpdate()
	{
		if (pEnemyBase == null)
		{
			return;
		}

		OutScreenCheck(pEnemyBase.GetTransform());
		if (bScreenOut.Equals(false) && TouchScreenCheck(pEnemyBase.GetTransform()).Equals(true))
		{
			bScreenOut = true;
		}

		if (bLookAt.Equals(true))
        {
			vDirection = GameManager.Instance.pPlayerObject.transform.position - transform.position;
			transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(vDirection.y, vDirection.x) * Mathf.Rad2Deg) + 90.0f, Vector3.forward);
		}

		if (bDamaged.Equals(true))
        {
			fTime += Time.deltaTime;
			fColorTime += Time.deltaTime;
			if (fColorTime >= 0.02f)
			{
				fColorTime = 0.0f;
				bColorInversion = (bColorInversion.Equals(true)) ? false : true;
			}
			GetEnemyBase().GetSpriteRenderer().color = new Color(1.0f, bColorInversion.Equals(true) ? 0.0f : 1.0f, 1.0f, 1.0f);

			if (fTime >= 0.2f)
            {
				bDamaged = false;
				fTime = 0.0f;
				fColorTime = 0.0f;
				bColorInversion = false;
				GetEnemyBase().GetSpriteRenderer().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D pCollider)
	{
		if (pCollider.name.Equals("HitPoint"))
		{
			Player_Main pPlayerMain = pCollider.transform.parent.GetComponent<Player_Main>();
			Player_Base pPlayerBase = pPlayerMain.GetPlayerBase();

			if (pPlayerBase.GetDeath().Equals(false) && pPlayerBase.GetInvincible().Equals(false))
			{
				Timing.RunCoroutine(pPlayerMain.PlayerDeath());
			}
		}
	}
	#endregion

	#region COMMON METHOD
	public void Init(GameObject pEnemyObject, Transform pTransform, Vector3 vPosition, Vector3 vRotation, Vector3 vScale, EEnemySpriteType enEnemySpriteType, string szEnemyTag, float fEnemyHP, float fDestroyPadding, bool bCounter, bool bOutScreenShot)
	{
		if (pEnemyBase == null)
		{
			pPatternList = new List<CoroutineHandle>();
			pEnemyBase = new Enemy_Base(pEnemyObject, pTransform, vPosition, vRotation, vScale, szEnemyTag);
			pEnemyBase.SetSpriteRenderer(pEnemyBase.GetTransform().GetChild(0).GetComponent<SpriteRenderer>());
			pEnemyBase.SetAnimator(pEnemyBase.GetTransform().GetChild(0).GetComponent<Animator>());
			pEnemyBase.SetCircleCollider(GetComponent<CircleCollider2D>());
		}
		else
		{
			pEnemyBase.Init(pEnemyObject, pTransform, vPosition, vRotation, vScale, szEnemyTag, EObjectType.Type_Enemy);
		}
		pPatternList.Clear();
		pDelegateMove = null;
		pDelegateAutoMove = null;
		pDelegateDestroy = null;
		pDelegateCounterPattern = null;
		pEnemyBase.SetChildRotationZ(0, 180.0f);
		pEnemyBase.GetSpriteRenderer().sprite = GameManager.Instance.pEnemySpriteArray[(int)enEnemySpriteType];
		pEnemyBase.SetEnemyCurrentHP(fEnemyHP);
		pEnemyBase.SetEnemyMaxHP(fEnemyHP);
		pEnemyBase.SetDestroyPadding(fDestroyPadding);
		pEnemyBase.SetCounter(bCounter);
		pEnemyBase.SetOutScreenShot(bOutScreenShot);
		iFireCount = 0;
		bScreenOut = false;
		bColorInversion = false;
		bDamaged = false;
		bLookAt = false;
	}
	// public void EnemyMove()
	// {
	// 	pEnemyBase.GetTransform().Translate(new Vector2(pEnemyBase.GetEnemyMoveSpeedX() * Time.deltaTime, pEnemyBase.GetEnemyMoveSpeedY() * Time.deltaTime));
	// }
	// public void EnemyMoveAcceleration()
	// {
	// 	// ACCELERATION X
	// 	pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveSpeedX() + pEnemyBase.GetEnemyMoveAccelerationSpeedX());
	// 	if (pEnemyBase.GetEnemyMoveSpeedX() >= pEnemyBase.GetEnemyMoveAccelerationSpeedXMax())
	// 	{
	// 		pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveAccelerationSpeedXMax());
	// 		pEnemyBase.SetEnemyMoveAccelerationSpeedX(0.0f);
	// 		pEnemyBase.SetEnemyMoveAccelerationSpeedXMax(0.0f);
	// 	}
	// 
	// 	// ACCELERATION Y
	// 	pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveSpeedY() + pEnemyBase.GetEnemyMoveAccelerationSpeedY());
	// 	if (pEnemyBase.GetEnemyMoveSpeedY() >= pEnemyBase.GetEnemyMoveAccelerationSpeedYMax())
	// 	{
	// 		pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveAccelerationSpeedYMax());
	// 		pEnemyBase.SetEnemyMoveAccelerationSpeedY(0.0f);
	// 		pEnemyBase.SetEnemyMoveAccelerationSpeedYMax(0.0f);
	// 	}
	// }
	// public void EnemyMoveDeceleration()
	// {
	// 	// DECELERTAION X
	// 	pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveSpeedX() - pEnemyBase.GetEnemyMoveDecelerationSpeedX());
	// 	if (pEnemyBase.GetEnemyMoveSpeedX() <= pEnemyBase.GetEnemyMoveDecelerationSpeedXMin())
	// 	{
	// 		pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveDecelerationSpeedXMin());
	// 		pEnemyBase.SetEnemyMoveDecelerationSpeedX(0.0f);
	// 		pEnemyBase.SetEnemyMoveDecelerationSpeedXMin(0.0f);
	// 	}
	// 
	// 	// DECELERATION Y
	// 	pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveSpeedY() - pEnemyBase.GetEnemyMoveDecelerationSpeedY());
	// 	if (pEnemyBase.GetEnemyMoveSpeedY() <= pEnemyBase.GetEnemyMoveDecelerationSpeedYMin())
	// 	{
	// 		pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveDecelerationSpeedYMin());
	// 		pEnemyBase.SetEnemyMoveDecelerationSpeedY(0.0f);
	// 		pEnemyBase.SetEnemyMoveDecelerationSpeedYMin(0.0f);
	// 	}
	// }
	public void OutScreenCheck(Transform pTransform)
	{
		vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);
		float fDestroyPadding = pEnemyBase.GetDestroyPadding();

		if (vTempPosition.x < 0.0f - fDestroyPadding || vTempPosition.x > Screen.width + fDestroyPadding ||
			vTempPosition.y < 0.0f - fDestroyPadding || vTempPosition.y > Screen.height + fDestroyPadding)
		{
			// if (vTempPosition.x < 0.0f - fDestroyPadding)
            // {
			// 	Debug.Log("TEST 1");
            // }
			// else if (vTempPosition.x > Screen.width + fDestroyPadding)
            // {
			// 	Debug.Log("TEST 2");
			// }
			// else if (vTempPosition.y < 0.0f - fDestroyPadding)
            // {
			// 	Debug.Log("TEST 3");
			// }
			// else if (vTempPosition.y > Screen.height + fDestroyPadding)
            // {
			// 	Debug.Log("TEST 4");
			// }
			// Debug.Log(string.Format("fDestroyPadding = {0}", pEnemyBase.GetDestroyPadding()));
			// Debug.Log(string.Format("vTempPosition = x {0}, y {1}", vTempPosition.x, vTempPosition.y));
			DestroyEnemy(false, false);
		}
	}
	public bool TouchScreenCheck(Transform pTransform)
	{
		vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);
		if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y < 0 || vTempPosition.y > Screen.height)
		{
			return true;
		}
		return false;
	}
	public void SetDamaged()
    {
		bDamaged = true;
		fTime = 0.0f;
    }
	public void DestroyEnemy(bool bAttackDestroy = true, bool bSound = true)
	{
		if (bSound.Equals(true)) SoundManager.Instance.PlaySFX((ESFXType)UnityEngine.Random.Range((int)ESFXType.Type_SFX_EnemyDestroy1, (int)ESFXType.Type_SFX_EnemyDestroy2 + 1));
		if (bAttackDestroy.Equals(true))
        {
			GameManager.Instance.iTempScore += (100 * GameManager.Instance.iPhase);
			GameManager.Instance.iDestroyCount++;
		}

		foreach (var vPattern in pPatternList)
        {
			Timing.KillCoroutines(vPattern);
        }
		if (pDelegateDestroy != null) pDelegateDestroy();
		if (pDelegateCounterPattern != null) Timing.RunCoroutine(pDelegateCounterPattern(this.gameObject, GameManager.Instance.pPlayerObject), "Enemy_CounterPattern");
		Timing.RunCoroutine(EffectManager.Instance.ExtractEffect(GetEnemyBase().GetPosition(),
			new Vector3(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)), new Vector3(3.5f, 3.5f, 1.0f), EEffectType.Type_Enemy_Destroy_02, 1.25f), "Enemy_DestroyEffect");

		pDelegateMove = null;
		pDelegateDestroy = null;
		pDelegateCounterPattern = null; 
		Destroy(this.gameObject);
	}
	#endregion
}