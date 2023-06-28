using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;  // Snake's position in the grid
    private float gridMoveTimer; // Time until the next movement
    private LevelGrid levelGrid; // holding the reference to the LevelGrid script
    [SerializeField] private float gridMoveTimerMax; // Time set between two movement instances

    private List<Transform> snakeSegmentsTransformList; // creating list of Transform component to store the snake segment transform
    [SerializeField] private Transform segmentPrefab;

    private int snakeBodySize; // variable to store the length of snake
    //private List<Vector2Int> snakeMovePositionList; // List to store location of all parts of the snake

    [SerializeField] private ScoreHandler scoreHandler; // Reference of ScoreHandler script

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(1, 0); // by default snake will move towards right
        gridPosition = new Vector2Int(0,0);
        //gridMoveTimerMax = 1f;  // setting gridMoveTimer equals to 1 sec
        gridMoveTimer = gridMoveTimerMax;

        snakeSegmentsTransformList = new List<Transform>(); // Initialize the snake segments list
        snakeSegmentsTransformList.Add(this.transform); // Adding the snake head to the list of segments

        snakeBodySize = 1;   // Initially there will be only snake head
        //snakeMovePositionList = new List<Vector2Int>(); // initialize the list
    }

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.y = +1;
                gridMoveDirection.x = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.y = -1;
                gridMoveDirection.x = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = +1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = -1;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            // Move the snake segments from its end towards snake head.. It simulates the forward motion of snake
            if(snakeBodySize != 1)
            {
                for (int i = snakeSegmentsTransformList.Count - 1; i > 0; i--)
                {
                    snakeSegmentsTransformList[i].position = snakeSegmentsTransformList[i - 1].position;
                }
            }

            gridPosition += gridMoveDirection;

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

            // Enable the BoxCollider2D of snake segment as snake has moved by 1 unit
            snakeSegmentsTransformList[snakeBodySize-1].GetComponent<BoxCollider2D>().enabled = true;

            KeepSnakeOnScreen();
        }  
    }

    private void KeepSnakeOnScreen()
    {
        // Screen Wrapping Function
        if(Mathf.Abs(gridPosition.x) > levelGrid.GetLevelGridExtents().x + 1f)
        {
            gridPosition.x *= -1;
        } else if(Mathf.Abs(gridPosition.y) > levelGrid.GetLevelGridExtents().y + 1f)
        {
            gridPosition.y *= -1;
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // returns the value in degrees
        if(n < 0)
        {
            n += 360;
        }
        return n - 90;
    }

    public Vector2Int GetGridPosition()
    {
        // returns the current position of snake
        return gridPosition;
    }

    //public List<Vector3> GetFullSnakeGridPositionList()
    //{
    //    List<Vector3> snakeGridPositionList = new List<Vector3>();
    //    foreach (Transform t in snakeSegmentsTransformList)
    //    {
    //        snakeGridPositionList.Add(t.position);
    //    }
    //    return snakeGridPositionList;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MassGainer"))
        {
            SnakeGrow(); // Grow the snake after it eats the food
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("MassBurner"))
        {
            SnakeShrink();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SnakeBody"))
        {
            SceneManager.LoadScene("GameOverScene");
        }  
    }

    private void SnakeGrow()
    {
        Transform _segment = Instantiate(segmentPrefab);
        _segment.GetComponent<BoxCollider2D>().enabled = false;
        _segment.position = snakeSegmentsTransformList[snakeSegmentsTransformList.Count - 1].position; // setting the position of snake segment as end of snake
        snakeSegmentsTransformList.Add( _segment );
        snakeBodySize++;  // increase the snake body size count by 1
        scoreHandler.ScoreIncrease(); // UPdate the score as snake grows
    }

    private void SnakeShrink()
    {
        // Remove the last 2 segment of the snake and destroy the game objects
        // decrease the snake size -> if snake size <= 0 -> Game Over
        snakeBodySize -= 2;
        if (snakeBodySize > 0)
        {
            List<Transform> segmentsToRemove = snakeSegmentsTransformList.GetRange(snakeSegmentsTransformList.Count - 2, 2);
            foreach(Transform segmentBody in segmentsToRemove)
            {
                Destroy(segmentBody.gameObject);
            }
            // Need to remove the segment transforms from the list
            snakeSegmentsTransformList.RemoveRange(snakeSegmentsTransformList.Count - 2, 2);
            scoreHandler.ScoreDecrease();
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public int GetSnakeSize()
    {
        return snakeBodySize;
    }
}
