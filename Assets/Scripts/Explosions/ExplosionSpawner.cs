using UnityEngine;
using Zenject;

namespace Explosions
{
    public class ExplosionSpawner
    {
        [Inject]
        private ExplosionFactory _factory;

        public Explosion Spawn(Vector3 position)
        {
            var explosion = _factory.Create(); // not using pool as it's very small object

            explosion.transform.position = position;

            return explosion;
        }
    
        public class ExplosionFactory : PlaceholderFactory<Explosion> { }
    }
}