using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GET_Widget_REST : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitGET()
    {
        var playerName = PlayerName.text;
        if (playerName == "")
        {
            TextConfirmation.text = "Invalid name";
            return;
        }

        SimpleRestTest.Instance.GET_FROM_FIREBASE(playerName, playerData =>{
            if (playerData == null)
                TextConfirmation.text = "Player not found";
            else
                TextConfirmation.text = $"User \"{playerData.Name}\" found, Creation: \"{playerData.CreationDate}\"";
        });
    }
}
