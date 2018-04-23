using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatText : MonoBehaviour {
    private float speed;
    private Vector3 direction;
    private float fadeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float translation = speed * Time.deltaTime;

        transform.Translate(direction * translation);


	}

    public void Initialize(float speed, Vector3 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }
}
