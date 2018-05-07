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
    private float _riseHeight = 10f;
    [SerializeField]
    private float _horizontalDistance = .2f;
    private float _rotated = 0;
    private bool _riseDone = false;
    private bool _horizontalDone = false;
    private bool _rotateDone = false;
    private Vector3 _forward;
    private float _horizontalMoved = 0;

    public override void Start()
    {
        base.Start();
        _forward = transform.forward;
    }

    public override bool Move()
    {
        Debug.Log("fall triggered");
        while (Mathf.Abs(transform.eulerAngles.x) < 90)
        {
            Vector3 toRise = Vector3.up * _riseSpeed * Time.deltaTime;
            if (transform.position.y + toRise.y > _riseHeight)
            {
                toRise.y = _riseHeight - transform.position.y;
                _riseDone = true;
            }
            transform.Translate(toRise, Space.World);
        }
        if (! _horizontalDone)
        {
            Vector3 toMove = _forward * _riseSpeed * Time.deltaTime;
            if (Vector3.Distance(Vector3.zero, toMove) + _horizontalMoved <= _horizontalDistance)
            {
                transform.Translate(toMove, Space.World);
                _horizontalMoved += Vector3.Distance(Vector3.zero, toMove);
            }
            else{
                _horizontalDone = true;
            }
        }
        if (! _rotateDone)
        {
            float toRotate = _turnSpeed * Time.deltaTime;
            if (transform.eulerAngles.x + toRotate >= 90)
            {
                toRotate = 90 - transform.eulerAngles.x;
                _rotateDone = true;
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
        if (_rotateDone && _riseDone && _horizontalDone)
        {
            return true;
        }
        else{
            return false;
        }
    }

}
