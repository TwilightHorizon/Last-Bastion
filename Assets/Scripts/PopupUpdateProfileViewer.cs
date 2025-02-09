using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PopupUpdateProfileViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNickname;
    [SerializeField]
    private TextMeshProUGUI textGamerID;

    public void UpdateNickname()
    {
        textNickname.text = UserInfo.Data.nickname == null ? UserInfo.Data.gamerID : UserInfo.Data.nickname;

        textGamerID.text = UserInfo.Data.gamerID;
    }


}
