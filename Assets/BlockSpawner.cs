using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;

    public float spawnSpeed = 0.5f;

    public float minX = -8f;
    public float maxX = 8f;

    public GameObject normalBlockPrefab;
    public GameObject powerUpBlockPrefab;
    [Range(0,100)]
    public float powerUpChance = 5f;
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnBlock();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }
    

    private void SpawnBlock()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);

        float randomAngle = Random.Range(-10f, 10f);
        Quaternion spawnRot = Quaternion.Euler(0f, 0f, randomAngle);

        float roll = Random.Range(0f, 100f);

GameObject prefabToSpawn;

if (roll < powerUpChance)
{
    prefabToSpawn = powerUpBlockPrefab;
}
else
{
    prefabToSpawn = normalBlockPrefab;
}
Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

    }
}
