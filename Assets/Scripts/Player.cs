using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;
    [SerializeField]
    private GameObject _levelManager;
    [SerializeField]
    private GameObject _camera;
    public Vector3 direction = Vector3.forward;


    private float _progress = 0;
    private bool _collision = false;
    private float _height;


    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0.1f, 1, 0);
        _height = _camera.transform.position.y;
    }

    void FixedUpdate()
    {
        if (!_collision)
        {
            if (Input.GetKey("c"))
            { // press C to crouch
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else if (_camera.transform.position.y < _height)
            { // or use player height
              // TODO test if this actually works
                transform.localScale = new Vector3(1, _camera.transform.position.y / _height, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            Debug.Log(_camera.transform.position.y);
            AutoMove();
        }
    }

    public float GetProgress()
    {
        return _progress;
    }

    private void AutoMove()
    {
        direction = _camera.transform.forward;
        direction.y = 0;
        Vector3 startPos = transform.position;
        transform.Translate(direction * speed * Time.deltaTime);
        Vector3 endPos = transform.position;
        _progress += Vector3.Distance(endPos, startPos);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //GetComponent<Rigidbody>().velocity = direction * speed;
            GetComponent<Rigidbody>().freezeRotation = false;
            _collision = true;
            Debug.Log("Player collided");
            _levelManager.GetComponent<LevelManager>().FailLevel();
        }

    }
}
