using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    Vector3 nextspawnPoint;

    public void Spawn()
    {
        GameObject tempGround = Instantiate(groundPrefab , nextspawnPoint , Quaternion.identity);
        nextspawnPoint = tempGround.transform.GetChild(1).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
