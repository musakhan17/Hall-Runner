using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxObstacle : HallObstacle
{

    private Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override bool Move()
    {
        anim.SetBool("trigger", true);
        return true;
    }

}
