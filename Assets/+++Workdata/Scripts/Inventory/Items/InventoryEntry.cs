using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    [SerializeField] private GameObject selectButtons;

    [SerializeField] private GameObject apple, bread, itemInfo;

    [SerializeField] private Inventory _inventory;

    private Button item;

    private BaseEventData highlighted;
    
    public bool selected;
    
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
