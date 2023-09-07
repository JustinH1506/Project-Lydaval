using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    public Transform playerBattleStation;

    public Transform enemyBattleStation;

    public TextMeshProUGUI playerName;

    public TextMeshProUGUI enemyName;

    Stats playerStats;

    Stats enemyStats;

    public BattleState state;
    
    void Start()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    void SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerStats = playerGO.GetComponent<Stats>();

        playerName.text = playerStats.unitName;


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyStats = enemyGO.GetComponent<Stats>();

        enemyName.text = enemyStats.unitName;   
    }
}
