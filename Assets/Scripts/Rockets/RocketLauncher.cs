using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RocketLauncher : MonoBehaviour
{
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

    public void Launch()
    {
        launchIndex = (launchIndex + 1) % launchTransforms.Length;
        var t = launchTransforms[launchIndex];

        _rocketPool.Launch(t.position, t.rotation);
    }
}
