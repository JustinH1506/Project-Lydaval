using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    TurnChange,
    Won,
    Lost
}

public class BattleSystem : MonoBehaviour
{
    #region Scripts

    private EnemyManager enemyManager;

    #endregion
    
    #region GameObjects

    [SerializeField] private GameObject enemyButtons, skillButtons, tutorialPanel, levelUpPanel;
    
    #endregion

    #region Variables

    public int enemyId, enemyI, playerId, playerI, turnId;

    private int random1, random2 = 2;

    private int deadEnemies, deadPlayers;

    private int enemyAdder;

    public bool pressed, attacking;
    
    public string turnStart = "   turn begins";

    public TextMeshProUGUI unitName, inBetweenText, levelUpText;

    #endregion
    
    #region Buttons

    [SerializeField] private Button attackButton;
    [SerializeField] private Button skill, heal, taunt;

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
    [SerializeField] private List<GameObject> enemyWormList, enemyThiefList, enemyBossList;
    
    [SerializeField] private List<Stats> characterList;
    [SerializeField] public List<Stats> enemyStatsList, playerStatsList;
    
    [SerializeField] public List<Transform> playerBattleStationList, enemyBattleStationList;
    
    [SerializeField] public List<Image> targetingIndicatorList;
    
    [SerializeField] public List<Button> targetingButtonsList;
    
    [SerializeField] private List<Slider> enemyHpList, playerHpList;

    #endregion

    #region Methods

    /// <summary>
    /// We find the enemyManager.
    /// </summary>
    private void Awake()
    {
        enemyManager = GameObject.Find("---EnemyManager").GetComponent<EnemyManager>();
    }

    /// <summary> Set Battle State to START, call SetEnemiesToList and Start Coroutine SetUpBattle </summary>
    void Start()
    {
        state = BattleState.Start;
        SetEnemiesToList();
        StartCoroutine(SetUpBattle());
    }

    /// <summary> If the Battle state is WON We go back to the first scene and save the playerStats in the GameStateManager. 
    /// else if it´s LOST we load scene 0.
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
    /// We set the Player Name, set inBetweenText active, set it to the name and teh string turnStart,
    /// set the turn dependend on the charatcerlists turn id and make pressed false.
    /// </summary>
    private void PlayerTurn()
    {
        unitName.text = characterList[turnId].data.unitName;
        
        inBetweenText.gameObject.SetActive(true);

        inBetweenText.text = characterList[turnId].data.unitName + " " +  turnStart;
        
        characterList[turnId].SetTurn();

        pressed = false;
    }

    /// <summary>
    /// We remove Stats that are null in characterList, playerStatsList and playerHpList.
    /// </summary>
    private void ClearList()
    {
        characterList.RemoveAll(Stats => Stats == null);

        playerStatsList.RemoveAll(Stats => Stats == null);

        playerHpList.RemoveAll(Stats => Stats == null);
    }

    /// <summary>
    /// Depended on which enemyType it is we add that enemy to the list, make the targetingbuttons active and the enemyHpList active.
    /// </summary>
    private void SetEnemiesToList()
    {
        if (enemyManager.enemyType == EnemyType.THIEF)
        {
            enemyAdder = 0;

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyThiefList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
            }
        }

