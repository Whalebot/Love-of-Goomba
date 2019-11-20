using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public GameObject target;
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
        agent.SetDestination(target.transform.position);
    }
}

