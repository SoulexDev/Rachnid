using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuReferences : MonoBehaviour
{
    public Transform saveContent;
    public Button createGameButton;
    public Button saveGameButton;
    public TMP_InputField saveInput;
    public static MenuReferences Instance;
    private void Awake()
    {
        Instance = this;
    }
}