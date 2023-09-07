using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance { get; private set; }    

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There are more!!!");
        }
        else
        {
            Instance = this;
        }
    }
}
