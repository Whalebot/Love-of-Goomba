using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public float aimHeight;
    public static float aimOffset = 1F;
    // Start is called before the first frame update
    void Start()
    {
        aimOffset = aimHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
