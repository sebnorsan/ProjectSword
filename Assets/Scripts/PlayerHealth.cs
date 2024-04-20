using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "enemyAttacks")
        {
            currentHealth -= collision.gameObject.GetComponent<EnemyDamage>().damageAmount;
            healthBar.SetHealth(currentHealth);
        }
    }
}
