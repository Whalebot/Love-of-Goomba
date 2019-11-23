using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaytestUI : MonoBehaviour
{
    Text text;
    Status status;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = status.health.ToString();
    }
}
