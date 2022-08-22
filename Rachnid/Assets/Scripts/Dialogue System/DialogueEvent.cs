using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    public static event Action<DialogueQueue> OnDialogue;
    public static void InvokeOnDialogue(DialogueQueue queue)
    {
        OnDialogue?.Invoke(queue);
    }
}
[Serializable]
public class DialogueQueue
{
    public string[] dialogueSectors;
}