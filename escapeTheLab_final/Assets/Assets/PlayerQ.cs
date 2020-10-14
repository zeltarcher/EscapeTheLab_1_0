using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQ : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    //This is where we add HealthBar inside Playr script
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        //healthbar add
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //healthbar add
        healthBar.SetHealth(currentHealth);
    }
}
