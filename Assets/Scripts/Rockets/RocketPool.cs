using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Rockets
{
    public class RocketPool
    {
        private List<Rocket> _pool = new();
    
        [Inject]
        private Rocket.Factory _rocketFactory;

        public Rocket Launch(Vector3 position, Quaternion rotation)
        {
            var newRocket = FindAvailableRocketOrNull();

            if (!newRocket)
            {
                newRocket = _rocketFactory.Create();
                _pool.Add(newRocket);
            }

            var t = newRocket.transform;
        
            t.position = position;
            t.rotation = rotation;

            newRocket.gameObject.SetActive(true);
            newRocket.leftTime = newRocket.lifeTime;
        
            return newRocket;
        }

        private Rocket FindAvailableRocketOrNull() => _pool.FirstOrDefault(r => !r.gameObject.activeInHierarchy);
    }
}