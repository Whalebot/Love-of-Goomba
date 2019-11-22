using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public GameObject target;
    public bool isActive;
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



    public void Activate() {

        isActive = true;
        agent.enabled = true;
    }
}

