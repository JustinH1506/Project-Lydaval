using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
   HealthItem,
}
public class Items : MonoBehaviour
{
   public string newName;

   public ItemType itemType;

   public int healAmount;
}
