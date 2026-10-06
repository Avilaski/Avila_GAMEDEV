using UnityEngine;

public class RotatingLight : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 150f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    [SerializeField] private float minDelay = 0f;
    [SerializeField] private float maxDelay = 0f;

    private bool canRotate = false;

    void Start()
    {
        float randomDelay = Random.Range(minDelay, maxDelay);
        Invoke("StartRotation", randomDelay);
    }

    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(rotationAxis * spinSpeed * Time.deltaTime, Space.Self);
        }
    }

    void StartRotation()
    {
        canRotate = true;
    }
}
