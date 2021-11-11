using UnityEngine;

public static class Math
{
    public static Quaternion LookAt2D(Vector3 objectPosition, Vector3 targetPosition)
    {
        return Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(targetPosition.y - objectPosition.y, targetPosition.x - objectPosition.x) - 90f);
    }
}
