using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public class StatManager : MonoBehaviour
{
    [SerializeField] private Stats heroStats;
    
    [SerializeField] private Stats tankStats;
        
    [SerializeField] private Stats healerStats;
    
    private void Start()
    {
        if (GameStateManager.instance.data.heroStatData.maxHealth == 0)
        {
            GameStateManager.instance.data.heroStatData = heroStats.data;
        }

        if (GameStateManager.instance.data.healerStatData.maxHealth == 0)
        {
            GameStateManager.instance.data.healerStatData = healerStats.data;
        }
        
        if (GameStateManager.instance.data.tankStatData.maxHealth == 0)
        {
            GameStateManager.instance.data.tankStatData = tankStats.data;
        }
        
        Destroy(gameObject);
    }
}