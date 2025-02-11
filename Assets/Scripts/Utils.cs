using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneNames { Intro = 0, Login, Lobby, Level01, Level02, Level03, Level04, Level05, Level06, Level07, Level08, Level09, Level10, Level11, Level12, LevelINF}
public static class Utils
{

    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName = "")
    {
        if( sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

    }   

    public static void LoadScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

}
