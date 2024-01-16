using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPartyStats : MonoBehaviour
{
    public enum CharacterType
    {
        Hero,
        Healer,
        Tank
    }

    public CharacterType _characterType;

    public TextMeshProUGUI statsText;

    public void Start()
    {
        ChangeStats();
    }

    public void ChangeStats()
    {
        if (_characterType == CharacterType.Hero)
        {
            Stats.Data statData = GameStateManager.instance.data.heroStatData;

            statsText.text = statData.unitName + "<br>" + "<br>" + "Level: " + statData.level + "<br>" + "<br>" +
                             statData.currentHealth + "/" + statData.maxHealth + "<br>" + "<br>" + "Attack: " +
                             statData.attack + "<br>" + "<br>" + "Defense: " + statData.defense + "<br>" + "<br>" +
                             "Speed: " + statData.speed + "<br>" + "<br>" + "Xp: " + statData.xp + "/" +
                             statData.neededXp;
        }
        
        if (_characterType == CharacterType.Healer)
        {
            Stats.Data statData = GameStateManager.instance.data.healerStatData;

            statsText.text = statData.unitName + "<br>" + "<br>" + "Level: " + statData.level + "<br>" + "<br>" +
                             statData.currentHealth + "/" + statData.maxHealth + "<br>" + "<br>" + "Attack: " +
                             statData.attack + "<br>" + "<br>" + "Defense: " + statData.defense + "<br>" + "<br>" +
                             "Speed: " + statData.speed + "<br>" + "<br>" + "Xp: " + statData.xp + "/" +
                             statData.neededXp;
        }
        
        if (_characterType == CharacterType.Tank)
        {
            Stats.Data statData = GameStateManager.instance.data.tankStatData;

            statsText.text = statData.unitName + "<br>" + "<br>" + "Level: " + statData.level + "<br>" + "<br>" +
                             statData.currentHealth + "/" + statData.maxHealth + "<br>" + "<br>" + "Attack: " +
                             statData.attack + "<br>" + "<br>" + "Defense: " + statData.defense + "<br>" + "<br>" +
                             "Speed: " + statData.speed + "<br>" + "<br>" + "Xp: " + statData.xp + "/" +
                             statData.neededXp;
        }
    }
}
