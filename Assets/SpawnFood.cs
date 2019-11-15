using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject Food;
    float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 5f;
        InvokeRepeating("spawning", SnakeHead.gamespeed, spawnTime);
    }

    void spawning()
    {
        float posX = Random.Range(-34, 34);
        float posY = Random.Range(-24, 24);
        Vector2 pos = new Vector2(posX, posY);

        Instantiate(Food, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
