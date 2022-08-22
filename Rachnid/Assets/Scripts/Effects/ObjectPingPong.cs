using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPingPong : MonoBehaviour
{
    [SerializeField] private float hoverHeight = 0.2f;
    [SerializeField] private float hoverSpeed = 0.1f;
    [SerializeField] private float rotateSpeed = 90;
    private float initYPos;
    private void Awake()
    {
        initYPos = transform.position.y;
    }
    private void Update()
    {
        float frequency = Time.time * hoverSpeed;
        float yHeight = Mathf.PingPong(frequency, hoverHeight);

        transform.position = new Vector3(transform.position.x, initYPos + yHeight, transform.position.z);
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
    }
}