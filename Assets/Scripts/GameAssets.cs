using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance { get { return instance; } }

    public Sprite snakeHeadSprite;
    //public Sprite foodSprite;
    public List<SpriteRenderer> foodItems;
    public List<SpriteRenderer> powerUps;

    private void Awake()
    {
        instance = this;
    }
}
