using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POST_Widget_REST : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitPOST()
    {
        var playerName = PlayerName.text;
        if (playerName == "")
        {
            TextConfirmation.text = "Invalid name";
            return;
        }

        var data = new PlayerData(playerName);
        SimpleRestTest.Instance.POST_IN_FIREBASE(data,()=>
        {
            TextConfirmation.text = $"Player \"{playerName}\" created!";
        });

    }

}
