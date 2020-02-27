using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] Vector2 startPosition;
    [SerializeField] float speed = 5.0f;
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


    private void Start()
    {
        lives = row * col;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (grid[i, j] != 0)
                {
                    GameObject temp = Instantiate(cube, this.transform);
                    temp.transform.localPosition = new Vector2(-j * space, -i * space);
                    /*if (grid[i, j] == 1) temp.GetComponent<Renderer>().material.color = Color.cyan;
                    else if (grid[i, j] == 3) temp.GetComponent<Renderer>().material.color = Color.blue;
                    else if (grid[i, j] == 4) temp.GetComponent<Renderer>().material.color = Color.red;*/
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
        if (lives == 0)
        {
            text.SetActive(true);
            Time.timeScale = 0;
        }
    }



}
