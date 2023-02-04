using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected float distance;
    [SerializeField] protected Bullet bullet;
    private float nextFire;

    private void Start()
    {
        nextFire = Time.deltaTime;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
