using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{

    public GameObject towerPrefab;
    public GameObject followTowerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite;
        public float damage;
        public float slow; // 0.2 ~ 20% slow
        public float buff; // 0.2 => atk x 1.2
        public float earning;
        public float areaDamage;
        public float areaOfImpact;
        public float attackRate;
        public float attackRange;
        public int cost;
        public int sell;
        
    }

}
