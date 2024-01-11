using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    public static Inventory instance { get; private set; }

    public List<Items> itemList, equipmentList;

    [SerializeField] private InventoryEntry inventoryEntry;

    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private Transform entrySpawnPosition, entrySpawnPosition2;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void AddItemToList(Items item)
    {
        itemList.Add(item);

        if(itemList.Count == 1)
            Instantiate(entryPrefab, entrySpawnPosition);
        else
            Instantiate(entryPrefab, entrySpawnPosition2);
        
        inventoryEntry.Initialize(item);
    }

    public void AddEquipmentToList(Items item)
    {
        equipmentList.Add(item);
        
        inventoryEntry.Initialize(item);
    }
}
