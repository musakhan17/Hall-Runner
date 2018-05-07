using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DoorKnob : VRTK_InteractableObject {

    public GameObject door;
    private SteamVR_Controller.Device controller; // the controller property of the hand

    public void PlayAnimation()
    {
        if (controller.GetHairTrigger())
        {
            door = GameObject.Find("Front_Door");
            door.GetComponent<Animation>().Play("Door");
        }
    }

}
