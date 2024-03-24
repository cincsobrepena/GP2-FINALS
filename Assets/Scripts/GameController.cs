using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public List<GameObject> enemyPrefabList = new List<GameObject>();
    public float spawnDistance = 40f;
    public int minNumberOfEnemiesToSpawn;
    public int maxNumberOfEnemiesToSpawn;

    public float spawnEnemyInterval = 3f;
    private float timer = 0f;

    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.Play();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnEnemyInterval) {
            SpawnEnemies();
            timer = 0f; 
        }
    }
    private void SpawnEnemies()
    {
        int enemyPrefabIndex = Random.Range(0, enemyPrefabList.Count);
        int numberOfEnemiesToSpawn = Random.Range(minNumberOfEnemiesToSpawn, maxNumberOfEnemiesToSpawn);

        Debug.Log(numberOfEnemiesToSpawn);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 randomLocation = Random.insideUnitSphere.normalized;
            Vector3 spawnPosition = randomLocation * spawnDistance;
            spawnPosition = new Vector3(spawnPosition.x + spawnDistance, 1, spawnPosition.z + spawnDistance);
            Instantiate(enemyPrefabList[enemyPrefabIndex], spawnPosition, Quaternion.identity);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
