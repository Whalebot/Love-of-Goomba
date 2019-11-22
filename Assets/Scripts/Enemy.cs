using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isRanged;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootOrigin;
    [SerializeField] float range;
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
        if (TargetInRange())
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
        Debug.DrawLine(transform.position, transform.position + transform.forward * range, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, mask) && hit.collider.gameObject.CompareTag("Player"))
        {
            if (Time.time > shootDelay + lastShot)
            {
                lastShot = Time.time;
                Shoot();
            }
        }
    }

    bool TargetInRange()
    {
        return Vector3.Distance(transform.position, ai.target.transform.position) < range;
    }

    public void Shoot()
    {
        Instantiate(projectile, shootOrigin.position, shootOrigin.rotation);
    }
}
