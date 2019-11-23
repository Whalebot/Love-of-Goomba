using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Transform spawnLocation;
    public float spawnDelay;
    float lastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnDelay + lastSpawn)
        {
            lastSpawn = Time.time;
            Spawn();
        }
    }

    void Spawn() {
        GameObject tempObject = Instantiate(spawnPrefab, spawnLocation.position, transform.rotation);
        tempObject.GetComponent<AI>().Activate();
    }
}
