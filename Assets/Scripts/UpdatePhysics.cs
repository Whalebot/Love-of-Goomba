using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePhysics : MonoBehaviour
{
    Rigidbody rb;
    bool x;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = !x;
       if(x) rb.AddForce (Vector3.right);
        else rb.AddForce (Vector3.left);
    }
}
