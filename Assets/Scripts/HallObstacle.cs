using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallObstacle : MonoBehaviour
{
    public float triggerDistance = 5;
    public GameObject player;
    public bool isObstacle = false;
    protected bool _moveStarted = false;
    protected bool _moveDone = false;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= triggerDistance)
            {
                _moveStarted = true;
            }

            if (isObstacle && _moveStarted && !_moveDone)
            {
                _moveDone = Move();
            }
        }
    }

    /* makes per-frame move update, returns if move is done */
    public virtual bool Move()
    {
        return true;
    }

}
