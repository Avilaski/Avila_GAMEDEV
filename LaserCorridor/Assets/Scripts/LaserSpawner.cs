using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] laserPrefabs;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject despawnPoint;
    [SerializeField] float spawnInterval = 3f;
    float spawnTimer;
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnLaserPattern();
            spawnTimer = 0f;
        }

    }

    void SpawnLaserPattern()
    {
        int randomIndex = Random.Range(0, laserPrefabs.Length);
        GameObject selectedPrefab = laserPrefabs[randomIndex];
        GameObject spawnedPattern = Instantiate(
            selectedPrefab,
            spawnPoint.transform.position,
            selectedPrefab.transform.rotation
        );

        LaserMovement movement = spawnedPattern.GetComponent<LaserMovement>();
        if (movement != null)
        {
            movement.SetDespawnPoint(despawnPoint);
            movement.setSpeed(Random.Range(movement.getSpeed() - 2f, movement.getSpeed() + 2f));  
        }


    }

    public void resetLasers()
    {
        LaserMovement[] lasers = FindObjectsByType<LaserMovement>();


        foreach (LaserMovement laser in lasers)
        {
            Destroy(laser.gameObject);
        }

        spawnTimer = 0f;
    }
}