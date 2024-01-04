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

public class EnemySpawnScript : MonoBehaviour
{
    public EnemyManager enemyManager;
    
    public EnemyType enemyType;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyManager.enemyType = enemyType;
            
            SceneManager.LoadScene(2);
        }
    }
}
