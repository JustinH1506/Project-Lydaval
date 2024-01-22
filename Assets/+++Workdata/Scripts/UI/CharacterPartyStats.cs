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

    const string breakLineHash = "<br>" + "<br>";

    /// <summary>
    /// call ChangeStats. 
    /// </summary>
    public void Start()
    {
        ChangeStats();
    }

    /// <summary>
    /// Get the stats from GameStateManager and set them depending on the characterType.
    /// </summary>
    public void ChangeStats()
    {
        Stats.Data statData = new();
        var data = GameStateManager.instance.data;

        if (_characterType == CharacterType.Hero)
            statData = data.heroStatData;

        else if (_characterType == CharacterType.Healer)
            statData = data.healerStatData;

        else if (_characterType == CharacterType.Tank)
            statData = data.tankStatData;

        statsText.text = StatCollectionString(statData);
    }

    /// <summary>
    /// return a string that shows all stats in the inventory screen. 
    /// </summary>
    /// <param name="statData"></param>
    /// <returns></returns>
    private static string StatCollectionString(Stats.Data statData)
    {
        return statData.unitName + breakLineHash + "Level: " + statData.level + breakLineHash + "Hp: "+
               statData.currentHealth + "/" + statData.maxHealth + breakLineHash + "Attack: " +
               statData.attack + breakLineHash + "Defense: " + statData.defense + breakLineHash +
               "Speed: " + statData.speed + breakLineHash + "Xp: " + statData.xp + "/" +
               statData.neededXp;
    }
}