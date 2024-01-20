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

    [SerializeField] public ObjectData.Data objectData;

    public Dictionary<string, Enemy.Data> enemyPositionByGuid = new Dictionary<string, Enemy.Data>();

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
}
