using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public static InventoryData Instance;
    [Header("Flashlight Data")]
    public float batteryPercentage = 100;
    public bool canDeplete = false;
    public bool on;
    [Space]
    [Header("Flamethrower Data")]
    public float fillAmount = 100;
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

    public void KillFlashlight()
    {
        batteryPercentage = 0;
        canDeplete = true;
    }

    private void SaveManager_OnSave()
    {
        SaveData.current.playerData.batteryPercentage = batteryPercentage;
        SaveData.current.playerData.canDeplete = canDeplete;
        SaveData.current.playerData.fillAmount = fillAmount;
    }

    private void SaveManager_OnLoad()
    {
        batteryPercentage = SaveData.current.playerData.batteryPercentage;
        canDeplete = SaveData.current.playerData.canDeplete;
        fillAmount = SaveData.current.playerData.fillAmount;
    }
}