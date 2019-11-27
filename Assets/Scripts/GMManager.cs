using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMManager : MonoBehaviour
{
    public Camera GMCam;
    public GameObject fireObject;
    public bool validPosition;
    RaycastHit hit;
    public LayerMask rayMask;
    public LayerMask invalidLayer;
    Vector3 lastPosition;
    public float fireDelay;
    public int fireCost;

    float lastFire;
    // Start is called before the first frame update
    void Start()
    {
        GMCam = GameObject.FindGameObjectWithTag("GMCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Physics.Raycast(GMCam.transform.position, GMCam.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity, rayMask);

            {
                if (hit.collider == null)
                {
                    validPosition = false;
                }
                else if (((1 << hit.collider.gameObject.layer) & invalidLayer) != 0) { validPosition = false; }
                else if (hit.point == new Vector3(0, 0, 0))
                {
                    lastPosition = GMCam.WorldToScreenPoint(Input.mousePosition);
                    validPosition = true;
                }
                else if (hit.collider.gameObject != null)
                {
                    lastPosition = hit.point;
                    validPosition = true;
                }

                if (Time.time > fireDelay + lastFire && validPosition)
                {
                    SpawnFire();
                }
            }

        }
        else
        {
            validPosition = false;
        }

    }

    void SpawnFire()
    {
        if (GameManager.mana >= fireCost)
        {
            Instantiate(fireObject, lastPosition, Quaternion.identity);
            lastFire = Time.time;
            GameManager.mana -= fireCost;
        }
    }
}
