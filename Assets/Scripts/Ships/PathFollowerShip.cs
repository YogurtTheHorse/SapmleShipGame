using System;
using UnityEngine;

[RequireComponent(typeof(RocketLauncher))]
public class PathFollowerShip : MonoBehaviour
{
    private Vector3 _oldPosition;
    private RocketLauncher _rocketLauncher;

    private void Awake()
    {
        _rocketLauncher = GetComponent<RocketLauncher>();
    }

    private void Update()
    {
        var pos = transform.position;
        var delta = pos - _oldPosition;
        
        transform.LookAt(pos + delta);
        
        _oldPosition = pos;
    }

    private void Explode()
    {
        Destroy(transform.parent.gameObject);
    }
}