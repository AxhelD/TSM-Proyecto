using UnityEngine;

public class GolemHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damageFromWaterJet;
    private GolemScript golemScript;

    void Start()
    {
        currentHealth = maxHealth;
        golemScript = GetComponent<GolemScript>();
    } 

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Enemigo recibió daño: {amount}. Vida restante: {currentHealth}");
        if(currentHealth < 0)
        {
            currentHealth = 0;
            golemScript.Die();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"Colisión detectada con: {collider.tag}");
        if (collider.CompareTag("WaterJet"))
        {
            Debug.Log("Colisión con WaterJet detectada.");
            TakeDamage(damageFromWaterJet);
            collider.gameObject.SetActive(false);  
        }
    }
}
