using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [SerializeField] public PlayerMove.Data positionData;

    [SerializeField] public Stats.Data heroStatData;
    
    [SerializeField] public Stats.Data healerStatData;
    
    [SerializeField] public Stats.Data tankStatData;

    [SerializeField] public Inventory.Data inventoryData;

    public Dictionary<string, Enemy.Data> enemyPositionByGuid = new Dictionary<string, Enemy.Data>();
    
    public Dictionary<string, Items.PositionData> itemPositionByGuid = new Dictionary<string, Items.PositionData>();

    public void SpawnEnemy(string guid, Enemy.Data data)
    {
        if (!enemyPositionByGuid.ContainsKey(guid))
            enemyPositionByGuid.Add(guid, data);
        else
            enemyPositionByGuid[guid] = data;
    }

    public Enemy.Data GetEnemyPosition(string guid)
    {
        if (enemyPositionByGuid.TryGetValue(guid, out var data))
            return data;
        return null;
    }
    
    public void SpawnItem(string guid, Items.PositionData data)
    {
        if (!itemPositionByGuid.ContainsKey(guid))
            itemPositionByGuid.Add(guid, data);
        else
            itemPositionByGuid[guid] = data;
    }

    public Items.PositionData GetItemPosition(string guid)
    {
        if (itemPositionByGuid.TryGetValue(guid, out var data))
            return data;
        return null;
    }
}
