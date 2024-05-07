using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitaiActivationScript : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isActive = true;
        }
    }
}
