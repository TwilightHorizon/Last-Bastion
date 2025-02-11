using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScaling : MonoBehaviour
{

    private float scale = 1f;
    private Enemy enemy;
    //private Movement2D movement2D;
    private EnemyHP enemyHP;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemyHP = GetComponent<EnemyHP>();
        //movement2D = GetComponent<Movement2D>();

    }

    private void Update()
    {
        enemyHP.MaxHP += 50 * Time.deltaTime * scale;

        
    }



}
