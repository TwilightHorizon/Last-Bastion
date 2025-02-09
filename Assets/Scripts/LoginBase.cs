using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMessage;


    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }

    }

    /// <summary>
    /// 메시지를 설정합니다.
    /// </summary>
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }

    /// <summary>
    /// 입력이 올바르지 않을 때의 가이드를 설정합니다.
    /// </summary>
    protected void GuideForIncorrectInput(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }

    /// <summary>
    /// 필드값이 비어있는지 확인합니다. (image: field, field: 내용, result: 출력될 내용)
    /// </summary>
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectInput(image, $"\"{result}\" is empty!");
            return true;
        }
        return false;
    }
}
