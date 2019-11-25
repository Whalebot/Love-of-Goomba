using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isRanged;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootOrigin;

    [SerializeField] LayerMask mask;
    RaycastHit hit;
    [SerializeField] float shootDelay;
    float lastShot;

    AI ai;


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AI>();
        lastShot = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ai.state == AI.State.Attack)
        {
            if (isRanged)
            {
                ShootDetection();
            }
        }
    }

    void ShootDetection()
    {

        if (Time.time > shootDelay + lastShot)
        {
            lastShot = Time.time;
            Shoot();
        }
    }



    public void Shoot()
    {
        ai.attackStart = true;
      //  Instantiate(projectile, shootOrigin.position, ai.TargetDirection(shootOrigin));
    }

    public void AttackSpawn() { Instantiate(projectile, shootOrigin.position, ai.TargetDirection(shootOrigin)); }
}
