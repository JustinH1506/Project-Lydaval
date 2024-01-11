using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    public EnemyManager enemyManager;

    [SerializeField] private Data data;
    
    public EnemyType enemyType;

    private void Start()
    {
        Data loadedData = GameStateManager.instance.data.GetEnemyPosition(uniqueGuid);

        if (loadedData != null)
        {
            data = loadedData;

            transform.position = data.position;
            
            if(data.isDead)
                Destroy(gameObject);
                
        }
        else
        {
            GameStateManager.instance.data.SpawnEnemy(uniqueGuid, data);
            data.position = transform.position;
        }
    }

    private void LateUpdate()
    {
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
            enemyManager.enemyType = enemyType;

            data.isDead = true;
            
            SceneManager.LoadScene(2);
        }
    }
}
