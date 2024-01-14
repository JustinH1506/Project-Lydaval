using System;
using TMPro;
using UnityEngine;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    public void Initialize(Items.Data itemData)
    {
        newName.text = itemData.newName;
        
        amount.text = itemData.amount + "x";
    }
}
