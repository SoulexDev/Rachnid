using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    private bool on = true;
    private bool batteryDepleted = false;
    private InventoryData data => InventoryData.Instance;
    private void Update()
    {
        if (!Player.Instance.canMove)
            return;

        data.batteryPercentage = Mathf.Clamp(data.batteryPercentage, 0, 100);
        batteryDepleted = data.batteryPercentage <= 0;

        lightObj.SetActive(!batteryDepleted && on);
        if (batteryDepleted) return;

        if (Input.GetButtonDown("Fire1"))
        {
            on = !on;
        }
        if (data.canDeplete && on)
        {
            data.batteryPercentage -= 0.1f * Time.deltaTime;
        }
    }
}