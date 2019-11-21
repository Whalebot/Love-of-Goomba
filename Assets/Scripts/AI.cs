using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public GameObject target;
    public bool isActive;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (agent.isOnNavMesh) {
            agent.SetDestination(target.transform.position);
        }
    }

    public void Activate() {

        isActive = true;
        agent.enabled = true;
    }
}

