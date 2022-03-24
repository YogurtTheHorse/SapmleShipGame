using System;
using UnityEngine;

public class PathFollowerShip : MonoBehaviour
{
    private Vector3 _oldPosition;
    
    private void Update()
    {
        var pos = transform.position;
        var delta = pos - _oldPosition;
        
        transform.LookAt(pos + delta);
        
        _oldPosition = pos;
    }
}