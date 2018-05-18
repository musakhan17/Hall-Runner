using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour
{

    //most recent hallway object in game
    [SerializeField]
    private GameObject _currentHall;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject Continue_Display;
    [SerializeField]
    private GameObject Look_Around_Display;
    [SerializeField]
    private GameObject Run_Away_Display;
    [SerializeField]
    private GameObject PlayArea;

    private Transform _currentHallEnd;
    private bool _beginningDone = false;

    void Start()
    {
        Time.timeScale = 1;
        _currentHallEnd = _currentHall.transform.Find("End");
        Look_Around_Display.SetActive(true);
        PlayArea.SetActive(false);

    }

    void Update()
    {
        if (!_beginningDone)
        {
            Quaternion rotation = _player.transform.rotation;
            Debug.Log(rotation);
            if (rotation.y < -0.8 || rotation.y > 0.8)
            {
                Look_Around_Display.SetActive(false);
                Run_Away_Display.SetActive(true);
                _beginningDone = true;
                PlayArea.SetActive(true);
                StartCoroutine("HideRunDisplay");
            }
        }

        if (Vector3.Distance(_player.transform.position, _currentHallEnd.position) <= 3)
        {
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

    private IEnumerator HideRunDisplay()
    {
        yield return new WaitForSeconds(3f);
        Run_Away_Display.SetActive(false);
    }


}
