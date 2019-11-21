using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", PlayerManager.Instance.PlayerInput.Vertical);
        animator.SetFloat("Horizontal", PlayerManager.Instance.PlayerInput.Horizontal);
    }
}
