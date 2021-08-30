using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawns enemies every x amount of seconds.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnDelay;

    bool canSpawn;
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnEnemy");
        }
    }
    IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
