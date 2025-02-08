using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{   
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;

    [SerializeField]
    private TextMeshProUGUI textLevel;

    [SerializeField]
    private TextMeshProUGUI textAttackRate;

    [SerializeField]
    private TextMeshProUGUI textAttackRange;

    private TowerWeapon currentTower;


    private void Awake()
    {
        OffPanel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)
    {
        currentTower = towerWeapon.GetComponent<TowerWeapon>(); 
        
        gameObject.SetActive(true);

        UpdateTowerData();
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateTowerData()
    {
        textDamage.text = "Damage: " + currentTower.Damage;
        textAttackRate.text = "Attack Rate: " + currentTower.AttackRate;
        textAttackRange.text = "Attack Range: " + currentTower.AttackRange;
        textLevel.text = "Level: " + currentTower.Level;
    }

}
