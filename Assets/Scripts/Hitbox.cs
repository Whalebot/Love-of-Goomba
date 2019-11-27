using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public static float lastHitbox;
    [SerializeField] int damage;
    [SerializeField] int hitStun;
    [SerializeField] float pushback;
    [SerializeField] GameObject hitParticle;
    [SerializeField] LayerMask mask;
    float hitboxDelay = 0.5F;
    public enum TriggerVersion { Enter, Stay};
    public TriggerVersion thisTrigger = TriggerVersion.Enter;

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
        if (other.CompareTag("Player") && thisTrigger == TriggerVersion.Enter) {
            if (Time.time > lastHitbox + hitboxDelay)
            {
                lastHitbox = Time.time;
                DoDamage(other.gameObject);
                Instantiate(hitParticle, other.transform.position, Quaternion.identity);
            }

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && thisTrigger == TriggerVersion.Stay)
        {
            if (Time.time > lastHitbox + hitboxDelay)
            {
                lastHitbox = Time.time;
                DoDamage(other.gameObject);
                Instantiate(hitParticle, other.transform.position, Quaternion.identity);
            }

        }

    }

    void DoDamage(GameObject other)
    {
        Status status = other.GetComponent<Status>();
        status.Health -= damage;
        status.TakePushback(transform.forward * pushback);
        status.HitStun = hitStun;
    }
}
