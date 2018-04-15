using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObstacle : HallObstacle
{

    private float _rotated = 0;
    private float _prevAngle = 0;
    private bool _rotateDone = false;

    public override bool Move()
    {
        Debug.Log("table triggered");
        if (transform.eulerAngles.x >= 90)
        {
            _rotateDone = true;
        }
        if (!_rotateDone)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 90,
                                                     new Vector3(transform.position.x,
                                                                 2,
                                                                 transform.position.z));
        }
        else
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(Vector3.down * 1000);
            return true;
        }
        return false;
    }

}
