using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private LevelGrid levelGrid;
    private void Start()
    {
        levelGrid = new LevelGrid(40, 22);
    }

    
}
