using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    #region Scripts
    
    private EnemyManager enemyManager;
    
    #endregion 
    
    #region Variables

    public int enemyId, enemyI, playerId, playerI, turnId;
    
    private int deadEnemies, deadPlayers;

    private int enemyAdder;

    #endregion

    #region Gameobjects

    public GameObject playerPrefab;

    public GameObject enemyPrefab;

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
    [SerializeField] private List<GameObject> enemyWormList, enemyThiefList, enemyBoarList;

    #endregion

    #region Methods

    private void Awake()
    {
        enemyManager = GameObject.Find("---EnemyManager").GetComponent<EnemyManager>();
    }

    /// <summary> Set Battle State to START and Start Coroutine SetUpBattle </summary>
    void Start()
    {
        state = BattleState.START;
        SetEnemiesToList();
        StartCoroutine(SetUpBattle());
        Debug.Log("Working");
    }

    /// <summary> If the Battle state is WON ... . 
    /// else if it�s LOST ... .
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

    private void ClearList()
    {
        characterList.RemoveAll(Stats => Stats == null);
    }

    private void SetEnemiesToList()
    {
        if (enemyManager.enemyType == EnemyType.THIEF)
        {
            enemyAdder = Random.Range(0, 3);
            
            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyThiefList[i]);
            }
        }
        
        if (enemyManager.enemyType == EnemyType.BOAR)
        {
            enemyAdder = Random.Range(0, 3);
            
            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyBoarList[i]);
            }
            
        }
        
        if (enemyManager.enemyType == EnemyType.WORM)
        {
            enemyAdder = Random.Range(0, 3);
            
            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyWormList[i]);
            }
        }
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

    /// <summary> Instantiate player and enemy prefab and spawning them to the Battlestation�s. Get the Stats.  </summary>
    IEnumerator SetUpBattle()
    {
        for (int i = 0; i < playerPrefabList.Count; i++)
        {
            playerI = i;
            
            GameObject playerGo = Instantiate(playerPrefabList[i], playerBattleStationList[i]);
            playerStats = playerGo.GetComponent<Stats>();
            playerStatsList.Add(playerStats);

            playerHud.SetPlayerHud(playerStatsList[i]);
            
            characterList.Add(playerStatsList[i]);
        }
        
        for (int i = 0; i < enemyPrefabList.Count; i++)
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
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends�else the Enemy�s Turn starts.
    /// </summary>
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyStatsList[enemyId].TakeDamage(characterList[turnId].attack);

        if (enemyStatsList[enemyId].currentHealth <= 0)
            enemyStatsList[enemyId].currentHealth = 0;
        
        enemyHud.SetEnemyHp(enemyStatsList[enemyId].currentHealth, enemyStatsList[enemyId].maxHealth);

        yield return null;

        if (isDead)
        {
            targetingButtonsList[enemyId].interactable = false;
            
           Destroy(enemyStatsList[enemyId].gameObject);

           yield return null;
           
           ClearList();

           deadEnemies++;
        }

        if (deadEnemies == enemyAdder +1)
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

        if (turnId == characterList.Count)
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

    /// <summary> Getting playerStats Heal method. Waits no seconds and starts the enemy�s turn. </summary>
    IEnumerator PlayerHeal()
    {
        playerStats.Heal(5);

        playerHud.SetPlayerHp(characterList[turnId].currentHealth, characterList[turnId].maxHealth);

        yield return new WaitForSeconds(0f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    /// <summary> Waits for 2 seconds makes bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends else the Enemy�s Turn starts.
    /// </summary>
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        playerId = Random.Range(0, 3);
        
        bool isDead = playerStatsList[playerId].TakeDamage(characterList[turnId].attack);

        if (playerStatsList[playerId].currentHealth <= 0)
            playerStatsList[playerId].currentHealth = 0;
        
        playerHud.SetPlayerHp(playerStatsList[playerId].currentHealth, playerStatsList[playerId].maxHealth);

        yield return new WaitForSeconds(0f);

        if (isDead)
        {
            targetingButtonsList[playerId].interactable = false;

            Destroy(playerStatsList[playerId].gameObject);
            
            Destroy(playerPrefabList[playerId].gameObject);

            yield return null;
           
            ClearList();
        }
        
        if (playerStatsList[playerId].currentHealth <= 0)
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