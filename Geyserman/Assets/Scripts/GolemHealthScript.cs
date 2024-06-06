using UnityEngine;

public class GolemHealthScript : MonoBehaviour
{
    public int damageFromWaterJet;

    void Start()
    {

    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WaterJet"))
        {
                collision.gameObject.GetComponent<GolemScript>().TakeDamage(damageFromWaterJet);
        }
    }
}
