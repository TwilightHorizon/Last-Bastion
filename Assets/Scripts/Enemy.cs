using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyDestroyType { Kill = 0, Arrive}

public class Enemy : MonoBehaviour
{
    
    private int     wayPointCount;
    private Transform[] wayPoints;
    private int     currentWayPointIndex;
    private Movement2D movement2D;
    private EnemySpawner enemySpawner;


    [SerializeField]
    private int scorePoint = 100; // TODO: make different score incrememnt based on enemy type


    
    private GameController gameController;


    [SerializeField]
    private float enemyAttackDamage = 10.0f;

    [Header("Amount of Gold Earned From Enemy Killed")]
    [SerializeField]
    private int gold = 10;

    public float EnemyAttackDamage => enemyAttackDamage;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints, GameController gameController)
    {

        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;
        this.gameController = gameController;

        // 적 이동 경로 Waypoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentWayPointIndex].position;

        StartCoroutine(OnMove());

    }

    private IEnumerator OnMove()
    {

        NextMoveTo();

        while(true)
        {
            transform.Rotate(Vector3.forward * 10);

            if(Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 0.05f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }


    private void NextMoveTo()
    {
        if (currentWayPointIndex< wayPointCount - 1)
        {
            transform.position = wayPoints[currentWayPointIndex].position;

            currentWayPointIndex++;

            Vector3 direction = (wayPoints[currentWayPointIndex].position- transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            // Destroy(gameObject);
            gold = 0;
            // enemy arrived at the goal
            OnDie(EnemyDestroyType.Arrive);
        }


    }

    public void OnDie(EnemyDestroyType type)
    {

        enemySpawner.DestroyEnemy(type, this, gold);

        if(type == EnemyDestroyType.Kill)
        {
            gameController.Score += scorePoint;
        }
        // 

    }
    
}
