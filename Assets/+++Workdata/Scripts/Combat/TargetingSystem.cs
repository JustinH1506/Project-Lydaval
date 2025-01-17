using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    
    public int idChanger;

    /// <summary>
    /// changes enemyId depending on idChanger and sets indicator to that. 
    /// </summary>
    public void TargetEnemy()
    {
        battleSystem.targetingIndicatorList[battleSystem.enemyId].enabled = false;

        battleSystem.enemyId = idChanger;

        battleSystem.targetingIndicatorList[battleSystem.enemyId].enabled = true;
        
        if (idChanger > battleSystem.enemyStatsList.Count - 1)
        {
            battleSystem.enemyId = battleSystem.enemyStatsList.Count - 1;
        }
        //else if(idChanger < battleSystem.enemyStatsList.Count - 1)
    }
}