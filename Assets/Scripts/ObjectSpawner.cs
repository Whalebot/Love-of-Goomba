using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{    public GameObject spawnableGO;

    public Camera gameMasterCamera;
    [SerializeField] private LayerMask rayMask;
    Vector2 mousePos = new Vector2();
    Event currentEvent;
    public static bool spawnedOnce;
    public static GameObject thisInstanceOfSpawnableGO;
    bool placementIsValid = false;
    GameObject cancelSpawning;
    Animator cancelSpawningAnimator;
    RaycastHit hit;
    GameMasterPointsMechanic PointsMechanic;

    void Start()
    {
        spawnedOnce = false;
        cancelSpawning = FindObjectOfType<CancelSpawning>().gameObject;
        cancelSpawningAnimator = cancelSpawning.GetComponent<Animator>();
        PointsMechanic = FindObjectOfType<GameMasterPointsMechanic>();
        gameMasterCamera = GameObject.FindGameObjectWithTag("GMCam").GetComponent<Camera>();
    }

    public void spawnObject()
    {
        if (!spawnedOnce)
        {
            cancelSpawning.gameObject.GetComponent<Button>().interactable = true;
            cancelSpawning.gameObject.SetActive(true);
            cancelSpawningAnimator.SetBool("ShowCancelSpawningButton", true);
            thisInstanceOfSpawnableGO = Instantiate(spawnableGO, gameMasterCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)), Quaternion.identity);
            PointsMechanic.substractPoints(thisInstanceOfSpawnableGO);
            spawnedOnce = true;
        }
    }
    void OnGUI()
    {
        currentEvent = Event.current;
    }

    void Update()
    {
        PositionSpawnableGO(thisInstanceOfSpawnableGO);
        PlacementIsValidCheck(hit, thisInstanceOfSpawnableGO);
    }
    void PositionSpawnableGO(GameObject spawnedGO)
    {
        if (spawnedOnce == true)
        {
            foreach(GameObject spawnerButton in GameMasterPointsMechanic.spawnerButtons){
                spawnerButton.GetComponent<Button>().interactable = false;
            }
            Physics.Raycast(gameMasterCamera.transform.position, gameMasterCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity, rayMask);
            if (hit.point == new Vector3(0, 0, 0))
            {
                thisInstanceOfSpawnableGO.transform.position = gameMasterCamera.WorldToScreenPoint(Input.mousePosition);
            }
            else
            {
                thisInstanceOfSpawnableGO.transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(0) && spawnedOnce && placementIsValid)
            {
                ActivateAI();
                spawnedOnce = false;     
                foreach(GameObject spawnerButton in GameMasterPointsMechanic.spawnerButtons){
                    spawnerButton.GetComponent<Button>().interactable = true;
                }
                cancelSpawning.gameObject.GetComponent<Button>().interactable = false;
                cancelSpawningAnimator.SetBool("ShowCancelSpawningButton", false);
            }
            Debug.DrawRay(gameMasterCamera.transform.position, gameMasterCamera.ScreenPointToRay(Input.mousePosition).direction * 100, Color.white, 0f, true);
        }
    }

    void PlacementIsValidCheck(RaycastHit hitReference, GameObject spawnedGOCheckForPlacement)
    {
        if (spawnedOnce)
        {
            if (hitReference.normal == Vector3.up && spawnedGOCheckForPlacement.GetComponent<CheckForValidPlacement>().playerInBounds)
            {
                foreach (MeshRenderer mr in spawnedGOCheckForPlacement.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.GetComponent<MeshRenderer>().material.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                }
                placementIsValid = true;
            }
            else
            {
                foreach (MeshRenderer mr in spawnedGOCheckForPlacement.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f);
                }
                placementIsValid = false;
            }
        }
    }

    void ActivateAI()
    {
        if (thisInstanceOfSpawnableGO.GetComponent<AI>() != null)
            thisInstanceOfSpawnableGO.GetComponent<AI>().Activate();

    }
}
