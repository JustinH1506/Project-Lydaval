using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    #region TextMeshPriUGUI
    public TextMeshProUGUI unitName;

    public TextMeshProUGUI unitHp;
    #endregion

    #region Slider
    public Slider hpSlider;
    #endregion

    #region Methods

    /// <summary> Makes texts to Name of Unit, hp of unit and set the hp slider. </summary>
    public void SetHud(Stats stats)
    {
        unitName.text = stats.unitName;
        unitHp.text = stats.currentHealth + "/" + stats.maxHealth;
        hpSlider.maxValue = stats.maxHealth;
        hpSlider.value = stats.currentHealth;
    }

    /// <summary> Set hpSliders value to hp. /summary>

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
    #endregion
}