
using System.Collections.Generic;

[System.Serializable]
public class UserGameUpgradeData 
{

    public Dictionary<string, int> towerOneUpgrade;
    public Dictionary<string, int> towerTwoUpgrade;
    public Dictionary<string, int> towerThreeUpgrade;
    public Dictionary<string, int> towerFourUpgrade;
    public Dictionary<string, int> towerFiveUpgrade;
    public Dictionary<string, int> towerSixUpgrade;

    public void Reset()
    {
        towerOneUpgrade = new Dictionary<string, int>();
        towerTwoUpgrade = new Dictionary<string, int>();
        towerThreeUpgrade = new Dictionary<string, int>();
        towerFourUpgrade = new Dictionary<string, int>();
        towerFiveUpgrade = new Dictionary<string, int>();
        towerSixUpgrade = new Dictionary<string, int>();

        towerOneUpgrade.Add("damage", 1);
        towerOneUpgrade.Add("attackSpeed", 1);
        towerOneUpgrade.Add("range", 1);
        towerOneUpgrade.Add("cost", 1);

        towerTwoUpgrade.Add("damage", 1);
        // towerTwoUpgrade.Add("attackSpeed", 0);
        towerTwoUpgrade.Add("range", 1);
        towerTwoUpgrade.Add("cost", 1);

        towerThreeUpgrade.Add("slow", 1);
        // towerThreeUpgrade.Add("attackSpeed", 0);
        towerThreeUpgrade.Add("range", 1);
        towerThreeUpgrade.Add("cost", 1);

        towerFourUpgrade.Add("buff", 1);
        // towerFourUpgrade.Add("attackSpeed", 0);
        towerFourUpgrade.Add("range", 1);
        towerFourUpgrade.Add("cost", 1);

        towerFiveUpgrade.Add("earning", 1);
        // towerFiveUpgrade.Add("attackSpeed", 0);
        // towerFiveUpgrade.Add("range", 0);
        towerFiveUpgrade.Add("cost", 1);

        towerSixUpgrade.Add("damage", 1);
        // towerSixUpgrade.Add("areaDamage", 0);
        // towerSixUpgrade.Add("attackSpeed", 0);
        towerSixUpgrade.Add("range", 01);
        towerSixUpgrade.Add("cost", 1);
    }
}
