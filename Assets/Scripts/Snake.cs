using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer; // Time until the next movement
    private LevelGrid LevelGrid; // holding the reference to the LevelGrid script
    [SerializeField] private float gridMoveTimerMax; // Time set between two movement instances

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(1, 0); // by default snake will move towards right
        gridPosition = new Vector2Int(0,0);
        //gridMoveTimerMax = 1f;  // setting gridMoveTimer equals to 1 sec
        gridMoveTimer = gridMoveTimerMax;
    }

    public void Setup(LevelGrid levelGrid)
    {
        this.LevelGrid = levelGrid;
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
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

            LevelGrid.SnakeMoved(gridPosition);
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
        return gridPosition;
    }
}
