using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{

    [System.Serializable]
    public class Data
    {
        public bool houses, enemies;
    }

    public Data data;

    [SerializeField] private GameObject houses, burnedHouses, enemy;

    private void Start()
    {
        var loadedData = GameStateManager.instance.data.objectData;

        if (loadedData != null)
        {
            data = loadedData;

            if (data.houses)
            {
                houses.SetActive(false);
                burnedHouses.SetActive(true);
            }
            
            if(data.enemies)
                enemy.SetActive(true);
        }

        GameStateManager.instance.data.objectData = data;
    }
}
