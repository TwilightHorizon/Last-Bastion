using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate;
    //[SerializeField]
    //private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;
    //[SerializeField]
    //private int towerBuildCost = 50; // amount of gold required to build a tower
    [SerializeField]
    private PlayerGold playerGold;   // to acceses the player gold and reduce it

    [SerializeField]
    private SystemTextViewer systemTextViewer;
    [SerializeField]
    private GameController gameController;
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;

    private int towerType;


    private Dictionary<string, int>[] towerUpgrades = new Dictionary<string, int>[6];

    private void Awake()
    {
        towerUpgrades[0] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade;
        towerUpgrades[1] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade;
        towerUpgrades[2] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade;
        towerUpgrades[3] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade;
        towerUpgrades[4] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade;
        towerUpgrades[5] = BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade;

        //for(int i = 0; i<towerTemplate.Length; i++)
        //{
        //    for(int j = 0; j < towerTemplate[i].weapon.Length; j++)
        //    {
        //        towerTemplate[i].weapon[j].cost = towerTemplate[i].weapon[j].cost + towerUpgrades[i][towerTemplate[i].weapon[j].name];
        //    }
        //}

        // tower type 0

        // Tower Type 1

        SetUpUpgrades();


    }

    public void ReadyToSpawnTower(int type)
    {
        towerType = type;
        if (isOnTowerButton) return;
        if (towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }
        isOnTowerButton = true;

        followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);

        StartCoroutine(OnTowerCancelSystem());
    }

    public void SpawnTower(Transform tileTransform)
    {

        if (!isOnTowerButton)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        //if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        //{
        //    systemTextViewer.PrintText(SystemType.Money);   
        //    return;
        //}

        if (tile.IsBuildTower)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }


        isOnTowerButton = false;
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost; // reduce the player gold by the cost

        Vector3 position = tileTransform.position + Vector3.back;

        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(this, enemySpawner, playerGold, tile, gameController, towerUpgrades[towerType]);


        OnBuffAllBuffTowers();
        Destroy(followTowerClone);

        StopCoroutine(OnTowerCancelSystem());

    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                isOnTowerButton = false;
                Destroy(followTowerClone);
                break;
            }
            yield return null;
        }
    }


    public void OnBuffAllBuffTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        //Debug.Log(towers.Length); 
        for (int i = 0; i < towers.Length; ++i)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();
            // Debug.Log("running?");
            if (weapon.WeaponType == WeaponType.Buff)
            {
                //Debug.Log("problem?");
                weapon.OnBuffAroundTower();
            }
        }
    }

    private void SetUpUpgrades()
    {
        for (int i = 0; i < towerTemplate[0].weapon.Length; i++)
        {
            towerTemplate[0].weapon[i].damage += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"];
            towerTemplate[0].weapon[i].attackRange += 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"];
            towerTemplate[0].weapon[i].attackRate -= 0.001f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"];
            towerTemplate[0].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"];
        }

        // Tower Type 2
        for (int i = 0; i < towerTemplate[1].weapon.Length; i++)
        {
            towerTemplate[1].weapon[i].damage += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"];
            towerTemplate[1].weapon[i].attackRange += 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"];
            towerTemplate[1].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"];
        }

        // Tower Type 3
        for (int i = 0; i < towerTemplate[2].weapon.Length; i++)
        {
            towerTemplate[2].weapon[i].slow += 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"];
            towerTemplate[2].weapon[i].attackRange += 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"];
            towerTemplate[2].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"];
        }

        // Tower Type 4
        for (int i = 0; i < towerTemplate[3].weapon.Length; i++)
        {
            towerTemplate[3].weapon[i].buff += 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"];
            towerTemplate[3].weapon[i].attackRange += 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"];
            towerTemplate[3].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"];
        }

        // Tower Type 5
        for (int i = 0; i < towerTemplate[4].weapon.Length; i++)
        {
            towerTemplate[4].weapon[i].earning += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"];
            towerTemplate[4].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"];
        }

        // Tower Type 6
        for (int i = 0; i < towerTemplate[5].weapon.Length; i++)
        {
            towerTemplate[5].weapon[i].damage += BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"];
            towerTemplate[5].weapon[i].attackRange += 0.05f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"];
            // towerTemplate[5].weapon[i].attackRate -= 0.01f * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["attackSpeed"];
            towerTemplate[5].weapon[i].cost -= BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"];
        }
    }
}

