using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDamageScript : MonoBehaviour
{
    public int damageAmount;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GeysermanHealthScript>().DamagePlayer(damageAmount);
        }
    }
}
