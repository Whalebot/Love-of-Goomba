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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRanged)
        {
            ShootDetection();
        }
    }

    void ShootDetection()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * range, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, mask))
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
        Instantiate(projectile, shootOrigin.position, shootOrigin.rotation);
    }
}
