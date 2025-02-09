using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelViewer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textNickname;

    public void UpdateNickname()
    {
        textNickname.text = UserInfo.Data.nickname == null?
                            UserInfo.Data.gamerID : UserInfo.Data.nickname;
    }


}
