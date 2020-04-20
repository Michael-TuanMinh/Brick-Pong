using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed;
    [HideInInspector] public Vector2 direction;
    private Rigidbody2D rb;
    public int invert = 1; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        direction = Vector2.one.normalized;
    }

    private void Update()
    {
        if(!rb.simulated)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 1)
            rb.simulated = true;
        }
       
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector3)direction * speed * invert;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spring")
        {
            if(Mathf.Abs(direction.y) * 1.2f < Mathf.Abs(speed) * 2) direction.y *= -1.2f;
            else direction.y = -direction.y;
        }
    }


}
