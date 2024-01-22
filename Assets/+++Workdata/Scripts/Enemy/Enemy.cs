using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string uniqueGuid;
    
    [System.Serializable]
    public class Data
    {
        public SaveableVector3 position;
        public bool isDead;
    }

    [SerializeField] private Data data;

    [SerializeField] private ObjectData _objectData;
    
    public EnemyType enemyType;

    public EnemyManager enemyManager;

    [SerializeField] private GameObject player;

    [SerializeField] private int index;

    /// <summary>
    /// We create a local loadedData which is the GetEnemyPosition from the GameStateManager.
    /// If loadedData is not null we set our data to loadedData,
    /// We set position to data position,
    /// If isDead is true we set the gamObject inActive.
    /// Else we call the SpawnEnemy method with the uniqueGuid with data and set datas position to transforms position.
    /// We set the GameStateManagers positionData equal to our positionData.
    /// </summary>
    private void Start()
    {
        Data loadedData = GameStateManager.instance.data.GetEnemyPosition(uniqueGuid);

        if (loadedData != null)
        {
            data = loadedData;

            transform.position = data.position;
            
            if(data.isDead)
                gameObject.SetActive(false);
        }
        else
        {
            GameStateManager.instance.data.SpawnEnemy(uniqueGuid, data);
            data.position = transform.position;
        }
        
        data.position = transform.position;

        enemyManager = EnemyManager.instance;
    }

    /// <summary>
    /// If the gameObject is not in a scene uniqueGuid is blank.
    /// Else we get a Guid from the system. 
    /// </summary>
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(gameObject.scene.name))
        {
            uniqueGuid = "";
        }
        else if (string.IsNullOrEmpty(uniqueGuid))
        {
            uniqueGuid = System.Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// We call AudioManagers Clean Up, Change the enemyManagers enemyType to enemyType of this object,
    /// if enemyType is not Boss we set isDead true.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.CleanUp();
            
            enemyManager.ChangeEnemyType(enemyType);
            
            if(enemyType != EnemyType.BOSS)
                data.isDead = true;
        }
    }

    /// <summary>
    /// We call AudioManagers Clean Up, Change the enemyManagers enemyType to enemyType of this object,
    /// If it is not the Boss we set isDead to true else we set bossFightWon to true.
    /// Safe the Object data in GameStateManagers objectData and load scene 2. 
    /// </summary>
    public void StartCombat()
    {
        AudioManager.instance.CleanUp();
        
        enemyManager.ChangeEnemyType(enemyType);

        if(enemyType == EnemyType.WORM)
        {
            _objectData.data.fightWon = true;
            
            data.isDead = true;
        }
        else if (enemyType == EnemyType.THIEF)
        {
            data.isDead = true;
        }
        else if (enemyType == EnemyType.BOSS)
        {
            _objectData.data.bossFightWon = true;
        }

        GameStateManager.instance.data.objectData = _objectData.data;
            
        SceneManager.LoadScene(2);  
    }
}
