using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance { get { return instance; } }

    public Sprite snakeHeadSprite;

    private void Awake()
    {
        instance = this;
    }
}
