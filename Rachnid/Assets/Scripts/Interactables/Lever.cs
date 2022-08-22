using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    public UnityEvent leverEvent;

    public void Interact()
    {
        leverEvent.Invoke();
    }
}