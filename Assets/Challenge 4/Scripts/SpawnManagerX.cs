using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    EnemyX enemySpeed;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;


    public GameObject player;

    void Start()
    {
        //enemySpeed = GameObject.Find("Enemy").GetComponent<EnemyX>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        enemyCount = FindObjectsOfType<EnemyX>().Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
            
           // waveCount++;
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < waveCount; i++)
        {
            enemySpeed = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation).GetComponent<EnemyX>();
            enemySpeed.speed = waveCount*10;
        }

        waveCount++;
        //enemySpeed = GameObject.FindObjectsOfType("Enemy(Clone)").GetComponent<EnemyX>();
        //enemySpeed.speed+=100;
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
