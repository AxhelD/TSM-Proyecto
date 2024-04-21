using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKihaxIdleScript : MonoBehaviour
{
    public float speedFloat;
    public float rangeMove;

    [HideInInspector]
    public float initialPosition;

    [HideInInspector]
    public Vector2 actualPosition;

    void Start()
    {
        initialPosition = gameObject.transform.localPosition.y;
        actualPosition = gameObject.transform.localPosition;
    }

    void FixedUpdate()
    {
        MoveKihax();
    }

    public void MoveKihax()
    {
        float moveY = initialPosition + rangeMove * Mathf.Sin(Time.time * speedFloat);

        gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, moveY);
    }

}
