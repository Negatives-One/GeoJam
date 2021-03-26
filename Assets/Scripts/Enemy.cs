using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject player;

    public bool podeAtirar = true;

    public float shootDistance;

    public GameObject Bullet;

    public GameObject pathParent;

    private int pointTarget = 0;

    private int maxPoints = 0;

    public AudioSource sound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxPoints = pathParent.transform.childCount;
        sound = transform.GetChild(2).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(podeAtirar && Vector3.Distance(transform.position, player.transform.position) <= shootDistance)
        {
           // Shoot();
            podeAtirar = false;
        }
        AIDestinationSetter setter = GetComponent<AIDestinationSetter>();
        setter.target = pathParent.transform.GetChild(pointTarget);
        if (Vector3.Distance(transform.position, pathParent.transform.GetChild(pointTarget).position) < 5f)
        {
            pointTarget++;
        }
        if(pointTarget == maxPoints)
        {
            player.GetComponent<Player>().Passed();
            Destroy(gameObject);
            Destroy(this);
        }
    }


    private void Shoot()
    {
        Vector2 target = player.transform.position;
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        GameObject proj = Instantiate(Bullet);
        EnemyBullet script = proj.GetComponent<EnemyBullet>();
        script.dir = dir;
        script.velocity = 50;
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(proj.transform.position);
        float angle = AngleBetweenTwoPoints(Vector3.zero, dir);
        proj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - (float)1.5708));

        proj.transform.position = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Corte");
            sound.Play();
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
