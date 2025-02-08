using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private int towerBuildCost = 50; // amount of gold required to build a tower
    [SerializeField]
    private PlayerGold playerGold;   // to acceses the player gold and reduce it


    public void SpawnTimer(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        if(towerBuildCost > playerGold.CurrentGold)
        {
            return;
        }


        if (tile.IsBuildTower)
        {
            return;
        }

        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerBuildCost; // reduce the player gold by the cost
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);

    }

}

