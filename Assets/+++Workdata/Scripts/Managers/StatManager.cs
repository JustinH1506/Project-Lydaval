using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public class StatManager : MonoBehaviour
{
    private static StatManager instance;

    [SerializeField] private Stats heroStats;
    
    [SerializeField] private Stats tankStats;
        
    [SerializeField] private Stats healerStats;

    [SerializeField] public HeroData heroData;

    [SerializeField] public TankData tankData;

    [SerializeField] public HealerData healerData;

    
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        
        DontDestroyOnLoad(this);

        heroData.maxHealth = heroStats.data.maxHealth;

        heroData.currentHealth = heroStats.data.currentHealth;

        heroData.speed = heroStats.data.speed;

        heroData.attack = heroStats.data.attack;

        heroData.defense = heroStats.data.defense;

        heroData.level = heroStats.data.level;

        heroData.xp = heroStats.data.xp;

        heroData.neededXp = heroStats.data.neededXp;

        heroData.types = heroStats.data.types;
        
        tankData.maxHealth = tankStats.data.maxHealth;

        tankData.currentHealth = tankStats.data.currentHealth;

        tankData.speed = tankStats.data.speed;

        tankData.attack = tankStats.data.attack;

        tankData.defense = tankStats.data.defense;

        tankData.level = tankStats.data.level;

        tankData.xp = tankStats.data.xp;

        tankData.neededXp = tankStats.data.neededXp;

        tankData.types = tankStats.data.types;
        
        healerData.maxHealth = healerStats.data.maxHealth;

        healerData.currentHealth = healerStats.data.currentHealth;

        healerData.speed = healerStats.data.speed;

        healerData.attack = healerStats.data.attack;

        healerData.defense = healerStats.data.defense;

        healerData.level = healerStats.data.level;

        healerData.xp = healerStats.data.xp;

        healerData.neededXp = healerStats.data.neededXp;

        healerData.types = healerStats.data.types;

        var heroCurrentStats = GameStateManager.instance.data.heroStatData;

        if (heroCurrentStats != null)
        {
            heroData = heroCurrentStats;
        }
        
        var healerCurrentStats = GameStateManager.instance.data.healerStatData;

        if (healerCurrentStats != null)
        {
            healerData = healerCurrentStats;
        }
        
        var tankCurrentStats = GameStateManager.instance.data.tankStatData;

        if (tankCurrentStats != null)
        {
            tankData = tankCurrentStats;
        }
        
    }

    [System.Serializable]
    public class HeroData
    {
        public string unitName;
        
        public int maxHealth;

        public int currentHealth;

        public int speed;

        public int attack;

        public int defense;

        public int level;

        public int xp;

        public int neededXp;

        public CharacterTypes types;
    }
    
    [System.Serializable]
    public class TankData
    {
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

        public CharacterTypes types;
    }
    
    [System.Serializable]
    public class HealerData
    {
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

        public CharacterTypes types;
    }
}