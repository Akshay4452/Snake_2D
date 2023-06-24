using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid
{
    //private Vector2Int foodGridPosition;
    //private SpriteRenderer foodGameObject;  // storing the food game object to identify which food to be deleted after snake has eaten it
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

    public Vector2Int GetLevelGridExtents()
    {
        // Method to return the grid extents of level grid so that we can confine the snake on the screen
        Vector2Int gridExtents = new Vector2Int(width, height);
        return gridExtents;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        // Spawning the food after snake reference is set up in LevelGrid so that in SpawnFood() method, we would get snakeGridPosition
        //SpawnFood();
    }

    //public void SpawnFood()
    //{
    //    foodGridPosition = GetSpawnPosition();
    //    foodGameObject = GetFoodItemToSpawn();

    //    // Spawn food at random position in the scene
    //    foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    //}

    public Vector2Int GetSpawnPosition()
    {
        Vector2Int position = new Vector2Int(Random.Range(-width, width), Random.Range(-height, height));
        return position;
    }

    public SpriteRenderer GetFoodItemToSpawn()
    {
        SpriteRenderer food = GameAssets.Instance.foodItems[Random.Range(0, GameAssets.Instance.foodItems.Count)];
        return food;
    }

    //public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    //{
    //    // Function to identify if the snake has eaten the food
    //    if(snakeGridPosition == foodGridPosition)
    //    {
    //        Object.Destroy(foodGameObject);
    //        SpawnFood();
    //        Debug.Log("Snake Ate the Food");
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
