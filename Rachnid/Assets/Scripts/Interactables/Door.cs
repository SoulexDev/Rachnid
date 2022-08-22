using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool open = false;
    private Animator anims;
    private void Awake()
    {
        anims = GetComponent<Animator>();
    }

    public void Interact()
    {
        open = !open;
        SetDoorState(open);
    }
    public void SetDoorState(bool state)
    {
        anims.SetBool("Open", state);
    }
}