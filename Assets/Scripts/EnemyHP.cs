using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;

    private float currentHP;
    private bool isDead = false;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP { set; get; }
    public float CurrentHP => currentHP;


    private void Awake()
    {
        currentHP = maxHP;
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if(isDead)
        {
            return;
        }

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
