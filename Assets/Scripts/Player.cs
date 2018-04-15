using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;
    public GameObject camera;
    public Vector3 direction = Vector3.forward;


    private float _progress = 0;
    private bool _collision = false;
    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0.1f, 1, 0);
    }

    void FixedUpdate()
    {
        if (!_collision)
        {
            //GetComponent<Rigidbody>().velocity = direction * speed;
            direction = camera.transform.forward;
            direction.y = 0;
            Vector3 startPos = transform.position;
            transform.Translate(direction * speed * Time.deltaTime);
            Vector3 endPos = transform.position;
            _progress += Vector3.Distance(endPos, startPos);

            if (Input.GetKey("c"))
            { // press C to crouch
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * 5 * Time.deltaTime);
            }
            //GetComponent<Rigidbody>().AddForce(direction * speed);
            //transform.Translate(direction * speed * Time.deltaTime);
            //progress += speed * Time.deltaTime;
        }
    }

    public float GetProgress()
    {
        return _progress;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //GetComponent<Rigidbody>().velocity = direction * speed;
            GetComponent<Rigidbody>().freezeRotation = false;
            _collision = true;
            Debug.Log("Player collided");
        }

    }
}
