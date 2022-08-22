using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteraction : MonoBehaviour
{
    public static Slot hoveredSlot;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Click();
        }
    }
    void Click()
    {
        if (hoveredSlot == null || hoveredSlot.item == null)
            return;
        hoveredSlot.UseItem();
    }
}