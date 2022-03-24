using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class RocketPool
{
    private List<Rocket> _pool = new();
    
    [Inject]
    private Rocket.Factory _rocketFactory;

    public Rocket Launch(Vector3 position, Quaternion rotation)
    {
        var availableRocket = FindAvailableRocketOrNull();

        if (!availableRocket)
        {
            availableRocket = _rocketFactory.Create();
            _pool.Add(availableRocket);
        }

        var t = availableRocket.transform;
        
        t.position = position;
        t.rotation = rotation;

        return availableRocket;
    }

    private Rocket FindAvailableRocketOrNull() => _pool.FirstOrDefault(r => !r.gameObject.activeInHierarchy);
}