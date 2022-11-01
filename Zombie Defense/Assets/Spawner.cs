using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject player;
    public RoundController roundData;
    public float spawnDistance;
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
            float angle = Random.Range(0, 360);

            float xdist = spawnDistance * Mathf.Cos(angle);
            float ydist = spawnDistance * Mathf.Sin(angle);

            Vector3 pos = player.transform.position + new Vector3(xdist, ydist, 0);

            Instantiate(enemy, pos, Quaternion.identity);
            timeBtwSpawns = spawnDelay;
        } else {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
