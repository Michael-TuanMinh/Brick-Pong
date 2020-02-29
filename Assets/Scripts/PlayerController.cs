using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject brick;

    [Tooltip ("khoang cach giua 2 vien gach bang 1 phan bao nhieu chieu dai cua vien gach")]
    [SerializeField] float space;
    [SerializeField] GameObject text;

    private int row = 4;
    private int col = 12;
    public int lives = 100;
    [HideInInspector] public bool isAtLeftBorder = false;
    [HideInInspector] public bool isAtRightBorder = false;

    private int[,] grid = {
        { 0,0,1,1,1,3,3,1,1,1,0,0},
        { 0,1,1,1,1,3,3,1,1,1,1,0},
        { 1,1,1,1,1,3,3,1,1,1,1,1},
        { 0,4,4,4,0,0,0,0,4,4,4,0}
        };

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
        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.position = new Vector2(worldScreenWidth / 3.5f, -2);  
    }
    

    private void Update()
    {
        if (lives == 0)
        {
            text.SetActive(true);
            Time.timeScale = 0;
        }

        if(isAtLeftBorder || isAtRightBorder)
        {
            foreach(Rigidbody2D r in GetComponentsInChildren<Rigidbody2D>())
            {
                r.velocity = Vector2.zero;
            }
        }
    }

}
