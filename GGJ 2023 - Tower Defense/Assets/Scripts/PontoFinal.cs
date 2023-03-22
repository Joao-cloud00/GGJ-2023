using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoFinal : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        Debug.Log("FIM DE ROTA!");
        // if(LevelManager.instance.inGame)
        // {
        //     Debug.Log("FIM DE ROTA!");
        //     LevelManager.instance.inGame = false;
        // }

    }
}
