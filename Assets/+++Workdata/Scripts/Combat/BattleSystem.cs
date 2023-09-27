using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] Stats playerStats;

    [SerializeField] Stats enemyStats;
    #endregion

    #region BattleHuds
    public BattleHud playerHud;
    public BattleHud enemyHud;
    #endregion

    #region BattleState

    public BattleState state;

    #endregion

    #region Methods    
    /// <summary> Set Battle State to START and Start Couroutine SetUpBattle </summary>
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    /// <summary> If the Battle state is WON ... . 
    /// else if it´s LOST ... .
    /// </summary>
    public void EndBattle()
    {
        if (state == BattleState.WON)
        {
            SceneManager.LoadScene(0);
        }
        else if (state == BattleState.LOST)
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayerTurn()
    {

    }

    /// <summary> Returns if state is not PLAYERTURN. Start PlayerAttack Couroutine. </summary>
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    /// <summary>
    /// Returns if state is not PLAYERTURN. Starts PlayerHeal Courotine.
    /// </summary>
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    #endregion

    #region IEnumerator
    /// <summary> Instantiate player and enemy prefab and spawning them to the Battlestations. Get the Stats  </summary>
    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerStats = playerGO.GetComponent<Stats>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyStats = enemyGO.GetComponent<Stats>();

        playerHud.SetHud(playerStats);
        enemyHud.SetHud(enemyStats);

        yield return new WaitForSeconds(2f);

        if(playerStats.speed > enemyStats.speed)
        {
           state = BattleState.PLAYERTURN;
           PlayerTurn();
        }
        else if(enemyStats.speed > playerStats.speed)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if(enemyStats.speed == playerStats.speed)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    /// <summary> Making bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends´else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyStats.TakeDamage(playerStats.damage);

        enemyHud.SetHp(enemyStats.currentHealth);

        yield return new WaitForSeconds(0.05f);

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

    /// <summary> Getting playerStats Heal method. Waits no seconds and starts the enemys turn. </summary>
    IEnumerator PlayerHeal()
    {
        playerStats.Heal(5);

        playerHud.SetHp(playerStats.currentHealth);

        yield return new WaitForSeconds(0f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    /// <summary> Waits fior 2 seconds makes bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
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
    #endregion
}