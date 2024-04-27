using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShitaiAttackScript : MonoBehaviour
{
    public Animator animatorController;
    public bool hasArrived;


    void Start()
    {
        animatorController = gameObject.GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        
    }
}
