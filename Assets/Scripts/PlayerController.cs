using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] Vector2 startPosition;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float space;

    private int row = 4;
    private int col = 12;

    private int[,] grid = {
        { 0,0,1,1,1,3,3,1,1,1,0,0},
        { 0,1,1,1,1,3,3,1,1,1,1,0},
        { 1,1,1,1,1,3,3,1,1,1,1,1},
        { 0,4,4,4,0,0,0,0,4,4,4,0}
        };


    private Vector3 touchPosition;
    private Rigidbody myRigidbody;
    private Vector3 direction; // caculate direction base on touch

    

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        for (int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                if(grid[i,j] != 0)
                {
                    GameObject temp = Instantiate(cube, this.transform);
                    temp.transform.localPosition = new Vector2(-j * space ,-i * space);
                    /*if (grid[i, j] == 1) temp.GetComponent<Renderer>().material.color = Color.cyan;
                    else if (grid[i, j] == 3) temp.GetComponent<Renderer>().material.color = Color.blue;
                    else if (grid[i, j] == 4) temp.GetComponent<Renderer>().material.color = Color.red;*/
                }
            }
        }

        //this.transform.position = startPosition;
           
    }


    private void Update()
    {
        InputListener();
    }


    private void InputListener()
    {
        #region Mouse Input
       /* if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            myRigidbody.velocity = new Vector2(direction.x, 0) * speed;

            if (Input.GetMouseButtonUp(0))
                myRigidbody.velocity = Vector2.zero;
        }*/
        #endregion

        #region Mobile Input
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
        #endregion
    }
}
