using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwayer : MonoBehaviour
{
    private Vector3 initPos;
    [SerializeField] private float amount;
    [SerializeField] private float maxSway;
    [SerializeField] private float smoothAmount;
    private void Awake()
    {
        initPos = transform.localPosition;
    }
    private void Update()
    {
        if (!Player.Instance.canMove)
            return;
        Tilt();
    }
    void Tilt()
    {
        float moveX = Input.GetAxis("Mouse X") * amount;
        float moveY = Input.GetAxis("Mouse Y") * amount;
        moveX = Mathf.Clamp(moveX, -maxSway, maxSway);
        moveY = Mathf.Clamp(moveY, -maxSway, maxSway);
        Vector3 finalPos = new Vector3(moveX, 0, moveY);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initPos, Time.deltaTime * smoothAmount);
    }
}