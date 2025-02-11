using UnityEngine;
using System;
using BackEnd;

public class BackendFriendSystem : MonoBehaviour
{
    [SerializeField]
    private FriendSentRequestPage sentRequestPage;

    private string GetUserInfoBy(string nickname)
    {
        var bro = Backend.Social.GetUserInfoByNickName(nickname);
        string inDate = string.Empty;

        if (!bro.IsSuccess())
        {
            Debug.LogError($" : {bro}");
            return inDate;
        }

        try
        {
            LitJson.JsonData jsonData = bro.GetFlattenJSON()["row"];

            if (jsonData.Count <= 0)
            {
                Debug.LogWarning(" inDate  .");
                return inDate;
            }

            inDate = jsonData["inDate"].ToString();

            Debug.Log($"{nickname}�� inDate ���� {inDate} �Դϴ�.");
        }
        // JSON ������ �Ľ� ����
        catch (Exception e)
        {
            // try-catch ���� ���
            Debug.LogError(e);
        }

        return inDate;
    }

    public void SendRequestFriend(string nickname)
    {
        // RequestFriend() �޼ҵ带 �̿��� ģ�� �߰� ��û�� �� �� �ش� ģ���� inDate ������ �ʿ�
        string inDate = GetUserInfoBy(nickname);

        // inDate ������ ���� �������� "ģ�� ��û"�� ������.
        Backend.Friend.RequestFriend(inDate, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"{nickname} ģ�� ��û ���� ������ �߻��߽��ϴ�. : {callback}");
                return;
            }

            Debug.Log($"ģ�� ��û�� �����߽��ϴ�. : {callback}");

            // ģ�� ��û�� �����ϸ� ģ�� ��û ��� ��� �ҷ�����
            GetSentRequestList();
        });
    }

    public void GetSentRequestList()
    {
        Backend.Friend.GetSentRequestList(callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"ģ�� ��û ��� ��� ��ȸ ���� ������ �߻��߽��ϴ�. : {callback}");
                return;
            }

            // JSON ������ �Ľ� ����
            try
            {
                LitJson.JsonData jsonData = callback.GetFlattenJSON()["rows"];

                // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                if (jsonData.Count <= 0)
                {
                    Debug.LogWarning("ģ�� ��û ��� ��� �����Ͱ� �����ϴ�.");
                    return;
                }

                // ģ�� ��û ��� ��Ͽ� �ִ� ��� UI ��Ȱ��ȭ
                // sentRequestPage.DeactivateAll();

                foreach (LitJson.JsonData item in jsonData)
                {
                    FriendData friendData = new FriendData();

                    //friend.nickname		= item.ContainsKey("nickname") == true ? item["nickname"].ToString() : "NONAME";
                    friendData.nickname = item["nickname"].ToString().Equals("True") ? "NONAME" : item["nickname"].ToString();
                    friendData.inDate = item["inDate"].ToString();
                    friendData.createdAt = item["createdAt"].ToString();

                    // [ģ�� ��û]�� ���� �ð����κ��� ���� �Ⱓ�� �����ٸ� �ڵ����� ģ�� ��û ���
                    if (IsExpirationDate(friendData.createdAt))
                    {
                        RevokeSentRequest(friendData.inDate);
                        continue;
                    }

                    // ���� friend ������ �������� ģ�� ��û ��� UI Ȱ��ȭ
                    sentRequestPage.Activate(friendData);
                }
            }
            // JSON ������ �Ľ� ����
            catch (Exception e)
            {
                // try-catch ���� ���
                Debug.LogError(e);
            }
        });
    }

    public void RevokeSentRequest(string inDate)
    {
        Backend.Friend.RevokeSentRequest(inDate, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError($"ģ�� ��û ��� ���� ������ �߻��߽��ϴ�. : {callback}");
                return;
            }

            Debug.Log($"ģ�� ��û ��ҿ� �����߽��ϴ�. : {callback}");
        });
    }

    private bool IsExpirationDate(string createdAt)
    {
        // GetServerTime() - ���� �ð� �ҷ�����
        var bro = Backend.Utils.GetServerTime();

        if (!bro.IsSuccess())
        {
            Debug.LogError($"���� �ð� �ҷ����⿡ �����߽��ϴ�. : {bro}");
            return false;
        }

        // JSON ������ �Ľ� ����
        try
        {
            // createdAt �ð����κ��� 3�� ���� �ð�
            DateTime after3Days = DateTime.Parse(createdAt).AddDays(Constants.EXPIRATION_DAYS);
            // ���� ���� �ð�
            string serverTime = bro.GetFlattenJSON()["utcTime"].ToString();
            // ������� ���� �ð� = ���� �ð� - ���� ���� �ð�
            TimeSpan timeSpan = after3Days - DateTime.Parse(serverTime);

            if (timeSpan.TotalHours < 0)
            {
                return true;
            }
        }
        // JSON ������ �Ľ� ����
        catch (Exception e)
        {
            // try-catch ���� ���
            Debug.LogError(e);
        }

        return false;
    }
}

