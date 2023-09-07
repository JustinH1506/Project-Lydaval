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

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
