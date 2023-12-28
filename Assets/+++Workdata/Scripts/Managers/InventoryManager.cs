using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    public static InventoryManager instance { get; private set; }

    public List<GameObject> itemList, equipmentList;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    
}
