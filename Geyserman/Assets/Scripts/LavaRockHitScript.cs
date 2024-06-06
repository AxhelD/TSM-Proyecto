using UnityEngine;

public class LavaRockHitScript : MonoBehaviour
{
    public int damageToPlayer;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<GeysermanHealthScript>().DamagePlayer(damageToPlayer);
            Destroy(gameObject);
        }
    }
}
