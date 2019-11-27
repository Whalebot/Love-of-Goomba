using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Player player;
    public float gunFireRate;
    public float gunFireRatelvl2;
    public float gunFireRatelvl3;
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
    public int shottyBullets;
    public int shottyBulletslvl2;
    public int shottyBulletslvl3;
    public float shottySpread;
    public LayerMask mask;
    public GameObject gunSFX;
    public GameObject superGunSFX;
    public GameObject shottySFX;
    public GameObject smallExplosionFX;
    public GameObject bigExplosionFX;
    public GameObject chargeSFX;
    public GameObject bloodFX;
    public GameObject dustFX;
    public GameObject stingerHitBox;
    public GameObject backslideHitBox;
    public bool canShoot = true;
    public float cooldown;
    public float gunCharge;
    public int maxCharge;
    public int lvl;
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

        if(GameManager.killCount == 10)
        {
            lvl = 2;
            gunFireRate = gunFireRatelvl2;
            shottyBullets = shottyBulletslvl2;
        }
        if(GameManager.killCount == 20)
        {
            lvl = 3;
            gunFireRate = gunFireRatelvl3;
            shottyBullets = shottyBulletslvl3;
        }



        if(cooldown <= 0)
        {
            canShoot = true;
        }


        if(player.activeWeapon == 0 && Input.GetButton("Fire1"))
        {
            gunCharge += 1;
        }

        if(gunCharge == maxCharge)
        {
            Instantiate(chargeSFX, transform.position, Quaternion.identity);
        }

        anim.SetFloat("Charge", gunCharge);

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if(Input.GetButtonUp("Fire1"))
        {
            if (player.activeWeapon == 0 && gunCharge > maxCharge)
            {
                Instantiate(superGunSFX, transform.position, Quaternion.identity);
                Instantiate(smallExplosionFX, transform.position, Quaternion.identity);
                anim.SetTrigger("Shoot");
                cooldown = gunFireRate;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider)
                    {
                        if(hit.transform.GetComponent<Status>() != null)
                        {
                            if (hit.transform.gameObject.layer == 9)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= gunDamagelvl4;
                                status.TakePushback(transform.forward * gunPushback);
                                status.HitStun = gunHitStun;
                                Instantiate(bigExplosionFX, hit.point, Quaternion.identity);
                                Instantiate(bloodFX, hit.point, Quaternion.identity);
                            }
                            if (hit.transform.gameObject.layer == 10)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= gunDamagelvl4;
                                status.TakePushback(transform.forward * gunPushback);
                                status.HitStun = gunHitStun;
                                Instantiate(bigExplosionFX, hit.point, Quaternion.identity);
                            }
                        }
                        else
                        {
                            Instantiate(bigExplosionFX, hit.point, Quaternion.identity);
                        }
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
                        if(hit.transform.GetComponent<Status>() != null)
                        {
                            if (hit.transform.gameObject.layer == 9)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= gunDamage;
                                status.TakePushback(transform.forward * gunPushback);
                                status.HitStun = gunHitStun;
                                Instantiate(bloodFX, hit.point, transform.rotation);
                            }
                            if (hit.transform.gameObject.layer == 10)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= gunDamage;
                                status.TakePushback(transform.forward * gunPushback);
                                status.HitStun = gunHitStun;
                                Instantiate(dustFX, hit.point, transform.rotation);
                            }
                        }
                        else
                        {
                            Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal,transform.up));
                        }
                        
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

                for (int i = 0; i < shottyBullets; i++)
                {
                    Ray shottyRay = cam.ViewportPointToRay(new Vector3(Random.Range(0.5f - shottySpread, 0.5f + shottySpread), Random.Range(0.5f - shottySpread, 0.5f + shottySpread)));

                    if (Physics.Raycast(shottyRay, out hit, shottyRange, mask))
                    {
                        if (hit.collider && hit.transform.GetComponent<Status>() != null)
                        {
                            if (hit.transform.gameObject.layer == 9)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= shottyDamage;
                                status.TakePushback(transform.forward * shottyPushback);
                                status.HitStun = shottyHitStun;
                                Instantiate(bloodFX, hit.point, transform.rotation);
                            }
                            if (hit.transform.gameObject.layer == 10)
                            {
                                Status status = hit.transform.GetComponent<Status>();
                                status.Health -= shottyDamage;
                                status.TakePushback(transform.forward * shottyPushback);
                                status.HitStun = shottyHitStun;
                                Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                            }
                        }
                        else
                        {
                            Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                        }
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
        canShoot = false;
        cooldown = shottyFireRate;
        GameObject gunFX = Instantiate(shottySFX, transform.position + transform.forward * 0.7f, transform.rotation);
        gunFX.transform.parent = gameObject.transform;

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        for (int i = 0; i < shottyBullets; i++)
        {
            Ray shottyRay = cam.ViewportPointToRay(new Vector3(Random.Range(0.5f - shottySpread, 0.5f + shottySpread), Random.Range(0.5f - shottySpread, 0.5f + shottySpread)));

            if (Physics.Raycast(shottyRay, out hit, shottyRange, mask))
            {
                if (hit.collider && hit.transform.GetComponent<Status>() != null)
                {
                    if (hit.transform.gameObject.layer == 9)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                        Instantiate(bloodFX, hit.point, transform.rotation);
                    }
                    if (hit.transform.gameObject.layer == 10)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                        Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                    }
                }
                else
                {
                    Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                }
            }
        }
    }
    public void BackslideShoot()
    {
        canShoot = false;
        cooldown = shottyFireRate;
        GameObject gunFX = Instantiate(shottySFX, transform.position + transform.forward * 0.7f, transform.rotation);
        gunFX.transform.parent = gameObject.transform;

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        for (int i = 0; i < shottyBullets; i++)
        {
            Ray shottyRay = cam.ViewportPointToRay(new Vector3(Random.Range(0.5f - shottySpread, 0.5f + shottySpread), Random.Range(0.5f - shottySpread, 0.5f + shottySpread)));
            Ray shottyRayReverse = new Ray(transform.position, new Vector3(-shottyRay.direction.x, -shottyRay.direction.y, -shottyRay.direction.z));
            if (Physics.Raycast(shottyRayReverse, out hit, shottyRange, mask))
            {
                if (hit.collider && hit.transform.GetComponent<Status>() != null)
                {
                    if (hit.transform.gameObject.layer == 9)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                        Instantiate(bloodFX, hit.point, transform.rotation);
                    }
                    if (hit.transform.gameObject.layer == 10)
                    {
                        Status status = hit.transform.GetComponent<Status>();
                        status.Health -= shottyDamage;
                        status.TakePushback(transform.forward * shottyPushback);
                        status.HitStun = shottyHitStun;
                        Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                    }
                }
                else
                {
                    Instantiate(dustFX, hit.point, Quaternion.LookRotation(hit.normal, transform.up));
                }
            }
        }

    }
    public void HitboxDisable()
    {
        stingerHitBox.SetActive(false);
        backslideHitBox.SetActive(false);
    }

}
