using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSpawner : MonoBehaviour
{
    public bool mousePressed = false;
    public GameObject spawnableGO;
    public Camera gameMasterCamera;
    Vector2 mousePos = new Vector2();
    Event currentEvent;
    bool spawnedOnce = false;
    private GameObject thisInstanceOfSpawnableGO;
    bool placementIsValid = false;
    RaycastHit hit;
    void Start(){
    }

    public void spawnObject(){
        if(!spawnedOnce){
        thisInstanceOfSpawnableGO = Instantiate(spawnableGO, gameMasterCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)), Quaternion.identity);
        spawnedOnce = true;
        }
    }
     void OnGUI()
    {
        currentEvent = Event.current;
    }

    void Update(){
        PositionSpawnableGO(thisInstanceOfSpawnableGO);
        PlacementIsValidCheck(hit, thisInstanceOfSpawnableGO);
    }
    void PositionSpawnableGO(GameObject spawnedGO){
        if(spawnedOnce == true){
            
            Physics.Raycast(gameMasterCamera.transform.position, gameMasterCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity);
            if(hit.point == new Vector3(0,0,0)){
                thisInstanceOfSpawnableGO.transform.position = gameMasterCamera.WorldToScreenPoint(Input.mousePosition);
            }
            else{
                thisInstanceOfSpawnableGO.transform.position = hit.point;
            }
            
            if(Input.GetMouseButtonDown(0) && spawnedOnce && placementIsValid){
                spawnedOnce = false;
            }
            Debug.DrawRay(gameMasterCamera.transform.position, gameMasterCamera.ScreenPointToRay(Input.mousePosition).direction*100, Color.white, 0f, true);
        }
    }

    void PlacementIsValidCheck(RaycastHit hitReference, GameObject spawnedGOCheckForPlacement){
        if(spawnedOnce){
            if(hitReference.normal == Vector3.up && spawnedGOCheckForPlacement.GetComponent<CheckForValidPlacement>().playerInBounds){
                foreach(MeshRenderer mr in spawnedGOCheckForPlacement.GetComponentsInChildren<MeshRenderer>()){
                    mr.GetComponent<MeshRenderer>().material.color = new Color(0.6320754f,0.6320754f,0.6320754f);
                }
                placementIsValid = true;
            }
            else{
                foreach(MeshRenderer mr in spawnedGOCheckForPlacement.GetComponentsInChildren<MeshRenderer>()){
                    mr.GetComponent<MeshRenderer>().material.color = new Color(1f,0f,0f);
                }
                placementIsValid = false;
            }
        }
    }
}
