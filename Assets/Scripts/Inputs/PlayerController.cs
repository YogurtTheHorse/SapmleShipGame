using Rockets;
using Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    [RequireComponent(typeof(SpaceShip))]
    [RequireComponent(typeof(RocketLauncher))]
    public class PlayerController : MonoBehaviour
    {
        public float powerAcceleration = 1;

        public float rollPowerAcceleration = 0.3f;

        public float pitchPowerAcceleration = 0.3f;

        private SpaceShip _spaceShip;
        private RocketLauncher _rocketLauncher;

        public void Awake()
        {
            _spaceShip = GetComponent<SpaceShip>();
            _rocketLauncher = GetComponent<RocketLauncher>();
        }

        private void Update()
        {
            var speedDelta = Keyboard.current.leftShiftKey.isPressed ? powerAcceleration : -powerAcceleration;

            AddValueInRange(ref _spaceShip.power, speedDelta);

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                _spaceShip.pitchPower = 1;
            }
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            {
                _spaceShip.pitchPower = -1;
            }
            else
            {
                _spaceShip.pitchPower = 0;
            }


            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                _spaceShip.rollPower = -1;
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                _spaceShip.rollPower = 1;
            }
            else
            {
                _spaceShip.rollPower = 0;
            }


            if (Keyboard.current.qKey.isPressed)
            {
                _spaceShip.yawPower = -1;
            }
            else if (Keyboard.current.eKey.isPressed)
            {
                _spaceShip.yawPower = 1;
            }
            else
            {
                _spaceShip.yawPower = 0;
            }
        
            if (Keyboard.current.minusKey.wasPressedThisFrame)
            {
                Time.timeScale /= 2;
            }

            if (Keyboard.current.equalsKey.wasPressedThisFrame)
            {
                Time.timeScale *= 2;
            }

            if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
            {
                _rocketLauncher.Launch();
            }

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                _spaceShip.Reset();
            }
        }

        private void AddValueInRange(ref float v, float delta, float min = 0f, float max = 1f)
        {
            float t = v + delta * Time.deltaTime;

            if (t < min)
            {
                t = min;
            }
            else if (t > max)
            {
                t = max;
            }

            v = t;
        }
    }
}