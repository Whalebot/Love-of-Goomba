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

    [Space(10)]
    [HeaderAttribute("Field of View")]
    [SerializeField] public float range;
    [Range(0,360)]
    public float viewAngle;
    [SerializeField] LayerMask mask;
    RaycastHit hit;
   

    public enum State { Idle, Move, Attack, Hitstun };
    public State state = State.Move;

    void Awake()
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
        if(agent.enabled)
        agent.isStopped = false;
 
        if (status.hitStun > 0) state = State.Hitstun;

        if (TargetInRange() && InLineOfSight()) state = State.Attack;
    }
    void Attack()
    {
        if (status.hitStun > 0) state = State.Hitstun;
        if (agent.enabled)
            agent.isStopped = true;


        if (!TargetInRange() || !InLineOfSight()) state = State.Move;
    }
    void Hitstun()
    {
        if (agent.enabled)
            agent.isStopped = true;

        if (status.hitStun <= 0) state = State.Move;
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

    public bool InLineOfSight() {

        bool clearLine = Physics.Raycast(transform.position, TargetDirectionVector(), out hit, range, mask);
        Vector3 direction = target.transform.position - transform.position;
        bool withinAngle = (Vector3.Angle(transform.forward, direction) < viewAngle / 2);


        bool seePlayer = clearLine && withinAngle && hit.collider.gameObject.CompareTag("Player");

        Debug.DrawLine(transform.position, hit.point, Color.yellow);

        return seePlayer;
    }

    public Vector3 TargetDirectionVector()
    {
        Vector3 relativePos = target.transform.position + Vector3.up * AIManager.aimOffset - transform.position;
        return relativePos;
    }

    public Vector3 AngleToVector(float angleInDegrees) {
        angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }



    public void Activate() {

        isActive = true;
        agent.enabled = true;
    }
}

