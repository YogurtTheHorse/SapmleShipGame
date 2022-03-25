using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RocketLauncher : MonoBehaviour
{
    public float reloadTime = 0.3f;

    public float reloadLeft = 0;

    public Transform[] launchTransforms;

    public int launchIndex;

    [Inject]
    private RocketPool _rocketPool;

    public void Start()
    {
        if (launchTransforms == null || launchTransforms.Length == 0)
        {
            Debug.LogError("No launch transforms were set");
        }
    }

    public void Update()
    {
        if (reloadLeft > 0)
        {
            reloadLeft -= Time.deltaTime;
        }
    }

    public void Launch()
    {
        if (reloadLeft > 0)
            return;

        reloadLeft = reloadTime;
        
        launchIndex = (launchIndex + 1) % launchTransforms.Length;
        var t = launchTransforms[launchIndex];

        _rocketPool.Launch(t.position, t.rotation);
    }
}