using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    AI ai;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponentInParent<AI>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Walking", ai.state == AI.State.Move);
        anim.SetBool("Hitstun", ai.state == AI.State.Hitstun);
        anim.SetFloat("HitX", ai.status.knockbackDirection.x);
        anim.SetFloat("HitY", ai.status.knockbackDirection.y);
        anim.SetInteger("AttackID", ai.attackID);

        if (ai.status.hitStunStart) HitstunStart();
        if (ai.attackStart) AttackStart();
    }

    void HitstunStart() {
        ai.status.hitStunStart = false;
        anim.SetTrigger("Hit");
    }

    void AttackStart()
    {
        ai.attackStart = false;
        anim.SetTrigger("Attack");
    }
}
