using UnityEngine;

public class LavaRockHitScript : MonoBehaviour
{
    public int damageToPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<GeysermanHealthScript>().DamagePlayer(damageToPlayer);
            Destroy(this.gameObject);
        }
    }
}
