using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketControl : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] buttonTexts;




    private void Awake()
    {
        buttonTexts[0].text = "Damage Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"]})";

        buttonTexts[1].text = "Attack Speed Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"]})";

        buttonTexts[2].text = "Attack Range Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"]})";

        buttonTexts[3].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"]})";

        buttonTexts[4].text = "Damage Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"]})";

        buttonTexts[5].text = "Attack Range Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"]})";

        buttonTexts[6].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"]})";

        buttonTexts[7].text = "Slow Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"]})";

        buttonTexts[8].text = "Attack Range Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"]})";

        buttonTexts[9].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"]})";

        buttonTexts[10].text = "Buff Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"]})";

        buttonTexts[11].text = "Attack Range Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"]})";

        buttonTexts[12].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"]})";

        buttonTexts[13].text = "Earning Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"]})";

        buttonTexts[14].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"]})";

        buttonTexts[15].text = "Damage Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"]})";

        buttonTexts[16].text = "Attack Range Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"]})";

        buttonTexts[17].text = "Price Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"].ToString()
            + $" ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"]})";

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


    [SerializeField]
    private TopPanelViewer topPanelViewer;
    



    public void ButtonOne()
    {
        if(BackendGameData.Instance.UserGameData.gold < 20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"]-1)
        {
            return;
        }

        // tower 1 damage
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"] += 1;
        buttonTexts[0].text = "Damage Lv." + BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"].ToString() 
            + $" ({20* BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"]-1);
        topPanelViewer.UpdateGameData();
    }
    public void ButtonTwo()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"] += 1;
        buttonTexts[1].text = $"Attack Speed Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonThree()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"] += 1;
        buttonTexts[2].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonFour()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"] += 1;
        buttonTexts[3].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonFive()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"] += 1;
        buttonTexts[4].text = $"Damage Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonSix()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"] += 1;
        buttonTexts[5].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonSeven()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"] += 1;
        buttonTexts[6].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonEight()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"] += 1;
        buttonTexts[7].text = $"Slow Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonNine()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"] += 1;
        buttonTexts[8].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonTen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"] += 1;
        buttonTexts[9].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonEleven()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"] += 1;
        buttonTexts[10].text = $"Buff Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonTwelve()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"] += 1;
        buttonTexts[11].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonThirteen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"] += 1;
        buttonTexts[12].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonFourteen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"] += 1;
        buttonTexts[13].text = $"Earning Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonFifteen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"] += 1;
        buttonTexts[14].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonSixteen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"] += 1;
        buttonTexts[15].text = $"Damage Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonSeventeen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"] += 1;
        buttonTexts[16].text = $"Attack Range Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"] - 1);
        topPanelViewer.UpdateGameData();
    }

    public void ButtonEighteen()
    {
        if (BackendGameData.Instance.UserGameData.gold < 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"] - 1))
        {
            return;
        }
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"] += 1;
        buttonTexts[17].text = $"Price Lv.{BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"]} ({20 * BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"]})";
        BackendGameData.Instance.UserGameData.gold -= 20 * (BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"] - 1);
        topPanelViewer.UpdateGameData();
    }


}
