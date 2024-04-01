using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GeysermanHealthScript : MonoBehaviour
{
    public int geyserHealth = 100;
    public Image healthBar;
    public Image backHealthBar;
    public GameObject DeadImage;
    public float shrinkSpeed;

    private const float toDamageBarTimerMax = 0.15f;
    private float toDamageBarTimer;
    
    private void FixedUpdate()
    {
        toDamageBarTimer -= Time.deltaTime;
        if (toDamageBarTimer < 0) 
        {
            if (healthBar.fillAmount < backHealthBar.fillAmount) 
            {
                backHealthBar.fillAmount -= shrinkSpeed * Time.deltaTime; 
            }
        }
    }

    
    public void DamagePlayer(int damageToPlayer)
    {
        toDamageBarTimer = toDamageBarTimerMax;
        geyserHealth -= damageToPlayer;
        healthBar.fillAmount = (float)geyserHealth / 100f;

        if (geyserHealth <= 0) 
        { 
            
        }
    }

    public void HealGeyser(int heal) 
    {
        backHealthBar.fillAmount = healthBar.fillAmount;
        geyserHealth += heal;
        geyserHealth = Mathf.Clamp(geyserHealth, 0, 100);
        healthBar.fillAmount = (float)geyserHealth / 100f;

    }
}
