using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryKihaxFollowScript : MonoBehaviour
{
    public float speedToMove;
    public GameObject[] points;

    private int index;

    void FixedUpdate()
    {
        if (gameObject.transform.position != points[index + 1].transform.position)
        {
            PointPathSequence(points[index + 1]);
        }
        
        if (gameObject.transform.position == points[index + 1].transform.position)
        {
            if (index == points.Length - 2)
            {
                index = -1;
            } else if (index < points.Length) 
            {
                index += 1;
            }           
        }
    }

    void PointPathSequence(GameObject nextPoint)
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextPoint.transform.position, speedToMove * 0.1f);
    }
}
