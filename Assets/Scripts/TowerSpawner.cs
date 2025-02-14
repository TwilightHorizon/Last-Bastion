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

        if(!isOnTowerButton)
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

        clone.GetComponent<TowerWeapon>().Setup(this, enemySpawner, playerGold, tile, gameController);


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
        for(int i = 0; i< towers.Length; ++i)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();
            // Debug.Log("running?");
            if(weapon.WeaponType == WeaponType.Buff)
            {
                //Debug.Log("problem?");
                weapon.OnBuffAroundTower();
            }
        }
    }


}

