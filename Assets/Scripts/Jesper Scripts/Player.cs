﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    [System.Serializable]
    public class StickInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }
    [System.Serializable]
    public class DodgeForces
    {
        public float ForwardRoll;
        public float Slide;
        public float LeftRoll;
        public float RightRoll;
        public float BackFlip;
    }

    [SerializeField] float speed;
    [SerializeField] StickInput StickControl;
    [SerializeField] DodgeForces dodgeForces;

    MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }
    PlayerInput playerInput;
    Vector2 lookInput;
    Rigidbody rb;
    public bool canMove = true;
    public bool canInput = true;
    public int activeWeapon;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = PlayerManager.Instance.PlayerInput;
        PlayerManager.Instance.LocalPlayer = this;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        if (canMove)
        {
            MoveController.Move(direction);
        }
        lookInput.x = Mathf.Lerp(lookInput.x, playerInput.LookInput.x, 1f / StickControl.Damping.x);
        transform.Rotate(Vector3.up * lookInput.x * StickControl.Sensitivity.x);

        if (Input.GetButtonDown("Submit"))
        {
            if(playerInput.Horizontal > 0.5f)
            {
                anim.SetTrigger("FlipRight");
                return;
            }
            if (playerInput.Horizontal < -0.5f)
            {
                anim.SetTrigger("FlipLeft");
                return;
            }
            if(playerInput.Vertical < -0.5f)
            {
                anim.SetTrigger("Backflip");

                return;
            }
                anim.SetTrigger("ForwardRoll");             
        }

        if (Input.GetButtonDown("Switch"))
        {
            if(activeWeapon == 0)
            {
                activeWeapon = 1;
                anim.SetInteger("ActiveWeapon", 1);
            }
            else
            {
                activeWeapon = 0;
                anim.SetInteger("ActiveWeapon", 0);
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Forward Roll") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlipLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlipRight")
             || anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") || anim.GetCurrentAnimatorStateInfo(0).IsName("Backflip"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    public void Dodge()
    {
        if(anim.GetNextAnimatorStateInfo(0).IsName("Forward Roll"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * dodgeForces.ForwardRoll);
        }
        if (anim.GetNextAnimatorStateInfo(0).IsName("FlipLeft"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.right * dodgeForces.LeftRoll);
        }
        if (anim.GetNextAnimatorStateInfo(0).IsName("FlipRight"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.right * dodgeForces.RightRoll);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            rb.AddForce(transform.forward * dodgeForces.Slide);
        }
        if (anim.GetNextAnimatorStateInfo(0).IsName("Backflip"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * dodgeForces.BackFlip);
        }
    }

}
