using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    SpriteRenderer _spriteRenderer;
    Vector3 CurrentPointPosition, _lastPointPosition, posInit;
    public GameObject _posicaoInicial;
    [SerializeField] private int _currentWaypointIndex=0;
    
    [Header("Combate")]
    public Transform healthBar; //barra verde
    public GameObject healthBarObject; //Objeto pai das barras
    public float health,atckSpeed;
    public int MoveSpeed, danoTOMADO, dano, dinheiro;
    public bool vivo = true, podeatk;
    float tempoAtck;
    

    public Waypoint nome;
    private Spawner _spawner;

    private Vector3 healthBarScale; //tamanho da barra
    private float healthPercent; //Percentual de vida para calculo do tamanho da barra

    void Start()
    {
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spawner = GetComponent<Spawner>();

        // posInit = _posicaoInicial.transform.position;
        // transform.position = posInit;
        
    }

    void UpdateHealthbar()
    {
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
    }
    private void FixedUpdate() 
    {
        CausouDano();
    }

    void Update()
    {
        CurrentPointPosition = nome.GetWaypointPosition(_currentWaypointIndex);
        Move();
        Rotate();
        Morte();

        if(CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(danoTOMADO);
        }
        
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if(CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        Debug.Log("CurrentPointPositionReached ");
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if(distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.instance.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }
    private void EndPointReached()
    {
        podeatk = true;
        _currentWaypointIndex += 1;
        //OnEndReached?.Invoke(this);
        //_enemyHealth.ResetHealth();
        //ObjectPooler.ReturnToPool(gameObject);
        //Debug.Log(Waypoint.instance.Points.Length);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Inimigo toma dano");
        UpdateHealthbar();
    }
    void Morte()
    {
        if (health<=0)
        {
            LevelManager.instance.saldoJogador += dinheiro;
            vivo = false;
            //Destroy(this.gameObject);
        }
    
    }
    void CausouDano()
    {
        
        if(podeatk)
        {
            tempoAtck = tempoAtck + Time.deltaTime;
            if(tempoAtck >= atckSpeed)
            {
                Debug.Log("Fez o dano");
                tempoAtck=0;
                LevelManager.instance.TomouDano(dano);
            }
        }


    }

    // private void OnDestroy() //remove inimigos mortos da lista para mudar uma bool
    // {
    //     for (int i = 0; i < _spawner._pool.Count; i++)
    //     {
    //         if (this.gameObject == _spawner._pool[i])
    //         {
    //             _spawner._pool.RemoveAt(i);
    //             if (_spawner._pool.Count == 0)
    //             {
    //                 Debug.Log("zerado");
    //                 //Instantiate(drop[0],gameObject.transform.position,Quaternion.identity);
    //             }
    //         }
    //     }
    // }

}
