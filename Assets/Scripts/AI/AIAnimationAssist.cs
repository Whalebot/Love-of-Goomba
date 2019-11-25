using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationAssist : MonoBehaviour
{
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponentInParent<Enemy>();
    }

    void AttackSpawn() {
        enemy.AttackSpawn();
    }
}
