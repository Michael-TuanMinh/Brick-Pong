using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float startSpeed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(10, startSpeed));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Brick")
        {
            Debug.Log(collision.transform.position.x);
            float difference = collision.transform.position.x - collision.contacts[0].point.x;

            
           if (difference < collision.transform.position.x)
            {
                rb.AddForce(new Vector2(-Mathf.Abs(difference * 200), 0));
            }
            else
                rb.AddForce(new Vector2(Mathf.Abs(difference * 200), 0));
        }

  
    }
}
