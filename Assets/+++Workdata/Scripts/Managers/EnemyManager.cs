using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyType enemyType;

    public BattleSystem battleSystem;
    
    #region Instance
    public static EnemyManager Instance { get; private set; }
    #endregion

    #region Methods
    /// <summary>
    /// Sets this to an instance and looks if there are more.
    /// </summary>
    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There are more!!!");
        }
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    
}
