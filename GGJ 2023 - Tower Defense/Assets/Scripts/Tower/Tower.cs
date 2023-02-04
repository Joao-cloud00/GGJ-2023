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
    [SerializeField] private List<GameObject> enemyTarget = new List<GameObject>();
    Vector2 cursorPos;

    public int Price { get { return price; } set { price = value; } }
    public List<GameObject> EnemyTarget { get { return EnemyTarget; } set { EnemyTarget = value;} }

    private void Start()
    {
        nextFire = Time.time;
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

        if(!canMove && enemyTarget != null)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time > nextFire && enemyTarget.Count > 1)
        {
            Bullet _bullet = Instantiate(bullet, transform.position, Quaternion.identity);
            _bullet.target = enemyTarget[1];
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyTarget.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyTarget.Contains(collision.gameObject))
        {
            enemyTarget.Remove(collision.gameObject);
        }
    }
}
