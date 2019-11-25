using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationAssist : MonoBehaviour
{
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
}
