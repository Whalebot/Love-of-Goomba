using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaytestUI : MonoBehaviour
{
    public Text text;
    public Status status;
    public Shooter shooter;
    public Slider hp;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
        shooter = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shooter>();
        hp = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = shooter.lvl.ToString();
        if(hp != null)
      hp.value = status.health;
    }
}
