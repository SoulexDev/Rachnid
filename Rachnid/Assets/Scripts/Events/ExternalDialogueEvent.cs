using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalDialogueEvent : MonoBehaviour
{
    public void PlayDialogue(DialogueQueueMono queueMono)
    {
        DialogueEvent.InvokeOnDialogue(queueMono.queue);
    }
}