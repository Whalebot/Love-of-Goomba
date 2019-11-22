using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Player player;
    public int gunDamage;
    public int gunHitStun;
    public float gunPushback;
    public int shottyDamage;
    public int shottyHitStun;
    public float shottyPushback;
    public LayerMask mask;
    public GameObject gunSFX;
    public GameObject shottySFX;
    Animator anim;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Input.GetButtonDown("Fire1"))
        {
            if(player.activeWeapon == 0)
            {
                Instantiate(gunSFX, transform.position, Quaternion.identity);
                anim.SetTrigger("Shoot");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= gunDamage;
                        status.TakePushback(transform.forward * gunPushback);
                        status.HitStun = gunHitStun;
                    }
                }
            }
            if(player.activeWeapon == 1)
            {
                Instantiate(shottySFX, transform.position, Quaternion.identity);
                anim.SetTrigger("Shoot");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                    }
                }
            }
        }
    }
}
