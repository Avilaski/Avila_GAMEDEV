using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Camera aimCamera;
    [SerializeField] private Rigidbody ballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform aimPlane;
    [SerializeField] private LayerMask aimLayers;

    [SerializeField] private float flightTime = 0.8f;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private float ballLifetime = 8f;

    private float nextShotTime;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        if (Time.time < nextShotTime)
            return;

        Vector2 mousePosition =
            Mouse.current.position.ReadValue();

        Ray ray = aimCamera.ScreenPointToRay(mousePosition);
        Vector3 aimPoint;

        // Aim at the actual depth of a target or shelf.
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayers, QueryTriggerInteraction.Ignore)) {
            aimPoint = hit.point;
        } else {
            Plane plane = new Plane(aimCamera.transform.forward, aimPlane.position);

            if (!plane.Raycast(ray, out float distance))
                return;

            aimPoint = ray.GetPoint(distance);
        }

        Rigidbody ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        float duration = Mathf.Max(flightTime, 0.1f);

        Vector3 launchVelocity = (aimPoint - spawnPoint.position) / duration - 0.5f * Physics.gravity * duration;

        ball.AddForce( launchVelocity, ForceMode.VelocityChange);

        Destroy(ball.gameObject, ballLifetime);
        nextShotTime = Time.time + cooldown;
    }
}