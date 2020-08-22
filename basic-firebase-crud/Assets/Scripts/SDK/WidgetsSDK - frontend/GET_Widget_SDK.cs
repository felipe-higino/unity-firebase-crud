using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GET_Widget_SDK : MonoBehaviour
{

    [SerializeField] InputField PlayerName;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }
    
    public void OnSubmitGet()
    {
        var name = PlayerName.text; //-- cache text
        if (name == "")
        {
            TextConfirmation.text = "Invalid name input";
            return;
        }

        PlayerName.text = ""; //-------- reset text
        TextConfirmation.text = "submitted...";

        RequestFromSDK.Instance.GET_PLAYER_BY_NAME(name, (res, player)=> {
            switch (res)
            {
                case RequestFromSDK.ResponseStatus.SUCCESS:
                    if (player == null)
                    {
                        TextConfirmation.text = "Player not found";
                        break;
                    }
                    TextConfirmation.text = $"Player \"{player.Name}\" found! Creation: {player.CreationDate} Score:{player.Score}";
                    break;
                case RequestFromSDK.ResponseStatus.ERROR:
                    TextConfirmation.text = "Query error";
                    break;
                default:
                    break;
            }
        });
    }
}
