using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private LevelGrid levelGrid;
    private int gridX = 20;
    private int gridY = 11;   // grid extents in X and Y direction
    [SerializeField] Snake snake;
    private void Start()
    {
        levelGrid = new LevelGrid(gridX, gridY); // Instantiating the levelGrid object

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    
}
