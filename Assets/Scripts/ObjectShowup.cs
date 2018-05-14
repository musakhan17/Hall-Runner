
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShowup : MonoBehaviour
{

    [SerializeField]
    private GameObject _necklace;
    [SerializeField]
    private GameObject _descript;
    [SerializeField]
    private GameObject _nextbutton;
    [SerializeField]
    private float speed;
    private GameObject _player;
    [SerializeField]

    // Use this for initialization 
    void Start()
    {
        _descript.SetActive(false);
        _nextbutton.SetActive(false);
        _necklace.SetActive(false);
        StartCoroutine(Showup());
    }

    // Update is called once per frame 
    IEnumerator Showup()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Wait is over");
        _necklace.SetActive(true);
        _descript.SetActive(true);
        yield return new WaitForSeconds(2);
        _nextbutton.SetActive(true);
    }
    /* void ShifttoLeft()
     {
         Vector3 newPos = new Vector3(-218.0f, -121.0f, 0f);
         //float step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, _necklace.transform.position, Time.deltaTime *speed);
     }*/
    public void Continue(string levelName)
    {
        SceneLoader.LoadScene(levelName, _player.GetComponent<Player>().GetProgress());
    }

}
