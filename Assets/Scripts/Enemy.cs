﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private Queue<Vector3> _wayPoints = new Queue<Vector3>();
    [SerializeField]
    private float speed = 1f;
    private Vector3 _targetWayPoint;
    private bool _move = false;

    void Start()
    {
        _move = false;
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }
    public void StartMoving()
    {
        _move = true;
    }
    public void StopMoving()
    {
        _move = false;
    }
    void Update()
    {
        if (_targetWayPoint == null)
        {
            _targetWayPoint = _wayPoints.Dequeue();
        }

        if (!( _targetWayPoint == null) && _move)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.forward = Vector3.RotateTowards(transform.forward,
                                                  _targetWayPoint - transform.position,
                                                  speed * Time.deltaTime, 0f);
        transform.position = Vector3.MoveTowards(transform.position, _targetWayPoint, speed*Time.deltaTime);

        if(transform.position == _targetWayPoint)
        {
            _targetWayPoint = _wayPoints.Dequeue();
        }
    }

    public void AddWayPoint(Vector3 next)
    {
            _wayPoints.Enqueue(next);
    }
}
