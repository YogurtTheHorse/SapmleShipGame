using System;
using UnityEngine;

namespace RailsPath
{
    [Serializable]
    public class PathSection
    {
        public bool bindTangentToPrevious = true;

        public Vector3 startPoint, endPoint;

        public Vector3 startTangent, endTangent;
    }
}