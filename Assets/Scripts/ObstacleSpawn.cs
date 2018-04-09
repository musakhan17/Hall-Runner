using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{

    public GameObject[] obstacles;
    [SerializeField]
    private float frequency = 0.1f;
    // Use this for initialization
    void Start()
    {
        //get spawn points

        /*
        for each spawnpoint {
          if rand < frequency {
            get rand obstable
            figure out orientation and position
            instantiate
          }
        }
        */

    }

}
