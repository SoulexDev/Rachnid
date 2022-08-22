using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : ItemPickup
{
    public override void Interact()
    {
        Inventory.Instance.AddItem(item);
        Note note = item as Note;
        Notes.Instance.SetNoteMessage(note.message);
        Destroy(gameObject);
    }
}