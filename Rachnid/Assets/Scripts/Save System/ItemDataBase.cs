using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Database")]
public class ItemDataBase : ScriptableObject
{
    public List<Item> items = new List<Item>();
}