#region USING
using UnityEngine;
#endregion

public interface IObjectBase
{
    public SpriteRenderer GetSpriteRenderer();
    public Animator GetAnimator();

    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer);
    public void SetAnimator(Animator pAnimator);

    public void AllReset();
}

public interface IPoolBase
{
    public GameObject AddPool(Vector3 vPosition, Vector3 vRotation, Vector3 vScale);

    public void ReturnPool(GameObject pObject);
}