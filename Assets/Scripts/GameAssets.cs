using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance { get { return instance; } }

    public Sprite snakeHeadSprite;
    public List<GameObject> consumablesList;  // these all items have Consumables script attached to them

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
