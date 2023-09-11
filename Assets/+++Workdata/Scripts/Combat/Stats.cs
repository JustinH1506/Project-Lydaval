using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int maxMp;

    public int currentMp;

    public int speed;

    public int attack;

    public int magicAttack;

    public int defense;

    public int magicDefense;

    public int damage;

    private void Start()
    {
        maxHealth.ToString();
        currentHealth.ToString();
    }

    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            return true;
        else 
            return false;
    }


    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void  Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
