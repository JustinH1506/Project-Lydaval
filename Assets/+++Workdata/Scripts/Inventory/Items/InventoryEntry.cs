using System;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    public bool selected;

    [SerializeField] private GameObject selectButtons;

    [SerializeField] private Inventory _inventory;

    [SerializeField] private GameObject apple, bread;

    private void Awake()
    {
        _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        if(selected == false)
            selectButtons.SetActive(false);
        else if(selected)
            selectButtons.SetActive(true);
    }

    public void Initialize(Items.Data itemData)
    {
        newName.text = itemData.newName;
        
        amount.text = itemData.amount + "x";
        
        if(itemData.newName == ("Apple"))
        {
            apple.SetActive(true);
            
            bread.SetActive(false);
        }
        
        if(itemData.newName == ("Bread"))
        {
            bread.SetActive(true);
            
            apple.SetActive(false);
        }
    }

    public void Select()
    {
        if(selected)
        {
            selected = false;
            
            _inventory.LookForSelected();
        }
        else if(!selected)
        {
            _inventory.LookForSelected();
            
            selected = true;
        }
    }
}
