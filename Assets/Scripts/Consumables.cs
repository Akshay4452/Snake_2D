using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ConsumableType
{
    MassGainer,
    MassBurner,
    Shield,
    ScoreBooster,
    SpeedBooster,
} 

public class Consumables : MonoBehaviour
{
    private float spawnDuration;
    private float timer;
    [field: SerializeField] public ConsumableType consumableType { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        spawnDuration = 5f;  // food will be present for 5 seconds, after that if not eaten would be destroyed
        timer = spawnDuration;
    }

    public ConsumableType GetConsumableType()
    {
        return consumableType;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            timer = spawnDuration;
        }
    }
}
