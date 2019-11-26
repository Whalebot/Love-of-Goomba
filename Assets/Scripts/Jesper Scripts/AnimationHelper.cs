using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dodge()
    {
        player.Dodge();
    }

    void CanInput()
    {
        player.canInput = true;
    }

    void SlideCheck()
    {
        player.SlideCheck();
    }

    void CantMove()
    {
        player.canMove = false;
    }
}
