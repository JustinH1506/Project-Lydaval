using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public List<Items.Data> itemListData, setItemActiveList;
    }

    public Data data;

    [SerializeField] private InventoryEntry inventoryEntry;

    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private List<Transform> entrySpawnPositionList;
    
    [SerializeField] private List<InventoryEntry> inventoryEntryList;

    [SerializeField] private List<CharacterPartyStats> partyStatsList;

    public SelectCharacter characterSelect;

    /// <summary>
    /// call SpawnAfterLoading.
    /// </summary>
    private void Awake()
    {
        SpawnAfterLoading();
    }

    /// <summary>
    /// If nam of newItem is equal to item we get the amount higher.
    /// Else we Instantiate it. 
    /// </summary>
    /// <param name="item"></param>
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

    /// <summary>
    /// If localData is null we set our data else we spawn items with the data from the saved data. 
    /// </summary>
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

    /// <summary>
    /// If its selected we set selected to false.
    /// </summary>
    public void LookForSelected()
    {
        if (characterSelect != null)
        { 
            
            for (int i = 0; i < inventoryEntryList.Count; i++)
            {
                if(inventoryEntryList[i] != null)
                {
                    if (inventoryEntryList[i].selected)
                    {
                        inventoryEntryList[i].selected = false;

                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// If the Character has not full health the characters current health will get + the heal amount of the item. 
    /// </summary>
    public void HealPlayer()
    {
        for (int i = 0; i < inventoryEntryList.Count; i++)
        {
            if (inventoryEntryList[i].selected && characterSelect.characterType == SelectCharacter.CharacterType.Hero)
            {
                GameStateManager.instance.data.heroStatData.currentHealth += data.itemListData[i].healAmount;

                Stats.Data statsData = GameStateManager.instance.data.heroStatData;
                
                if(statsData.currentHealth < statsData.maxHealth)
                {
                    data.itemListData[i].amount--;

                    if (statsData.currentHealth > statsData.maxHealth)
                    {
                        statsData.currentHealth = statsData.maxHealth;
                        
                        partyStatsList[i].ChangeStats();
                    }
                }
                
                if (statsData.currentHealth > statsData.maxHealth)
                {
                    statsData.currentHealth = statsData.maxHealth;
                    
                    partyStatsList[i].ChangeStats();
                }

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

                Stats.Data statsData = GameStateManager.instance.data.healerStatData;
                
                if(statsData.currentHealth < statsData.maxHealth)
                {
                    data.itemListData[i].amount--;

                    if (statsData.currentHealth > statsData.maxHealth)
                    {
                        statsData.currentHealth = statsData.maxHealth;
                        
                        partyStatsList[i].ChangeStats();
                    }
                }
                
                if (statsData.currentHealth > statsData.maxHealth)
                {
                    statsData.currentHealth = statsData.maxHealth;
                    
                    partyStatsList[i].ChangeStats();
                }
                
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

                Stats.Data statsData = GameStateManager.instance.data.tankStatData;
                
                if(statsData.currentHealth < statsData.maxHealth)
                {
                    data.itemListData[i].amount--;

                    if (statsData.currentHealth > statsData.maxHealth)
                    {
                        statsData.currentHealth = statsData.maxHealth;
                        
                        partyStatsList[i].ChangeStats();
                    }
                }
                
                if (statsData.currentHealth > statsData.maxHealth)
                {
                    statsData.currentHealth = statsData.maxHealth;
                    
                    partyStatsList[i].ChangeStats();
                }

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