using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallSpawn : MonoBehaviour
{

    public float spawnDist = 10.0f;
    public float hallLength = 20.0f;
    [SerializeField]
    private GameObject _hallPrefab;
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject player;
    private Transform _currentHallEnd;

    private Queue<GameObject> _instantiatedHalls = new Queue<GameObject>();
    private bool _flipNext = true;

    void Start()
    {
        _currentHallEnd = _currentHall.transform.Find("End");
        _instantiatedHalls.Enqueue(_currentHall);

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, _currentHallEnd.position) <= spawnDist)
        {
            Quaternion rotation = Quaternion.identity;
            if(_flipNext)
            {
                rotation = Quaternion.Euler(0, 180, 0);
            }
            _flipNext = ! _flipNext;

            Vector3 scale = _currentHall.transform.localScale;
            GameObject newHall = Instantiate(_hallPrefab, _currentHallEnd.position, rotation);

            //flip new hall
            scale.x = scale.x * -1;
            newHall.transform.localScale = scale; 

            //move to correct location
            newHall.transform.position = _currentHallEnd.position;

            //Vector3 translate = Vector3.forward * hallLength;
            //transform.Translate(translate);

            //update current hall
            _currentHall = newHall;
            _currentHallEnd = _currentHall.transform.Find("End");
            _instantiatedHalls.Enqueue(_currentHall);
        }
        if(_instantiatedHalls.Count > 2)
        {
            Destroy(_instantiatedHalls.Dequeue());
        }
    }
}
