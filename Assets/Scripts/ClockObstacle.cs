using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockObstacle : MonoBehaviour
{

    public string orientation;
    public float triggerDistance = 5;
    public GameObject player;
    private Vector3 _fallDirection;
    private bool _fallStarted = false;
    private bool _fallDone = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (orientation == "right")
        {
            _fallDirection = Vector3.right;
        }
        else
        {
            _fallDirection = Vector3.left;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float angle = Mathf.Abs(transform.eulerAngles.z);
        if (_fallStarted &&  angle >= 90)
        {
            //GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(Vector3.down * 1000);
            _fallDone = true;
        }
        else if (!_fallDone && Vector3.Distance(player.transform.position, transform.position) <= triggerDistance)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(_fallDirection * 90,
                                                         new Vector3(transform.position.x,
                                                                     2,
                                                                     transform.position.z));
            _fallStarted = true;
            Debug.Log("fall triggered");
        }
    }
}
