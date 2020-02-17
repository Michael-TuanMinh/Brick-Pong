using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
        }
    }
}
