using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject redorbPrefab;
    public GameObject blueorbPrefab;
    public GameObject greenorbPrefab;
   
    
    private List<Vector3> orbSpawnPositions = new List<Vector3>();
    private void Awake()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
    private void OnTriggerExit(Collider other)
    {
        groundSpawner.Spawn();
        Destroy(gameObject , 1f);
    }
    void Start()
    {
        
        ObstaclesSpawn(true);
        orbsSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void orbsSpawn()
    {
        int orbsToSpawn = Random.Range(1, 4); // Can spawn 1, 2, or 3 orbs

        // Special case for only one orb in a horizontal line
        if (orbsToSpawn == 1)
        {
            int obstacleSpawnrange = Random.Range(2, 5);
            Vector3 spawnPosition = GetRandomPointInCollider();

            // Check if the spawn position is already occupied by an orb
            if (!orbSpawnPositions.Contains(spawnPosition))
            {
                orbSpawnPositions.Add(spawnPosition);

                if (obstacleSpawnrange == 2)
                {
                    GameObject temp = Instantiate(redorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
                else if (obstacleSpawnrange == 3)
                {
                    GameObject temp = Instantiate(blueorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
                else if (obstacleSpawnrange == 4)
                {
                    GameObject temp = Instantiate(greenorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
            }
        }
        // For other cases (2 or 3 orbs), use your existing logic
        else
        {
            for (int i = 0; i < orbsToSpawn; i++)
            {
                int obstacleSpawnrange = Random.Range(2, 5);
                Vector3 spawnPosition = GetRandomPointInCollider();

                // Check if the spawn position is already occupied by an orb
                if (!orbSpawnPositions.Contains(spawnPosition))
                {
                    orbSpawnPositions.Add(spawnPosition);

                    if (obstacleSpawnrange == 2)
                    {
                        GameObject temp = Instantiate(redorbPrefab, transform);
                        temp.transform.position = spawnPosition;
                    }
                    else if (obstacleSpawnrange == 3)
                    {
                        GameObject temp = Instantiate(blueorbPrefab, transform);
                        temp.transform.position = spawnPosition;
                    }
                    else if (obstacleSpawnrange == 4)
                    {
                        GameObject temp = Instantiate(greenorbPrefab, transform);
                        temp.transform.position = spawnPosition;
                    }
                }
            }
        }
    }


    public void SpawnThreeOrbsInLine()
    {
        for (int i = 2; i < 5; i++)
        {
            Vector3 spawnPosition = transform.GetChild(i).position;
            spawnPosition.y = 1; // Adjust the y-coordinate as needed

            // Check if the spawn position is already occupied by an orb
            if (!orbSpawnPositions.Contains(spawnPosition))
            {
                orbSpawnPositions.Add(spawnPosition);

                if (i == 2)
                {
                    GameObject temp = Instantiate(redorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
                else if (i == 3)
                {
                    GameObject temp = Instantiate(blueorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
                else if (i == 4)
                {
                    GameObject temp = Instantiate(greenorbPrefab, transform);
                    temp.transform.position = spawnPosition;
                }
            }
        }
    }


    Vector3 GetRandomPointInCollider()
    {
        int maxAttempts = 10; // Maximum attempts to find a suitable position

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int obstacleIndex = Random.Range(2, 5); // Avoid obstacles at ends
            Transform spawnPoint = transform.GetChild(obstacleIndex).transform;

            Vector3 point = spawnPoint.position;
            point.y = 1; // Adjust the y-coordinate as needed

            // Check if there's an orb at the chosen point
            bool isOccupied = orbSpawnPositions.Exists(position => Vector3.Distance(position, point) < 0.5f);

            if (!isOccupied)
            {
                return point;
            }
        }

        // If we exceed maxAttempts, return a fallback point (you can adjust this)
        int obstacleIndex1 = Random.Range(2, 5);
        return transform.GetChild(obstacleIndex1).transform.position; // Fallback to middle point
    }
    public void ObstaclesSpawn(bool kk)
    {
        if (kk)
        {
            float spawnProb = Random.Range(0f, 1f);

            // Keep track of previous spawn points
            List<int> previousSpawnPoints = new List<int>();

            // Special case for only one obstacle in a horizontal line
            if (spawnProb < 0.2f)
            {
                int obstacleSpawnrange = Random.Range(2, 5);
                Transform spawnPoint = transform.GetChild(obstacleSpawnrange).transform;
                previousSpawnPoints.Add(obstacleSpawnrange); // Add spawn point to the list
                GameObject g = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));
                g.transform.SetParent(transform);
            }
            else
            {
                // For other cases, use your existing logic
                if (spawnProb < 0.8f)
                {
                    int obstacleSpawnrange = Random.Range(2, 5);
                    while (previousSpawnPoints.Contains(obstacleSpawnrange))
                    {
                        obstacleSpawnrange = Random.Range(2, 5);
                    }
                    Transform spawnPoint = transform.GetChild(obstacleSpawnrange).transform;
                    previousSpawnPoints.Add(obstacleSpawnrange); // Add spawn point to the list
                    GameObject g = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));
                    g.transform.SetParent(transform);
                }

                if (spawnProb < 0.3f)
                {
                    int obstacleSpawnrange = Random.Range(2, 5);
                    while (previousSpawnPoints.Contains(obstacleSpawnrange))
                    {
                        obstacleSpawnrange = Random.Range(2, 5);
                    }
                    Transform spawnPoint = transform.GetChild(obstacleSpawnrange).transform;
                    previousSpawnPoints.Add(obstacleSpawnrange); // Add spawn point to the list
                    GameObject g = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));
                    g.transform.SetParent(transform);
                }
            }
        }
    }




    public void HideObstaclesFor10Seconds(bool ss)
    {
        if (ss)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

            foreach (var obstacle in obstacles)
            {
                obstacle.SetActive(false);
            }

            // Schedule the method to unhide obstacles after 10 seconds
            Invoke("ShowObstacles", 5f);
        }
    }

    private void ShowObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (var obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }

        // After showing the obstacles, spawn new obstacles
        ObstaclesSpawn(true);
    }

}
