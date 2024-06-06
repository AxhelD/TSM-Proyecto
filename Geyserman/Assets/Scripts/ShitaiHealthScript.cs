using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShitaiHealthScript : MonoBehaviour
{
    public int shitaiHealth = 100;
    public Image healthBar;
    public Image backHealthBar;
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

    public void DamageToEnemy(int damageToEnemy)
    {
        toDamageBarTimer = toDamageBarTimerMax;
        shitaiHealth -= damageToEnemy;
        healthBar.fillAmount = (float)shitaiHealth / 100f;

        if (shitaiHealth <= 0)
        {
            gameObject.GetComponent<RandomShitaiAttackScript>().numAttack = 5;
        }
    }
}
