using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed; 
    private Transform playerPos;
    private Player player;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject heart;
    public GameObject gem;
    public AudioClip sound;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip healthLossSound;
    private int lowSpeed = 6;
    private int highSpeed = 10;
    private int randSound;
    

    // Start is called before the first frame update
    void Start()
    {
        int randSpeed = (Random.Range(lowSpeed, highSpeed));
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
            SoundManager.PlaySound(healthLossSound);
            Debug.Log(player.health);
            Destroy(gameObject);
        }

        if(other.CompareTag("Projectile")){

            
            int randSound = (Random.Range(1,4));
            if(randSound == 1){
            SoundManager.PlaySound(sound);
            }
            if(randSound == 2){
            SoundManager.PlaySound(sound2);
            }
            if(randSound == 3){
            SoundManager.PlaySound(sound3);
            }
            Debug.Log(randSound);
            
            
            int posHeart = (Random.Range(1,10));
            if(posHeart == 3){
                Instantiate(heart,transform.position,Quaternion.identity);
                Debug.Log("Heart");
            }
            if(posHeart == 6){
                Instantiate(gem,transform.position,Quaternion.identity);
                Debug.Log("Gem");
            }
            if(player.magicArrow == false){
                Destroy(other.gameObject);
            }

            Destroy(gameObject);


        }
    }




}
