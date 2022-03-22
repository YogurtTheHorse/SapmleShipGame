using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour
{
    [Range(0, 1), Tooltip("Current power on engine")]
    public float power = 0;

    [Range(-1, 1), Tooltip("Current roll rotation power")]
    public float rollPower = 0;

    [Range(-1, 1), Tooltip("Current pitch rotation power")]
    public float pitchPower = 0;

    [Tooltip("Speed of changing roll angle per second. Degrees.")]
    public float fullRollSpeed = 120f;

    [Tooltip("Speed of changing pitch angle per second. Degrees.")]
    public float fullPitchSpeed = 30f;

    [Tooltip("Max speed of movement")]
    public float fullThrustForce = 50;

    public float liftForceCoefficient = 0.01f;
    public float dragCoefficient = 0.01f;

    public float maxVelocity = 130;

    private Vector3 _liftForce, _thrustForce;
    private Vector3 _dragAbsoluteForce;

    private Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        var trans = transform;
        var velocity = _rigidbody.velocity;
        
        trans.Rotate(Vector3.forward, rollPower * fullRollSpeed * Time.fixedDeltaTime);
        trans.Rotate(Vector3.right, pitchPower * fullPitchSpeed * Time.fixedDeltaTime);

        var forwardSpeed = Vector3.Dot(velocity, trans.forward);

        _liftForce = Vector3.up * forwardSpeed * forwardSpeed * liftForceCoefficient;
        _thrustForce = Vector3.forward * power * fullThrustForce;

        var sqrVelocity = velocity.sqrMagnitude;
        _dragAbsoluteForce = -velocity.normalized * sqrVelocity * dragCoefficient;

        _rigidbody.AddRelativeForce(_thrustForce + _thrustForce);
        _rigidbody.AddForce(_dragAbsoluteForce);
        
        if (_rigidbody.velocity.magnitude > maxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxVelocity;
        }
    }

    public void OnDrawGizmos()
    {
        var t = transform;
        var pos = t.position;
        var rot = t.rotation;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + rot * _liftForce);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + Physics.gravity);
        Gizmos.DrawLine(pos, pos + _dragAbsoluteForce);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, pos + rot * _thrustForce);

        if (_rigidbody)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pos, pos + _rigidbody.velocity);
        }
    }
}