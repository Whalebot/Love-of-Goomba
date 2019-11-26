using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{

    Player player;
    Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponentInParent<Player>();
        shooter = transform.parent.GetComponentInChildren<Shooter>();
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

    void Stinger()
    {
        player.Stinger();
    }
    void StingerShoot()
    {
        shooter.StingerShoot();
    }
    void Backslide()
    {
        player.Backslide();
    }
    void BackslideShoot()
    {
        shooter.BackslideShoot();
    }
    void HitBoxDisable()
    {
        shooter.HitboxDisable();
    }

    void CantMove()
    {
        player.canMove = false;
    }
    void stopMovement()
    {
        player.StopMovement();
    }
}
