using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI gemDisplay;
    public TextMeshProUGUI speedDisplay;
    public TextMeshProUGUI reloadDisplay;
    public TextMeshProUGUI magicArrowDisplay;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int health = 5;
    public int gems = 0;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float delayShootTime = 0.5f;
    public int powerUp = 0; //1(increased speed), 2(increased reload speed), 3 (trishot)
    public bool magicArrow = false;

    private Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "Health: " + health;
        gemDisplay.text = "Gems: " + gems;
        if(health > 0){
            ProcessInputs();   
        }
        animator.SetFloat("health", health);

        //play shooting animation
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_side") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_side"))
            {
                animator.Play("Attack_side", 0, 1f);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_back") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_back"))
            {
                animator.Play("Attack_back", 0, 1f);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_front") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_front"))
            {
                animator.Play("Attack_front", 0, 1f);
            }
        }

        if(health <= 0){
            SceneManager.LoadScene(0);
        }

        getPowerUp();
    }

    void FixedUpdate(){
        if(health > 0){
            Move();
        }
    }

    void ProcessInputs(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

    }

    void Move(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //set animation direction
        bool IsMovingHorizontally = false;
        bool IsMovingUp = false;
        bool IsMovingDown = false;

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {

            if (moveDirection.x > 0 || moveDirection.x < 0)
                IsMovingHorizontally = true;
            if (moveDirection.y > 0)
                IsMovingUp = true;
            if (moveDirection.y < 0)
                IsMovingDown = true;

            //Flip sprite if moving to the left
            if (moveDirection.x > 0)
                spriteRenderer.flipX = false;
            else if(moveDirection.x < 0)
                spriteRenderer.flipX = true;
        }
        animator.SetBool("IsMovingHorizontally", IsMovingHorizontally);
        animator.SetBool("IsMovingUp", IsMovingUp);
        animator.SetBool("IsMovingDown", IsMovingDown);
        
    


        // Vector2 aimDirection = mousePosition - rb.position;
        //float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = aimAngle;
    }

    void getPowerUp(){
        if(gems >= 3){
            gems = 0;
            powerUp += 1;
            if(powerUp == 1){
                moveSpeed += 0.7f;
                speedDisplay.text = "SPEEDY BOOTS";
            }
            if(powerUp == 2){
                delayShootTime = 0.15f;
                reloadDisplay.text = "SPEEDY RELOAD";
            }
            if(powerUp >= 3){
                magicArrow = true;
                magicArrowDisplay.text = "MAGIC ARROWS";
            }
        }
    }
}
