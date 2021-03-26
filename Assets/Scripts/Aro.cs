using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    private bool isIn = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Hook" && !isIn)
        {
            other.gameObject.GetComponent<PlayerHook>().NoAro(transform.position);
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isIn)
        {
            isIn = false;
        }
    }
}
