using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isRanged;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootOrigin;

    [SerializeField] LayerMask mask;
    [SerializeField] float shootDelay;
    [SerializeField] float lastShot;
    RaycastHit hit;
    AI ai;


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ai.TargetInRange())
        {
            //ai.state = AI.State.Attack;
            if (isRanged)
            {
                ShootDetection();
            }
        }
        else { ai.state = AI.State.Move; }

    }

    void ShootDetection()
    {
        Debug.DrawLine(transform.position, transform.position + ai.TargetDirectionVector() * ai.range, Color.blue);
        if (Physics.Raycast(transform.position, ai.TargetDirectionVector(), out hit, ai.range, mask) && hit.collider.gameObject.CompareTag("Player"))
        {
            if (Time.time > shootDelay + lastShot)
            {
                lastShot = Time.time;
                Shoot();
            }
        }
    }



    public void Shoot()
    {
        Instantiate(projectile, shootOrigin.position, ai.TargetDirection());
    }
}
