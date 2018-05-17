using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static float _progress = 0;

    public static void LoadScene(string name, float progress)
    {
        _progress = progress;
        SceneManager.LoadScene(name);
    }

    public static float GetPreviousProgress()
    {
        return _progress;
    }

    public static void ResetProgress()
    {
        _progress = 0;
    }
}
