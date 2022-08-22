using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private ItemDataBase dataBase;
    public List<Slot> inventorySlots;
    [SerializeField] private List<Slot> noteSlots;
    private void Awake()
    {
        Instance = this;
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }
    private void OnDestroy()
    {
        SaveManager.OnSave -= SaveManager_OnSave;
        SaveManager.OnLoad -= SaveManager_OnLoad;
    }
    private void SaveManager_OnSave()
    {
        SaveData.current.playerData.itemIndex.Clear();
        SaveData.current.playerData.noteIndex.Clear();
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            SaveData.current.playerData.itemIndex.Add(GetItemIndexFromDataBase(inventorySlots[i].item));
        }
        for (int i = 0; i < noteSlots.Count; i++)
        {
            SaveData.current.playerData.itemIndex.Add(GetItemIndexFromDataBase(noteSlots[i].item));
        }
    }

    private void SaveManager_OnLoad()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].FillSlot(dataBase.items[SaveData.current.playerData.itemIndex[i]]);
        }
        for (int i = 0; i < noteSlots.Count; i++)
        {
            noteSlots[i].FillSlot(dataBase.items[SaveData.current.playerData.itemIndex[i]]);
        }
    }

    public void AddItem(Item item)
    {
        List<Slot> searchList = inventorySlots;
        if(item is Note)
        {
            searchList = noteSlots;
        }

        for (int i = 0; i < searchList.Count; i++)
        {
            if (searchList[i].item == null)
            {
                searchList[i].FillSlot(item);
                break;
            }
        }
    }
    public void Craft(Recipe recipe)
    {
        bool canCraft = true;
        Slot[] slots = new Slot[recipe.items.Length];
        for (int i = 0; i < recipe.items.Length; i++)
        {
            if (!ItemExists(recipe.items[i], ref slots[i]))
            {
                canCraft = false;
                break;
            }
        }
        if (canCraft)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].FillSlot(null);
            }
            AddItem(recipe.itemToCraft);
        }
    }
    bool ItemExists(Item item, ref Slot slot)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == item)
            {
                slot = inventorySlots[i];
                return true;
            }
        }
        return false;
    }
    public int GetItemIndexFromDataBase(Item item)
    {
        for (int i = 0; i < dataBase.items.Count; i++)
        {
            if (item == dataBase.items[i])
                return i;
        }
        return 0;
    }
    public Item GetItemFromDataBase(int index)
    {
        for (int i = 0; i < dataBase.items.Count; i++)
        {
            if (index == i)
                return dataBase.items[i];
        }
        return null;
    }
}