using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Player
{
    Player1,
    Player2
}

public class Snake : MonoBehaviour
{
    [field: SerializeField] public Player playerID { get; private set; }
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;  // Snake's position in the grid
    private float gridMoveTimer; // Time until the next movement
    private LevelGrid levelGrid; // holding the reference to the LevelGrid script
    [SerializeField] private float gridMoveTimerMax; // Time set between two movement instances
    [SerializeField] private float powerCoolDownTime;

    private List<Transform> snakeSegmentsTransformList; // creating list of Transform component to store the snake segment transform
    [SerializeField] private Transform segmentPrefab;

    private int snakeBodySize; // variable to store the length of snake
    //private List<Vector2Int> snakeMovePositionList; // List to store location of all parts of the snake

    [SerializeField] private ScoreHandler scoreHandler; // Reference of ScoreHandler script
    //[SerializeField] private TextPopUpHandler textPopUpHandler;  // Reference to text pop up handler

    // Booleans to store if power up is collected
    private bool isSpeedBoosted;
    private bool isShieldActivated;
    private bool isScoreBoosted;

    private bool isSnakeAlive;

    private void Awake()
    {
        if(playerID == Player.Player1)
        {
            gridMoveDirection = new Vector2Int(1, 0); // by default snake will move towards right
            gridPosition = new Vector2Int(0, 0);
        }
        else
        {
            gridMoveDirection = new Vector2Int(-1, 0);  // Snake 2 will move towards right
            gridPosition = new Vector2Int(0, 2);  // its starting position would be 2 units up from Player1
        }

        gridMoveTimer = gridMoveTimerMax;

        snakeSegmentsTransformList = new List<Transform>(); // Initialize the snake segments list
        snakeSegmentsTransformList.Add(this.transform); // Adding the snake head to the list of segments

        snakeBodySize = 1;   // Initially there will be only snake head
        //snakeMovePositionList = new List<Vector2Int>(); // initialize the list

        // Initialize all the booleans related to power ups
        isSpeedBoosted= false;
        isShieldActivated=false;
        isScoreBoosted= false;

        isSnakeAlive = true;  // at start the snake is alive until its dead
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
        if(playerID == Player.Player1)
        {
            Player1Controller();
        }
        else
        {
            Player2Controller();
        }
        
    }

    private void Player2Controller()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.y = +1;
                gridMoveDirection.x = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.y = -1;
                gridMoveDirection.x = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = +1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = -1;
            }
        }
    }

    public void Player1Controller()
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
        ConsumableType _consumableType = collision.GetComponent<Consumables>().GetConsumableType();
        switch(_consumableType)
        {
            case ConsumableType.MassGainer:
                SnakeGrow(); // Grow the snake after it eats the food
                Destroy(collision.gameObject);
                break;
            case ConsumableType.MassBurner:
                if(!isShieldActivated)
                {
                    // Only shrink the snake when shield is not activated
                    SnakeShrink();
                    Destroy(collision.gameObject);
                } 
                else
                {
                    Debug.Log("Mass Burner can't hurt the snake right now!!");
                    Destroy(collision.gameObject);
                }
                break;
            case ConsumableType.SpeedBooster:
                if(!isSpeedBoosted)
                {
                    // Collect the Speed Booster only if its not already collected
                    StartCoroutine(PowerCoolDown(_consumableType, powerCoolDownTime));
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Speed Booster is Already Activated");
                    Destroy(collision.gameObject);
                }
                break;
            case ConsumableType.Shield:
                if (!isShieldActivated)
                {
                    // Collect the Shield only if its not already collected
                    StartCoroutine(PowerCoolDown(_consumableType, powerCoolDownTime));
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Shield is Already Activated");
                    Destroy(collision.gameObject);
                }
                break;
            case ConsumableType.ScoreBooster:
                if (!isScoreBoosted)
                {
                    // Collect the Score Booster only if its not already collected
                    StartCoroutine(PowerCoolDown(_consumableType, powerCoolDownTime));
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Score Booster is Already Activated");
                    Destroy(collision.gameObject);
                }
                break;
            default:
                break;
        }
    }

    IEnumerator PowerCoolDown(ConsumableType _con, float powerCoolDownTime)
    {
        switch (_con)
        {
            case ConsumableType.SpeedBooster:
                // make gridMoveTimerMax half of its original value
                gridMoveTimerMax /= 2;
                isSpeedBoosted = true;  // Activate the power up boolean
                yield return new WaitForSeconds(powerCoolDownTime);
                gridMoveTimerMax *= 2;
                isSpeedBoosted = false; // After waiting for cooldown period, reset the power up boolean
                break;
            case ConsumableType.Shield:
                Debug.Log("Snake Shield Activated");
                isShieldActivated = true;
                yield return new WaitForSeconds(powerCoolDownTime);
                Debug.Log("Shield Deactivated");
                isShieldActivated = false;
                break;
            case ConsumableType.ScoreBooster:
                scoreHandler.ScoreBooster();
                Debug.Log("Score Booster Activated");
                isScoreBoosted = true;
                yield return new WaitForSeconds(powerCoolDownTime);
                scoreHandler.DeactivateScoreBooster();
                Debug.Log("Score Booster Deactivated");
                isScoreBoosted = false;
                break;
            default: 
                break;
        }  
    }

    private void SnakeGrow()
    {
        Transform _segment = Instantiate(segmentPrefab);
        _segment.GetComponent<BoxCollider2D>().enabled = false;
        _segment.position = snakeSegmentsTransformList[snakeSegmentsTransformList.Count - 1].position; // setting the position of snake segment as end of snake
        snakeSegmentsTransformList.Add( _segment );
        snakeBodySize++;  // increase the snake body size count by 1
        scoreHandler.ScoreIncrease(playerID); // Update the score as snake grows
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
            scoreHandler.ScoreDecrease(playerID);
        }
        else
        {
            isSnakeAlive= false; 
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public int GetSnakeSize()
    {
        return snakeBodySize;
    }

    public bool isSnakeDead()
    {
        return !isSnakeAlive;
    }
}
