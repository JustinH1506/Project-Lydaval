using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CharacterTypes
{
    Enemy,
    Hero,
    Tank,
    Healer
}
public class Stats : MonoBehaviour
{
    #region Variables

    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int speed;

    public int attack;

    public int defense;

    public int level;

    public int xp;

    public int neededXp;

    public int enemyGiveXp;

    //public bool isEnemy;

    public CharacterTypes types;

    #endregion
    
    #region Scripts

    [SerializeField] private BattleSystem battleSystem;

    [SerializeField] private StatManager.Data data;
    
    #endregion

    #region Methods

    private void Awake()
    {
        battleSystem = GameObject.Find("Battle_System").GetComponent<BattleSystem>();
        
        currentHealth = maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            types = CharacterTypes.Enemy;
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


    public void HasEnoughXp()
    {
        if (xp >= neededXp)
            LevelUp();
    }

    /// <summary>
    /// Calculates the currentHealth minus the dmg and returns true if the currentHealth is equal to or less then 0.
    /// </summary>
    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            return true;
        
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

    public void LevelUp()
    {
        if (types == CharacterTypes.Hero)
        {
            level++;

            int randomNumber = Random.Range(5, 15);

            maxHealth += randomNumber;

            currentHealth += randomNumber;

            speed += Random.Range(0, 3);

            attack += Random.Range(2, 5);

            defense += Random.Range(1, 3);

            xp -= neededXp;

            neededXp = (neededXp *= 2);
        }
        
        if (types == CharacterTypes.Healer)
        {
            level++;

            int randomNumber = Random.Range(10, 20);

            maxHealth += randomNumber;

            currentHealth += randomNumber;

            speed += Random.Range(2, 5);

            attack += Random.Range(0, 3);

            defense += Random.Range(0, 3);
            
            xp -= neededXp;

            neededXp = (neededXp *= 2);
        }
        
        if (types == CharacterTypes.Tank)
        {
            level++;

            int randomNumber = Random.Range(20, 30);

            maxHealth += randomNumber;

            currentHealth += randomNumber;

            speed += Random.Range(0, 3);

            attack += Random.Range(0, 3);

            defense += Random.Range(3, 5);
            
            xp -= neededXp;

            neededXp = (neededXp *= 2);
        }
        
        battleSystem.SetHp();
    }
    #endregion
}