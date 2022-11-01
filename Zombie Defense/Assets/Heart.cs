using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private float despawnTime = 7f;
    private Player player;
    public AudioClip healthPickup;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        if(despawnTime <= 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            SoundManager.PlaySound(healthPickup);
            player.health++;
            Destroy(gameObject);
        }
    }
}
