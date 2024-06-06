using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitaiActivationScript : MonoBehaviour
{
    public GameObject bossHealthBar;

    [HideInInspector]
    public bool isActive = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isActive = true;

            bossHealthBar.SetActive(true);
        }
    }
}
