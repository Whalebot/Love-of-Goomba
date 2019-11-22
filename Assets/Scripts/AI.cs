using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public GameObject target;
    public bool isActive;
    [SerializeField] public float range;
    NavMeshAgent agent;
    Status status;

    public enum State { Idle, Move, Attack, Hitstun };
    public State state = State.Move;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<Status>();
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (status.hitStun > 0) state = State.Hitstun;

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Hitstun:
                Hitstun();
                break;

            default: break;
        }
    }

    void Idle()
    {
       
    }
    void Move()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target.transform.position);
        }
        if (status.hitStun > 0) state = State.Hitstun;
    }
    void Attack()
    {
        if (status.hitStun > 0) state = State.Hitstun;

    }
    void Hitstun()
    {

    }

    public bool TargetInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) < range;
    }

    public Quaternion TargetDirection()
    {
        Vector3 relativePos = target.transform.position + Vector3.up * AIManager.aimOffset - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        return rotation;
    }

    public Vector3 TargetDirectionVector()
    {
        Vector3 relativePos = target.transform.position + Vector3.up * AIManager.aimOffset - transform.position;
        return relativePos;
    }

    public void Activate() {

        isActive = true;
        agent.enabled = true;
    }
}

