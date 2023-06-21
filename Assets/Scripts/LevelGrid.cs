using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;  // storing the food game object to identify which food to be deleted after snake has eaten it
    private int width;
    private int height;
    private float foodSpawnTime;
    private Snake snake;  // Holding the reference to the snake 

    // Constructor to initialize the width and height of the level grid
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        // Spawning the food after snake reference is set up in LevelGrid so that in SpawnFood() method, we would get snakeGridPosition
        SpawnFood();
    }

    public void SpawnFood()
    {
        // Using do-while loop to avoid next food object spawn right on top of the snake
        do {
            foodGridPosition = new Vector2Int(Random.Range(-width, width), Random.Range(-height, height));
        } while (snake.GetGridPosition() == foodGridPosition);
        

        // Spawn the food gameobject of type spriterenderer
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite; // Assign food sprite from GameAsset

        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

    }

    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        // Function to identify if the snake has eaten the food
        if(snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Debug.Log("Snake Ate the Food");
        }
    }
}
