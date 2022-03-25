using System;
using UnityEngine;

[RequireComponent(typeof(SpaceShip))]
public class ShipAI : MonoBehaviour
{
    public Vector3 targetPosition;

    public Vector3 projectedTargetPoint;
    public float distanceFromShipPlane, pitchDelta, rollDelta, currentRoll;

    private SpaceShip _ship;
    private Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ship = GetComponent<SpaceShip>();
    }

    public void Start()
    {
        _ship.power = 1; // AI always fly
    }

    public void Update()
    {
        var t = transform;
        var position = t.position;
        var up = t.up;

        targetPosition = position + Vector3.forward * 300f + Vector3.up * 30f + Vector3.right * 150f;

        projectedTargetPoint = MathUtils.ClosestPointOnPlane(position, up, targetPosition);
        distanceFromShipPlane = MathUtils.DistanceFromPlane(position, up, targetPosition);
        var pitchRadDelta = Mathf.Atan2(distanceFromShipPlane, (projectedTargetPoint - position).magnitude);
        pitchDelta = 10f + pitchRadDelta * Mathf.Rad2Deg;

        _ship.pitchPower = ClampAngleToPower(pitchDelta, 30f);
        
        var relativePoint = t.rotation * (projectedTargetPoint - position);
        Rotate(relativePoint.x > 0);
    }

    private void Rotate(bool right)
    {
        var targetRoll = right ? 15 : -15;
        currentRoll = Vector3.Angle(transform.up, Vector3.up);
        rollDelta = MathUtils.OptimizeAngle(targetRoll - currentRoll);

        _ship.rollPower = ClampAngleToPower(-rollDelta, 180f);
    }

    private float ClampAngleToPower(float angle, float clamp) => Mathf.Clamp(angle, -clamp, clamp) / clamp;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, projectedTargetPoint);
        Gizmos.DrawSphere(projectedTargetPoint, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(projectedTargetPoint, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.1f);
    }
}