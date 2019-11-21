using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool homing;
    [SerializeField] float velocity;
    [SerializeField] int damage;
    [SerializeField] GameObject hitParticle;
    Collider col;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.velocity = transform.forward * velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(hitParticle, pos, rot);
        Destroy(gameObject);
    }

    void DoDamage() {

    }
}
