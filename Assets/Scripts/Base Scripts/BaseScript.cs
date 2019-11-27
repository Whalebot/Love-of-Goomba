using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    Status status;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        manager.GMBase = status;
    }

    // Update is called once per frame
    void Update()
    {
        if (status.isDead) {
            Die();
        }
    }

    void Die() {
        GameManager.gameOver = true ;
        Destroy(gameObject);
    }
}
