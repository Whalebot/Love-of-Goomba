using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForValidPlacement : MonoBehaviour
{
    public bool playerInBounds;

    // Start is called before the first frame update
    void Start()
    {
        playerInBounds = true;
    }

    void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        playerInBounds = false;
    }

    void OnTriggerExit(Collider other)
    {
        playerInBounds = true;
    }
}
