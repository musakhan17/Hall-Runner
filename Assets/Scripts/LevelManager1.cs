using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour
{

    //player distance at which to spawn next hall
    public string scene;

    //most recent hallway object in game
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject Continue_Display;
 
    private Transform _currentHallEnd;

    void Start()
    {
        Time.timeScale = 1;
        _currentHallEnd = _currentHall.transform.Find("End");

    }

    void Update()
    {
        if (Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= 1.5)
        {
            Debug.Log("Musa");
            EndLevel();
        }

    }
    /*
      when player successfully reaches end of level
      */
    private void EndLevel()
    {
        Time.timeScale = 0;
        Continue_Display.gameObject.SetActive(true);
    }

    /*
    load next level - called on click by button
     */
    public void Continue(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


}
