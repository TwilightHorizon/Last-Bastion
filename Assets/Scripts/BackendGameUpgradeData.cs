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

}
