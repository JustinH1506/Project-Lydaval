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

    public void Start()
    {
        ChangeStats();
    }

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

    private static string StatCollectionString(Stats.Data statData)
    {
        return statData.unitName + breakLineHash + "Level: " + statData.level + breakLineHash + "Hp: "+
               statData.currentHealth + "/" + statData.maxHealth + breakLineHash + "Attack: " +
               statData.attack + breakLineHash + "Defense: " + statData.defense + breakLineHash +
               "Speed: " + statData.speed + breakLineHash + "Xp: " + statData.xp + "/" +
               statData.neededXp;
    }
}