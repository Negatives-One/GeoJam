using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 dir;

    public float velocity = 1f;

    public Rigidbody2D rigid;

    public GameObject player;

    public bool arrived = false;

    public GameObject anterior;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
}
