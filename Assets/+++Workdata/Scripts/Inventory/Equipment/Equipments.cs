using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
  Weapon,
  Armor,
}
public class Equipments : MonoBehaviour
{
  public string newName;

  public EquipmentType equipmentType;
  
  public int attack, defense;
}
