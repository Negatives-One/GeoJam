using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Projectile
{

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = dir * velocity;
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player>().Hitted();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Parede"))
        {
            Destroy(gameObject);
        }
    }
}