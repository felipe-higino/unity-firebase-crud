using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POST_Widget : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text TextConfirmation;

    public void OnSubmitPOST()
    {
        var playerName = PlayerName.text;
        if (playerName == "")
            return;

        var data = new PlayerData(playerName);
        SimpleRestTest.Instance.POST_IN_FIREBASE(data);

        TextConfirmation.text = $"Player \"{playerName}\" created!";
    }
}
