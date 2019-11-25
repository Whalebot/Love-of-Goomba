using System.Collections;
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

        if (status.hitStun > 0) state = State.Hitstun;

        if (TargetInRange())
        {
         //   ManualRotation();

        }

        if (TargetInRange() && InLineOfSight()) state = State.Attack;
    }
    void Attack()
    {
        if (status.hitStun > 0) state = State.Hitstun;
        if (agent.enabled)
            agent.isStopped = true;

        ManualRotation();

        if (!TargetInRange() || !InLineOfSight()) state = State.Move;
    }
    void Hitstun()
    {
        if (agent.enabled)
            agent.isStopped = true;

        if (status.hitStun <= 0 && CheckForGround()) state = State.Move;
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

    public bool InLineOfSight()
    {

        bool clearLine = Physics.Raycast(transform.position, TargetDirectionVector(), out hit, range, mask);

        Vector3 direction = target.transform.position - transform.position;
        bool withinAngle = (Vector3.Angle(transform.forward, direction) < viewAngle / 2);


        bool seePlayer = clearLine && withinAngle && hit.collider.gameObject.name == ("Player");

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

