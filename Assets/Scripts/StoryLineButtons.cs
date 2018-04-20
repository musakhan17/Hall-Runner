using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLineButtons : MonoBehaviour {

    bool _destination = true;
    
	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody>().freezeRotation = true;
        //GetComponent<Rigidbody>().centerOfMass = new Vector3(0.1f, 1, 0);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void buttonClick(Collider other)
    {
        if (other.tag == "Button1")
        {
            
        }
        else if (other.tag == "Button2")
        {
            
        }
    }
}
