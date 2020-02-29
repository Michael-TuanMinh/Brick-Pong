using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject breakEffect;

    private Rigidbody2D rb;
    private float deltaX, deltaY;
    private bool isDraging;
    private Vector2 touchPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.parent.GetComponent<PlayerController>())
        {
#if UNITY_EDITOR
            InputMouseDrag();
#else
            InputTouchDrag();
#endif
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
           if(transform.parent.GetComponent<PlayerController>()) transform.parent.GetComponent<PlayerController>().isAtLeftBorder = true;
            
        }
        else if (collision.tag == "RightBorder")
        {
            if (transform.parent.GetComponent<PlayerController>()) transform.parent.GetComponent<PlayerController>().isAtRightBorder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LeftBorder")
        {
            if (transform.parent.GetComponent<PlayerController>()) transform.parent.GetComponent<PlayerController>().isAtLeftBorder = false;
        }
        else if (collision.tag == "RightBorder")
        {
            if (transform.parent.GetComponent<PlayerController>()) transform.parent.GetComponent<PlayerController>().isAtRightBorder = false;
        }
    }

    private void InputMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            deltaX = touchPos.x - transform.position.x;
            deltaY = touchPos.y - transform.position.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
        }

        if (isDraging)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (transform.parent.GetComponent<PlayerController>().isAtLeftBorder && touchPos.x - deltaX > transform.position.x) // move to the left
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

        }
    }

    private void InputTouchDrag()
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

                    if (transform.parent.GetComponent<PlayerController>().isAtLeftBorder && touchPos.x - deltaX > transform.position.x) // move to the left
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

}
