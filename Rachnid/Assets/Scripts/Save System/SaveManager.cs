using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public delegate void Save();
    public static event Save OnSave;

    public delegate void Load();
    public static event Load OnLoad;
    public static SaveManager Instance;
    public string currentSave;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CreateNewSave(string saveName)
    {
        if (saveName == "")
            return;

        currentSave = saveName;
        SerializationManager.Save(saveName, new SaveData());
        SceneManager.LoadScene(1);
    }
    public void SaveGame()
    {
        OnSave.Invoke();
        SerializationManager.Save(currentSave, SaveData.current);
    }
    public async void LoadGame(string saveName)
    {
        currentSave = saveName;

        SaveData.current = (SaveData)SerializationManager.Load(currentSave);

        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(SaveData.current.sceneData.sceneIndex);
        while (!loadingLevel.isDone)
        {
            await Task.Yield();
        }
        OnLoad.Invoke();
    }
    private void OnLevelWasLoaded(int level)
    {
        if(MenuReferences.Instance.saveGameButton != null)
            MenuReferences.Instance.saveGameButton.onClick.AddListener(() => SaveGame());
    }
}