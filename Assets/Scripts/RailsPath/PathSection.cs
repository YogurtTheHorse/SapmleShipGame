using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PathSection
{
    public bool bindTangentToPrevious = true;

    public Vector3 startPoint, endPoint;

    public Vector3 startTangent, endTangent;
}