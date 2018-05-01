
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
    [SerializeField]
    private

    // Use this for initialization 
    void Start()
    {
        _descript.SetActive(false);
        _nextbutton.SetActive(false);
        StartCoroutine(Showup());
    }

    // Update is called once per frame 
    IEnumerator Showup()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over");
        _descript.SetActive(true);
        _nextbutton.SetActive(true);
    }
    void Update()
    {
        Vector3 newPos = new Vector3(-213.0f, -117.0f, 0f);
        //float step = speed * Time.deltaTime; 
        _necklace.transform.position = _necklace.transform.position + newPos;
    }
}
