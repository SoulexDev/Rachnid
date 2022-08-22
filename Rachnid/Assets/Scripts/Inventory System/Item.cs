using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Items/Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public GameObject itemPrefab;
    public Sprite icon;
    public string toolTip;
    public virtual void Use(Slot slot)
    {
        //ItemSpawner.Instance.SpawnItem(this);
    }
}