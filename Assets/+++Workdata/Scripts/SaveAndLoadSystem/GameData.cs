using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    [SerializeField] public PlayerMove.Data positionData;

    [SerializeField] public StatManager.HeroData heroStatData;
    
    [SerializeField] public StatManager.HealerData healerStatData;
    
    [SerializeField] public StatManager.TankData tankStatData;
}
