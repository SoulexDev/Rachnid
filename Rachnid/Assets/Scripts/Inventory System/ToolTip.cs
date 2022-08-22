using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ToolTipBox toolTipBox;
    //private Color color;
    public string message;
    private void Start()
    {
        toolTipBox = FindObjectOfType<ToolTipBox>();
    }
    public void SetToolTip(string toolTip)
    {
        message = toolTip;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Slot thisSlot = InventoryInteraction.hoveredSlot = GetComponent<Slot>();
        if(thisSlot.item != null)
            ToolTipBox.Instance.ShowToolTip(message);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipBox.Instance.HideToolTip();
        InventoryInteraction.hoveredSlot = null;
    }
}