using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects;
    private List<int> objectState = new List<int>();
    private void Awake()
    {
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }

    private void SaveManager_OnSave()
    {
        SaveData.current.sceneData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        objectState.Clear();
        for (int i = 0; i < gameObjects.Count; i++)
        {
            objectState.Add(GetState(gameObjects[i]));
        }
        SaveData.current.sceneData.objectState = objectState;
    }
    private void SaveManager_OnLoad()
    {
        objectState = SaveData.current.sceneData.objectState;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if(objectState[i] == 0)
            {
                Destroy(gameObjects[i]);
            }
        }
    }
    int GetState(GameObject obj)
    {
        return obj == null ? 0 : 1;
    }
}