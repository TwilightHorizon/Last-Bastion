using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class RegisterAccount : LoginBase
{

    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputPW;
    [SerializeField]
    private Image imageConfirmPW;
    [SerializeField]
    private TMP_InputField inputConfirmPW;
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputEmail;

    [SerializeField]
    private Button buttonRegisterAccount;


    public void OnClickRegisterAccount()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);

        if (IsFieldDataEmpty(imageID, inputID.text, "ID"))                          return;
        if (IsFieldDataEmpty(imagePW, inputPW.text, "PW"))                          return;
        if (IsFieldDataEmpty(imageConfirmPW, inputConfirmPW.text, "Confirm PW"))    return;
        if (IsFieldDataEmpty(imageEmail, inputEmail.text, "Email"))                 return;

        if(!inputPW.text.Equals(inputConfirmPW.text))
        {
            GuideForIncorrectInput(imageConfirmPW, "Password does not match!");
            return;
        }   

        if(!inputEmail.text.Contains("@"))
        {
            GuideForIncorrectInput(imageEmail, "Invalid email address format! (ex. address@xx.xx)");
            return;
        }

        buttonRegisterAccount.interactable = false;
        SetMessage("Registering...");

        CustomSignup();

    }

    private void CustomSignup()
    {
        Backend.BMember.CustomSignUp(inputID.text, inputPW.text, inputEmail.text, callback =>
        {
            
            buttonRegisterAccount.interactable = true;
            if(callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputEmail.text, callback => 
                {
                    if(callback.IsSuccess())
                    {
                        SetMessage($"Register success! Welcome {inputID.text}!");

                        // After success in creating new account, create new gamedata for this new user account
                        BackendGameData.Instance.GameDataInsert();
                        BackendGameUpgradeData.Instance.GameUpgradeDataInsert();

                        Utils.LoadScene(SceneNames.Lobby);
                    }
                });
            }
            else
            {
                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message = "Invalid ID or PW!";
                        break;
                    case 409:
                        message = "ID already exists!";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if(message.Equals("ID"))
                {
                    GuideForIncorrectInput(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }


        });
    }

}
