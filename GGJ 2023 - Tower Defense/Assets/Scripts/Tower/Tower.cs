using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected float distance;
    [SerializeField] protected int price;
    [SerializeField] protected Bullet bullet;
    private float nextFire;
    private bool canMove = false;
    Vector2 cursorPos;

    public int Price { get { return price; } set { price = value; } }

    private void Start()
    {
        nextFire = Time.deltaTime;
        canMove = true;
    }

    private void Update()
    {
        if(canMove)
        {
            cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);

            if (Input.GetMouseButtonDown(0))
            {
                canMove = false;
            }
        }

        if(!canMove)
        {
            Fire();
        }
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
