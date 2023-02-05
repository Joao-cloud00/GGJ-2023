using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public int enemyCount, spawnerZerado,objetoSpawn; //objetoSpawn precisa ser zerado //ao inicio de cada wave[]
    [SerializeField] private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    //[SerializeField] String Alo;

    [SerializeField] private Waypoint waypointTeste;

    bool spawnouTudo, testeBOOL;
    public List<Waves> wave;
    private float _spawnTimer;
    public int _enemiesSpawned;                                    

    //private ObjectPooler _pooler;
    public List<GameObject> _pool;

    private void Start()
    {
        objetoSpawn = 0;
        //enemyCount = wave[LevelManager.instance.wave].enemies.Count;
        //_pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        enemyCount = wave[LevelManager.instance.wave].enemies.Count;
        // if(_pool.Count == enemyCount && enemyCount!=0 )
        // {
        //   spawnouTudo = true;  
        //   testeBOOL = true;

        // }
        // if(_pool.Count == 0 && LevelManager.instance.inGame && testeBOOL)
        // {
        //     Debug.Log("rodou");
        //     //testeBOOL= false;
        // }

        
        //Debug.Log("_poolCount " + _pool.Count);
        //Debug.Log("enemyCount " +  enemyCount);
        //Debug.Log("spawnouTudo " +  spawnouTudo);
        //Debug.Log(wave[LevelManager.instance.wave].enemies.Count);
        //Debug.Log(wave[LevelManager.instance.wave]);
        
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
        CheckEnemies();
        

    }


    private void SpawnEnemy()
    {
        GameObject newInstance = Instantiate(wave[LevelManager.instance.wave].enemies[objetoSpawn]);
        _pool.Add(newInstance);
        //GameObject _pool = _pooler.GetInstanceFromPool();
        _pool[objetoSpawn].GetComponent<Enemy>().nome = waypointTeste;
        //_pool.SetActive(true);
        //GameObject _pool = ObjectPool.SharedInstance.GetPooledObject();
        if (_pool != null)
        {    
            _pool[objetoSpawn].transform.position = gameObject.transform.position;
            _pool[objetoSpawn].transform.rotation = gameObject.transform.rotation;
            //_pool[objetoSpawn].SetActive(true); 
        }
        objetoSpawn++;
    }

    private void CheckEnemies()
    {
        
        for(int i = 0; i <= _pool.Count; i++)
        {
            if(!_pool[i].GetComponent<Enemy>().vivo && _pool[i].activeSelf )
            {

                //Destroy(_pool[i]);
                LevelManager.instance.numeroInimigos --; 
                _pool[i].SetActive(false);
                // if(_pool.Count == enemyCount)
                // {
                //   spawnouTudo = true;  

                // }
                // if(spawnouTudo)
                // {
                
                //     Destroy(_pool[i]);
                //     _pool.RemoveAt(i);
                //     LevelManager.instance.numeroInimigos --;                   
                // }

            }
            // if(_pool.Count == enemyCount)
            // {
            //   spawnouTudo = true;  

            // }

            if(i>=_pool.Count)
            {
                i=0;
            }

        }
            // if(_pool.Count < 1 && LevelManager.instance.inGame)
            // {
            //     Debug.Log("rodou");
            //     spawnouTudo = false;
            // }
        
    }
    

}
