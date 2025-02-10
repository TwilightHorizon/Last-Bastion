using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelViewer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textNickname;


    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private Slider sliderExperience;
    [SerializeField]
    private TextMeshProUGUI textHeart;
    [SerializeField]
    private TextMeshProUGUI textJewel;
    [SerializeField]
    private TextMeshProUGUI textGold;

    private void Awake()
    {
        BackendGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);
    }


    public void UpdateNickname()
    {
        textNickname.text = UserInfo.Data.nickname == null?
                            UserInfo.Data.gamerID : UserInfo.Data.nickname;
    }

    public void UpdateGameData()
    {
        textLevel.text = $"Lv.{BackendGameData.Instance.UserGameData.level}";
        sliderExperience.value = BackendGameData.Instance.UserGameData.experience / 100;
        textHeart.text = $"{BackendGameData.Instance.UserGameData.heart} / 30";
        textJewel.text = $"{BackendGameData.Instance.UserGameData.jewel}";
        textGold.text = $"{BackendGameData.Instance.UserGameData.gold}";
    }

}
