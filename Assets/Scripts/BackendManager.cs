using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{

    private void Awake()
    {
        BackendSetup();
    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize();
        if (bro.IsSuccess())
        {
            Debug.Log($"Backend initialized successfully: {bro}");
        }
        else
        {
            Debug.LogError($"Backend initialization failed: {bro}");
        }
    }
}
