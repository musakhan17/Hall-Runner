using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockObstacle : HallObstacle
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

    [SerializeField]
    private float _riseSpeed = 30f;
    [SerializeField]
    private float _turnSpeed = 30f;

    [SerializeField]
    private float _riseHeight = 0.8f;
    private float _rotated = 0;
    private bool _riseDone = false;

    public override bool Move()
    {
        if(transform.position.y < _riseHeight && ! _riseDone)
        {
            Vector3 toRise = Vector3.up * _riseSpeed * Time.deltaTime;
            if (transform.position.y + toRise.y > _riseHeight)
            {
                toRise.y = _riseHeight - transform.position.y;
                _riseDone = true;
            }
            transform.Translate(toRise);
        }
        if (_rotated < 90)
        {
            float toRotate = _turnSpeed * Time.deltaTime;
            if (transform.eulerAngles.x + toRotate > 90)
            {
                toRotate = 90 - transform.eulerAngles.x;
            }
            transform.Rotate(toRotate, 0, 0);
            _rotated += toRotate;
        }
        /*
        GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 90,
                                                     new Vector3(transform.position.x,
                                                                 2,
                                                                 transform.position.z));
                                                                 */
        //GetComponent<Rigidbody>().AddForce(Vector3.down * 1000);
        if (transform.position.y >= _riseHeight && _rotated >= 90)
        {
            return true;
        }
        else{
            return false;
        }
    }

}
