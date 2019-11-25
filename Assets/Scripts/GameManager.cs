using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject GMBase;
    public int GMMoney;
    public int income;
    public float incomeTimer;
    public float lastIncome;
    [SerializeField] float timeCounter;
    public GameMasterPointsMechanic gmPoint;
    // Start is called before the first frame update
    void Start()
    {
        lastIncome = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.time;

        if (GMBase == null) gameOver = true;

        if (Time.time > incomeTimer + lastIncome)
        {
            lastIncome = Time.time;
            GiveIncome();
        }

    }

    public void GiveIncome() {
        gmPoint.GameMasterPointsForSpawning += income;
    }
}
