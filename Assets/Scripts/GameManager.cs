using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject GMBase;
    public int GMMoney;
    public int income;
    [SerializeField] float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.time;

        if (GMBase == null) gameOver = true;
    }
}
