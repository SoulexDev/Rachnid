using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public static Notes Instance;
    [SerializeField] private TextMeshProUGUI noteText;
    private void Awake()
    {
        Instance = this;
    }
    public void SetNoteMessage(string message)
    {
        noteText.text = message;
    }
}