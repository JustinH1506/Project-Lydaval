using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public List<Items.Data> itemListData, equipmentList;
    }

    public Data data;

    [SerializeField] private InventoryEntry inventoryEntry;

    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private List<Transform> entrySpawnPositionList;
    
    public void AddItemToList(Items.Data item)
    {
        for (int i = 0; i < data.itemListData.Count; i++)
        {
            if(data.itemListData[i].newName == item.newName)
            {
                data.itemListData[i].amount += item.amount;

                inventoryEntry.Initialize(data.itemListData[i]);

                GameStateManager.instance.data.inventoryData = data;
                
                break;
            }
            
            if (data.itemListData[i].newName == "")
            {
                Instantiate(entryPrefab, entrySpawnPositionList[i]);

                data.itemListData[i] = item;
                
                inventoryEntry.Initialize(item);
                
                GameStateManager.instance.data.inventoryData = data;

                break;
            }
        }
    }
    
    public void AddEquipmentToList(Items.Data item)
    {
        data.equipmentList.Add(item);
        
        inventoryEntry.Initialize(item);
    }
}