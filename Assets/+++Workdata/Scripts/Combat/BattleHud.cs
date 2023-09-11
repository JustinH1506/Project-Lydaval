using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI unitName;

    public TextMeshProUGUI unitHp;

    public Slider hpSlider;

    public void SetHud(Stats stats)
    {
        unitName.text = stats.unitName;
        unitHp.text = stats.currentHealth + "/" + stats.maxHealth;
        hpSlider.maxValue = stats.maxHealth;
        hpSlider.value = stats.currentHealth;
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
}
