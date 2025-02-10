using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScenario : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;
    [SerializeField]
    private Progress progress;
    [SerializeField]
    private GameObject panel;

    private void Awake()
    {
        panel.SetActive(true);
        progress.Play();

        user.GetUserInfoFromBackend();


    }
    private void Start()
    {
        BackendGameData.Instance.GameDataLoad();
    }

}
