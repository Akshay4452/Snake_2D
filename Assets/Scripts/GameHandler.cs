using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private LevelGrid levelGrid;
    [SerializeField] Snake snake;
    private void Start()
    {
        levelGrid = new LevelGrid(20, 11); // Instantiating the levelGrid object

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    
}
