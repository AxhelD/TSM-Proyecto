using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject counterReference;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("WaterJet") | collider.CompareTag("Player"))
        {
            explosion.Play();
            Destroy(gameObject);

            counterReference.GetComponent<EnemyCounterScript>().UpdateCounter();
        }
    }
}

