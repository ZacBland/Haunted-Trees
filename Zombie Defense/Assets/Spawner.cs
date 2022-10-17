using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform[] spawnSpots;
    private float timeBtwSpawns;
    private float startTimeBtwSpawns = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawns <= 0){
            int randPos = Random.Range(0, spawnSpots.Length - 1);
            Instantiate(enemy, spawnSpots[randPos].position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        } else {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
