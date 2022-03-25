using System;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 ClosestPointOnPlane(Vector3 planeOffset, Vector3 planeNormal, Vector3 point) =>
        point + DistanceFromPlane(planeOffset, planeNormal, point) * planeNormal;

    public static float DistanceFromPlane(Vector3 planeOffset, Vector3 planeNormal, Vector3 point) =>
        Vector3.Dot(planeOffset - point, planeNormal);

    public static float OptimizeAngle(float angle)
    {
        if (angle < -180)
        {
            return angle + 360f;
        }

        if (angle > 180)
        {
            return angle - 360f;
        }

        return angle;
    }

    public static float DistanceAndDiameterToPixelSize(float distance, float diameter)
    {
        if (!Camera.main)
        {
            throw new InvalidOperationException("Main camera is not set");
        }

        return diameter * Mathf.Rad2Deg * Screen.height / (distance * Camera.main.fieldOfView);
    }
}