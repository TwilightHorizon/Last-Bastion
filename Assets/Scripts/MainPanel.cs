using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{

    public void ButtonClickGameStart()
    {
        Utils.LoadScene(SceneNames.Level01);
    }


}
