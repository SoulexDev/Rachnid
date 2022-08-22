using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    private Inventory inventory => Inventory.Instance;
    private Hotbar hotbar => Hotbar.Instance;
    private int slotIndex => inventory.inventorySlots.IndexOf(this);
    [SerializeField] private ToolTip toolTip;
    private void Awake()
    {
        ItemChanged();
    }
    public void FillSlot(Item newItem)
    {
        item = newItem;
        ItemChanged();
    }
    void ItemChanged()
    {
        bool itemNull = item == null;
        itemIcon.sprite = itemNull ? null : item.icon;
        itemIcon.enabled = !itemNull;
        toolTip.SetToolTip(itemNull ? "" : item.toolTip);

        if (hotbar == null || inventory == null)
            return;

        if(inventory.inventorySlots.Contains(this) && slotIndex < hotbar.hotbarSlots.Count)
            hotbar.hotbarSlots[slotIndex].UpdateImage(itemIcon.sprite);
    }
    public void UseItem()
    {
        item.Use(this);
    }
}