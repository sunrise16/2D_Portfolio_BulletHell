#region USING
using UnityEngine;
#endregion

public static class Utility
{
    #region BULLET DESTINATION
    public static float GetAimedAngle(GameObject pObject, GameObject pTargetObject)
    {
        return Mathf.Atan2(GetAimedDestination(pObject, pTargetObject).y, GetAimedDestination(pObject, pTargetObject).x) * Mathf.Rad2Deg;
    }
    public static float GetAimedAngle(Vector2 vPosition, Vector2 vTargetPosition)
    {
        return Mathf.Atan2(GetAimedDestination(vPosition, vTargetPosition).y, GetAimedDestination(vPosition, vTargetPosition).x) * Mathf.Rad2Deg;
    }
    public static float GetRandomAngle(GameObject pObject)
    {
        return Mathf.Atan2(GetRandomDestination(pObject).y, GetRandomDestination(pObject).x) * Mathf.Rad2Deg;
    }
    public static float GetRandomAngle(Vector2 vPosition)
    {
        return Mathf.Atan2(GetRandomDestination(vPosition).y, GetRandomDestination(vPosition).x) * Mathf.Rad2Deg;
    }
    public static Vector2 GetAimedDestination(GameObject pObject, GameObject pTargetObject)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        Vector2 vTargetPosition = pTargetObject.GetComponent<Transform>().position;
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public static Vector2 GetAimedDestination(Vector2 vObjectPosition, GameObject pTargetObject)
    {
        Vector2 vTargetPosition = pTargetObject.GetComponent<Transform>().position;
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public static Vector2 GetAimedDestination(Vector2 vObjectPosition, Vector2 vTargetPosition)
    {
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public static Vector2 GetRandomDestination(GameObject pObject)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        Vector2 vTargetPosition = new Vector2(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f));
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public static Vector2 GetRandomDestination(Vector2 vObjectPosition)
    {
        Vector2 vTargetPosition = new Vector2(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f));
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    #endregion

    #region BULLET CREATE POSITION
    public static Vector2 GetBulletCreatingPosition(GameObject pObject)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;

        return new Vector2(vObjectPosition.x, vObjectPosition.y);
    }
    public static Vector2 GetBulletCreatingPosition(Vector2 vObjectPosition)
    {
        return new Vector2(vObjectPosition.x, vObjectPosition.y);
    }
    public static Vector2 GetBulletCreatingPosition(GameObject pObject, float fRadius, float fAngle)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fRadian = fAngle * Mathf.PI / 180;

        return new Vector2(vObjectPosition.x + (fRadius * Mathf.Cos(fRadian)), vObjectPosition.y + (fRadius * Mathf.Sin(fRadian)));
    }
    public static Vector2 GetBulletCreatingPosition(Vector2 vObjectPosition, float fRadius, float fAngle)
    {
        float fRadian = fAngle * Mathf.PI / 180;

        return new Vector2(vObjectPosition.x + (fRadius * Mathf.Cos(fRadian)), vObjectPosition.y + (fRadius * Mathf.Sin(fRadian)));
    }
    public static Vector2 GetRandomBulletCreatingPosition(GameObject pObject, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public static Vector2 GetRandomBulletCreatingPosition(Vector2 vObjectPosition, float fRadius)
    {
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public static Vector2 GetRandomBulletCreatingPosition(GameObject pObject, float fMargin, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX, fAddPositionY;

        fAddPositionX = Random.Range(-fRadius, fRadius);
        if (Mathf.Abs(fAddPositionX) < fMargin)
        {
            fAddPositionX = fAddPositionX < 0.0f ? fAddPositionX - (fMargin - Mathf.Abs(fAddPositionX)) : fAddPositionX + (fMargin - Mathf.Abs(fAddPositionX));
        }
        fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public static Vector2 GetRandomBulletCreatingPosition(Vector2 vObjectPosition, float fMargin, float fRadius)
    {
        float fAddPositionX, fAddPositionY;

        fAddPositionX = Random.Range(-fRadius, fRadius);
        if (Mathf.Abs(fAddPositionX) < fMargin)
        {
            fAddPositionX = fAddPositionX < 0.0f ? fAddPositionX - (fMargin - Mathf.Abs(fAddPositionX)) : fAddPositionX + (fMargin - Mathf.Abs(fAddPositionX));
        }
        fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public static Vector2 GetRandomBulletCreatingPositionMax(GameObject pObject, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));
        int iPositionMultiply = Random.Range(0, 2);

        return new Vector2(vObjectPosition.x + fAddPositionX, (iPositionMultiply.Equals(0) ? vObjectPosition.y + fAddPositionY : vObjectPosition.y - fAddPositionY));
    }
    public static Vector2 GetRandomBulletCreatingPositionMax(Vector2 vObjectPosition, float fRadius)
    {
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));
        int iPositionMultiply = Random.Range(0, 2);

        return new Vector2(vObjectPosition.x + fAddPositionX, (iPositionMultiply.Equals(0) ? vObjectPosition.y + fAddPositionY : vObjectPosition.y - fAddPositionY));
    }
    #endregion
}