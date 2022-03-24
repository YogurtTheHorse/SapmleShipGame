using System;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public bool loop = true;
    
    public PathSection[] sections = {};

    public int currentSection = 0;
}