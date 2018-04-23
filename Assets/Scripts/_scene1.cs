using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _scene1 : MonoBehaviour
{

    //most recent hallway object in game
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject _player;


    void Start()
    {
        Time.timeScale = 1;

    }

    void Update()
    {

    }

    /*
    load next level - called on click by button
     */
    public void Continue(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


}
