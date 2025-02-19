using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketControl : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] buttonTexts;




    private void Awake()
    {
        StartCoroutine(UpdateUpgrade());
    }

    private IEnumerator UpdateUpgrade()
    {
        while (true)
        {
            yield return new WaitForSeconds(300);
            BackendGameUpgradeData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataUpdate();
        }
    }







    public void ButtonOne()
    {
        // tower 1 damage
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"] += 1;
        buttonTexts[0].text = "Damage Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"].ToString() 
            + $"{20* BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"]}";
        BackendGameData.Instance.UserGameData.gold -= 20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"];
    }
    public void ButtonTwo()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"] += 1;
        buttonTexts[1].text = $"Attack Speed Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"]})";
    }

    public void ButtonThree()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"] += 1;
        buttonTexts[2].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"]})";
    }

    public void ButtonFour()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"] += 1;
        buttonTexts[3].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"]})";
    }


    public void ButtonFive()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"] += 1;
        buttonTexts[4].text = $"Damage Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"]})";
    }

    public void ButtonSix()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"] += 1;
        buttonTexts[5].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"]})";
    }

    public void ButtonSeven()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"] += 1;
        buttonTexts[6].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"]})";
    }

    public void ButtonEight()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"] += 1;
        buttonTexts[7].text = $"Slow Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"]})";
    }

    public void ButtonNine()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"] += 1;
        buttonTexts[8].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"]})";
    }

    public void ButtonTen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"] += 1;
        buttonTexts[9].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"]})";
    }

    public void ButtonEleven()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"] += 1;
        buttonTexts[10].text = $"Buff Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"]})";
    }

    public void ButtonTwelve()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"] += 1;
        buttonTexts[11].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"]})";
    }

    public void ButtonThirteen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"] += 1;
        buttonTexts[12].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"]})";
    }

    public void ButtonFourteen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"] += 1;
        buttonTexts[13].text = $"Earning Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"]})";
    }

    public void ButtonFifteen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"] += 1;
        buttonTexts[14].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"]})";
    }

    public void ButtonSixteen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"] += 1;
        buttonTexts[15].text = $"Damage Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"]})";
    }

    public void ButtonSeventeen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"] += 1;
        buttonTexts[16].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"]})";
    }

    public void ButtonEighteen()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"] += 1;
        buttonTexts[17].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"]})";
    }




}
