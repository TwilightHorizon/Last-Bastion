using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Cannon = 0, Laser}
public enum WeaponState {SearchTarget = 0, TryAttackCannon, TryAttackLaser}

public class TowerWeapon : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private TowerTemplate towerTemplate;
    
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private WeaponType weaponType;
    //[SerializeField]
    //private float attackRate = 0.5f;
    //[SerializeField]
    //private float attackRange = 2.0f;

    //[SerializeField]
    //private float attackDamage = 10.0f;

    [Header("Cannon")]
    [SerializeField]
    private GameObject projectilePrefab;

    [Header("Laser")]
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform hitEffect;
    [SerializeField]
    private LayerMask targetLayer;



    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;

    private int level = 0;

    //public float Damage => attackDamage;
    //public float AttackRate => attackRate;
    //public float AttackRange => attackRange;
    public int Level => level + 1;
    public int MaxLevel => towerTemplate.weapon.Length;
    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float AttackDamage => towerTemplate.weapon[level].damage;
    public float AttackRate => towerTemplate.weapon[level].attackRate;
    public float AttackRange => towerTemplate.weapon[level].attackRange;

    private SpriteRenderer spriteRenderer;
    private PlayerGold playerGold;
    private Tile ownerTile;

    public void Setup(EnemySpawner enemyspawnerrrr, PlayerGold playerGold, Tile ownerTile)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.ownerTile = ownerTile;
        this.enemySpawner = enemyspawnerrrr;
        this.playerGold = playerGold;
        // 최초 상태를 WeaponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
        
        if(weaponType == WeaponType.Laser)
        {
            DisableLaser();
        }
    }


    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());

        weaponState = newState;


        StartCoroutine(weaponState.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);

    }

    private IEnumerator SearchTarget()
    {
        // Debug.Log("SearchTarget");
        while (true)
        {
            ////Debug.Log("1");
            //float closestDistSqr = Mathf.Infinity;
            //for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            //{
            //    float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            //    if (distance <= towerTemplate.weapon[level].attackRange && distance <= closestDistSqr)
            //    {
            //        closestDistSqr = distance;
            //        attackTarget = enemySpawner.EnemyList[i].transform;
            //    }
            //}

            attackTarget = FindClosestAttackTarget();


            if (attackTarget != null)
            {

                if (weaponType == WeaponType.Cannon)
                {
                    ChangeState(WeaponState.TryAttackCannon);
                }
                else if(weaponType == WeaponType.Laser)
                {
                    ChangeState(WeaponState.TryAttackLaser);
                }

            }
            yield return null;
        }
    }


    private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null) return false;

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > towerTemplate.weapon[level].attackRange)
        {
            attackTarget = null;
            return false;
        }
        return true;
    }

    private IEnumerator TryAttackCannon()
    {

        while (true)
        {
            //if (attackTarget == null)
            //{
            //    ChangeState(WeaponState.SearchTarget);
            //    break;
            //}

            //float distance = Vector3.Distance(attackTarget.position, transform.position);
            //if (distance > towerTemplate.weapon[level].attackRange)
            //{
            //    attackTarget = null;
            //    ChangeState(WeaponState.SearchTarget);
            //    break;
            //}

            if (!IsPossibleToAttackTarget())
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(towerTemplate.weapon[level].attackRate);
            SpawnProjectile();

        }
    }

    private IEnumerator TryAttackLaser()
    {
        EnableLaser();
        while (true)
        {
            if (!IsPossibleToAttackTarget())
            {
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            SpawnLaser();

            yield return null;

        }
    }

    private Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;
        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            if (distance <= towerTemplate.weapon[level].attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
            }
        }

        return attackTarget;
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }
    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }

    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, towerTemplate.weapon[level].attackRange, targetLayer);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform == attackTarget)
            {
                lineRenderer.SetPosition(0, spawnPoint.position);
                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
                hitEffect.position = hit[i].point;

                attackTarget.GetComponent<EnemyHP>().TakeDamage(towerTemplate.weapon[level].damage * Time.deltaTime);
                return;
            }
        }

    }
    public bool Upgrade()
    {
        if (playerGold.CurrentGold < towerTemplate.weapon[level + 1].cost)
        {
            return false; // can't upgrade cuz poor
        }
        level++;
        // Debug.Log(towerTemplate.weapon[level].sprite);

        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

        if (weaponType == WeaponType.Laser)
        {
            lineRenderer.startWidth = 0.05f + level * 0.05f;
            lineRenderer.endWidth = 0.05f;
        }
    

        return true;
    }

    public void Sell()
    {


        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
