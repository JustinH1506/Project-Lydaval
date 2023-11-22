using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager Instance { get; private set; }
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