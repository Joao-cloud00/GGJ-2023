using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    SpriteRenderer _spriteRenderer;
    public int MoveSpeed;
    [SerializeField] private int _currentWaypointIndex=0;
    Vector3 CurrentPointPosition, _lastPointPosition, posInit;
    public GameObject _posicaoInicial;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        posInit = _posicaoInicial.transform.position;
        transform.position = posInit;
        
    }

    void Update()
    {
        CurrentPointPosition = Waypoint.instance.GetWaypointPosition(_currentWaypointIndex);
        Move();
        Rotate();

        if(CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
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
}
