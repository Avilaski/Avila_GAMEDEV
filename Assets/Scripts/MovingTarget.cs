using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingTarget : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float knockbackImpulse = 3f;

    private Rigidbody body;
    private Vector3 startPosition;
    private float elapsed;
    private bool knockedDown;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        startPosition = body.position;

        body.isKinematic = true;
        body.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (knockedDown)
            return;

        elapsed += Time.fixedDeltaTime;

        float offset = Mathf.Sin(elapsed * speed) * distance;

        body.MovePosition(
            startPosition + Vector3.forward * offset
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (knockedDown)
            return;

        Rigidbody ball = collision.rigidbody;

        if (ball == null || !ball.CompareTag("Ball"))
            return;

        knockedDown = true;
        body.isKinematic = false;
        body.WakeUp();

        Vector3 direction = body.worldCenterOfMass - ball.worldCenterOfMass;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            direction = Vector3.forward;

        direction = (direction.normalized + Vector3.up * 0.3f).normalized;

        body.AddForceAtPosition(direction * knockbackImpulse, collision.GetContact(0).point, ForceMode.Impulse);
    }
}