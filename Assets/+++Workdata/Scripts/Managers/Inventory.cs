using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public List<Items.Data> itemListData;
    }

    public Data data;

    [SerializeField] private InventoryEntry inventoryEntry;

    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private List<Transform> entrySpawnPositionList;
    
    [SerializeField] private List<InventoryEntry> inventoryEntryList;

    private void Start()
    {
        SpawnAfterLoading();
    }

    public void AddItemToList(Items.Data item)
    {
        for (int i = 0; i < data.itemListData.Count; i++)
        {
            if(data.itemListData[i].newName == item.newName)
            {
                data.itemListData[i].amount += item.amount;

                inventoryEntryList[i].Initialize(data.itemListData[i]);

                GameStateManager.instance.data.inventoryData = data;
                
                break;
            }
            
            if (data.itemListData[i].newName == "")
            {
                inventoryEntry.Initialize(item);
                
                GameObject spawnedEntry = Instantiate(entryPrefab, entrySpawnPositionList[i]);

                inventoryEntryList[i] = spawnedEntry.GetComponent<InventoryEntry>();

                data.itemListData[i] = item;
                
                GameStateManager.instance.data.inventoryData = data;

                break;
            }
        }
    }

    private void SpawnAfterLoading()
    {
        Data localData = GameStateManager.instance.data.inventoryData;

        if (localData == null)
        { 
            GameStateManager.instance.data.inventoryData = data;
        }
        else
        {
            data = GameStateManager.instance.data.inventoryData;
            for (int i = 0; i < data.itemListData.Count; i++)
            { 
                if(data.itemListData[i].newName != "")
                {
                    inventoryEntryList[i] = inventoryEntry;
                    
                    inventoryEntry.Initialize(data.itemListData[i]);
                    
                    Instantiate(entryPrefab, entrySpawnPositionList[i]);
                }
            }
        }
    }
}