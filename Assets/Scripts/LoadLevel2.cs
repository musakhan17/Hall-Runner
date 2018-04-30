using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{

    /*
    load next level - called on click by button
     */
    public void Continue(string level2)
    {
        SceneManager.LoadScene(level2);
    }
}
