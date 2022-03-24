using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Rocket rocketPrefab;
    public Transform[] launchTransforms;

    public int launchIndex;
    
    public void Start()
    {
        if (!rocketPrefab)
        {
            Debug.LogError("No rocket prefab was set");
        }

        if (launchTransforms == null || launchTransforms.Length == 0)
        {
            Debug.LogError("No launch transforms were set");
        }
    }

    public void Launch()
    {
        launchIndex = (launchIndex + 1) % launchTransforms.Length;
        var t = launchTransforms[launchIndex];

        Instantiate(rocketPrefab, t.position, t.rotation);
    }
}
