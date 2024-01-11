using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    [SerializeField] public PlayerMove.Data positionData;

    [SerializeField] public Stats.Data heroStatData;
    
    [SerializeField] public Stats.Data healerStatData;
    
    [SerializeField] public Stats.Data tankStatData;
}
