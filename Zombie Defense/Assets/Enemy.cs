using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed; 
    private Transform playerPos;
    private Player player;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        int randSpeed = (Random.Range(5, 9));
        speed = ((float)randSpeed)/8;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        bool isMovingDown = false;
        bool isMovingUp = false;
        bool isMovingHorizontally = false;
        if(transform.position.x - playerPos.position.x > 0)
        {
            isMovingHorizontally = true;
            spriteRenderer.flipX = false;
        }
        else if(transform.position.x - playerPos.position.x < 0)
        {
            isMovingHorizontally = true;
            spriteRenderer.flipX = true;
        }

        if(transform.position.y - playerPos.position.y < 0)
        {
            isMovingUp = true;
        }
        else if(transform.position.y - playerPos.position.y > 0)
        {
            isMovingDown = true;
        }
        animator.SetBool("IsMovingDown", isMovingDown);
        animator.SetBool("IsMovingUp", isMovingUp);
        animator.SetBool("IsMovingHorizontally", isMovingHorizontally);


    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            player.health--;
            Debug.Log(player.health);
            Destroy(gameObject);
        }

        if(other.CompareTag("Projectile")){
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
