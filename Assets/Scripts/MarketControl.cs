using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketControl : MonoBehaviour
{

    
    public void ButtonOne()
    {
        // tower 1 damage
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["damage"] += 1;
    }
    public void ButtonTwo()
    {
        // tower 1 speed
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["attackSpeed"] += 1;
    }

    public void ButtonThree()
    {
        // tower 1 range
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["range"] += 1;
    }

    public void ButtonFour()
    {
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerOneUpgrade["cost"] += 1;
    }


    public void ButtonFive()
    {
        // tower 2 damage
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["damage"] += 1;
    }

    public void ButtonSix()
    {
        // tower 2 range
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["range"] += 1;
    }

    public void ButtonSeven()
    {
        // tower 2 cost
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerTwoUpgrade["cost"] += 1;
    }

    public void ButtonEight()
    {
        // tower 3 slow
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["slow"] += 1;
    }

    public void ButtonNine()
    {
        // tower 3 range
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["range"] += 1;
    }

    public void ButtonTen()
    {
        // tower 3 cost
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerThreeUpgrade["cost"] += 1;
    }

    public void ButtonEleven()
    {
        // tower 4 buff
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["buff"] += 1;
    }

    public void ButtonTwelve()
    {
        // tower 4 range
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["range"] += 1;
    }

    public void ButtonThirteen()
    {
        // tower 4 cost
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFourUpgrade["cost"] += 1;
    }

    public void ButtonFourteen()
    {
        // tower 5 earning
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["earning"] += 1;
    }

    public void ButtonFifteen()
    {
        // tower 5 cost
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerFiveUpgrade["cost"] += 1;
    }

    public void ButtonSixteen()
    {
        // tower 6 damage
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["damage"] += 1;
    }

    public void ButtonSeventeen()
    {
        // tower 6 range
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["range"] += 1;
    }

    public void ButtonEighteen()
    {
        // tower 6 cost
        BackendGameUpgradeData.Instance.UserGameUpgradeData.towerSixUpgrade["cost"] += 1;
    }




}
