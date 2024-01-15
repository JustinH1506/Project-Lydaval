using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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

    public SelectCharacter characterSelect;

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
                
                inventoryEntryList[i].GetComponent<Button>().interactable = true;

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
                    GameObject spawnedEntry = Instantiate(entryPrefab, entrySpawnPositionList[i]);

                    inventoryEntryList[i] = spawnedEntry.GetComponent<InventoryEntry>();
                    
                    inventoryEntryList[i].Initialize(data.itemListData[i]);
                }
            }
        }
    }

    public void LookForSelected()
    {
        if (characterSelect != null)
        { 
            for (int i = 0; i < inventoryEntryList.Count; i++)
            {
                if(inventoryEntryList[i].selected)
                {
                    inventoryEntryList[i].selected = false;
                    
                    break;
                }
            }
        }
    }

    public void HealPlayer()
    {
        for (int i = 0; i < inventoryEntryList.Count; i++)
        {
            if (inventoryEntryList[i].selected && characterSelect.characterType == SelectCharacter.CharacterType.Hero)
            {
                GameStateManager.instance.data.heroStatData.currentHealth += data.itemListData[i].healAmount;

                data.itemListData[i].amount--;

                if(data.itemListData[i].amount <= 0)
                {
                    inventoryEntryList[i].selected = false;
                    
                    inventoryEntryList[i].GetComponent<Button>().interactable = false;
                }
                
                inventoryEntryList[i].Initialize(data.itemListData[i]);

                break;
            }
            
            if (inventoryEntryList[i].selected && characterSelect.characterType == SelectCharacter.CharacterType.Healer)
            {
                GameStateManager.instance.data.healerStatData.currentHealth += data.itemListData[i].healAmount;

                data.itemListData[i].amount--;
                
                if(data.itemListData[i].amount <= 0)
                {
                    inventoryEntryList[i].selected = false;
                    
                    inventoryEntryList[i].GetComponent<Button>().interactable = false;
                }
                
                inventoryEntryList[i].Initialize(data.itemListData[i]);

                break;
            }
            
            if (inventoryEntryList[i].selected && characterSelect.characterType == SelectCharacter.CharacterType.Tank)
            {
                GameStateManager.instance.data.tankStatData.currentHealth += data.itemListData[i].healAmount;

                data.itemListData[i].amount--;

                if(data.itemListData[i].amount <= 0)
                {
                    inventoryEntryList[i].selected = false;
                    
                    inventoryEntryList[i].GetComponent<Button>().interactable = false;
                }
                
                inventoryEntryList[i].Initialize(data.itemListData[i]);
                
                break;
            }
        }
    }
}