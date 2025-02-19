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

    [SerializeField]
    private TextMeshProUGUI textUpgradeCost;

    [SerializeField]
    private TextMeshProUGUI textSellCost;



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

        if(currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = "Dmg: " + currentTower.AttackDamage + " + " + "<color=green>" + currentTower.AddedDamage.ToString("F1") + "</color>";
            textAttackRate.text = "Fire Rate: " + currentTower.AttackRate;
            textAttackRange.text = "Range: " + currentTower.AttackRange;
        }
        else if (currentTower.WeaponType == WeaponType.Area)
        {
            textDamage.text = "Dmg: " + currentTower.AreaDamage + " + " + "<color=green>" + currentTower.AddedDamage.ToString("F1") + "</color>";
            textAttackRange.text = "Impact Range: " + currentTower.AreaOfImpact;
        }
        else
        {
            imageTower.rectTransform.sizeDelta = new Vector2(59, 59);
            if(currentTower.WeaponType == WeaponType.Slow)
            {
                textDamage.text = "Slow: " + currentTower.Slow * 100 + "%";

            }
            else if(currentTower.WeaponType == WeaponType.Buff)
            {
                textDamage.text = "Buff: " + currentTower.Buff * 100 + "%"; 
            }
            else if(currentTower.WeaponType == WeaponType.Money)
            {
                textDamage.text = "Money: " + currentTower.Earning + "/s";
            }

            textAttackRange.text = "Range: " + currentTower.AttackRange;

            textAttackRate.text = "Fire Rate: -";
        }


        imageTower.sprite = currentTower.TowerSprite;
        //textAttackRate.text = "Fire Rate: " + currentTower.AttackRate;
        
        textLevel.text = "Level: " + currentTower.Level;
        textUpgradeCost.text = "$"+currentTower.UpgradeCost.ToString();
        textSellCost.text = "$" + currentTower.SellCost.ToString();



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
