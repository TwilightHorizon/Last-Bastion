using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.UI;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private TowerAttackRange towerAttackRange;

    [SerializeField]
    private Button buttonUpgrade;

    [SerializeField]
    private SystemTextViewer systemTextViewer;

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

        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.AttackRange);
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        imageTower.sprite = currentTower.TowerSprite;
        textDamage.text = "Damage: " + currentTower.AttackDamage;
        textAttackRate.text = "Fire Rate: " + currentTower.AttackRate;
        textAttackRange.text = "Range: " + currentTower.AttackRange;
        textLevel.text = "Level: " + currentTower.Level;

        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;

    }

    public void OnClickEventTowerUpgrade()
    {
        bool isSuccess = currentTower.Upgrade();

        if (isSuccess)
        {
            UpdateTowerData();
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.AttackRange);
        }
        else
        {
            systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void OnClickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();
    }

}
