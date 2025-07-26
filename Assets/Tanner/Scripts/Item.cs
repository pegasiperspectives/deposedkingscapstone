using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]     // This attribute allows you to create new Item assets from the Unity Editor's "Create" menu
public class Item : ScriptableObject
{
    public int id;              // Unique identifier for this item
    public string itemName;     // Display name of the item
    public int value;           // A value you can use for scoring, selling, etc.
    public Sprite icon;         // Icon to display in the UI for this item



}
