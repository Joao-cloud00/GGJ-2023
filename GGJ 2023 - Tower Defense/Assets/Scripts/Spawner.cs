using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    private int enemyCount;
    [SerializeField] private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [SerializeField] private Waypoint waypointTeste;

    public List<Waves> wave;
    private float _spawnTimer;
    private int _enemiesSpawned, objetoSpawn; //objetoSpawn precisa ser zerado 
                                                    //ao inicio de cada wave[]

    //private ObjectPooler _pooler;

    private void Start()
    {
        enemyCount = wave[LevelManager.instance.wave].enemies.Count;
        //_pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        Debug.Log(wave[LevelManager.instance.wave].enemies.Count);
        Debug.Log(wave[LevelManager.instance.wave]);
        if(LevelManager.instance.inGame)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0)
            {
                _spawnTimer = delayBtwSpawns;
                if (_enemiesSpawned < enemyCount)
                {
                    _enemiesSpawned++;
                    SpawnEnemy();
                }
            }
        }

    }


    private void SpawnEnemy()
    {

        GameObject newInstance = Instantiate(wave[LevelManager.instance.wave].enemies[objetoSpawn]);
        //GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.GetComponent<Enemy>().nome = waypointTeste;
        //newInstance.SetActive(true);
        //GameObject newInstance = ObjectPool.SharedInstance.GetPooledObject();
        if (newInstance != null)
        {    
            newInstance.transform.position = gameObject.transform.position;
            newInstance.transform.rotation = gameObject.transform.rotation;
            newInstance.SetActive(true); 
        }
        objetoSpawn++;
    }

}
