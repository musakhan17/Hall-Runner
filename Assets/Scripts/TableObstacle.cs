using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObstacle : HallObstacle
{
    /*
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {

        if (isObstacle && !_fallStarted && Vector3.Distance(player.transform.position, transform.position) <= triggerDistance)
        {
            _fallStarted = true;
            StartCoroutine("Fall");
        }
    }
    */

    public override IEnumerator Move()
    {
        Debug.Log("fall triggered");
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
        _moveDone = true;
    }

}
