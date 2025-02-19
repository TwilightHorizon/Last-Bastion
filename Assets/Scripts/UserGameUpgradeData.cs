
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

        towerOneUpgrade.Add("damage", 0);
        towerOneUpgrade.Add("attackSpeed", 0);
        towerOneUpgrade.Add("range", 0);
        towerOneUpgrade.Add("cost", 0);

        towerTwoUpgrade.Add("damage", 0);
        // towerTwoUpgrade.Add("attackSpeed", 0);
        towerTwoUpgrade.Add("range", 0);
        towerTwoUpgrade.Add("cost", 0);

        towerThreeUpgrade.Add("slow", 0);
        // towerThreeUpgrade.Add("attackSpeed", 0);
        towerThreeUpgrade.Add("range", 0);
        towerThreeUpgrade.Add("cost", 0);

        towerFourUpgrade.Add("buff", 0);
        // towerFourUpgrade.Add("attackSpeed", 0);
        towerFourUpgrade.Add("range", 0);
        towerFourUpgrade.Add("cost", 0);

        towerFiveUpgrade.Add("earning", 0);
        // towerFiveUpgrade.Add("attackSpeed", 0);
        // towerFiveUpgrade.Add("range", 0);
        towerFiveUpgrade.Add("cost", 0);

        towerSixUpgrade.Add("damage", 0);
        // towerSixUpgrade.Add("areaDamage", 0);
        // towerSixUpgrade.Add("attackSpeed", 0);
        towerSixUpgrade.Add("range", 0);
        towerSixUpgrade.Add("cost", 0);
    }
}
