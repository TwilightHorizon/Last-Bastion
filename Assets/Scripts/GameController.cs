using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{


    [SerializeField]
    TowerTemplate[] towerTemplate;

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


        Reverse();

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
        BackendGameData.Instance.UserGameData.gold += score*stageNumber*1000;
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


    private void Reverse()
    {
        for (int i = 0; i < towerTemplate[0].weapon.Length; i++)
        {
            towerTemplate[0].weapon[i].damage -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"];
            towerTemplate[0].weapon[i].attackRange -= 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"];
            towerTemplate[0].weapon[i].attackRate += 0.001f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"];
            towerTemplate[0].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"];
        }

        // Reverse Tower Type 2
        for (int i = 0; i < towerTemplate[1].weapon.Length; i++)
        {
            towerTemplate[1].weapon[i].damage -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"];
            towerTemplate[1].weapon[i].attackRange -= 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"];
            towerTemplate[1].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"];
        }

        // Reverse Tower Type 3
        for (int i = 0; i < towerTemplate[2].weapon.Length; i++)
        {
            towerTemplate[2].weapon[i].slow -= 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"];
            towerTemplate[2].weapon[i].attackRange -= 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"];
            towerTemplate[2].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"];
        }

        // Reverse Tower Type 4
        for (int i = 0; i < towerTemplate[3].weapon.Length; i++)
        {
            towerTemplate[3].weapon[i].buff -= 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"];
            towerTemplate[3].weapon[i].attackRange -= 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"];
            towerTemplate[3].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"];
        }

        // Reverse Tower Type 5
        for (int i = 0; i < towerTemplate[4].weapon.Length; i++)
        {
            towerTemplate[4].weapon[i].earning -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"];
            towerTemplate[4].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"];
        }

        // Reverse Tower Type 6
        for (int i = 0; i < towerTemplate[5].weapon.Length; i++)
        {
            towerTemplate[5].weapon[i].damage -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"];
            towerTemplate[5].weapon[i].attackRange -= 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"];
            // towerTemplate[5].weapon[i].attackRate += 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["attackSpeed"];
            towerTemplate[5].weapon[i].cost += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"];
        }
    }

}
