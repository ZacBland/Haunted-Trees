using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform[] spawnSpots;
    public RoundController roundData;
    private float timeBtwSpawns;
    public float spawnDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay = roundData.spawnDelay;
        if(spawnDelay != roundData.spawnDelay){
        timeBtwSpawns = spawnDelay;
        }
        if(timeBtwSpawns <= 0){
            int randPos = Random.Range(0, spawnSpots.Length - 1);
            Instantiate(enemy, spawnSpots[randPos].position, Quaternion.identity);
            timeBtwSpawns = spawnDelay;
        } else {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
