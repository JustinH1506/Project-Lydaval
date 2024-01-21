using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    THIEF,
    BOAR,
    WORM,
    BOSS
}
public class EnemyManager : MonoBehaviour
{
    public EnemyType enemyType;
    
    #region Instance
    public static EnemyManager instance { get; private set; }

    public int combatIndex;
    
    #endregion

    #region Methods
    /// <summary>
    /// Sets this to an instance and looks if there are more.
    /// </summary>
    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeEnemyType(EnemyType type)
    {
        enemyType = type;
        
        AudioManager.instance.CleanUp();
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.battleMusic);
        
        SceneManager.LoadScene(2);
    }
    #endregion
}
