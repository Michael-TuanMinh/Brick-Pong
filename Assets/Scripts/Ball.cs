using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed;
    [HideInInspector] public Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector3)direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Brick" || collision.gameObject.tag == "TopBottomBounds")
        {
            direction.y = -direction.y;
        }
        else if(collision.gameObject.tag == "LeftRightBounds")
        {
            direction.x = -direction.x;
        }
        
    }

 
}
