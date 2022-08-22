using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Batteries")]
public class Batteries : Item
{
    public override void Use(Slot slot)
    {
        InventoryData.Instance.batteryPercentage += 100;
        slot.FillSlot(null);
    }
}