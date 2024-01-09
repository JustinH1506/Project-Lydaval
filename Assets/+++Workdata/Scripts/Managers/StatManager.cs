using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    [SerializeField] private Data data;
    
    public int maxHealth = 10;
    
    public int currentHealth;
    
    public int speed;
    
    public int attack;
     
    public int defense;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        data.maxHealth += maxHealth;
    }  
    
    [System.Serializable]
    public class Data
    {
        [SerializeField] public Stats heroStats, healerStats, tankStats;
             
        public int maxHealth;
     
        public int currentHealth;
     
        public int speed;
     
        public int attack;
     
        public int defense;
    }
}
