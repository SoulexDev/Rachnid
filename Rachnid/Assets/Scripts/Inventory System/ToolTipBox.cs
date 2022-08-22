using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTipBox : MonoBehaviour
{
    public static ToolTipBox Instance;
    public TextMeshProUGUI textField;
    public Image toolTipImageBox;
    public GameObject toolTipObject;
    //[SerializeField] private GameObject slotSelect;
    //private bool toolTipHidden = true;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        toolTipObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
        if(Menus.Instance.menuOpen == Menus.MenuOpen.None)
        {
            toolTipObject.SetActive(false);
        }
    }
    public void ShowToolTip(string message)
    {
        //toolTipHidden = false;
        toolTipObject.SetActive(true);
        textField.text = message;
        Vector2 boundSize = new Vector2(textField.preferredWidth + 20, textField.preferredHeight + 20);
        toolTipImageBox.rectTransform.sizeDelta = boundSize;
    }
    public void HideToolTip()
    {
        //toolTipHidden = true;
        toolTipObject.SetActive(false);
        textField.text = "";
    }
}