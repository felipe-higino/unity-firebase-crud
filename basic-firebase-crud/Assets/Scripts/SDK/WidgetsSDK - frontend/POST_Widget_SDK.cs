using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POST_Widget_SDK : MonoBehaviour
{
    [SerializeField] InputField PlayerName;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitPOST()
    {
        var name = PlayerName.text; //-- cache text
        if (name == "")
        {
            TextConfirmation.text = "Invalid name input";
            return;
        }

        PlayerName.text = ""; //-------- reset text
        TextConfirmation.text = "submitted...";

        //creating object in database
        var player = new PlayerData(name);
        RequestFromSDK.Instance.CREATE_PLAYER(player, res =>
        { 
            switch (res)
            {
                case RequestFromSDK.ResponseStatus.SUCCESS:
                    TextConfirmation.text = $"Player \"{name}\" created!";
                    break;
                case RequestFromSDK.ResponseStatus.ERROR:
                    TextConfirmation.text = $"Fail to create \"{name}\" Player";
                    break;
                default:
                    break;
            }
        });
    }
}
