using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavesList : MonoBehaviour
{
    [SerializeField] private GameObject saveButton;
    private string[] saveFiles;
    private void Start()
    {
        MenuReferences.Instance.createGameButton.onClick.AddListener(() => CreateNew());
        if (Directory.Exists(SerializationManager.savePath))
            saveFiles = Directory.GetFiles(SerializationManager.savePath);
        else
            return;

        foreach (var save in saveFiles)
        {
            string saveName = save.Contains(SerializationManager.savePath) ? save.Replace(SerializationManager.savePath, "") : save;
            saveName = saveName.Contains(SerializationManager.fileExtension) ? saveName.Replace(SerializationManager.fileExtension, "") : saveName;
            Button sButton = Instantiate(saveButton, MenuReferences.Instance.saveContent).GetComponent<Button>();
            sButton.GetComponentInChildren<TextMeshProUGUI>().text = saveName;
            sButton.onClick.AddListener(() => SaveManager.Instance.LoadGame(saveName));
        }
    }
    public void CreateNew()
    {
        SaveManager.Instance.CreateNewSave(MenuReferences.Instance.saveInput.text);
    }
}