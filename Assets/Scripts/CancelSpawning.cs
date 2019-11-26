using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelSpawning : MonoBehaviour
{
    GameObject currentSpawnableObjectInstance;
    bool currentObjectSpawnedOnce;
    public Animator thisButtonAnimator;
    GameMasterPointsMechanic PointsMechanic;

    void Start(){
        thisButtonAnimator = GetComponent<Animator>();
        PointsMechanic = FindObjectOfType<GameMasterPointsMechanic>();
    }
    void Update()
    {
       // currentSpawnableObjectInstance = ObjectSpawner.thisInstanceOfSpawnableGO;
        currentObjectSpawnedOnce = ObjectSpawner.spawnedOnce;
    }

    public void RemoveCurrentSpawnableObject(){
            Destroy(currentSpawnableObjectInstance);
            thisButtonAnimator.SetBool("ShowCancelSpawningButton", false);
            ObjectSpawner.spawnedOnce = false;
            GetComponent<Button>().interactable = false;
            PointsMechanic.giveBackLostPoints(currentSpawnableObjectInstance);
    }
}
