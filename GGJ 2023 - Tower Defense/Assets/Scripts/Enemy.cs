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
    public float health;
    public int MoveSpeed, danoTOMADO;
    public Waypoint nome;

    private Vector3 healthBarScale; //tamanho da barra
    private float healthPercent; //Percentual de vida para calculo do tamanho da barra

    void Start()
    {
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        // posInit = _posicaoInicial.transform.position;
        // transform.position = posInit;
        
    }

    void UpdateHealthbar()
    {
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
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
        _currentWaypointIndex += 1;
        //OnEndReached?.Invoke(this);
        //_enemyHealth.ResetHealth();
        //ObjectPooler.ReturnToPool(gameObject);
        Debug.Log(Waypoint.instance.Points.Length);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthbar();
    }
    void Morte()
    {
        if (health<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
