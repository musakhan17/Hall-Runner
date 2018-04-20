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
    [SerializeField]
    private float _fadeDuration = 2f;


    void Start()
    {
        FadeToBlack();
        Invoke("FadeToBlack", _fadeDuration);
        Time.timeScale = 1;

    }

    private void FadeToBlack()
    {
        SteamVR_Fade.Start(Color.clear, 0f);

        SteamVR_Fade.Start(Color.black, _fadeDuration);

    }

    private void FadeFromBlack()
    {
        SteamVR_Fade.Start(Color.black, 0f);
        SteamVR_Fade.Start(Color.clear, _fadeDuration);
    }

    /*
    load next level - called on click by button
     */
    public void Continue(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


}
