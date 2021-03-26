using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLiner : Enemy
{

    public GameObject projectil;

    public float speed = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * Time.deltaTime, 0);
    }

    private void Update()
    {

        if (podeAtirar)
        {
            projectil = Instantiate(projectil, transform.position, Quaternion.identity, transform);
        }
    }


    IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(2);
        float spawnY = 0f;
    }

}
