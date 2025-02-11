using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class LevelRankLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject levelRankDataPrefab;      // ��ŷ ���� ����� ���� UI ������ ����
    [SerializeField]
    private Scrollbar scrollbar;            // scrollBar�� value ���� (Ȱ��ȭ�� �� 1���� ���̵���)
    [SerializeField]
    private Transform rankDataParent;       // ScrollView�� Content ������Ʈ
    [SerializeField]
    private LevelRankData myRankData;           // �� ��ŷ ������ ����ϴ� UI ���ӿ�����Ʈ

    private List<LevelRankData> rankDataList;

    private void Awake()
    {
        rankDataList = new List<LevelRankData>();

        // 1 ~ 20�� ��ŷ ����� ���� UI ������Ʈ ����
        for (int i = 0; i < Constants.MAX_RANK_LIST; ++i)
        {
            GameObject clone = Instantiate(levelRankDataPrefab, rankDataParent);
            rankDataList.Add(clone.GetComponent<LevelRankData>());
        }
    }

    private void OnEnable()
    {
        scrollbar.value = 1;
        GetRankList();
        GetMyRank();
    }

    private void GetRankList()
    {
        // 1 ~ 20�� ��ŷ ���� �ҷ�����
        Backend.URank.User.GetRankList(Constants.LEVEL_RANKING_UUID, Constants.MAX_RANK_LIST, callback =>
        {
            if (callback.IsSuccess())
            {
                // JSON ������ �Ľ� ����
                try
                {
                    Debug.Log($" : {callback}");

                    LitJson.JsonData rankDataJson = callback.FlattenRows();

                    // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                    if (rankDataJson.Count <= 0)
                    {
                        // 1 ~ 20������ �����͸� �� �����ͷ� ����
                        for (int i = 0; i < Constants.MAX_RANK_LIST; ++i)
                        {
                            SetRankData(rankDataList[i], i + 1, "-", 0);
                        }

                        // Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        int rankerCount = rankDataJson.Count;

                        // ��ŷ ������ �ҷ��� ����� �� �ֵ��� ����
                        for (int i = 0; i < rankerCount; ++i)
                        {
                            rankDataList[i].Rank = int.Parse(rankDataJson[i]["rank"].ToString());
                            rankDataList[i].Level = int.Parse(rankDataJson[i]["score"].ToString());

                            // �г����� ������ �������� ���� ������ ������ �� �ֱ� ������
                            // �г����� �������� �ʴ� ������ �г��� ��� gamerId�� ���
                            rankDataList[i].Nickname = rankDataJson[i].ContainsKey("nickname") == true ?
                                                       rankDataJson[i]["nickname"]?.ToString() : UserInfo.Data.gamerID;
                        }
                        // ���� limitCount�� ������ ���ں��� ���� ��ŷ�� ��ϵ� ���ڰ� ������ �������� �� ������ ����
                        for (int i = rankerCount; i < Constants.MAX_RANK_LIST; ++i)
                        {
                            SetRankData(rankDataList[i], i + 1, "-", 0);
                        }
                    }
                }
                // JSON ������ �Ľ� ����
                catch (System.Exception e)
                {
                    // try-catch ���� ���
                    Debug.LogError(e);
                }
            }
            else
            {
                // 1 ~ 20������ �����͸� �� �����ͷ� ����
                for (int i = 0; i < Constants.MAX_RANK_LIST; ++i)
                {
                    SetRankData(rankDataList[i], i + 1, "-", 0);
                }

                Debug.LogError($"��ŷ ��ȸ �� ������ �߻��߽��ϴ� : {callback}");
            }
        });
    }

    private void GetMyRank()
    {
        // �� ��ŷ ���� �ҷ�����
        Backend.URank.User.GetMyRank(Constants.LEVEL_RANKING_UUID, callback =>
        {
            // �г����� ������ gamerId, �г����� ������ nickname ���
            string nickname = UserInfo.Data.nickname == null ? UserInfo.Data.gamerID : UserInfo.Data.nickname;

            if (callback.IsSuccess())
            {
                // JSON ������ �Ľ� ����
                try
                {
                    LitJson.JsonData rankDataJson = callback.FlattenRows();

                    // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                    if (rankDataJson.Count <= 0)
                    {
                        // ["������ ����", "�г���", 0]�� ���� ���
                        SetRankData(myRankData, 1000000000, nickname, 0);

                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
                    }
                    else
                    {
                        myRankData.Rank = int.Parse(rankDataJson[0]["rank"].ToString());
                        myRankData.Level = int.Parse(rankDataJson[0]["score"].ToString());

                        // �г����� ������ �������� ���� ������ ������ �� �ֱ� ������
                        // �г����� �������� �ʴ� ������ �г��� ��� gamerId�� ���
                        myRankData.Nickname = rankDataJson[0].ContainsKey("nickname") == true ?
                                              rankDataJson[0]["nickname"]?.ToString() : UserInfo.Data.gamerID;
                    }
                }
                // �ڽ��� ��ŷ ���� JSON ������ �Ľ̿� �������� ��
                catch (System.Exception e)
                {
                    // ["������ ����", "�г���", 0]�� ���� ���
                    SetRankData(myRankData, 1000000000, nickname, 0);

                    // try-catch ���� ���
                    Debug.LogError(e);
                }
            }
            else
            {
                // �ڽ��� ��ŷ ���� �����Ͱ� �������� ���� ��
                if (callback.GetMessage().Contains("userRank"))
                {
                    // ["������ ����", "�г���", 0]�� ���� ���
                    SetRankData(myRankData, 1000000000, nickname, 0);
                }
            }
        });
    }

    private void SetRankData(LevelRankData rankData, int rank, string nickname, int score)
    {
        rankData.Rank = rank;
        rankData.Nickname = nickname;
        rankData.Level = score;
    }
}

