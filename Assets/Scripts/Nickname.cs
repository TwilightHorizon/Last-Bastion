using BackEnd;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Nickname : LoginBase
{
    [System.Serializable]
    public class NicknameEvent : UnityEngine.Events.UnityEvent { }

    public NicknameEvent onNicknameEvent = new NicknameEvent();

    [SerializeField]
    private Image imageNickname;
    [SerializeField]
    private TMP_InputField inputFieldNickname;
    [SerializeField]
    private Button buttonUpdateNickname;

    private void OnEnable()
    {
        ResetUI(imageNickname);
        SetMessage("Enter your nickname!");
    }

    public void OnClickUpdateNickname()
    {
        ResetUI(imageNickname);
        if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

        buttonUpdateNickname.interactable = false;

        UpdateNickname();
    }

    private void UpdateNickname()
    {
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {

            buttonUpdateNickname.interactable = true;
            if (callback.IsSuccess())
            {
                SetMessage($"Changed nickname to {inputFieldNickname.text}.");
                onNicknameEvent?.Invoke();
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode())){
                    case 400:
                        message = "Empty nickname or over 20 characters or there is a space";
                        break;
                    case 409:
                        message = "Nickname already exists";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                GuideForIncorrectInput(imageNickname, message);
            }

        });
    }


}
