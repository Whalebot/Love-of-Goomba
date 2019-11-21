using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health;
    public int hitStun;
    Rigidbody rb;

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

    public int Health {
        get {
            return health;
        }
        set {
            health = value;
            if (health <= 0) Death();
        }
    }

    public int HitStun {
        get { return hitStun; }
        set { hitStun = value; }
    }



    public void TakePushback(Vector3 direction) {
        rb.AddForce(direction, ForceMode.Impulse);
    }

    void ResolveHitStun() {
        if (hitStun > 0) hitStun--;
    }

    void Death() {
        Destroy(gameObject);
    }
}
