using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;

    [SerializeField]
    private bool isInf = false;

    private float currentHP;
    private bool isDead = false;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;


    public float initialMaxHP;
    // private InfiniteScaling infScaling;
    
    

    public float MaxHP
    {
        set => maxHP = value;
        get => maxHP;
    }
    public float CurrentHP => currentHP;
    public float scaling = 0f;

    private GameObject[] gameController;

    private void Awake()
    {
        if (isInf){

            gameController = GameObject.FindGameObjectsWithTag("GameController");
            // infScaling = GetComponent<InfiniteScaling>();
            // Debug.Log(scaling);
            maxHP += gameController[0].GetComponent<GameController>().Score / 20f;
            // Debug.Log(gameController[0].GetComponent<GameController>().Score);
        }



        currentHP = maxHP;
        initialMaxHP = maxHP;
        //Debug.Log(currentHP);

        enemy = GetComponent<Enemy>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {

        // Debug.Log(currentHP);
        if(isDead)
        {
            return;
        }
        //  Debug.Log(currentHP);
        currentHP -= damage;
        
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if(currentHP <= 0)
        {
            isDead = true;
            enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;
    }

}
