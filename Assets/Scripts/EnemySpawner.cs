﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //[Header("Prefab for the enemy that spawns")]
    //[SerializeField]
    //private GameObject enemyPrefab;

    private Wave currentWave;

    //[Header("Time between enemy spawns")]
    //[SerializeField]
    //private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    private InfiniteScaling infScaling;


    [Header("Enemy HP Slider")]
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;


    [Header("Player Stats")]
    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private PlayerGold playerGold;

    [SerializeField]
    private GameController gameController;

    private int currentEnemyCount = 0;
    private int maxEnemyCount = 0;
    private List<Enemy> enemyList;
    public List<Enemy> EnemyList => enemyList;

    [SerializeField]
    private Button buttonStartWave;

    private bool finalWave = false;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => maxEnemyCount;

    private void Awake()
    {
        enemyList  = new List<Enemy>();
        infScaling = GetComponent<InfiniteScaling>();   
        // StartCoroutine(SpawnEnemy());
    }

    public void StartWave(Wave wave, bool lastWave)
    {
        gameController.isWaveOn = true;
        finalWave = lastWave;
        currentWave = wave;
        // infScaling.takeThisNumber = 0;
        for (int i = 0; i < currentWave.maxEnemyCount.Length; i++) 
        {
            currentEnemyCount += currentWave.maxEnemyCount[i];
        }
        maxEnemyCount = currentEnemyCount;


        // currentEnemyCount = currentWave.maxEnemyCount;
        StartCoroutine(SpawnEnemy());  
        
    }

    private IEnumerator SpawnEnemy()
    {

        buttonStartWave.interactable = false;

        for (int i = 0; i < currentWave.maxEnemyCount.Length; i++)
        {
            int spawnEnemyCount = 0;

            while (spawnEnemyCount < currentWave.maxEnemyCount[i])
            //while(true)
            {
                // GameObject clone = Instantiate(enemyPrefab);

                // 웨이브에 등장하는 적 중 하나를 랜덤으로 선택, 적 오브젝트 생성
                int enemyIndex = i;



                GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
                
                Enemy enemy = clone.GetComponent<Enemy>();
                EnemyHP enemyHP = clone.GetComponent<EnemyHP>();
                


                enemy.Setup(this, wayPoints, gameController);
                enemyList.Add(enemy);
                

                SpawnEnemyHPSlider(clone);

                spawnEnemyCount++;
                yield return new WaitForSeconds(currentWave.spawnTime);
            }
        }

        gameController.isWaveOn = false;

        buttonStartWave.interactable = true;

        if (finalWave)
        {
            StartCoroutine(WaitUntilEnemyAllDie());
        }
        // Debug.Log("Loop Ended!");
        // 

    }

    private IEnumerator WaitUntilEnemyAllDie()
    {
        while (true)
        {

            if(currentEnemyCount == 0)
            {
                gameController.GameOver();
                yield break;
            }

            yield return new WaitForSeconds(1.0f);


        }
    }
    


    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {   
        // When Enemy arrived at the goal
        if(type == EnemyDestroyType.Arrive)
        {
            // do EnemyAttackDamage amount of damage to player
            playerHP.TakeDamage(enemy.EnemyAttackDamage);
        }
        else if(type == EnemyDestroyType.Kill)
        {
            // When Enemy is killed by player, gain gold
            playerGold.CurrentGold += gold;
        }

        currentEnemyCount--;
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);

    }


    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // 적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        // Tip. UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보인다
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
