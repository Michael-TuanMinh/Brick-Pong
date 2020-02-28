using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] Vector2 startPosition;
    [SerializeField] float speed = 5.0f;

    [Tooltip ("khoang cach giua 2 vien gach bang 1 phan bao nhieu chieu dai cua vien gach")]
    [SerializeField] float space;
    [SerializeField] GameObject text;

    private int row = 4;
    private int col = 12;
    public int lives = 100;

    private int[,] grid = {
        { 0,0,1,1,1,3,3,1,1,1,0,0},
        { 0,1,1,1,1,3,3,1,1,1,1,0},
        { 1,1,1,1,1,3,3,1,1,1,1,1},
        { 0,4,4,4,0,0,0,0,4,4,4,0}
        };


    private Vector3 touchPosition;
    private Rigidbody2D myRigidbody;
    private Vector3 direction; // caculate direction base on touch
    

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       
        lives = row * col;

        for (int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                if(grid[i,j] != 0)
                {
                    GameObject temp = Instantiate(brick, this.transform);
                    
                    temp.transform.localPosition = new Vector2(-j * temp.transform.localScale.x / space, -i * temp.transform.localScale.x / space);
                }
                else
                {
                    lives--;
                }
            }
        }

        this.transform.position = startPosition;
           
    }


    private void Update()
    {
        InputListener();
        if (lives == 0)
        {
            text.SetActive(true);
            Time.timeScale = 0;
        }
    }


    private void InputListener()
    {
#if UNITY_EDITOR
        //float mousePosition = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 1.8f, 3.1f);
        float mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        this.transform.position = new Vector3(mousePosition, this.transform.position.y, 0);


#endif

#if UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            myRigidbody.velocity = new Vector2(direction.x, 0) * speed;

            if (touch.phase == TouchPhase.Ended)
                myRigidbody.velocity = Vector2.zero;
        }
#endif
    }
}
