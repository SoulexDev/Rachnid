using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] protected Item item;
    public virtual void Interact()
    {
        Inventory.Instance.AddItem(item);
        Destroy(gameObject);
    }
}
