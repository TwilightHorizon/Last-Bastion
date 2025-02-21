using BackEnd;
using UnityEngine;
using LitJson;

public class UserInfo : MonoBehaviour
{

    [System.Serializable]
    public class UserInfoEvent : UnityEngine.Events.UnityEvent { }

    public UserInfoEvent onUserInfoEvent = new UserInfoEvent();

    private static UserInfoData data = new UserInfoData();
    public static UserInfoData Data => data;


    [SerializeField]
    private GameObject loadingPanel;

    public void GetUserInfoFromBackend()
    {
        Backend.BMember.GetUserInfo(callback =>
        {
            if (callback.IsSuccess())
            {
                try
                {
                    JsonData json = callback.GetReturnValuetoJSON()["row"];

                    data.gamerID = json["gamerId"].ToString();
                    data.countryCode = json["countryCode"]?.ToString();
                    data.nickname = json["nickname"]?.ToString();
                    data.inDate = json["inDate"].ToString();
                    data.emailForFindPassword = json["emailForFindPassword"]?.ToString();
                    data.subscriptionType = json["subscriptionType"].ToString();
                    data.federationID = json["federationId"]?.ToString();
                }
                catch(System.Exception e)
                {
                    data.Reset();
                    Debug.LogError(e);
                }
            }
            else
            {
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }


            turnOffScreen();


            onUserInfoEvent?.Invoke();

        });
    }

    private void turnOffScreen()
    {
        loadingPanel.SetActive(false);
    }


}

public class UserInfoData
{
    public string gamerID;
    public string countryCode;
    public string nickname;
    public string inDate;
    public string emailForFindPassword;
    public string subscriptionType;
    public string federationID;

    public void Reset()
    {
        gamerID = "Offline";
        countryCode = "Unknown";
        nickname = "Noname";
        inDate = string.Empty;
        emailForFindPassword = string.Empty;
        subscriptionType = string.Empty;
        federationID = string.Empty;
    }
}
