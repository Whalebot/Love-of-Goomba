using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
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
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { DoDamage(other.gameObject); }
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void DoDamage(GameObject other)
    {
        Status status = other.GetComponent<Status>();
        status.Health -= damage;
        status.TakePushback(transform.forward * pushback);
        status.HitStun = hitStun;
    }
}
