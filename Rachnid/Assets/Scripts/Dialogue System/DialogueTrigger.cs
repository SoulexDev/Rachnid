using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueQueue dialogueQueue;
    //bool _enabled;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        //if (enabled)
        //{
        //    DialogueEvent.InvokeOnDialogue(dialogueQueue);
        //    enabled = false;
        //}
        DialogueEvent.InvokeOnDialogue(dialogueQueue);
        Destroy(gameObject);
    }
}