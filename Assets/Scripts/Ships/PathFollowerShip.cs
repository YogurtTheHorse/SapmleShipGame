using Rockets;
using UnityEngine;
using Zenject;

namespace Ships
{
    [RequireComponent(typeof(RocketLauncher))]
    public class PathFollowerShip : MonoBehaviour
    {
        public float shootAngle = 15f;
    
        [Inject(Id = "player")]
        private GameObject _player;
    
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

            var playerTrans = _player.transform;
            var vectorToPlayer = playerTrans.position - pos;
            var angleToPlayer = Vector2.Angle(playerTrans.forward, vectorToPlayer);

            if (angleToPlayer < shootAngle)
            {
                _rocketLauncher.Launch();
            }
        }

        private void Explode()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}