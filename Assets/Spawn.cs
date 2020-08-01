using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] Rocket rocket;
    [SerializeField] GameObject shop;
    float minSpawnDelay = 0;
    float maxSpawnDelay = 4;
    int minSize = 1;
    int maxSize = 2;
    float spawnPercent = 20f;
    int randomIndex;
    public float maxAxis = 0;
    public float minAxis = 0;
    private Vector2 screenBound;
    // Start is called before the first frame update
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnPercent = 20f;
        for (int index = 0; index < spawnObjects.Length; index++)
        {
            SpawnObject spawnObject;
            spawnObject = spawnObjects[index].GetComponent<SpawnObject>();
            spawnObject.weight = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        rocket = Rocket.Instance;
        spawnDelay -= Time.deltaTime;
        if (maxAxis < transform.position.x && transform.position.x > 0f)
        {
            maxAxis = transform.position.x;
        }
        if (minAxis > transform.position.x && transform.position.x < 0f)
        {
            minAxis = transform.position.x;
        }

        if (spawnDelay <= 0.0f)
        {
            SpawnObject();
            SpawnShop();
        }
    }

    private void LevelSetUp()
    {
        int levelupScore = 15;
        if (rocket.score >= levelupScore)
        {
            levelupScore += 15;
            maxSpawnDelay -= 1;
            spawnPercent -= 2f;
            maxSize += 1;
            if (spawnPercent <= 0.1f)
            {
                spawnPercent = 0.1f;
            }
            if (maxSpawnDelay <= 1f)
            {
                maxSpawnDelay = 1f;
            }
            if (maxSize >= 20)
            {
                maxSize = 20;
            }
            for (int index = 0; index < spawnObjects.Length; index++)
            {
                SpawnObject spawnObject;
                spawnObject = spawnObjects[index].GetComponent<SpawnObject>();
                spawnObject.weight += 2;
            }
        }
    }
    private void SpawnShop()
    {
        if (ChancePercent(spawnPercent) == true)
        {
            GameObject shopSpawn = Instantiate(shop) as GameObject;
            shopSpawn.transform.position = GetRandomPosition();
        }
    }
    private bool ChancePercent(float percent) // percent from 0f - 100f.
    {
        float chancePercent = percent / 100f;
        if (Random.value <= chancePercent)
        {
            return true;
        }
        return false;
    }

    private void SpawnObject()
    {
        GetRandomObject();
        LevelSetUp();
        if (spawnObjects != null)
        {
            GameObject spawn = Instantiate(spawnObjects[randomIndex]) as GameObject;
            spawn.transform.position = GetRandomPosition();
            spawn.transform.localScale = GetRandomSize();
        }
        else
        {
            print("Did you for get to input spawn object?");
        }
        spawnDelay = Random.Range(minSpawnDelay,maxSpawnDelay);
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(GetRandomValue(minAxis, maxAxis), transform.position.y, transform.position.z);
        return randomPosition;
    }

    private Vector3 GetRandomSize()
    {
        float randomSizeNumber = GetRandomValue(minSize, maxSize);
        randomSizeNumber *= 100; //have to multiply becuz the rock prefab too small.
        Vector3 randomSize = new Vector3(randomSizeNumber, randomSizeNumber, randomSizeNumber);
        return randomSize;
    }

    private float GetRandomValue(float minValue, float maxValue)
    {
        float randomValue = Random.Range(minValue, maxValue);
        return randomValue;
    }

    private void GetRandomObject()
    {
        randomIndex = Random.Range(0, spawnObjects.Length - 1);
    }


}
