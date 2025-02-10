using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [Header("Player HP")]
    [SerializeField]
    private TextMeshProUGUI textTMP;
    [SerializeField]
    private PlayerHP playerHP;

    [Header("Player Gold")]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;
    [SerializeField]
    private PlayerGold playerGold;

    [Header("Wave Count")]
    [SerializeField]
    private TextMeshProUGUI textWave;
    [SerializeField]
    private WaveSystem waveSystem;

    [Header("Enemy Count")]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;
    [SerializeField]
    private EnemySpawner enemySpawner;

    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private GameController gameController;

    private void Update()
    {
        textTMP.text = playerHP.CurrentHP + " / " + playerHP.MaxHP;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = waveSystem.CurrentWaveIndex + " / " + waveSystem.MaxWave; // 무한 모드 만들면 무한모드로 바꾸기. 아님 아예 MaxWave를 infinity로 바꾸기?
        
        if(enemySpawner.CurrentEnemyCount == 0)
        {
            textEnemyCount.text = "0 / 0";
        }
        else textEnemyCount.text = enemySpawner.CurrentEnemyCount + " / " + enemySpawner.MaxEnemyCount;

        textScore.text = $"Score: {gameController.Score}";

    }

}
