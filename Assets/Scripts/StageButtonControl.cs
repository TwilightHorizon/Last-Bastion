using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonControl : MonoBehaviour
{
    [SerializeField]
    private Button[] button;
    [SerializeField]
    private Button buttonInfinity;

    private void Awake()
    {
        buttonInfinity.enabled = false;
        for (int i = 0; i < button.Length; i++)
        {
            button[i].enabled = false;
            // button[i].GetComponent<SpriteRenderer>().color = new Color()
            
        }

        int highestStage = BackendGameData.Instance.UserGameData.highestStage;

        for (int i = 0; i < highestStage+1; i++)
        {
            button[i].enabled = true;
        }

        if(highestStage == 2) buttonInfinity.enabled = true;

    }




}
