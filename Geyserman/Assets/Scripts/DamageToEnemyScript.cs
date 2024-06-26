﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemyScript : MonoBehaviour
{
    public int damageToEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shitai")) 
        {
            other.gameObject.GetComponent<ShitaiHealthScript>().DamageToEnemy(damageToEnemy);
        }
    }
}
