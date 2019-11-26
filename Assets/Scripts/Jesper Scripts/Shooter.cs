using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Player player;
    public float gunFireRate;
    public int gunDamage;
    public int gunDamagelvl2;
    public int gunDamagelvl3;
    public int gunDamagelvl4;
    public int gunHitStun;
    public float gunPushback;
    public float gunPushbacklvl2;
    public float shottyFireRate;
    public int shottyDamage;
    public int shottyHitStun;
    public float shottyPushback;
    public float shottyRange;
    public LayerMask mask;
    public GameObject gunSFX;
    public GameObject superGunSFX;
    public GameObject shottySFX;
    public GameObject smallExplosionFX;
    public GameObject bigExplosionFX;
    public GameObject chargeSFX;
    public GameObject bloodFX;
    public GameObject stingerHitBox;
    public bool canShoot = true;
    public float cooldown;
    public float gunCharge;
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

        if(cooldown <= 0)
        {
            canShoot = true;
        }


        if(player.activeWeapon == 0 && Input.GetButton("Fire1"))
        {
            gunCharge += 1;
        }

        if(gunCharge == 300)
        {
            Instantiate(chargeSFX, transform.position, Quaternion.identity);
        }

        anim.SetFloat("Charge", gunCharge);

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if(Input.GetButtonUp("Fire1"))
        {
            if (player.activeWeapon == 0 && gunCharge > 300)
            {
                Instantiate(superGunSFX, transform.position, Quaternion.identity);
                Instantiate(smallExplosionFX, transform.position, Quaternion.identity);
                anim.SetTrigger("Shoot");
                canShoot = false;
                cooldown = gunFireRate;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= gunDamagelvl4;
                        status.TakePushback(transform.forward * gunPushback);
                        status.HitStun = gunHitStun;
                        Instantiate(bigExplosionFX, hit.point, Quaternion.identity);
                        Instantiate(bloodFX, hit.point, Quaternion.identity);
                    }
                }
            }
            gunCharge = 0;
        }

        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            if(player.activeWeapon == 0)
            {
                GameObject gunFX = Instantiate(gunSFX, transform.position, transform.rotation);
                gunFX.transform.parent = gameObject.transform;
                anim.SetTrigger("Shoot");
                canShoot = false;
                cooldown = gunFireRate;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= gunDamage;
                        status.TakePushback(transform.forward * gunPushback);
                        status.HitStun = gunHitStun;
                        Instantiate(bloodFX, hit.point, transform.rotation);
                    }
                }
            }
            if(player.activeWeapon == 1)
            {
                GameObject gunFX = Instantiate(shottySFX, transform.position +transform.forward*0.7f, transform.rotation);
                gunFX.transform.parent = gameObject.transform;
                anim.SetTrigger("Shoot");
                canShoot = false;
                cooldown = shottyFireRate;
                if (Physics.Raycast(ray, out hit, shottyRange, mask))
                {
                    if (hit.collider)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                        Instantiate(bloodFX, hit.point, transform.rotation);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(cooldown > 0)
        {
            cooldown -= 1;
        }
    }

    public void StingerShoot()
    {
        GameObject gunFX = Instantiate(shottySFX, transform.position + transform.forward * 0.7f, transform.rotation);
        gunFX.transform.parent = gameObject.transform;
        stingerHitBox.SetActive(true);
    }
    public void StingerDisable()
    {
        stingerHitBox.SetActive(false);
    }

}
