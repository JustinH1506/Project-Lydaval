using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public enum EnemyType
{
    THIEF,
    BOAR,
    WORM,
    BOSS
}

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

    [SerializeField] private GameObject player;

    [SerializeField] private int index;

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
    }

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeEnemyType());

            
        }
    }

    public void StartCombat()
    {
        EnemyManager.Instance.enemyType = enemyType;

        if(enemyType == EnemyType.WORM)
        {
            _objectData.data.enemies = true;

            _objectData.data.houses = true;

            _objectData.data.fightWon = true;
            
            data.isDead = true;
        }
        else if (enemyType == EnemyType.THIEF)
        {
            _objectData.data.enemies = true;

            _objectData.data.houses = true;
            
            data.isDead = true;
        }
        else if (enemyType == EnemyType.BOSS)
        {
            _objectData.data.enemies = true;

            _objectData.data.houses = true;
            
            _objectData.data.bossFightWon = true;
        }

        GameStateManager.instance.data.objectData = _objectData.data;
            
        SceneManager.LoadScene(2);  
    }

    IEnumerator ChangeEnemyType()
    {
        EnemyManager.Instance.enemyType = enemyType;
        
        if(enemyType != EnemyType.BOSS)
            data.isDead = true;

        yield return new WaitForSeconds(1f);
            
        SceneManager.LoadScene(2);
    }
}
