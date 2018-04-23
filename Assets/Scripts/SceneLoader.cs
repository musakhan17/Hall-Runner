using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static float _progress = 0;

    private static bool _butt = true;

    public static void LoadScene(string name, float progress)
    {
        _progress = progress;
        SceneManager.LoadScene(name);
    }

    public static float GetPreviousProgress()
    {
        return _progress;
    }

  /*  public static bool GetNextScene(string name, bool)
    {

    }

   public void  OnTriggerEnter(Collider other)
    {

    }*/
}
