using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Snake snake;
    //[SerializeField] private float foodSpawnDuration;
    [SerializeField] private float consumableSpawnDelay;
    //[SerializeField] private float powerUpSpawnDelay;
    private LevelGrid levelGrid;

    private int gridX = 19;
    private int gridY = 10;   // grid extents in X and Y direction

    private Vector2Int consumableGridPosition;
    private GameObject consumableGameObject;  // storing the food game object to identify which food to be deleted after snake has eaten it
    private List<GameObject> totalConsumablesSpawned; // List to store the total consumables spawned on the game scene
    private float timer;
    private int maxConsumablesOnScreen = 4;  // max consumables allowed on the screen at once

    private void Start()
    {
        levelGrid = new LevelGrid(gridX, gridY); // Instantiating the levelGrid object
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
        levelGrid.GameHandlerSetup(this);
        totalConsumablesSpawned = new List<GameObject>();

        timer = consumableSpawnDelay;

        SpawnConsumable(); // spawn the food when game starts
    }

    private void SpawnConsumable()
    {
        // Getting the grid position to spawn and food item from the list (Mass Burner / Gainer)
        consumableGridPosition = levelGrid.GetSpawnPosition();
        consumableGameObject = levelGrid.GetConsumableToSpawn();
        totalConsumablesSpawned.Insert(0, consumableGameObject);

        if(totalConsumablesSpawned.Count <= maxConsumablesOnScreen)
        {
            consumableGameObject = Instantiate(levelGrid.GetConsumableToSpawn(), new Vector3(consumableGridPosition.x, consumableGridPosition.y), Quaternion.identity);
        }
    }


    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnConsumable();
            totalConsumablesSpawned.RemoveAt(totalConsumablesSpawned.Count - 1);
            timer = consumableSpawnDelay;
        }
    }
}
