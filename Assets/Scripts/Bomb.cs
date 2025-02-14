using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private Movement2D movement2D;
    private Transform target;
    private float damage;
    private float areaOfImpact;


    public void Setup(Transform target, float damage, float areaOfImpact)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;
        this.areaOfImpact = areaOfImpact;
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        
        }
        else // just for just in case
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {

            

            if(enemy != null)
            {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                // Debug.Log("Enemy Checking in Bomb" + distance);
                if (distance < areaOfImpact)
                {
                    enemy.GetComponent<EnemyHP>().TakeDamage(damage);
                }
            }
        }

        // collision.GetComponent<EnemyHP>().TakeDamage(damage); 
        Destroy(gameObject);

    }
}
