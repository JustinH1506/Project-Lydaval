using TMPro;
using UnityEngine;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    public void Initialize(Items item)
    {
        newName.text = item.newName;
        
        amount.text = item.amount + "x";
    }
}
