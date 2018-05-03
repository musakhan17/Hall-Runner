using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;

    public Vector3 direction = Vector3.forward;
    public int lives;

    private float _progress = 0;
    private bool _collision = false;
    private float _height;


    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0.1f, 1, 0);
    }

    void FixedUpdate()
    {
        if (!collision)
        {
            GetComponent<Rigidbody>().velocity = direction * speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * 5 * Time.deltaTime);
            }
            //GetComponent<Rigidbody>().AddForce(direction * speed);
            //transform.Translate(direction * speed * Time.deltaTime);
            //progress += speed * Time.deltaTime;
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

    private void AutoMove()
    {
        direction = _camera.transform.forward;
        direction.y = 0;
        Vector3 startPos = transform.position;
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
        Vector3 endPos = transform.position;
        _progress += Vector3.Distance(endPos, startPos);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
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
