using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    #region Scripts

    [SerializeField] private BattleSystem battleSystem;

    #endregion
    
    #region TextMeshProUGUI

    public List<TextMeshProUGUI> unitName;

    public List<TextMeshProUGUI> unitHp;

    #endregion

    #region Slider

    public List<Slider> hpSlider;

    #endregion

    #region Methods

    /// <summary> Makes texts to Name of Unit, hp of unit and set the hp slider. </summary>
    public void SetPlayerHud(Stats stats)
    {
        unitName[battleSystem.playerI].text = stats.unitName;
        unitHp[battleSystem.playerI].text = stats.currentHealth + "/" + stats.maxHealth;
        hpSlider[battleSystem.playerI].maxValue = stats.maxHealth;
        hpSlider[battleSystem.playerI].value = stats.currentHealth;
    }
    
    /// <summary> Makes texts to Name of Unit, hp of unit and set the hp slider. </summary>
    public void SetEnemyHud(Stats stats)
    {
        unitName[battleSystem.enemyI].text = stats.unitName;
        unitHp[battleSystem.enemyI].text = stats.currentHealth + "/" + stats.maxHealth;
        hpSlider[battleSystem.enemyI].maxValue = stats.maxHealth;
        hpSlider[battleSystem.enemyI].value = stats.currentHealth;
    }

    /// <summary> Sets hpSliders value to hp. /summary>
    public void SetPlayerHp(int hp)
    {
        hpSlider[battleSystem.playerId].value = hp;
    }
    
    /// <summary> Sets hpSliders value to hp. /summary>
    public void SetEnemyHp(int hp)
    {
        hpSlider[battleSystem.enemyId].value = hp;
        
    }
    #endregion
}