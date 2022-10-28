using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject shot;
    private Transform playerPos;
    private float delayShootTime = 0.5f;
    private float shootTime;
    void Start(){
        playerPos = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(shootTime <= 0){
        if(Input.GetMouseButtonDown(0)){
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(shot, playerPos.position, Quaternion.Euler(0f, 0f, angle - 90f));
            shootTime = delayShootTime;
        }
        }
        else{
            shootTime -= Time.deltaTime;
        }

    }
}
