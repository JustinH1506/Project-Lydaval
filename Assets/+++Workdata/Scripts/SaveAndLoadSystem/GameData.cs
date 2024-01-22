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

    [SerializeField] public AudioSettings.Data audioData;

    public Dictionary<string, Enemy.Data> enemyPositionByGuid = new Dictionary<string, Enemy.Data>();

    /// <summary>
    /// If we have no enemy we add to the list else we spawn enemy with certain Guid.
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="data"></param>
    public void SpawnEnemy(string guid, Enemy.Data data)
    {
        if (!enemyPositionByGuid.ContainsKey(guid))
            enemyPositionByGuid.Add(guid, data);
        else
            enemyPositionByGuid[guid] = data;
    }

    /// <summary>
    /// We call TryGetValue if ther is something we return data else we return null.
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public Enemy.Data GetEnemyPosition(string guid)
    {
        if (enemyPositionByGuid.TryGetValue(guid, out var data))
            return data;
        return null;
    }
}
