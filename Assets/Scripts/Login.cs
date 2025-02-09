using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using System;

public class Login : LoginBase
{
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputFieldPW;

    [SerializeField]
    private Button buttonLogin;

    [SerializeField]
    private SceneNames nextScene;

    public void OnClickLogin()
    {
        ResetUI(imageID, imagePW);

        if(IsFieldDataEmpty(imageID, inputFieldID.text, "ID")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "PW")) return;

        buttonLogin.interactable = false;

        StartCoroutine(nameof(LoginProcess));

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);

    }
    private void ResponseToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProcess));

            if (callback.IsSuccess())
            {

                SetMessage($"Welcome {inputFieldID.text}!");
                Utils.LoadScene(SceneNames.Lobby);

            }
            else
            {
                buttonLogin.interactable = true;
                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 401:
                        message = "ID or PW is incorrect!";
                        break;
                    case 403:
                        message = "ID is not verified!";
                        break;
                    case 404:
                        message = "ID is not found!";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                if (message.Contains("PW"))
                {
                    GuideForIncorrectInput(imagePW, message);
                }
                else
                {
                    GuideForIncorrectInput(imageID, message);
                }

            }
        });
    }

    private IEnumerator LoginProcess()
    {
        float time = 0f;
        while(true)
        {
            time += Time.deltaTime;
            
            SetMessage($"Login Processing... {time:F1}");


            yield return null;
        }
    }

}