        if (enemyManager.enemyType == EnemyType.BOSS)
        {
            enemyAdder = 0;

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyBossList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
            }
        }

        if (enemyManager.enemyType == EnemyType.WORM)
        {
            enemyAdder = 0;

            for (int i = 0; i <= enemyAdder; i++)
            {
                enemyPrefabList.Add(enemyWormList[i]);
                targetingButtonsList[i].gameObject.SetActive(true);
                enemyHpList[i].gameObject.SetActive(true);
                tutorialPanel.SetActive(true);
            }
        }
    }

    /// <summary>
    /// We set the PlayerHud hp. 
    /// </summary>
    public void SetHp()
    {
        for (int i = 0; i < playerStatsList.Count; i++)
        {
            playerI = i;

            playerHud.SetPlayerHud(playerStatsList[i]);
        }
    }


    /// <summary> Returns if state is not PlayerTurn or the button is pressed.
    /// Make a local TargetingSystem named target and that equal to targetSystem.
    /// If attacking is true and targetSystems idChanger is equal to the enemyId pressed is true and
    /// we Start PlayerAttack courotine.
    /// Start PlayerAttack Coroutine. </summary>
    public void OnAttackButton(TargetingSystem targetSystem)
    {
        if (state != BattleState.PlayerTurn || pressed)
            return;

        TargetingSystem target = targetSystem;
        
        if (attacking && targetSystem.idChanger == enemyId )
        {
                pressed = true;

               StartCoroutine(PlayerAttack());
        }
    }

    /// <summary> Returns if state is not PlayerTurn or the button is pressed.
    /// Make a local TargetingSystem named target and that equal to targetSystem.
    /// If attacking is true and targetSystems idChanger is equal to the enemyId pressed is true and
    /// we Start PlayerAttackSkill courotine.
    /// Start PlayerAttack Coroutine. </summary>
    public void OnAttackSkillButton(TargetingSystem targetSystem)
    {
        if (state != BattleState.PlayerTurn || pressed)
            return;

        TargetingSystem target = targetSystem;
        
        if (!attacking && targetSystem.idChanger == enemyId )
        {
            pressed = true;

            StartCoroutine(PlayerAttackSkill());
        }
    }

    /// <summary> Returns if state is not PlayerTurn or the button is pressed.
    /// We make pressed true and Start Courotine PlayerHealSkill. 
    ///  </summary>
    public void OnPlayerHealSkillButton()
    {
        if (state != BattleState.PlayerTurn || pressed)
            return;
        
        pressed = true;

        StartCoroutine(PlayerHealSkill());
    }

    /// <summary> Returns if state is not PlayerTurn or the button is pressed.
    /// We make pressed true and Start Courotine OnPlayerTauntSkill. 
    ///  </summary>
    public void OnPlayerTauntSkillButton()
    {
        if (state != BattleState.PlayerTurn || pressed)
            return;

        pressed = true;

        StartCoroutine(PlayerTauntSkill());
    }
    
    #endregion

    #region IEnumerator

    /// <summary> We Instantiate the Characters, set them in the CharacterTurnList,
    /// sortTheList dependend on the speed value, reverse the list.
    /// If the Character in CharacterList with TurnId is enemy we start enemy turn else we start PlayerTurn. </summary>
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

    /// <summary>
    /// Set buttons, animation and sound when attacking, call SetEnemyHp,
    /// make a bool equal to enemyStatsLists enemyId and attack that with the damage method from that enemy.
    /// If won we set the clearUp audio  the audio and Initialize village music call EndBattle.
    /// else we call change Turn.
    /// </summary>
    IEnumerator PlayerAttack()
    {
        attackButton.interactable = false;
        
        characterList[turnId].anim.SetTrigger("Attack");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.attackSound, transform.position);

        yield return new WaitForSeconds(0.5f);
        
        enemyStatsList[enemyId].anim.SetTrigger("Damage");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.hurtSound, transform.position);
        
        bool isDead = enemyStatsList[enemyId].TakeDamage(characterList[turnId].data.attack);

        if (enemyStatsList[enemyId].data.currentHealth <= 0)
            enemyStatsList[enemyId].data.currentHealth = 0;

        enemyHud.SetEnemyHp(enemyStatsList[enemyId].data.currentHealth, enemyStatsList[enemyId].data.maxHealth);

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
                    
                    attacking = false;

                    break;
                }
            }
        }

        if (deadEnemies == enemyAdder + 1)
        {
            state = BattleState.Won;
            
            AudioManager.instance.CleanUp();
            
            AudioManager.instance.InitializeMusic(FmodEvents.instance.villageMusic);
            
            EndBattle();
        }
        else
        {
            attackButton.interactable = true;
            
            characterList[turnId].SetTurnFalse();

            attacking = false;
            
            state = BattleState.TurnChange;
            
            StartCoroutine(TurnChange());
        }
    }

    
    /// <summary>
    /// Set buttons, animation and sound when attacking, call SetEnemyHp,
    /// make a bool equal to enemyStatsLists enemyId and attack that with the damage method from that enemy times 2.
    /// If won we set the clearUp audio  the audio and Initialize village music call EndBattle.
    /// else we call change Turn.
    /// </summary>
    IEnumerator PlayerAttackSkill()
    {
        skill.interactable = false;
        
        characterList[turnId].anim.SetTrigger("Attack");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.attackSound, transform.position);

        yield return new WaitForSeconds(0.75f);
        
        enemyStatsList[enemyId].anim.SetTrigger("Damage");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.hurtSound, transform.position);
        
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
            
            AudioManager.instance.CleanUp();
            
            AudioManager.instance.InitializeMusic(FmodEvents.instance.villageMusic);
            
            EndBattle();
        }
        else
        {
            characterList[turnId].SetTurnFalse();
            
            state = BattleState.TurnChange;
            
            attacking = true;

            StartCoroutine(TurnChange());
        }
    }

    /// <summary>
    /// If the player isnt at full currentHealth we heal the selected characters hp plus ten.
    /// If the eny is at full health we return.
    /// If it healed but the character is over the maximumHealth we set the currentHealth to maximum health.
    /// </summary>
    IEnumerator PlayerHealSkill()
    {
        heal.interactable = false;
        
        characterList[turnId].cooldown = 2;

        for (int i = 0; i < playerStatsList.Count; i++)
        {
            if (playerStatsList[i].select)
            {
                if(playerStatsList[i].data.currentHealth >= playerStatsList[i].data.maxHealth)
                {
                    playerStatsList[i].data.currentHealth = playerStatsList[i].data.maxHealth;

                    playerStatsList[i].select = false;
                    
                    heal.interactable = true;

                    pressed = false;
                    
                    break;
                }
                
                if (playerStatsList[i].data.currentHealth < playerStatsList[i].data.maxHealth)
                {
                    characterList[turnId].anim.SetTrigger("Attack");
        
                    characterList[turnId].healButton.transform.parent.gameObject.SetActive(false);

                    yield return new WaitForSeconds(0.5f);
                    
                    playerStatsList[i].data.currentHealth += 10;
                    
                    if(playerStatsList[i].data.currentHealth >= playerStatsList[i].data.maxHealth)
                    {
                        playerStatsList[i].data.currentHealth = playerStatsList[i].data.maxHealth;
                    }
                    playerStatsList[i].select = false;
                }

                heal.interactable = true;
                
                SetHp();
                
                characterList[turnId].SetTurnFalse();
                
                state = BattleState.TurnChange;
                
                StartCoroutine(TurnChange());
            }
        }
        yield return null;
    }

    /// <summary>
    /// Make the taunt true and sets a cooldown to 3.
    /// Sets the attack animation, call SetTurnFalse for current Character and set call Turn Change.
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerTauntSkill()
    {
        taunt.interactable = false;
        
        characterList[turnId].taunt = true;
        
        characterList[turnId].cooldown = 3;
        
        characterList[turnId].anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        taunt.interactable = true;
        
        characterList[turnId].SetTurnFalse();
        
        state = BattleState.TurnChange;

        StartCoroutine(TurnChange());
    }

    
    /// <summary>
    /// Set levelUpPanel active waits for 2.5 seconds and sets it inactive.
    /// </summary>
    /// <returns></returns>
    public IEnumerator LevelUp()
    {
        levelUpPanel.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        
        levelUpPanel.SetActive(false);
    }

    /// <summary>
    /// turnId gets +1 and dependend on which charatcer is the next in the CharacterList we call enemy or Playerturn.
    /// </summary>
    /// <returns></returns>
    IEnumerator TurnChange()
    {
        turnId++;
        
        enemyButtons.SetActive(false);
            
        skillButtons.SetActive(false);

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

        if (characterList[turnId].data.types != CharacterTypes.Enemy)
        {
            state = BattleState.PlayerTurn;
            
            yield return new WaitForSeconds(1.5f);
            
            PlayerTurn();
        }
        else
        {
            state = BattleState.EnemyTurn;
            
            state = BattleState.TurnChange;
            
            yield return new WaitForSeconds(1.5f);

            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary> Set buttons, animation and sound when attacking, call SetEnemyHp,
    /// make a bool equal to enemyStatsLists enemyId and attack that with the damage method from that enemy.
    /// If lost we set the clearUp audio  the audio and Initialize mainMenu music call EndBattle.
    /// else we call change Turn.
    /// </summary>
    IEnumerator EnemyTurn()
    {
        inBetweenText.gameObject.SetActive(true);
        
        inBetweenText.text =  characterList[turnId].data.unitName + " " + turnStart;
        
        yield return new WaitForSeconds(2f);

        playerId = Random.Range(random1, random2);

        for (int i = 0; i < playerStatsList.Count; i++)
        {
            if (playerStatsList[i].taunt)
            {
                playerId = i;
            }
        }

        characterList[turnId].anim.SetTrigger("Attacking");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.attackSound, transform.position);

        yield return new WaitForSeconds(0.75f);
        
        playerStatsList[playerId].anim.SetTrigger("Damage");
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.hurtSound, transform.position);

        bool isDead = playerStatsList[playerId].TakeDamage(characterList[turnId].data.attack);

        if (playerStatsList[playerId].data.currentHealth <= 0)
            playerStatsList[playerId].data.currentHealth = 0;

        playerHud.SetPlayerHp(playerStatsList[playerId].data.currentHealth, playerStatsList[playerId].data.maxHealth);
        
        yield return new WaitForSeconds(0.5f);

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
            
            AudioManager.instance.CleanUp();
            
            EndBattle();
        }
        else
        {
            state = BattleState.TurnChange;
            
            StartCoroutine(TurnChange());
        }
    }

    /// <summary>
    /// Makes the levelUpText to the stats that are getting changed after getting a level up. 
    /// </summary>
    public void LevelUpText()
    {
        string breakLineHash = "<br>" + "<br>";
        
        Stats.Data statData = characterList[turnId].data;
        
        levelUpText.text = characterList[turnId].data.unitName + breakLineHash + "Level: " + statData.level + breakLineHash + "Hp: "+
                           statData.currentHealth + "/" + statData.maxHealth + breakLineHash + "Attack: " +
                           statData.attack + breakLineHash + "Defense: " + statData.defense + breakLineHash +
                           "Speed: " + statData.speed + breakLineHash + "Xp: " + statData.xp + "/" +
                           statData.neededXp;
    }
    #endregion
}