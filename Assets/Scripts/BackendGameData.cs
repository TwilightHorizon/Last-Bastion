using UnityEngine;
using UnityEngine.Events;
using BackEnd;

public class BackendGameData
{

    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();


    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BackendGameData();
            }
            return instance;
        }
    }


    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private string gameDataRowInDate = string.Empty;

    /// <summary>
    /// 뒤끝 콘솔 테이블에 새로운 유저 정부 추가
    /// </summary>
    public void GameDataInsert()
    {
        userGameData.Reset();

        Param param = new Param()
        {
            { "level", userGameData.level },
            { "experience", userGameData.experience},
            { "gold", userGameData.gold},
            { "jewel", userGameData.jewel },
            { "heart", userGameData.heart },
            { "dailyBestScore", userGameData.dailyBestScore }
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameDataRowInDate = callback.GetInDate();
                Debug.Log($"게임 정보 데이터 삽입 성공. : {callback}");

            }
            else
            {
                Debug.LogError($"게임 정보 데이터 삽입 실패. : {callback}");
            }
        });

    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에서 유저 정보 불러올때
    /// </summary>
    public void GameDataLoad()
    {
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback => 
        {
            if (callback.IsSuccess()) 
            {
                Debug.Log($"게임 정보 데이터 불러오기에 성공! : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if(gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재 XXX");

                    }
                    else
                    {
                        gameDataRowInDate               = gameDataJson[0]["inDate"].ToString();

                        userGameData.level              = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.experience         = float.Parse(gameDataJson[0]["experience"].ToString());
                        userGameData.gold               = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.jewel              = int.Parse(gameDataJson[0]["jewel"].ToString());
                        userGameData.heart              = int.Parse(gameDataJson[0]["heart"].ToString());
                        userGameData.dailyBestScore     = int.Parse(gameDataJson[0]["dailyBestScore"].ToString()) ;

                        onGameDataLoadEvent?.Invoke();

                    }
                }
                catch(System.Exception e)
                {
                    userGameData.Reset();
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

    /// <summary>
    /// 뒤끝 콘솔 테이블에 있는 유저 데이터 갱신
    /// </summary>
    public void GameDataUpdate(UnityAction action = null)
    {
        if(userGameData == null) 
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다.");
            return;
        }

        Param param = new Param()
        {
            { "level", userGameData.level},
            { "experience", userGameData.experience},
            { "gold", userGameData.gold},
            { "jewel", userGameData.jewel},
            { "heart", userGameData.heart},
            { "dailyBestScore", userGameData.dailyBestScore }
        };

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.LogError("유저의 inDate 정보가 없어서 게임 데이터 수정에 실패");
            
        }

        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 데이터 정보 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param, callback =>
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