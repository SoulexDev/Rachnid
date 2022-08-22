using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    [SerializeField] private Transform spawnPos;
    private GameObject previousObj;
    private Item previousItem;
    private void Awake()
    {
        Instance = this;
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }
    private void SaveManager_OnSave()
    {
        SaveData.current.playerData.handItemIndex = Inventory.Instance.GetItemIndexFromDataBase(previousItem);
    }
    private void SaveManager_OnLoad()
    {
        SpawnItem(Inventory.Instance.GetItemFromDataBase(SaveData.current.playerData.handItemIndex));
    }

    public void SpawnItem(Item item)
    {
        if (previousItem == item)
            return;
        else
            previousItem = item;

        if(previousObj != null)
        {
            Destroy(previousObj);
        }
        if (item == null)
            return;

        if(item.itemPrefab != null)
        {
            previousObj = Instantiate(item.itemPrefab, spawnPos.position, Quaternion.LookRotation(spawnPos.forward, Vector3.up), spawnPos);
        }
    }
}