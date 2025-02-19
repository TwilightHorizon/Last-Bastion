using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackendGameUpgradeData 
{
    [System.Serializable]
    public class GameUpgradeDataLoadEvent : UnityEvent { }
    public GameUpgradeDataLoadEvent onGameDataLoadEvent = new GameUpgradeDataLoadEvent();


    private static BackendGameUpgradeData instance = null;
    public static BackendGameUpgradeData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendGameUpgradeData();
            }
            return instance;
        }
    }






}
