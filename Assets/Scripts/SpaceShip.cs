using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour
{
    [Tooltip("Left wing point of force applying for rolling")]
    public Transform leftWing;

    [Tooltip("Right wing point of force applying for rolling")]
    public Transform rightWing;

    [Tooltip("Point of applying pitch and yaw force")]
    public Transform tailPoint;

    [Range(0, 1), Tooltip("Current power on engine")]
    public float power = 0;

    [Range(-1, 1), Tooltip("Current roll rotation power")]
    public float rollPower = 0;

    [Range(-1, 1), Tooltip("Current pitch rotation power")]
    public float pitchPower = 0;

    [Range(-1, 1), Tooltip("Current yaw rotation power")]
    public float yawPower = 0;

    [Tooltip("Force applying on wings for rolling.")]
    public float fullRollForce = 120f;

    [Tooltip("Force applying on tail for pitching.")]
    public float fullPitchForce = 30f;

    [Tooltip("Force applying on tail for yawing.")]
    public float fullYawForce = 30f;

    [Tooltip("Max speed of movement")]
    public float fullThrustForce = 50;

    public float liftForceCoefficient = 0.01f;

    public Vector3 dragCoefficient = Vector3.zero;
    public Vector3 angularDagCoefficient = Vector3.zero;

    public float forwardSpeed;

    public Vector3 liftForce;
    public Vector3 thrustForce;
    public Vector3 pitchForce;
    public Vector3 rollForce;
    public Vector3 yawForce;
    public Vector3 dragForce;

    private Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Vector3.zero;
    }

    public void FixedUpdate()
    {
        var trans = transform;
        var rot = trans.rotation;
        var velocity = _rigidbody.velocity;
        var localUp = trans.up;

        pitchForce = localUp * fullPitchForce * pitchPower;
        rollForce = localUp * fullRollForce * rollPower;
        yawForce = -trans.right * fullYawForce * yawPower;

        _rigidbody.AddForceAtPosition(pitchForce + yawForce, tailPoint.position);
        _rigidbody.AddForceAtPosition(-rollForce, leftWing.position);
        _rigidbody.AddForceAtPosition(rollForce, rightWing.position);

        forwardSpeed = Vector3.Dot(velocity, trans.forward);

        var relativeVelocity = Quaternion.Inverse(rot) * velocity;
        var angularVelocity = _rigidbody.angularVelocity;
        var sqrVelocity = relativeVelocity.normalized * relativeVelocity.sqrMagnitude;
        var sqrAngularVelocity = angularVelocity.normalized * angularVelocity.sqrMagnitude;
        var rotatedAngularDragCoefficients = rot * angularDagCoefficient;

        dragForce = Vector3.Scale(sqrVelocity, dragCoefficient);
        liftForce = Vector3.up * forwardSpeed * forwardSpeed * liftForceCoefficient;
        thrustForce = Vector3.forward * power * fullThrustForce;

        _rigidbody.angularVelocity += Vector3.Scale(
            _rigidbody.angularVelocity,
            Vector3.Scale(sqrAngularVelocity, rotatedAngularDragCoefficients)
        );
        _rigidbody.AddRelativeForce(thrustForce + dragForce + liftForce);
    }

    public void OnDrawGizmos()
    {
        var t = transform;
        var pos = t.position;
        var rot = t.rotation;
        var lwPos = leftWing.position;
        var rwPos = rightWing.position;
        var tailPos = tailPoint.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + rot * liftForce);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + Physics.gravity);

        // drag axis
        Gizmos.DrawLine(pos, pos + t.right * dragForce.x);
        Gizmos.DrawLine(pos, pos + t.up * dragForce.y);
        Gizmos.DrawLine(pos, pos + t.forward * dragForce.z);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(pos, pos + rot * dragForce);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, pos + rot * thrustForce);
        Gizmos.DrawLine(lwPos, lwPos - rollForce);
        Gizmos.DrawLine(rwPos, rwPos + rollForce);
        Gizmos.DrawLine(tailPos, tailPos + pitchForce);

        if (_rigidbody)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pos, pos + _rigidbody.velocity);
        }
    }
}