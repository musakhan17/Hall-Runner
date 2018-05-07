﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;

    public Vector3 direction = Vector3.forward;
    public int lives;

    private float _progress = 0;
    private Vector3 _lastPosition;
    private bool _collision = false;
    private float _height;


    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0.1f, 1, 0);
        _height = _camera.transform.position.y;
        _lastPosition = transform.position;
        speed = _levelManager.GetComponent<LevelManager>()._playerSpeed;
    }

    void Update()
    {
        if (!collision)
        {
            if (Input.GetKey("c"))
            { // press C to crouch
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
           // else if (_camera.transform.position.y < _height)
            //{ // or use player height
              //  transform.localScale = new Vector3(1, _camera.transform.position.y / _height, 1);
            //}
            else
            {
              transform.localScale = new Vector3(1, 1, 1);
              transform.GetComponent<CapsuleCollider>().height = _camera.transform.position.y;
            }
            //Debug.Log(_camera.transform.position.y);
            //AutoMove();
            Vector3 newPos = transform.position;
            AddProgress(Vector3.Distance(newPos, _lastPosition));
            _lastPosition = newPos;
        }
    }

    public void AutoMove()
    {
        direction = _camera.transform.forward;
        direction.y = 0;
        Vector3 translate = direction * speed * Time.deltaTime;
        //transform.Translate(translate);

        //update position of everything under root parent, so motion tracking doesn't move
        //anything away from parents
        Transform[] allInTree = transform.root.GetComponentsInChildren<Transform>();
        Vector3[] positions = new Vector3[allInTree.Length];
        for (int i = 0; i < allInTree.Length; i++)
        {
            positions[i] = allInTree[i].position;
        }
        for (int i = 0; i < allInTree.Length; i++)
        {
            allInTree[i].position = positions[i] + translate;
        }

    }

    public Vector3 GetHorizontalDirection()
    {
        direction = _camera.transform.forward;
        direction.y = 0;
        return direction;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetProgress()
    {
        return _progress;
    }

    public void AddProgress(float val)
    {
        _progress += val;
    }

    public void SetProgress(float val)
    {
        _progress = val;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy")
        {
            lives--;
            if (lives < 1)
            {
                //GetComponent<Rigidbody>().velocity = direction * speed;
                GetComponent<Rigidbody>().freezeRotation = false;
                _collision = true;
                Debug.Log("Player collided");
                _levelManager.GetComponent<LevelManager>().FailLevel();
            }
        }
    }
}
