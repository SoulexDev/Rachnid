using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBobber : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10;

    [SerializeField] private Transform obj;
    [SerializeField] private Transform lookTarget;

    [SerializeField] private float toggleSpeed = 3;
    private Vector3 startPos;
    private CharacterController controller;
    private PlayerController pController;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        pController = GetComponent<PlayerController>();
        startPos = obj.localPosition;
    }
    private void Update()
    {
        if (!_enable || !Player.Instance.canMove) return;

        CheckMotion();
        ResetPosition();
        obj.rotation = Quaternion.LookRotation(lookTarget.position - obj.position, Vector3.up);
    }
    void PlayMotion(Vector3 motion)
    {
        obj.localPosition += motion;
    }
    void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!pController.grounded) return;

        PlayMotion(FootStepMotion());
    }
    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
    void ResetPosition()
    {
        if (obj.localPosition == startPos) return;
        obj.localPosition = Vector3.Lerp(obj.localPosition, startPos, Time.deltaTime);
    }
}