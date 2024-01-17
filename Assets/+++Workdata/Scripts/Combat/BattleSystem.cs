using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleSystem : MonoBehaviour
{
    #region Scripts

    private EnemyManager enemyManager;

    #endregion

    #region Variables

    public int enemyId, enemyI, playerId, playerI, turnId;

    private int random1, random2 = 2;

    private int deadEnemies, deadPlayers;

    private int enemyAdder;

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
    [SerializeField] public List<Stats> enemyStatsList, playerStatsList;
    [SerializeField] public List<Button> targetingButtonsList, skillButtonList;
    [SerializeField] List<Stats> characterList;
    [SerializeField] private List<GameObject> enemyWormList, enemyThiefList, enemyBoarList;
    [SerializeField] private List<Slider> enemyHpList, playerHpList;

    #endregion

    #region Buttons

    [SerializeField] private Button skill;

    #endregion

    #region Methods

    private void Awake()
    {
        enemyManager = GameObject.Find("---EnemyManager").GetComponent<EnemyManager>();
    }

    /// <summary> Set Battle State to START and Start Coroutine SetUpBattle </summary>
    void Start()
    {
        state = BattleState.Start;
        SetEnemiesToList();
        StartCoroutine(SetUpBattle());
    }

    /// <summary> If the Battle state is WON ... . 
    /// else if it´s LOST ... .
    /// </summary>
    private void EndBattle()
    {
        if (state == BattleState.Won)
        {
            for (int i = 0; i < playerStatsList.Count; i++)
            {
                if (playerStatsList[i].data.types == CharacterTypes.Hero)
                {
                    GameStateManager.instance.data.heroStatData = playerStatsList[i].data;
                }

                if (playerStatsList[i].data.types == CharacterTypes.Healer)
                {
                    GameStateManager.instance.data.healerStatData = playerStatsList[i].data;
                }

                if (playerStatsList[i].data.types == CharacterTypes.Tank)
                {
                    GameStateManager.instance.data.tankStatData = playerStatsList[i].data;
                }
            }

            SceneManager.LoadScene(1);
        }
        else if (state == BattleState.Lost)
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void PlayerTurn()
    {
      characterList[turnId].SetTurn();
    }

    private void ClearList()
    {
        characterList.RemoveAll(Stats => Stats == null);

        playerStatsList.RemoveAll(Stats => Stats == null);

        playerHpList.RemoveAll(Stats => Stats == null);
    }

    private void SetEnemiesToList()
    {
        if (enemyManager.enemyType == EnemyType.THIEF)
        {
            enemyAdder = Random.Range(0, 3);

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyThiefList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
            }
        }

        if (enemyManager.enemyType == EnemyType.BOAR)
        {
            enemyAdder = Random.Range(0, 3);

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyBoarList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
            }
        }

        if (enemyManager.enemyType == EnemyType.WORM)
        {
            enemyAdder = Random.Range(0, 3);

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyWormList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetHp()
    {
        for (int i = 0; i < playerStatsList.Count; i++)
        {
            playerI = i;

            playerHud.SetPlayerHud(playerStatsList[i]);
        }
    }


    /// <summary> Returns if state is not PlayerTurn. Start PlayerAttack Coroutine. </summary>
    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnAttackSkillButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttackSkill());
    }

    public void OnPlayerHealSkillButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerHealSkill());
    }

    public void OnPlayerTauntSkillButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerTauntSkill());
    }
    
    #endregion

    #region IEnumerator

    /// <summary> Instantiate player and enemy prefab and spawning them to the Battlestation´s. Get the Stats. </summary>
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
                return (stats.GetComponent<Stats>().data.speed).CompareTo(stats1.GetComponent<Stats>().data.speed);
            });
            characterList.Reverse();
        }

        for (int i = 0; i < playerStatsList.Count; i++)
        {
            if (playerStatsList[i].data.types == CharacterTypes.Hero)
            {
                playerStatsList[i].data = GameStateManager.instance.data.heroStatData;
            }

            if (playerStatsList[i].data.types == CharacterTypes.Healer)
            {
                playerStatsList[i].data = GameStateManager.instance.data.healerStatData;
            }

            if (playerStatsList[i].data.types == CharacterTypes.Tank)
            {
                playerStatsList[i].data = GameStateManager.instance.data.tankStatData;
            }
        }

        yield return null;

        if (characterList[turnId].data.types != CharacterTypes.Enemy)
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
        else if (characterList[turnId].data.types == CharacterTypes.Enemy)
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary> Making bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends´else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator PlayerAttack()
    {
        characterList[turnId].anim.SetTrigger("Attack");

        yield return new WaitForSeconds(2f);
        
        //enemyStatsList[enemyId].anim.SetTrigger("Hurt");
        
        bool isDead = enemyStatsList[enemyId].TakeDamage(characterList[turnId].data.attack);

        if (enemyStatsList[enemyId].data.currentHealth <= 0)
            enemyStatsList[enemyId].data.currentHealth = 0;

        enemyHud.SetEnemyHp(enemyStatsList[enemyId].data.currentHealth, enemyStatsList[enemyId].data.maxHealth);

        yield return null;

        if (isDead)
        {
            targetingButtonsList[enemyId].interactable = false;

            targetingIndicatorList[enemyId].enabled = false;

            enemyHpList[enemyId].gameObject.SetActive(false);

            for (int i = 0; i < playerStatsList.Count; i++)
            {
                playerStatsList[i].data.xp += enemyStatsList[enemyId].data.enemyGiveXp;

                playerStatsList[i].HasEnoughXp();
            }

            Destroy(enemyStatsList[enemyId].gameObject);

            yield return null;

            ClearList();

            deadEnemies++;

            for (int i = 0; i < enemyStatsList.Count; i++)
            {
                if (enemyStatsList[i] != null)
                {
                    enemyId = i;

                    targetingIndicatorList[i].enabled = true;

                    break;
                }
            }
        }

        if (deadEnemies == enemyAdder + 1)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            characterList[turnId].SetTurnFalse();
            StartCoroutine(TurnChange());
        }
    }

    IEnumerator PlayerAttackSkill()
    {
        skill.interactable = false;
        
        characterList[turnId].anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);
        
        bool isDead = enemyStatsList[enemyId].TakeDamage(characterList[turnId].data.attack * 2);

        characterList[turnId].cooldown = 2;

        if (enemyStatsList[enemyId].data.currentHealth <= 0)
            enemyStatsList[enemyId].data.currentHealth = 0;

        enemyHud.SetEnemyHp(enemyStatsList[enemyId].data.currentHealth, enemyStatsList[enemyId].data.maxHealth);

        yield return null;

        if (isDead)
        {
            targetingButtonsList[enemyId].interactable = false;

            targetingIndicatorList[enemyId].enabled = false;

            enemyHpList[enemyId].gameObject.SetActive(false);

            for (int i = 0; i < playerStatsList.Count; i++)
            {
                playerStatsList[i].data.xp += enemyStatsList[enemyId].data.enemyGiveXp;

                playerStatsList[i].HasEnoughXp();
            }

            Destroy(enemyStatsList[enemyId].gameObject);

            yield return null;

            ClearList();

            deadEnemies++;

            for (int i = 0; i < enemyStatsList.Count; i++)
            {
                if (enemyStatsList[i] != null)
                {
                    enemyId = i;

                    targetingIndicatorList[i].enabled = true;

                    break;
                }
            }
        }

        if (deadEnemies == enemyAdder + 1)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            characterList[turnId].SetTurnFalse();
            StartCoroutine(TurnChange());
        }
    }

    IEnumerator PlayerHealSkill()
    {
        characterList[turnId].cooldown = 2;
        
        characterList[turnId].anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < playerStatsList.Count; i++)
        {
            if (playerStatsList[i].select)
            {
                if(playerStatsList[i].data.currentHealth >= playerStatsList[i].data.maxHealth)
                {
                    playerStatsList[i].data.currentHealth = playerStatsList[i].data.maxHealth;

                    playerStatsList[i].select = false;
                    
                    break;
                }
                
                if (playerStatsList[i].data.currentHealth < playerStatsList[i].data.maxHealth)
                {
                    playerStatsList[i].data.currentHealth += 10;
                    
                    if(playerStatsList[i].data.currentHealth >= playerStatsList[i].data.maxHealth)
                    {
                        playerStatsList[i].data.currentHealth = playerStatsList[i].data.maxHealth;
                    }
                    playerStatsList[i].select = false;
                }
                
                characterList[turnId].healButton.transform.parent.gameObject.SetActive(false);
                
                SetHp();
                
                characterList[turnId].SetTurnFalse();
                
                StartCoroutine(TurnChange());
            }
        }
        yield return null;
    }

    IEnumerator PlayerTauntSkill()
    {
        characterList[turnId].taunt = true;
        
        characterList[turnId].cooldown = 3;
        
        characterList[turnId].anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);
        
        characterList[turnId].SetTurnFalse();
        StartCoroutine(TurnChange());
    }

    IEnumerator TurnChange()
    {
        turnId++;

        if (turnId >= characterList.Count)
            turnId = 0;
        
        if (characterList[turnId].taunt)
        {
            characterList[turnId].taunt = false;
        }

        if (characterList[turnId].cooldown > 0)
        {
            characterList[turnId].cooldown--;
        }
        else if(characterList[turnId].cooldown == 0)
        {
            skill.interactable = true;
        }
        else
        {
            skill.interactable = false;
        }

        yield return null;

        if (characterList[turnId].data.types != CharacterTypes.Enemy)
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary> Waits for 2 seconds makes bool isDead to the Take Damage Method from enemyStats. Set The enemyHud Hp.
    /// Wait for 0.05 seconds. If isDead is true the Battle is Won and the Battle Ends else the Enemy´s Turn starts.
    /// </summary>
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        playerId = Random.Range(random1, random2);

        for (int i = 0; i < playerStatsList.Count; i++)
        {
            if (playerStatsList[i].taunt)
            {
                playerId = i;
            }
        }

        characterList[turnId].anim.SetTrigger("Attacking");

        yield return new WaitForSeconds(1f);
        
        playerStatsList[playerId].anim.SetTrigger("Damage");

        bool isDead = playerStatsList[playerId].TakeDamage(characterList[turnId].data.attack);

        if (playerStatsList[playerId].data.currentHealth <= 0)
            playerStatsList[playerId].data.currentHealth = 0;

        playerHud.SetPlayerHp(playerStatsList[playerId].data.currentHealth, playerStatsList[playerId].data.maxHealth);

        if (isDead)
        {
            if (playerStatsList[playerId].data.currentHealth <= 0)
            {
                deadPlayers++;
            }

            playerHud.ClearPlayerStats();
            
            Destroy(playerStatsList[playerId].gameObject);

            Destroy(playerHpList[playerId].gameObject);

            yield return null;

            ClearList();

            if (playerStatsList != null)
            {
                for (int i = 0; i < playerStatsList.Count; i++)
                {
                    if (playerStatsList[i] != null)
                    {
                        random1 = i;
                        break;
                    }
                }

                for (int j = playerStatsList.Count - 1; j >= 0; j--)
                {
                    if (playerStatsList[j] != null)
                    {
                        random2 = j;
                        break;
                    }
                }
            }
        }

        if (deadPlayers == 3)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            StartCoroutine(TurnChange());
        }
    }
    #endregion
}