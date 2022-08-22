using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IEnemy
{
    [SerializeField] protected float damage = 20;
    public enum States { Dead, Idle, Chase, Attack }
    public States state;
    [SerializeField] protected Transform target;
    protected NavMeshAgent agent;
    protected Animator anims;
    public float health = 100;
    protected bool attacking = false;
    [SerializeField] protected float attackTime = 1;
    private int animState;
    private float animSmoothState;
    [SerializeField] private GameObject spiderM;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anims = spiderM.GetComponent<Animator>();
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(transform.position, out closestHit, 100, NavMesh.AllAreas))
            transform.position = closestHit.position;
        agent.enabled = false;
        agent.enabled = true;

        target = GameObject.Find("Player").transform;
    }

    public virtual void Update()
    {
        animSmoothState = Mathf.Lerp(animSmoothState, animState, Time.deltaTime * 5);
        anims.SetFloat("MoveState", animSmoothState);
        if (state == States.Dead)
        {
            return;
        }
        if (!agent.isOnNavMesh)
            return;
        float targetDist = Vector3.Distance(transform.position, target.position);
        if(!attacking)
            state = targetDist < 3 ? States.Attack : targetDist < 25 ? States.Chase : States.Idle;
        switch (state)
        {
            case States.Idle:
                agent.isStopped = true;
                animState = 0;
                break;
            case States.Chase:
                agent.isStopped = false;
                animState = 1;
                Chase();
                break;
            case States.Attack:
                animState = 0;
                agent.isStopped = true;
                if (!attacking)
                    StartCoroutine(Attack());
                break;
            default:
                break;
        }
        //agent.isStopped = state != States.Chase;
    }
    public virtual void Chase()
    {
        agent.SetDestination(target.position);
    }
    public virtual IEnumerator Attack()
    {
        attacking = true;
        anims.SetTrigger("Attack");

        yield return new WaitForSeconds(attackTime);
        Collider[] cols = Physics.OverlapSphere(transform.position, 3);
        foreach (Collider col in cols)
        {
            if (col.TryGetComponent(out IPlayer player))
            {
                player.Damage(damage);
            }
        }
        attacking = false;
    }

    public virtual void Damage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            state = States.Dead;
            agent.isStopped = true;
            anims.SetTrigger("Die");
        }
    }
}