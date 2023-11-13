using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    #region Variables

    public int enemyId, enemyI, playerId, playerI, turnId;
    
    private int deadEnemies, deadPlayers;

    #endregion

    #region Gameobjects

    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    #endregion

    #region Transform

  //  public Transform playerBattleStation;

    //public Transform enemyBattleStation;

    #endregion

    #region TextMeshProUGUI

   // public TextMeshProUGUI playerName;

   // public TextMeshProUGUI enemyName;

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

    #region Lists

    [SerializeField] List<GameObject> playerPrefabList, enemyPrefabList;
    [SerializeField] public List<Transform> playerBattleStationList, enemyBattleStationList;
    [SerializeField] public List<Image> targetingIndicatorList;
    [SerializeField] public List<Stats> enemyStatsList, playerStatsList, characterStatsList;
    [SerializeField] List<Button> targetingButtonsList;
    [SerializeField] List<Stats> characterList;

    #endregion

    #region Methods

    /// <summary> Set Battle State to START and Start Coroutine SetUpBattle </summary>
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
        Debug.Log("Working");
    }

    /// <summary> If the Battle state is WON ... . 
    /// else if it´s LOST ... .
    /// </summary>
    private void EndBattle()
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
    private void PlayerTurn()
    {
        
    }

    /// <summary> Returns if state is not PLAYERTURN. Start PlayerAttack Coroutine. </summary>
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    /// <summary>
    /// Returns if state is not PLAYERTURN. Starts PlayerHeal Coroutine.
    /// </summary>
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    #endregion

    #region IEnumerator

    /// <summary> Instantiate player and enemy prefab and spawning them to the Battlestation´s. Get the Stats.  </summary>
    IEnumerator SetUpBattle()
    {
        for (int i = 0; i < 3; i++)
        {
            playerI = i;
            
            GameObject playerGo = Instantiate(playerPrefabList[i], playerBattleStationList[i]);
            playerStats = playerGo.GetComponent<Stats>();
            playerStatsList.Add(playerStats);

            playerHud.SetPlayerHud(playerStatsList[i]);
            
            characterList.Add(playerStatsList[i]);
        }
        
        for (int i = 0; i < 4; i++)
        {
            enemyI = i;
            
            GameObject enemyGo = Instantiate(enemyPrefabList[i], enemyBattleStationList[i]);
            enemyStats = enemyGo.GetComponent<Stats>();
            enemyStatsList.Add(enemyStats);

            enemyHud.SetEnemyHud(enemyStatsList[i]);
            
            characterList.Add(enemyStatsList[i]);
        }

        if (characterList.Count > 0)
        {
            characterList.Sort(delegate(Stats stats, Stats stats1)
            {
                return (stats.GetComponent<Stats>().speed).CompareTo(stats1.GetComponent<Stats>().speed);
            });
            characterList.Reverse();
        }
        
        yield return new WaitForSeconds(2f);
        
        if (!characterList[turnId].isEnemy)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else if (characterList[turnId].isEnemy)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary> Making bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends´else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyStatsList[enemyId].TakeDamage(characterList[turnId].attack);

        enemyHud.SetEnemyHp(enemyStatsList[enemyId].currentHealth);

        yield return null;

        if (isDead)
        {
            targetingButtonsList[enemyId].interactable = false;
        }
        
        if (enemyStatsList[enemyId].currentHealth <= 0)
        {
                deadEnemies++;
        }
        
        if (deadEnemies == 4)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(TurnChange());
        }
    }

    IEnumerator TurnChange()
    {
        turnId++;

        if (turnId > characterList.Count -1 )
            turnId = 0;

        yield return null;
        
        if(!characterList[turnId].isEnemy)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary> Getting playerStats Heal method. Waits no seconds and starts the enemy´s turn. </summary>
    IEnumerator PlayerHeal()
    {
        playerStats.Heal(5);

        playerHud.SetPlayerHp(characterList[turnId].currentHealth);

        yield return new WaitForSeconds(0f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    /// <summary> Waits for 2 seconds makes bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);
        playerId = Random.Range(0, 2);
        
        bool isDead = playerStatsList[playerId].TakeDamage(characterList[turnId].attack);

        playerHud.SetPlayerHp(playerStatsList[playerId].currentHealth);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            
        }
        
        if (enemyStatsList[enemyId].currentHealth <= 0)
        {
            deadPlayers++;
        }
        

        if(deadPlayers == 3)
        {
            Destroy(playerStatsList[playerId].gameObject);
            
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            StartCoroutine(TurnChange());
        }
    }
    #endregion
}