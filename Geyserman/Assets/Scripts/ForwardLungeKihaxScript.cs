using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardLungeKihaxScript : MonoBehaviour
{
    public float speed;

    public GameObject player;

    private Vector3 vector;


    private void FixedUpdate()
    {
        vector = player.transform.position - transform.position;

        if (vector.magnitude < 20)
        {
            GetComponent<BasicKihaxIdleScript>().enabled = false;
            transform.Translate(vector.normalized * speed);
            GetComponent<BasicKihaxIdleScript>().initialPosition = transform.position.y;
        }
        else
        {
            GetComponent<BasicKihaxIdleScript>().enabled = true;
        }
    }
}
