using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject breakEffect;

    private Rigidbody2D rb;
    private float deltaX, deltaY;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;
                case TouchPhase.Moved:

                    if(transform.parent.GetComponent<PlayerController>().isAtLeftBorder && touchPos.x - deltaX > transform.position.x) // move to the left
                    {
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }
                    else if (transform.parent.GetComponent<PlayerController>().isAtRightBorder && touchPos.x - deltaX < transform.position.x) // move to the right
                    {
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }

                    else if (!transform.parent.GetComponent<PlayerController>().isAtLeftBorder && !transform.parent.GetComponent<PlayerController>().isAtRightBorder)
                    {
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GameObject effect = Instantiate(breakEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f), Quaternion.identity);
            MainModule mainModule = effect.GetComponent<ParticleSystem>().main;
            mainModule.startColor = GetComponentInChildren<SpriteRenderer>().color;

            if(GetComponentInParent<PlayerController>() != null)
            {
                GetComponentInParent<PlayerController>().lives--;
            }
            else
            {
                GetComponentInParent<AI>().lives--;
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LeftBorder")
        {
            transform.parent.GetComponent<PlayerController>().isAtLeftBorder = true;
            
        }
        else if (collision.tag == "RightBorder")
        {
            transform.parent.GetComponent<PlayerController>().isAtRightBorder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LeftBorder")
        {
            transform.parent.GetComponent<PlayerController>().isAtLeftBorder = false;
        }
        else if (collision.tag == "RightBorder")
        {
            transform.parent.GetComponent<PlayerController>().isAtRightBorder = false;
        }
    }



}
