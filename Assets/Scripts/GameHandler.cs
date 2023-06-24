using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Snake snake;
    //[SerializeField] private float foodSpawnDuration;
    [SerializeField] private float foodSpawnDelay;
    private LevelGrid levelGrid;

    private int gridX = 19;
    private int gridY = 10;   // grid extents in X and Y direction

    private Vector2Int foodGridPosition;
    private SpriteRenderer foodGameObject;  // storing the food game object to identify which food to be deleted after snake has eaten it
    private List<SpriteRenderer> totalFoodSpawned; // List to store the total food items spawned on the game scene
    private float timer;
    private int maxFoodItemsOnScreen = 4;  // max food items allowed on the screen at once

    private void Start()
    {
        levelGrid = new LevelGrid(gridX, gridY); // Instantiating the levelGrid object
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
        levelGrid.GameHandlerSetup(this);
        totalFoodSpawned = new List<SpriteRenderer>();

        timer = foodSpawnDelay;

        SpawnFood(); // spawn the food when game starts
    }

    private void SpawnFood()
    {
        // Getting the grid position to spawn and food item from the list (Mass Burner / Gainer)
        foodGridPosition = levelGrid.GetSpawnPosition();
        foodGameObject = levelGrid.GetFoodItemToSpawn();
        totalFoodSpawned.Insert(0, foodGameObject);

        if(totalFoodSpawned.Count <= maxFoodItemsOnScreen)
        {
            // Instantiate the Food Game Object
            foodGameObject = Instantiate(levelGrid.GetFoodItemToSpawn(), new Vector3(foodGridPosition.x, foodGridPosition.y), Quaternion.identity);
        }
        Debug.Log("Food items in the list: " + totalFoodSpawned.Count);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnFood();
            totalFoodSpawned.RemoveAt(totalFoodSpawned.Count - 1);
            timer = foodSpawnDelay;
        }
    }
}
