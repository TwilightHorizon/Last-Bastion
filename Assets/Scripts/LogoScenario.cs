using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScenario : MonoBehaviour
{

    [SerializeField]
    private Progress progress;
    [SerializeField]
    private SceneNames nextScene;

    private void Awake()
    {
        SystemSetup();
    }


    private void SystemSetup()
    {
        Application.runInBackground = true;
        
        // W:H = 16:9
        // 16H = 9W
        // H = 9W/16

        int width = Screen.width;
        int height = (int)(width*9.0f/16);
        Screen.SetResolution(width, height, true);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);

    }

    private void OnAfterProgress()
    {
        Utils.LoadScene(nextScene);
    }

}

