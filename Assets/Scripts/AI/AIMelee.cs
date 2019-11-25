using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMelee : MonoBehaviour
{
    public float force;
    [SerializeField] float attackDelay;
    [SerializeField] GameObject hitbox;
    float lastAttack;
    [SerializeField] float startupTime;
    [SerializeField] float duration;
    AI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AI>();
        lastAttack = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ai.state == AI.State.Attack)
        {
            ShootDetection();
        }
    }

    void ShootDetection()
    {

        if (Time.time > attackDelay + lastAttack)
        {
            lastAttack = Time.time;
            StartCoroutine("StartupTime");
        }
    }

    IEnumerator AttackTimer() {
        yield return new WaitForSeconds(duration);
        AttackEnd();
    }

    IEnumerator StartupTime()
    {
        yield return new WaitForSeconds(startupTime);
        Shoot();
    }

    public void Shoot()
    {
        ai.attackID = 1;
        ai.attackStart = true;
    }

    public void AttackStart(){
        print("poof");
        ai.status.rb.AddForce(ai.TargetDirectionVector() * force, ForceMode.Impulse);
        hitbox.SetActive(true);
        StartCoroutine("AttackTimer");
    }

    public void AttackEnd() {
        hitbox.SetActive(false);
    }
}
