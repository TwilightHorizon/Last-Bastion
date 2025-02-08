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
        public float attackRate;
        public float attackRange;
        public int cost;
        public int sell;
        
    }

}
