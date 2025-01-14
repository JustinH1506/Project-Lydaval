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

    /// <summary> Makes texts to Name of Unit, hp of unit and sets the hp sliders values. </summary>
    public void SetPlayerHud(Stats stats)
    {
        unitName[battleSystem.playerI].text = stats.data.unitName;
        unitHp[battleSystem.playerI].text = stats.data.currentHealth + "/" + stats.data.maxHealth;
        hpSlider[battleSystem.playerI].maxValue = stats.data.maxHealth;
        hpSlider[battleSystem.playerI].value = stats.data.currentHealth;
    }

    /// <summary>
    /// Remove unitHp and slider from list.
    /// </summary>
    public void ClearPlayerStats()
    {
        unitHp.Remove(unitHp[battleSystem.playerId]);

        hpSlider.Remove(hpSlider[battleSystem.playerId]);
    }
    
    /// <summary> Makes texts to Name of Unit, hp of unit and set the hp slider. </summary>
    public void SetEnemyHud(Stats stats)
    {
        unitName[battleSystem.enemyI].text = stats.data.unitName;
        unitHp[battleSystem.enemyI].text = stats.data.currentHealth + "/" + stats.data.maxHealth;
        hpSlider[battleSystem.enemyI].maxValue = stats.data.maxHealth;
        hpSlider[battleSystem.enemyI].value = stats.data.currentHealth;
        unitName[battleSystem.enemyI].gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }

    /// <summary> Sets hpSliders value to hp. </summary>
    public void SetPlayerHp(int hp, int maxHp)
    {
        hpSlider[battleSystem.playerId].value = hp;

        unitHp[battleSystem.playerId].text = hp + "/" + maxHp;
    }
    
    /// <summary> Sets hpSliders value to hp. </summary>
    public void SetEnemyHp(int hp, int maxHp)
    {
        hpSlider[battleSystem.enemyId].value = hp;
        
        unitHp[battleSystem.enemyId].text = hp + "/" + maxHp;
    }
    #endregion
}