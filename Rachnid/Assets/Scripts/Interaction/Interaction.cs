using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Transform camTransform;
    private void Awake()
    {
        camTransform = Camera.main.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.SphereCast(camTransform.position, 0.05f, camTransform.forward, out RaycastHit hit, 2))
            {
                if(hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}