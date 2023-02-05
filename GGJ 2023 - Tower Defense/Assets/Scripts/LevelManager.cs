using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int wave = 0, numeroInimigos,tempoPiniciar,vidaMaxJogador,saldoJogador,saldoInit;
    static public LevelManager instance;
    public bool inGame; //indica que esta rolando uma wave de inimigos 
                                    //(ativado/desativado por botao ou tempo)
    float tempoFinal;
    [SerializeField] private GameObject botaoStart;
    [SerializeField] Spawner[] spawners;

    public Text txtVida;
    public Text txtDinheiro;


    private void Awake()
    {
        saldoJogador = saldoInit;
        if (instance == null)
        {
           instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Update() 
    {
        txtDinheiro.text = saldoJogador.ToString();
        txtVida.text = vidaMaxJogador.ToString();
        //Debug.Log(numeroInimigos);
        if(numeroInimigos==0 && inGame)
        {
            AumentarWave();
        }
        Vitoria();
        Morte();
    }

    public void TomouDano(int dano)
    {
        vidaMaxJogador-= dano;
        
    }

    void Morte()
    {
        if(vidaMaxJogador<=0)
        {
            Debug.Log("Morreu");
        }
    }


    void Vitoria()
    {

        if(wave >= spawners[0].wave.Count)
        {
            Debug.Log("Vitoria");
        }
    }

    public void ComecarWave() 
    {
        for(int i=0; i< spawners.Length; i++)
        {
            numeroInimigos += spawners[i].enemyCount;
        }
        inGame = true;
        botaoStart.SetActive(false);
    }

    void AumentarWave()
    {
        Enemy[] inimigos = Object.FindObjectsOfType<Enemy>(true);
        Debug.Log("inimigosLength "+inimigos.Length);

        // for(int i=0; i< spawners.Length; i++)
        // {
        //     spawners[i].objetoSpawn = 0;
        //     spawners[i]._pool.Clear();
        //     enemyCount = wave[LevelManager.instance.wave].enemies.Count;
        // }
        foreach(Spawner spwan in spawners)
        {
            spwan.objetoSpawn = 0;
            spwan._pool.Clear();
            spwan._enemiesSpawned = 0;
            //spwan.enemyCount = spwan.wave[wave].enemies.Count;
        }

        foreach(Enemy enemy in inimigos)
        {
            Debug.Log("foreach");
            Destroy(enemy.gameObject);
            
        } 
        tempoFinal=0;
        botaoStart.SetActive(true);
        inGame = false;
        wave ++ ;
    }

    void FixedUpdate()
    {
        ComecarAutomaticamente();
        
    }
    public void ComecarAutomaticamente()
    {
        if( wave!= 0)
        {
            tempoFinal += Time.deltaTime;
            if ((tempoFinal >= tempoPiniciar && inGame == false))
            {
                ComecarWave();
            }            
        }


    }
    public void AcabouWave()
    {
        //if(Enemy.FindGameObjectWithTag)
    }


}
