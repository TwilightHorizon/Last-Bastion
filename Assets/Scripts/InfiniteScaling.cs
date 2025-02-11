using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScaling : MonoBehaviour
{
    [SerializeField]
    private float scale = 0.02f;
    private Enemy enemy;
    //private Movement2D movement2D;
    private EnemyHP enemyHP;
    public float takeThisNumber;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemyHP = GetComponent<EnemyHP>();
        //movement2D = GetComponent<Movement2D>();

    }

    private void Update()
    {
        takeThisNumber += Time.deltaTime * scale;
        // Debug.Log("take this number: " + takeThisNumber);
        
    }



}
