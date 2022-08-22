using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    private void Awake()
    {
        DialogueEvent.OnDialogue += DialogueEvent_OnDialogue;
    }
    private void OnDestroy()
    {
        DialogueEvent.OnDialogue -= DialogueEvent_OnDialogue;
    }

    private void DialogueEvent_OnDialogue(DialogueQueue dialogueQueue)
    {
        StopAllCoroutines();
        StartCoroutine(ShowDialogue(dialogueQueue));
    }
    IEnumerator ShowDialogue(DialogueQueue queue)
    {
        for (int i = 0; i < queue.dialogueSectors.Length; i++)
        {
            dialogueText.maxVisibleCharacters = 0;
            dialogueText.text = queue.dialogueSectors[i];

            for (int x = 0; x < dialogueText.text.Length + 1; x++)
            {
                dialogueText.maxVisibleCharacters = x;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(3);
        }
        dialogueText.text = "";
    }
}