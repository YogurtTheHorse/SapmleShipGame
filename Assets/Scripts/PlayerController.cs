using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpaceShip))]
public class PlayerController : MonoBehaviour
{
    public float powerAcceleration = 1;

    public float rollPowerAcceleration = 0.3f;

    public float pitchPowerAcceleration = 0.3f;

    private SpaceShip _spaceShip;

    public void Awake()
    {
        _spaceShip = GetComponent<SpaceShip>();
    }

    private void Update()
    {
        var speedDelta = Keyboard.current.leftShiftKey.isPressed ? powerAcceleration : -powerAcceleration;

        AddValueInRange(ref _spaceShip.power, speedDelta);

        if (Keyboard.current.wKey.isPressed)
        {
            _spaceShip.pitchPower = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            _spaceShip.pitchPower = -1;
        }
        else
        {
            _spaceShip.pitchPower = 0;
        }


        if (Keyboard.current.dKey.isPressed)
        {
            _spaceShip.rollPower = -1;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            _spaceShip.rollPower = 1;
        }
        else
        {
            _spaceShip.rollPower = 0;
        }
        
#if UNITY_EDITOR
        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            Time.timeScale /= 2;
        }

        if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            Time.timeScale *= 2;
        }
#endif
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