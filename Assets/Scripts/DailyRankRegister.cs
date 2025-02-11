using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class DailyRankRegister : MonoBehaviour
{

    public void Process(int newScore)
    {
        UpdateMyBestRankData(newScore);




    }

    private void UpdateMyRankData(int newScore)
    {
        string rowInDate = string.Empty;


        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            if(!callback.IsSuccess())
            {
                Debug.LogError($"데이터 조회 중 문제가 발생!. {callback}");
                return;
            }

            Debug.Log($"데이터 조회 성공. {callback}");


            if(callback.FlattenRows().Count > 0)
            {
                rowInDate = callback.FlattenRows()[0]["inDate"].ToString();
            }
            else
            {
                Debug.LogError("데이터 존재 XXX");
                return;
            }

            Param param = new Param()
            {
                
                { "dailyBestScore", newScore }

            };

            Backend.URank.User.UpdateUserScore(Constants.DAILY_BESTSCORE_UUID, Constants.USER_DATA_TABLE, rowInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"랭킹 등록 성공:{callback}");
                }
                else
                {
                    Debug.LogError($"랭킹 등록 실패 : {callback}");
                }


            });


        });
    }

    private void UpdateMyBestRankData(int newScore)
    {
        Backend.URank.User.GetMyRank(Constants.DAILY_BESTSCORE_UUID, callback =>
        {
            if (callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData rankDataJson = callback.FlattenRows();
                    if (rankDataJson.Count <= 0) Debug.LogWarning("데이터가 존재하지 않아요!");
                    else
                    {
                        int bestScore = int.Parse(rankDataJson[0]["score"].ToString());
                        if(newScore > bestScore)
                        {
                            UpdateMyRankData(newScore);
                            Debug.Log("최고 점수 갱신");
                        }
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }

            }
            else
            {
                Debug.Log("userRank: " + callback.GetMessage());
                if (callback.GetMessage().Contains("userRank")) //자신의 랭킹 정보가 존재하지 않을때 걍 현재 점수를 새로운 랭킹으로 등록
                {
                    UpdateMyRankData( newScore );
                }
            }
        });
    }

}
