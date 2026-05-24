using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs;

    public float spawnRate = 1.5f;
    public float minX = -2.5f;
    public float maxX = 2.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnFood), 1f, spawnRate);
    }

    void SpawnFood()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

        int randomIndex = Random.Range(0, foodPrefabs.Length);

        Instantiate(foodPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}