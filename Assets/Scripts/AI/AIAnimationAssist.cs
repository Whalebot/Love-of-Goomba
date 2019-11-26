using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationAssist : MonoBehaviour
{
    public GameObject footL;
    public GameObject footR;
    Enemy enemy;
    AIMelee melee;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponentInParent<Enemy>();
        melee = transform.parent.GetComponentInParent<AIMelee>();
    }

    void AttackSpawn()
    {
        if (enemy != null)
            enemy.AttackSpawn();
    }

    void MeleeAttack()
    {
        print(melee);
        if (melee != null)
            melee.AttackStart();
    }

    void FootL() { Instantiate(footL, transform.position, transform.rotation); }
    void FootR() { Instantiate(footR, transform.position, transform.rotation); }
}
