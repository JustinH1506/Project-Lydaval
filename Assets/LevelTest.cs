using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTest : MonoBehaviour
{
    [SerializeField] private GameObject statsPrefab;
    [SerializeField] private Stats playerStats;

    private void Awake()
    {
        playerStats = statsPrefab.GetComponent<Stats>();
        
        BuffPrefab();
    }

    public void BuffPrefab()
    {
        playerStats.maxHealth += 5;
    }
}
