using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private List<Door> doors = new List<Door>();
    private List<bool> doorStates = new List<bool>();

    private void Awake()
    {
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }

    private void SaveManager_OnSave()
    {
        doorStates.Clear();
        for (int i = 0; i < doors.Count; i++)
        {
            doorStates.Add(doors[i].open);
        }
        SaveData.current.sceneData.doorStates = doorStates;
    }

    private void SaveManager_OnLoad()
    {
        doorStates = SaveData.current.sceneData.doorStates;
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetDoorState(doorStates[i]);
        }
    }
}