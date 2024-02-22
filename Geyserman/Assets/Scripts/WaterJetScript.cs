using System.Collections;
using UnityEngine;

public class WaterJetScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayExplosion());
    }

    private IEnumerator DelayExplosion()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
