using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyType enemyType;
    
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
            Destroy(gameObject);
        }
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    
}
