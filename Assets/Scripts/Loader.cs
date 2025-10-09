using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static string SceneToLoad;
    private static string LoadingScene;

    public static void LoadScene(string sceneName)
    {
        SceneToLoad = sceneName;
        SceneManager.LoadScene("Loading");
    }

    public static void LoaderCall()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    public static void ReloadScene()
    {
        SceneToLoad = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Loading");
    }
}
