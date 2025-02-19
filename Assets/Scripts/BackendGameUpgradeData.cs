using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BackEnd;

public class BackendGameUpgradeData 
{
    [System.Serializable]
    public class GameUpgradeDataLoadEvent : UnityEvent { }
    public GameUpgradeDataLoadEvent onGameDataLoadEvent = new GameUpgradeDataLoadEvent();


    private static BackendGameUpgradeData instance = null;
    public static BackendGameUpgradeData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendGameUpgradeData();
            }
            return instance;
        }
    }

    private UserGameUpgradeData userGameUpgradeData = new UserGameUpgradeData();
    public UserGameUpgradeData UserGameUpgradeData => userGameUpgradeData;  

    private string gameUpgradeDataRowInDate = string.Empty;

    public void GameUpgradeDataInsert()
    {
        userGameUpgradeData.Reset();

        Param param = new Param()
        {
            { "towerOneUpgrade", userGameUpgradeData.towerOneUpgrade },
            { "towerTwoUpgrade", userGameUpgradeData.towerTwoUpgrade },
            { "towerThreeUpgrade", userGameUpgradeData.towerThreeUpgrade },
            { "towerFourUpgrade", userGameUpgradeData.towerFourUpgrade },
            { "towerFiveUpgrade", userGameUpgradeData.towerFiveUpgrade },
            { "towerSixUpgrade", userGameUpgradeData.towerSixUpgrade }

        };
        Backend.GameData.Insert("USER_UPGRADE", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameUpgradeDataRowInDate = callback.GetInDate();
                Debug.Log($"게임 정보 데이터 삽입 성공. : {callback}");

            }
            else
            {
                Debug.LogError($"게임 정보 데이터 삽입 실패. : {callback}");
            }
        });


    }


    public void GameDataLoad()
    {
        Backend.GameData.GetMyData("USER_UPGRADE", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"게임 정보 데이터 불러오기에 성공! : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재 XXX");

                    }
                    else
                    {
                        gameUpgradeDataRowInDate = gameDataJson[0]["inDate"].ToString();

                        foreach (string itemKey in gameDataJson[0]["towerOneUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerOneUpgrade"][itemKey].ToString()));
                        }

                        foreach (string itemKey in gameDataJson[0]["towerTwoUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerTwoUpgrade"][itemKey].ToString()));
                        }

                        foreach (string itemKey in gameDataJson[0]["towerThreeUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerThreeUpgrade"][itemKey].ToString()));
                        }

                        foreach (string itemKey in gameDataJson[0]["towerFourUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerFourUpgrade"][itemKey].ToString()));
                        }

                        foreach (string itemKey in gameDataJson[0]["towerFiveUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerFiveUpgrade"][itemKey].ToString()));
                        }

                        foreach (string itemKey in gameDataJson[0]["towerSixUpgrade"].Keys)
                        {
                            BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade.Add(itemKey, int.Parse(gameDataJson[0]["towerSixUpgrade"][itemKey].ToString()));
                        }

                        onGameDataLoadEvent?.Invoke();

                    }
                }
                catch (System.Exception e)
                {
                    userGameUpgradeData.Reset();
                    Debug.LogError(e);
                }

            }
            // 실패 시

            else
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패! : {callback}");
            }
        });
    }
    public void GameDataUpdate(UnityAction action = null)
    {
        if (userGameUpgradeData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다.");
            return;
        }

        Param param = new Param()
        {
            { "towerOneUpgrade", userGameUpgradeData.towerOneUpgrade },
            { "towerTwoUpgrade", userGameUpgradeData.towerTwoUpgrade },
            { "towerThreeUpgrade", userGameUpgradeData.towerThreeUpgrade },
            { "towerFourUpgrade", userGameUpgradeData.towerFourUpgrade },
            { "towerFiveUpgrade", userGameUpgradeData.towerFiveUpgrade },
            { "towerSixUpgrade", userGameUpgradeData.towerSixUpgrade }
        };

        if (string.IsNullOrEmpty(gameUpgradeDataRowInDate))
        {
            Debug.LogError("유저의 inDate 정보가 없어서 게임 데이터 수정에 실패");

        }

        else
        {
            Debug.Log($"{gameUpgradeDataRowInDate}의 게임 데이터 정보 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_UPGRADE", gameUpgradeDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 수정 성공, {callback}");
                    action?.Invoke();
                }
                else
                {
                    Debug.LogError($"실패 수정 정보 : {callback}");
                }
            });
        }

    }
}
