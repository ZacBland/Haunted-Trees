using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector2 target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, target) < 0.01f) {
            Destroy(gameObject);
        }
    }
}
