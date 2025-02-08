using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab for the enemy that spawns")]
    [SerializeField]
    private GameObject enemyPrefab;
    

    [Header("Time between enemy spawns")]
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;


    [Header("Enemy HP Slider")]
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;


    private List<Enemy> enemyList;
    public List<Enemy> EnemyList => enemyList;

    [Header("Player Stats")]
    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private PlayerGold playerGold;




    private void Awake()
    {
        enemyList  = new List<Enemy>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

           

            yield return new WaitForSeconds(spawnTime);
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
