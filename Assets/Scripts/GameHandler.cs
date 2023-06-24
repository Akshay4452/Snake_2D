using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Consumables consumables;
    [SerializeField] Snake snake;
    [SerializeField] private int foodSpawnDelay;
    private LevelGrid levelGrid;

    private int gridX = 19;
    private int gridY = 10;   // grid extents in X and Y direction

    private float foodSpawnDuration;
    private Vector2Int foodGridPosition;
    private SpriteRenderer foodGameObject;  // storing the food game object to identify which food to be deleted after snake has eaten it
    private List<SpriteRenderer> totalFoodSpawned; // List to store the total food items spawned on the game scene
    private float timer;
    private int maxFoodItemsOnScreen = 4;  // max food items allowed on the screen at once

    private void Start()
    {
        levelGrid = new LevelGrid(gridX, gridY); // Instantiating the levelGrid object
        foodSpawnDuration = consumables.spawnDuration;
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
        totalFoodSpawned= new List<SpriteRenderer>();

        timer = foodSpawnDuration;

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
    }

    private void Update()
    {
        
    }
}
