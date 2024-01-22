using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
  public enum CharacterType
  {
    Hero,
    Healer,
    Tank
  }

  public CharacterType characterType;
  
  private Inventory _inventory;

  /// <summary>
  /// Get inventory and set characterslect to this. 
  /// </summary>
  public void Awake()
  {
    _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    
    _inventory.characterSelect = this;
  }

  /// <summary>
  /// charatcerSelect gets to this and we call HealPlayer. 
  /// </summary>
  public void SelectPlayer()
  {
    _inventory.characterSelect = this;
    
    _inventory.HealPlayer();
  }
}
