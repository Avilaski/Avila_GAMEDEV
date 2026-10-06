using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float moveSpeed = 13f;
    [SerializeField] GameObject despawnPoint;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, despawnPoint.transform.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(this.transform.position, despawnPoint.transform.position) < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDespawnPoint(GameObject point)
    {
        despawnPoint = point;
    }

    public float getSpeed()
    {
        return moveSpeed;
    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }

}