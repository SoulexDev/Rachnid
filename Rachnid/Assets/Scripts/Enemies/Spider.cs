using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : EnemyAI
{
    [SerializeField] private Transform spiderRot;
    public override void Update()
    {
        base.Update();

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2) && !agent.isStopped)
        {
            spiderRot.rotation = Quaternion.Slerp(spiderRot.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(agent.velocity, hit.normal), hit.normal), Time.deltaTime * 7.5f);
        }
    }
} 