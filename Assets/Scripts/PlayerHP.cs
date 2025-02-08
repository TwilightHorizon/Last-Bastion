using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{

    [SerializeField]
    private float maxHP = 100.0f;

    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0.0f)
        {
            // end game

        }
    }   

}
