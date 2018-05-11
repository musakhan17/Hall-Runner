using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchObstacle : HallObstacle
{

    private float _rotated = 0;
    private float _startAngle = 0;
    private bool _rotateDone = false;
    private Vector3 _force;
    private float _speed = 90;
    private bool _started = false;

    public override void Start()
    {
        base.Start();
        _startAngle = transform.eulerAngles.y;
        _force = transform.forward * 90;
    }
    public override bool Move()
    {
        if (! _started)
        {
            Debug.Log("Bench triggered");
            _started = true;

        }
        if (Mathf.Abs(transform.eulerAngles.y - _startAngle) >= 30)
        {
            _rotateDone = true;
        }
        if (!_rotateDone)
        {
            /*GetComponent<Rigidbody>().AddForceAtPosition(_force,
                                                     new Vector3(transform.position.x + 5,
                                                                 -1,
                                                                 transform.position.z));
                                                                 */
            transform.Rotate(Vector3.up, _speed * Time.deltaTime);
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
