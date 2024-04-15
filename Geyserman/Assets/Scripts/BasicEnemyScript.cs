using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    public ParticleSystem explosion;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("WaterJet") | collider.CompareTag("Player"))
        {
            explosion.Play();
            Destroy(gameObject);
        }
    }
}

