using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    THIEF,
    BOAR,
    WORM
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
    
    public EnemyType enemyType;

    [SerializeField] private GameObject player;

    private SpriteRenderer sr;

    [SerializeField] private int index;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

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

    private void Update()
    {
        if (transform.position.y < player.transform.position.y)
            sr.sortingOrder = 10;
        else
        {
            sr.sortingOrder = -10;
        }
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
            EnemyManager.Instance.enemyType = enemyType;

            data.isDead = true;
            
            SceneManager.LoadScene(index);
        }
    }

    public void StartCombat()
    {
        EnemyManager.Instance.enemyType = enemyType;

        EnemyManager.Instance.combatIndex = 2;

        data.isDead = true;
            
        SceneManager.LoadScene(index);  
    }
}
