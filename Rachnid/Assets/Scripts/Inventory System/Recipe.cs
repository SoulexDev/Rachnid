using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe", fileName = "New Recipe")]
public class Recipe : ScriptableObject
{
    public Item itemToCraft;
    public Item[] items;
}