using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    #region Variables
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
    #endregion

    #region Methods
    /// <summary>
    /// Gets maxHealt and currentHealth to a string.
    /// </summary>
    private void Start()
    {
        maxHealth.ToString();
        currentHealth.ToString();
    }

    /// <summary>
    /// Calculates the currentHealt minus the dmg and returns true if the currentHealt is equal to or less then 0.
    /// </summary>
    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            return true;
        else 
            return false;
    }

    /// <summary>
    /// heals the current healt by a certain ammount and makes the currentHealt to maxHealt if it goes over maxHealth.
    /// </summary>
    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    #endregion
}