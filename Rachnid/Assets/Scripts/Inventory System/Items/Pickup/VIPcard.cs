using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VIPcard : ItemPickup
{
    public UnityEvent pickupEvent;
    [SerializeField] private DialogueQueue queue;
    public override void Interact()
    {
        Inventory.Instance.AddItem(item);
        pickupEvent.Invoke();
        Destroy(gameObject);
    }
    public void InvokeDialogue()
    {
        DialogueEvent.InvokeOnDialogue(queue);
    }
}