using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelRankData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textRank;
    [SerializeField]
    private TextMeshProUGUI textNickname;
    [SerializeField]
    private TextMeshProUGUI textLevel;


    private int rank;
    private string nickname;
    private int level;

    public int Rank
    {
        set
        {
            if (value <= Constants.MAX_RANK_LIST)
            {
                rank = value;
                textRank.text = rank.ToString();
            }
            else
            {
                textRank.text = "No Rank";
            }
        }
        get => rank;
    }


    public string Nickname
    {
        set
        {
            nickname = value;
            textNickname.text = nickname;
        }
        get => nickname;
    }

    public int Level
    {
        set
        {
            level = value;
            textLevel.text = level.ToString();
        }
        get => level;
    }

}
