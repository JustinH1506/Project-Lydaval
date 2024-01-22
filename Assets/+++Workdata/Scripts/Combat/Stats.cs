using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum CharacterTypes
{
    Enemy,
    Hero,
    Tank,
    Healer
}
public class Stats : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string unitName;

        public int maxHealth;

        public int currentHealth;

        public int speed;

        public int attack;

        public int defense;

        public int level = 1;

        public int xp;

        public int neededXp;

        public int enemyGiveXp;

        public CharacterTypes types;
    }
    
    #region Scripts

    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] public Data data;
    
    #endregion
    
    #region Variables
    
    public GameObject skillButton, healButton;
    
    public Animator anim;

    public int cooldown;

    public bool select;

    public bool taunt;
    
    #endregion

    #region Methods
    
    /// <summary>
    /// Look which CharacterType this is and sets skillButton and heal button to those. 
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        battleSystem = GameObject.Find("Battle_System").GetComponent<BattleSystem>();
        
        data.currentHealth = data.maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            data.types = CharacterTypes.Enemy;
        }
        
        if (data.types == CharacterTypes.Hero)
        {
            skillButton = GameObject.Find("Player_Skill_Button");
            healButton = GameObject.Find("Player_Heal_Hero_Button");
        }       
        else if(data.types == CharacterTypes.Healer)
        {
            skillButton = GameObject.Find("Player_Heal_Button");
            healButton = GameObject.Find("Player_Heal_Healer_Button");
        }        
        else if(data.types == CharacterTypes.Tank)
        {
            skillButton = GameObject.Find("Player_Taunt_Button");
            healButton = GameObject.Find("Player_Heal_Tank_Button");
        }
        
        if(skillButton != null)
        {
            skillButton.SetActive(false);
        }

        if (healButton != null)
        {
            healButton.GetComponent<Button>().onClick.AddListener(Select);
            healButton.GetComponent<Button>().onClick.AddListener(battleSystem.OnPlayerHealSkillButton);
        }
    }
    
    /// <summary>
    /// Gets maxHealth and currentHealth to a string.
    /// Set Parent from heal and skill button inactive. 
    /// </summary>
    private void Start()
    {
        data.maxHealth.ToString();
        data.currentHealth.ToString();
        
        if (healButton != null)
        {
            healButton.gameObject.transform.parent.gameObject.SetActive(false);
            skillButton.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Calls level up if xp are bigger or equal to neededXp.
    /// Call LevelUpText and LevelUp from battleSystem. 
    /// </summary>
    public void HasEnoughXp()
    {
      if (data.xp >= data.neededXp)
            LevelUp();

      battleSystem.LevelUpText();

      battleSystem.LevelUp();
    }

    /// <summary>
    /// Calculates the currentHealth minus the dmg and returns true if the currentHealth is equal to or less then 0.
    /// </summary>
    public bool TakeDamage(int dmg)
    {
        data.currentHealth -= dmg;

        if (data.currentHealth <= 0)
            return true;
        
        return false;
    }

    /// <summary>
    /// Set depending on which CharacterType it is the stats between some numbers. 
    /// </summary>
    public void LevelUp()
    {
        if (data.types == CharacterTypes.Hero)
        {
            data.level++;

            int randomNumber = Random.Range(5, 15);

            data.maxHealth += randomNumber;

            data.currentHealth += randomNumber;

            data.speed += Random.Range(0, 3);

            data.attack += Random.Range(2, 5);

            data.defense += Random.Range(1, 3);

            data.xp -= data.neededXp;

            data.neededXp = data.neededXp *= 2;
        }
        
        if (data.types == CharacterTypes.Healer)
        {
            data.level++;

            int randomNumber = Random.Range(10, 20);

            data.maxHealth += randomNumber;

            data.currentHealth += randomNumber;

            data.speed += Random.Range(2, 5);

            data.attack += Random.Range(0, 3);

            data.defense += Random.Range(0, 3);
            
            data.xp -= data.neededXp;

            data.neededXp = (data.neededXp *= 2);
        }
        
        if (data.types == CharacterTypes.Tank)
        {
            data.level++;

            int randomNumber = Random.Range(20, 30);

            data.maxHealth += randomNumber;

            data.currentHealth += randomNumber;

            data.speed += Random.Range(0, 3);

            data.attack += Random.Range(0, 3);

            data.defense += Random.Range(3, 5);
            
            data.xp -= data.neededXp;

            data.neededXp = (data.neededXp *= 2);
        }
        
        battleSystem.SetHp();
    }

    /// <summary>
    /// set select to true.
    /// </summary>
    public void Select()
    {
        select = true;
    }


    /// <summary>
    /// Set skillButton active.
    /// If cooldwon is higher than 0 the button is not interactable else it is. 
    /// </summary>
    public void SetTurn()
    {
        skillButton.SetActive(true);

        if (cooldown > 0)
            skillButton.GetComponent<Button>().interactable = false;
        else
            skillButton.GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// Set skill button inactive. 
    /// </summary>
    public void SetTurnFalse()
    {
        skillButton.SetActive(false);
    }

    #endregion
}