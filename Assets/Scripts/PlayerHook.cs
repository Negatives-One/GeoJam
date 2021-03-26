using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : Projectile
{
    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!arrived)
        {
            rigid.velocity = dir * velocity;
        }
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().currentState = Player.states.idle;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 globalPositionOfContact = col.contacts[0].point;
        Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        arrived = true;
        player.GetComponent<Player>().StartTrajectory(globalPositionOfContact, 100f, this.gameObject, anterior, col.contacts[0].normal);
        player.GetComponent<Player>().projColisionPoint = globalPositionOfContact;
        Destroy(this.gameObject);
    }

    public void NoAro(Vector3 pos)
    {
        transform.position = pos;
        Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        arrived = true;
        player.GetComponent<Player>().StartTrajectory(pos, 100f, this.gameObject, anterior, Vector2.zero);
        player.GetComponent<Player>().projColisionPoint = pos;
        Destroy(this.gameObject);
    }
}
