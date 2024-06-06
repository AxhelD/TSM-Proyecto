using UnityEngine;

public class SpawnLifeScript : MonoBehaviour
{
    private float spawnHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Geyser"))
        {
            spawnHealth = GetComponentInParent<SpawnScript>().spawnHealth;
            spawnHealth -= 10;
        }
    }
}
