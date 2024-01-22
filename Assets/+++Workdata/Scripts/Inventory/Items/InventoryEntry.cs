using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newName, amount;

    [SerializeField] private GameObject selectButtons;

    [SerializeField] private GameObject apple, bread;

    [SerializeField] private Inventory _inventory;

    private Button item;

    private BaseEventData highlighted;
    
    public bool selected;
    
    /// <summary>
    /// We find inventory. 
    /// </summary>
    private void Awake()
    {
        _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    /// <summary>
    /// If slected is false we set selectedButtons false else we set them true.
    /// </summary>
    private void Update()
    {
        if(selected == false)
            selectButtons.SetActive(false);
        else if(selected)
            selectButtons.SetActive(true);
    }

    
    /// <summary>
    /// We set the name and amount depending on the name. 
    /// </summary>
    /// <param name="itemData"></param>
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

    /// <summary>
    /// If selected is true we set selected to false and start LookForSelected.
    /// Else we  LookForSlelected and set selecetd to true. 
    /// </summary>
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
