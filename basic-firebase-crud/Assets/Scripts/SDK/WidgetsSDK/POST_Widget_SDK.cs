using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POST_Widget_SDK : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitPOST()
    {
        var name = PlayerName.text;
        if (name == "")
        {
            TextConfirmation.text =
                "Invalid name input";
            return;
        }
        var player = new PlayerData(name);
        RequestFromSDK.Instance.POST(player);
        TextConfirmation.text = "submitted...";
    }
}
