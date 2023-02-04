using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] protected GameObject target;
    Rigidbody2D rb;
    Vector2 direction, rotation;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();

        direction = target.transform.position - transform.position;
        rotation = transform.position - target.transform.position;
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //transform.up = target.transform.position - transform.position;
        
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Destroy(gameObject);
    }
}
