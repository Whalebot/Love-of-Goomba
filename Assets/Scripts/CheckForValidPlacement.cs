using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForValidPlacement : MonoBehaviour
{
     public bool playerInBounds;

    // Start is called before the first frame update
    void Start()
    {
        playerInBounds = false;
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "ValidPlacementTag"){
            playerInBounds = true;
        }
    }
    
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "ValidPlacementTag"){
            playerInBounds = false;
        }
    }
}
