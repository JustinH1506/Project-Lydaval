using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    #region Gameobjects
    public GameObject playerPrefab;

    public GameObject enemyPrefab;
    #endregion

    #region Transform
    public Transform playerBattleStation;

    public Transform enemyBattleStation;
    #endregion

    #region TextMeshProUGUI
    public TextMeshProUGUI playerName;

    public TextMeshProUGUI enemyName;
    #endregion

    #region Stats
    Stats playerStats;

    Stats enemyStats;
    #endregion

    #region BattleHuds
    public BattleHud playerHud;
    public BattleHud enemyHud;
    #endregion

    #region BattleState

    public BattleState state;

    #endregion

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerStats = playerGO.GetComponent<Stats>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyStats = enemyGO.GetComponent<Stats>();

        playerHud.SetHud(playerStats);
        enemyHud.SetHud(enemyStats);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyStats.TakeDamage(playerStats.damage);

        enemyHud.SetHp(enemyStats.currentHealth);

        yield return new WaitForSeconds(2f);

        if (isDead) 
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerStats.Heal(5);

        playerHud.SetHp(playerStats.currentHealth);

        yield return new WaitForSeconds(0f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);

        bool isDead = playerStats.TakeDamage(enemyStats.damage);

        playerHud.SetHp(playerStats.currentHealth);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void EndBattle()
    {
        if(state == BattleState.WON)
        {

        }
        else if(state == BattleState.LOST)
        {

        }
    }

    public void PlayerTurn()
    {

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}