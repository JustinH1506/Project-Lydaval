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
    public Animator anim;

    public int cooldown;

    public bool select;

    public bool taunt;

    public GameObject skillButton;
    
    #endregion

    #region Methods

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
            skillButton = GameObject.Find("Player_Skill_Button");
        else if(data.types == CharacterTypes.Healer)
            skillButton = GameObject.Find("Player_Heal_Button");
        else if(data.types == CharacterTypes.Tank)
            skillButton = GameObject.Find("Player_Taunt_Button");
    }
    
    /// <summary>
    /// Gets maxHealth and currentHealth to a string.
    /// </summary>
    private void Start()
    {
        data.maxHealth.ToString();
        data.currentHealth.ToString();
    }


    public void HasEnoughXp()
    {
      if (data.xp >= data.neededXp)
            LevelUp();
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
    /// heals the current health by a certain amount and makes the currentHealth to maxHealth if it goes over maxHealth.
    /// </summary>
    public void Heal(int healing)
    {
        data.currentHealth += healing;
        
        if (data.currentHealth > data.maxHealth)
        {
            data.currentHealth = data.maxHealth;
        }
    }

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

    public void Select()
    {
        select = true;
    }

    public void SetTurn()
    {
        skillButton.SetActive(true);
    }

    public void SetTurnFalse()
    {
        skillButton.SetActive(false);
    }

    #endregion
}