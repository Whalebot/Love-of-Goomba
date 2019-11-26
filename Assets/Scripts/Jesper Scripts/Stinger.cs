using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{

    public int stingerDamage;
    public int stingerPushback;
    public int stingerHitStun;
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
        status.Health -= stingerDamage;
        status.TakePushback(transform.forward * stingerPushback);
        status.HitStun = stingerHitStun;
        Instantiate(bloodFX, other.transform.position, transform.rotation);
    }

}
