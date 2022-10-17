using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public int health = 5;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
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
}
