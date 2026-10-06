using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}