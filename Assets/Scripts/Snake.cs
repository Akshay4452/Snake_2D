using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;  // Snake's position in the grid
    private float gridMoveTimer; // Time until the next movement
    private LevelGrid levelGrid; // holding the reference to the LevelGrid script
    [SerializeField] private float gridMoveTimerMax; // Time set between two movement instances

    private int snakeBodySize; // variable to store the length of snake
    private List<Vector2Int> snakeMovePositionList; // List to store location of all parts of the snake

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(1, 0); // by default snake will move towards right
        gridPosition = new Vector2Int(0,0);
        //gridMoveTimerMax = 1f;  // setting gridMoveTimer equals to 1 sec
        gridMoveTimer = gridMoveTimerMax;

        snakeBodySize = 0;   // Initially there will be only snake head will be present
        snakeMovePositionList = new List<Vector2Int>(); // initialize the list
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
            // Adding the current grid position of the snake to the snakeMovePositionList
            // snakeMovePositionList.Insert(0, gridPosition);

            gridPosition += gridMoveDirection;

            // Increase the body size of the snake once it has eaten the food
            // bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            //if (snakeAteFood)
            //{
            //    // snake has eaten the food, grow its body
            //    snakeBodySize++;
            //}

            //if (snakeMovePositionList.Count >= snakeBodySize + 1) 
            //{
            //    // remove the last element of the snakemovepositionlist
            //    snakeMovePositionList.RemoveAt(snakeMovePositionList.Count- 1);
            //}

            //// We create and destroy the sprites once we move from particular position
            //for(int i = 0; i < snakeMovePositionList.Count; i++)
            //{
            //    Vector2Int snakeMovePosition = snakeMovePositionList[i];
            //    World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * 0.5f, Color.red);
            //    FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
            //}

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

            KeepSnakeOnScreen();
        }  
    }

    private void KeepSnakeOnScreen()
    {
        if(Mathf.Abs(gridPosition.x) >= levelGrid.GetLevelGridExtents().x)
        {
            gridPosition.x *= -1;
        } else if(Mathf.Abs(gridPosition.y) >= levelGrid.GetLevelGridExtents().y)
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

    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MassGainer")
        {
            Debug.Log("Snake Ate Food");
        } else if(collision.tag == "MassBurner")
        {
            Debug.Log("Snake Got Burnt");
        }
    }
}
