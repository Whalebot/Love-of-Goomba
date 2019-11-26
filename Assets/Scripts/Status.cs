using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health;
    public int hitStun;
    public bool inHitStun;
    [HideInInspector] public bool hitStunStart;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector2 knockbackDirection;
    public delegate void OnHealthChangeDelegate(int newVal);
    public event OnHealthChangeDelegate OnHealthChange;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ResolveHitStun();
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (health == value) return;
            health = value;
            if (OnHealthChange != null)
                OnHealthChange(health);

            if (health <= 0) Death();
        }
    }

    public int HitStun
    {
        get { return hitStun; }
        set
        {
            hitStun = value;
            hitStunStart = true;
        }
    }

    public void TakePushback(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode.Impulse);
        knockbackDirection = (direction - transform.position).normalized;
    }

    void ResolveHitStun()
    {
        if (hitStun > 0)
        {
            hitStun--;
            inHitStun = true;
        }
        if (HitStun <= 0) inHitStun = false;
    }

    void Death()
    {
        isDead = true;
    }
}
