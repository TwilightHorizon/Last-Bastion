using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private bool isInfinite = false;

    private int score = 0;
    [SerializeField]
    private EnemySpawner enemySpawner;
    
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private TextMeshProUGUI textResultScore;

    [SerializeField]
    private DailyRankRegister dailyBestScore;

    

    
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

    [Header("Stage Number")]
    [SerializeField]
    private int stageNumber;

    public bool isWaveOn = false;

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

        
        

        if(playerHP.CurrentHP <= 0)
        {

            panelGameOver.SetActive(true);
            textResultScore.text = score.ToString();
            effectGameOver.Play(200, 65);

            for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                Destroy(enemySpawner.EnemyList[i].gameObject);
            }

        }
        else
        {
            panelStageComplete.SetActive(true);
            textVictoryResultScore.text = score.ToString();
            effectVictory.Play(200, 65); 
            BackendGameData.Instance.UserGameData.highestStage = Mathf.Max(BackendGameData.Instance.UserGameData.highestStage, stageNumber);
        }


        if(isInfinite)
        {
            dailyBestScore.Process(score);

            

            // update info

            
        }
        BackendGameData.Instance.UserGameData.experience += 25 * stageNumber; // TODO: make increment changed based on CurrentScore or map
        if (BackendGameData.Instance.UserGameData.experience >= 100 * (BackendGameData.Instance.UserGameData.level / 2) + 100)
        {
            BackendGameData.Instance.UserGameData.experience = 0;
            BackendGameData.Instance.UserGameData.level++;
        }

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
