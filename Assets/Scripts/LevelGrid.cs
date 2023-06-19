using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid
{
    private Vector2Int foodGridPosition;
    private int width;
    private int height;
    private float foodSpawnTime;

    // Constructor to initialize the width and height of the level grid
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        SpawnFood();

        FunctionPeriodic.Create(SpawnFood, 1f);
    }

    public void SpawnFood()
    {
        foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        // Spawn the food gameobject of type spriterenderer
        GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.Instance.foodSprite; // Assign food sprite from GameAsset

        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);

    }
}
