using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backslide : MonoBehaviour
{

    public int backslideDamage;
    public int backslidePushback;
    public int backslideHitStun;
    public GameObject bloodFX;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Status status = other.transform.GetComponent<Status>();
        status.Health -= backslideDamage;
        status.TakePushback(transform.forward * backslidePushback);
        status.HitStun = backslideHitStun;
        Instantiate(bloodFX, other.transform.position, transform.rotation);
    }

}