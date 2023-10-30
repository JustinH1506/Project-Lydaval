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

    public int maxMp;

    public int currentMp;

    public int speed;

    public int attack;

    public int magicAttack;

    public int defense;

    public int magicDefense;

    public int damage;

    #endregion
    
    #region Scripts

    [SerializeField] private BattleSystem battleSystem;
    
    #endregion

    #region Methods

    private void Awake()
    {
        battleSystem = GetComponent<BattleSystem>();
    }
    
    /// <summary>
    /// Gets maxHealth and currentHealth to a string.
    /// </summary>
    private void Start()
    {
        maxHealth.ToString();
        currentHealth.ToString();

        currentHealth = maxHealth;
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
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    #endregion
}