using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyAPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private Player _player;

    private bool _stopSpawning = false;
    private int _enemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player.onThresholdReached += HandleThresholdReached; 
             
    }

    // Update is called once per frame
    void Update()
    {

    }
    //spawn objects every 5 seconds
    //create a coroutine of time IEnumerator -- Yield events
    //while loop
    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
       
        StartCoroutine(TripleShotPowerupRoutine());
    }
    void HandleThresholdReached(object Sender, EventArgs args)
    {
        //StartCoroutine(SpawnRoutineEnemyA());
        SpawnGala();
    }

    IEnumerator SpawnRoutine()
    {
        //while loop (infinite) 
        //instantiate an object - enemy prefab
        //yield wait for 5 seconds

        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 8.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
        //we never get here because of the while loop, otherwise it reads every line
    }
    /*IEnumerator SpawnRoutineEnemyA()
    {
        //while loop (infinite) 
        //instantiate an object - enemy prefab
        //yield wait for 5 seconds

        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8.5f, 0);
            GameObject newEnemy = Instantiate(_enemyAPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(10.0f);
        }
        //we never get here because of the while loop, otherwise it reads every line
    }*/

    public void SpawnGala()
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 8.5f, 0);
        GameObject newEnemy = Instantiate(_enemyAPrefab, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    IEnumerator TripleShotPowerupRoutine()
    {
        //while loop (infinite) 
        //instantiate an object - enemy prefab
        //yield wait for 5 seconds
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 8.5f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        //we never get here because of the while loop, otherwise it reads every line
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

   
}
