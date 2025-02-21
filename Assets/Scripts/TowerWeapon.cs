using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Cannon = 0, Laser, Slow, Buff, Money, Area}
public enum WeaponState {SearchTarget = 0, TryAttackCannon, TryAttackLaser, GenerateMoney, TryAttackArea}

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

    [Header("Area")]
    [SerializeField]
    private GameObject areaBombPrefab;


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
    public float Slow => towerTemplate.weapon[level].slow;
    public float Buff => towerTemplate.weapon[level].buff;
    public float Earning => towerTemplate.weapon[level].earning;
    public float AreaDamage => towerTemplate.weapon[level].areaDamage;
    public float AreaOfImpact => towerTemplate.weapon[level].areaOfImpact;

    public int UpgradeCost => Level < MaxLevel ? towerTemplate.weapon[level + 1].cost : 0;
    public int SellCost => towerTemplate.weapon[level].sell;


    public WeaponType WeaponType => weaponType;

    private SpriteRenderer spriteRenderer;
    private TowerSpawner towerSpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;


    private float addedDamage;

    private int buffLevel;


    // For Upgrades
    private Dictionary<string, int> upgrades;


    public float AddedDamage
    {
        set => addedDamage = Mathf.Max(0, value);
        get => addedDamage;
    }

    public int BuffLevel
    {
        set => buffLevel = Mathf.Max(0, value);
        get => buffLevel;   
    }

    private GameController gameController;



    public void Setup(TowerSpawner towerSpawner,  EnemySpawner enemyspawnerrrr, PlayerGold playerGold, Tile ownerTile, GameController gameController, Dictionary<string, int> upgrades)
    {

        this.gameController = gameController;
        this.upgrades = upgrades;
        this.towerSpawner = towerSpawner;
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.ownerTile = ownerTile;
        this.enemySpawner = enemyspawnerrrr;
        this.playerGold = playerGold;
        // 최초 상태를 WeaponState.SearchTarget으로 설정

        if (weaponType == WeaponType.Money) ChangeState(WeaponState.GenerateMoney);

        if(weaponType == WeaponType.Cannon || weaponType == WeaponType.Laser || weaponType == WeaponType.Area)
        {
            ChangeState(WeaponState.SearchTarget);

        }

        if (weaponType == WeaponType.Laser)
        {
            DisableLaser();
        }
    }

    public void OnBuffAroundTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        for(int i = 0;i < towers.Length; ++i)
        {
            
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();
            //Debug.Log(towers.Length);
            if (weapon.BuffLevel > Level)
            {
                continue;
            }

            if(Vector3.Distance(weapon.transform.position, transform.position) <= towerTemplate.weapon[level].attackRange)
            {
                if(weapon.WeaponType == WeaponType.Cannon || weapon.WeaponType == WeaponType.Laser || weapon.WeaponType == WeaponType.Area)
                {
                    weapon.AddedDamage = (weapon.AttackDamage) * (towerTemplate.weapon[level].buff);
                    weapon.BuffLevel = Level;
                }
            }
        }
    }

    public void OffBuffAroundTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        for (int i = 0; i < towers.Length; ++i)
        {

            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();
            Debug.Log(towers.Length);
            if (weapon.BuffLevel == 0)
            {
                continue;
            }

            if (Vector3.Distance(weapon.transform.position, transform.position) <= towerTemplate.weapon[level].attackRange )
            {
                if (weapon.WeaponType == WeaponType.Cannon || weapon.WeaponType == WeaponType.Laser || weapon.WeaponType == WeaponType.Area)
                {
                    weapon.AddedDamage = 0;
                    weapon.BuffLevel = 0;
                }
            }
        }
    }

    private IEnumerator GenerateMoney()
    {
        // Debug.Log("Hello");
        while (true)
        {
            if (gameController.isWaveOn)
            {
                playerGold.CurrentGold += Mathf.RoundToInt(Earning);

            }
            yield return new WaitForSeconds(1.0f);
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
                else if(weaponType == WeaponType.Area)
                {
                    ChangeState(WeaponState.TryAttackArea);
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

    private IEnumerator TryAttackArea()
    {
        while (true)
        {
            if (!IsPossibleToAttackTarget())
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            SpawnBomb();
            yield return new WaitForSeconds(towerTemplate.weapon[level].attackRate);
            
        }
        // yield return null;
    }


    private Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;
        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            if (enemySpawner.EnemyList[i] == null) continue;
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            if (distance <= towerTemplate.weapon[level].attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
            }
        }

        return attackTarget;
    }

    private void SpawnBomb()
    {
        GameObject clone = Instantiate(areaBombPrefab, spawnPoint.position, Quaternion.identity);
        float damage = towerTemplate.weapon[level].areaDamage + AddedDamage;
        clone.GetComponent<Bomb>().Setup(attackTarget, damage, towerTemplate.weapon[level].areaOfImpact);
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        // clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);

        float damage = towerTemplate.weapon[level].damage + AddedDamage;
        clone.GetComponent<Projectile>().Setup(attackTarget, damage);
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

                // attackTarget.GetComponent<EnemyHP>().TakeDamage(towerTemplate.weapon[level].damage * Time.deltaTime);

                float damage = towerTemplate.weapon[level].damage + AddedDamage;
                attackTarget.GetComponent<EnemyHP>().TakeDamage(damage * Time.deltaTime);

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

        towerSpawner.OnBuffAllBuffTowers();
        return true;
    }

    public void Sell()
    {


        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;

        bool flag = false;
        if (weaponType == WeaponType.Buff) flag = true;

        if (flag) OffBuffAroundTower();

        Destroy(gameObject);


    }
}
