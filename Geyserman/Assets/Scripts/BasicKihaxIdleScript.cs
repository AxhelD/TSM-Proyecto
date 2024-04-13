using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKihaxIdleScript : MonoBehaviour
{
    public float speedFloat;
    public float rangeMove;
    public float maxRangeMove;
    public float minRangeMove;

    private float initialPosition;

    void Start()
    {
        initialPosition = gameObject.transform.localPosition.y;
    }

    void FixedUpdate()
    {
        MoveKihax();
    }

    void MoveKihax()
    {
        float moveY = initialPosition + Mathf.Sin(Time.time * speedFloat) * rangeMove;

        gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, moveY);

        speedFloat = Mathf.Lerp(minRangeMove, maxRangeMove, Mathf.Abs(Mathf.Sin(Time.deltaTime * speedFloat)));
    }

}
