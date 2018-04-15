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
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
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

    /* makes per-frame move update, returns if move is done */
    public virtual bool Move()
    {
        /*
        while (Mathf.Abs(transform.eulerAngles.x) < 90)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 90,
                                                         new Vector3(transform.position.x,
                                                                     2,
                                                                     transform.position.z));
            yield return null;
        }
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(Vector3.down * 1000);
        _moveStarted = true;
        */
        return true;
    }

}
