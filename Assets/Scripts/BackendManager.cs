using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }

    private void Update()
    {
        
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

        Test();
    }

    void Test()
    {
        // BackendLogin.Instance.CustomSignUp("user1", "1234"); // [추가] 뒤끝 회원가입 함수
        Debug.Log("테스트를 종료합니다.");
    }
}
