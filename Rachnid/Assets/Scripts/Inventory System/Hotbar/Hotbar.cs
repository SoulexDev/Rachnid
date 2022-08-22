using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public static Hotbar Instance;
    public List<HotbarSlot> hotbarSlots;
    private int currentHotbarSlot;
    private float scroll;
    private Inventory inventory => Inventory.Instance;
    [SerializeField] private Image highlight;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateImage(inventory.inventorySlots[i].itemIcon.sprite);
        }
    }
    private void Update()
    {
        if (!Player.Instance.canMove)
            return;

        scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if(scroll != 0)
        {
            Scroll();
        }
    }
    void Scroll()
    {
        if (scroll < 0)
            currentHotbarSlot++;
        else
            currentHotbarSlot--;

        if (currentHotbarSlot < 0)
            currentHotbarSlot = hotbarSlots.Count - 1;
        if (currentHotbarSlot > hotbarSlots.Count - 1)
            currentHotbarSlot = 0;

        highlight.rectTransform.position = hotbarSlots[currentHotbarSlot].transform.position;

        ItemSpawner.Instance.SpawnItem(inventory.inventorySlots[currentHotbarSlot].item);
    }
}