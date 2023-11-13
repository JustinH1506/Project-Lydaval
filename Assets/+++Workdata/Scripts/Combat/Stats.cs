using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stats : MonoBehaviour
{
    #region Variables

    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int speed;

    public int attack;

    public int defense;

    public bool isEnemy;

    #endregion
    
    #region Scripts

    [SerializeField] private BattleSystem battleSystem;
    
    #endregion

    #region Methods

    private void Awake()
    {
        battleSystem = GetComponent<BattleSystem>();
        
        currentHealth = maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            isEnemy = true;
        }
    }
    
    /// <summary>
    /// Gets maxHealth and currentHealth to a string.
    /// </summary>
    private void Start()
    {
        maxHealth.ToString();
        currentHealth.ToString();
    }

    /// <summary>
    /// Calculates the currentHealth minus the dmg and returns true if the currentHealth is equal to or less then 0.
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
    /// heals the current health by a certain amount and makes the currentHealth to maxHealth if it goes over maxHealth.
    /// </summary>
    public void Heal(int healing)
    {
        currentHealth += healing;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    #endregion
}