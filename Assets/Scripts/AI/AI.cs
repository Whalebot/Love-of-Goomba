﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public GameObject target;
    public bool isActive;

    NavMeshAgent agent;
    [HideInInspector] public Status status;

    [Space(10)]
    [HeaderAttribute("Field of View")]
    [SerializeField] public float range;
    [Range(0, 360)]
    public float viewAngle;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask mask;
    RaycastHit hit;
    [Space(10)]
    [HeaderAttribute("Ground detection")]
    [SerializeField] float groundDistance;
    public int income;
    GameManager gm;

    [HideInInspector] public bool hitstunStart;
    [HideInInspector] public bool attackStart;

    public enum State { Idle, Move, Attack, Hitstun };
    public State state = State.Move;

    void Awake()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<Status>();
       
    }
    void Start()
    {
        if (gm != null)
            gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
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


        Debug.DrawLine(transform.position, transform.position - transform.up * groundDistance, Color.red);
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
        if (agent.enabled)
        {
            if (!TargetInRange() || !InLineOfSight())
                agent.isStopped = false;
        }

        if (status.hitStun > 0) TransitionToHitstun();

        if (TargetInRange() && !WithinAngle() && ClearLine())
        {
            ManualRotation();

        }

        if (TargetInRange() && InLineOfSight()) state = State.Attack;
    }
    void Attack()
    {
        if (status.hitStun > 0) TransitionToHitstun();
        if (agent.enabled)
            agent.isStopped = true;

        ManualRotation();

        if (!TargetInRange() || !InLineOfSight()) state = State.Move;
    }
    void Hitstun()
    {
        if (agent.enabled)
            agent.isStopped = true;

        if (status.hitStun <= 0 ) state = State.Move;
    }

    void TransitionToHitstun() {
        hitstunStart = true;
        state = State.Hitstun;
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
    public Quaternion TargetDirection(Transform origin)
    {
        Vector3 relativePos = target.transform.position + Vector3.up * AIManager.aimOffset - origin.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        return rotation;
    }


    public bool WithinAngle() {
        bool withinAngle = (Vector3.Angle(transform.forward, TargetDirectionVector()) < viewAngle / 2);
        return withinAngle;
    }

    public bool ClearLine() {
        bool clearLine = Physics.Raycast(transform.position, TargetDirectionVector(), out hit, range, mask) && hit.collider.gameObject.name == ("Player");
        return clearLine;
    }
    public bool InLineOfSight()
    {
        bool seePlayer = ClearLine()  && WithinAngle() ;
        Debug.DrawLine(transform.position, hit.point, Color.yellow);

        return seePlayer;
    }

    public bool CheckForGround()
    {
        bool groundBelow = Physics.Raycast(transform.position, -transform.up, groundDistance);
        return groundBelow;
    }

    public Vector3 TargetDirectionVector()
    {
        Vector3 relativePos = target.transform.position + Vector3.up * AIManager.aimOffset - transform.position;
        return relativePos.normalized;
    }

    public Vector3 AngleToVector(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void ManualRotation()
    {
        print("Rotating");
        Quaternion lookRotation = Quaternion.LookRotation(TargetDirectionVector());
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
    }

    public void Activate()
    {

        isActive = true;
        agent.enabled = true;
        if(gm != null)
        gm.income += income;
    }
}

