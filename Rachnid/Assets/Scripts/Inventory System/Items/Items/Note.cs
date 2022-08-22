using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Note")]
public class Note : Item
{
    public string message;
    public override void Use(Slot slot)
    {
        Notes.Instance.SetNoteMessage(message);
    }
}