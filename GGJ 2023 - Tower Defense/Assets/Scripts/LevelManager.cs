using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int wave = 0;
    static public LevelManager instance;
    public bool inGame; //indica que esta rolando uma wave de inimigos 
                                    //(ativado/desativado por botao ou tempo)
    float tempoFinal;

    private void Awake()
    {
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

    public void ComecarWave() 
    {
        inGame = true;
    }
    void AumentarWave()
    {
        wave ++ ;
    }

    void FixedUpdate()
    {
        ComecarAutomaticamente();
        
    }
    public void ComecarAutomaticamente()
    {
        tempoFinal += Time.deltaTime;
        if ((tempoFinal >= 30))
        {
            inGame=true;
        }

    }


}
