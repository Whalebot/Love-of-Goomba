using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool homing;
    [SerializeField] float velocity;
    [SerializeField] int damage;
    [SerializeField] int hitStun;
    [SerializeField] float pushback;
    [SerializeField] GameObject hitParticle;
    [SerializeField] LayerMask mask;
    Collider col;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // col = GetComponent<Collider>();
        rb.velocity = transform.forward * velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.gameObject);
        if (other.CompareTag("Player")) { DoDamage(other.gameObject); }
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(hitParticle, pos, rot);
        Destroy(gameObject);
    }
    */

    void DoDamage(GameObject other)
    {
        Status status = other.GetComponent<Status>();
        status.Health -= damage;
        status.TakePushback(transform.forward * pushback);
        status.HitStun = hitStun;
    }
}
