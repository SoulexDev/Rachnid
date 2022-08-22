using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image image;

    public void UpdateImage(Sprite icon)
    {
        image.sprite = icon;
        image.enabled = icon != null;
    }
}