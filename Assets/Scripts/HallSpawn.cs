using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallSpawn : MonoBehaviour
{

    public GameObject hall;
    public GameObject player;
    public float spawnDist = 10.0f;
    public float hallLength = 20.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= spawnDist)
        {
            Instantiate(hall, transform.position, Quaternion.identity);
            Vector3 translate = Vector3.forward * hallLength;
            transform.Translate(translate);
        }
    }
}
