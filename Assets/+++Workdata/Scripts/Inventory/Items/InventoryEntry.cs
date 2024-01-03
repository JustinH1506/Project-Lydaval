using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    public void Initialize(Items item)
    {
        newName.text = item.newName;
        
        amount.text = item.amount.ToString() + "x";
    }
}
