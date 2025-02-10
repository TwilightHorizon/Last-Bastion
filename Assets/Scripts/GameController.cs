using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int score = 0;
    [SerializeField]
    private EnemySpawner enemySpawner;
    
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private TextMeshProUGUI textResultScore;


    
    [SerializeField]
    private GameObject panelStageComplete;
    [SerializeField]
    private TextMeshProUGUI textVictoryResultScore;

    [SerializeField]
    private PlayerHP playerHP;


    [Header("UI Animation")]
    [SerializeField]
    private ScaleEffect effectGameOver;
    [SerializeField]
    private ScaleEffect effectVictory;

    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    public bool IsGameOver { set; get; } = false;

    public void GameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
        {
            enemySpawner.EnemyList[i].gameObject.SetActive(false);
        }


        if(playerHP.CurrentHP == 0)
        {

            panelGameOver.SetActive(true);
            textResultScore.text = score.ToString();
            effectGameOver.Play(200, 65);
        }
        else
        {
            panelStageComplete.SetActive(true);
            textVictoryResultScore.text = score.ToString();
            effectVictory.Play(200, 65); 
        }


        BackendGameData.Instance.UserGameData.experience += 125; // TODO
        if(BackendGameData.Instance.UserGameData.experience >= 100)
        {
            BackendGameData.Instance.UserGameData.experience = BackendGameData.Instance.UserGameData.experience - 100;
            BackendGameData.Instance.UserGameData.level++;
        }

        // update info

        BackendGameData.Instance.GameDataUpdate(AfterGameOver);


    }
    
    public void ButtonClickToLobby()
    {
        Utils.LoadScene(SceneNames.Lobby);
    }

    public void AfterGameOver()
    {
    }

}
