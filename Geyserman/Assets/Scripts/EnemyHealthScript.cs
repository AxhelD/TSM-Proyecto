using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int enemyHealth = 100;

    void Start()
    {
        
    }

    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
