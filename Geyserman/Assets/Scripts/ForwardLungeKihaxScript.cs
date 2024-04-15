using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardLungeKihaxScript : MonoBehaviour
{
    public float lungeSpeed;
    public float returnSpeed;

    private bool moveToPlayer = false;
    private Vector2 move;

    private void FixedUpdate()
    {

        if (moveToPlayer)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, move, lungeSpeed * 0.1f);

            if (gameObject.transform.localPosition.x == move.x && gameObject.transform.localPosition.y == move.y) 
            {
                moveToPlayer = false;
            }
        }
        else if (!moveToPlayer)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, GetComponent<BasicKihaxIdleScript>().actualPosition, returnSpeed * 0.1f);

            if (gameObject.transform.position.x == GetComponent<BasicKihaxIdleScript>().actualPosition.x && gameObject.transform.position.y == GetComponent<BasicKihaxIdleScript>().actualPosition.y)
            {
                GetComponent<BasicKihaxIdleScript>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        moveToPlayer = true;
        GetComponent<BasicKihaxIdleScript>().enabled = false;
        move =  other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    }
}
