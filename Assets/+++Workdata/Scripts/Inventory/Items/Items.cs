using System.Collections;
using UnityEngine;

public enum ItemType
{
   HealthItem,
   Weapon,
   Armor,
}
public class Items : MonoBehaviour
{
   private Items item;

   private Inventory _inventory;
   
   public string newName;

   public ItemType itemType;

   public int healAmount, atk, def;

   public int amount;

   public void Awake()
   {
      item = GetComponent<Items>();

      _inventory = GameObject.Find("---InventoryManager").GetComponent<Inventory>();
   }

   public void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         StartCoroutine(WaitForBeforeDestruction());
      }
   }

   public IEnumerator WaitForBeforeDestruction()
   {
      _inventory.AddItemToList(item);

      yield return null;
      
      Destroy(gameObject);
   }
}
