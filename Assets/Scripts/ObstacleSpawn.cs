﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _furnitureObstacles;
    [SerializeField]
    private GameObject[] _floatingObstacles;
    [SerializeField]
    private float _fireFrequency = 0.05f; //chance of spawning fire @ a spawn point
    [SerializeField]
    private float _furnitureFrequency = 0.5f; //chance of spawning an object @ a spawn point
    [SerializeField]
    private float _furnitureIsObstacleFrequency = 0.1f; //chance an object will become an obstacle
    [SerializeField]
    private float _obstacleTriggerDistance = 8f; //chance an object will become an obstacle
    private List<GameObject> _furnitureSpawnPoints = new List<GameObject>();
    private List<GameObject> _fireSpawnPoints = new List<GameObject>();


    public void Init(float fireFrequency, float furnitureFrequency, float furnitureIsObstacleFrequency,
                     float obstacleTriggerDistance)
    {
        _fireFrequency = fireFrequency;
        _furnitureFrequency = furnitureFrequency;
        _furnitureIsObstacleFrequency = furnitureIsObstacleFrequency;
        _obstacleTriggerDistance = obstacleTriggerDistance;
        Spawn();

    }
    private void Spawn()
    {
        //get spawn points
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.tag == "FurnitureSpawn")
            {
                _furnitureSpawnPoints.Add(child.gameObject);
            } else if (child.tag == "FireSpawn")
            {
                _fireSpawnPoints.Add(child.gameObject);
            }
        }

        foreach (GameObject location in _furnitureSpawnPoints)
        {
            if (Random.Range(0f, 1f) <= _furnitureFrequency)
            {
                //get random obstable
                int index = Random.Range(0, _furnitureObstacles.Length);
                //Debug.Log(index);
                GameObject obstacle = _furnitureObstacles[index];

                //pick side of hallway to spawn on
                Transform[] points = location.GetComponentsInChildren<Transform>();
                points = points.Where(c => c.tag == "SpawnPoint").ToArray();
                Transform spawnPoint = points[Random.Range(0, points.Length)];

                //instantiate
                GameObject newObject = Instantiate(obstacle,
                                                     spawnPoint.position,
                                                     spawnPoint.rotation
                                                    );
                newObject.transform.parent = gameObject.transform;
                //decide if object will be an obstacle
                HallObstacle hallObstacle = newObject.GetComponent<HallObstacle>();
                hallObstacle.triggerDistance = _obstacleTriggerDistance;
                if (hallObstacle != null)
                {
                    hallObstacle.isObstacle = Random.Range(0f, 1f) <= _furnitureIsObstacleFrequency
                                              ? true : false;
                }
            }
        }

        //generate fire obstacles
        foreach (GameObject location in _fireSpawnPoints)
        {
            if (Random.Range(0f, 1f) <= _fireFrequency)
            {
                //pick side of hallway to spawn on
                Transform[] points = location.GetComponentsInChildren<Transform>();
                points = points.Where(c => c.tag == "SpawnPoint").ToArray();
                Transform spawnPoint = points[Random.Range(0, points.Length)];

                //instantiate
                GameObject newObject =
                        Instantiate(_floatingObstacles[Random.Range(0, _floatingObstacles.Length)],
                                    spawnPoint.position, spawnPoint.parent.rotation);
                newObject.transform.parent = gameObject.transform;
            }
        }

    }
}
