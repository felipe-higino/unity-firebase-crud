using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DELETE_Widget_REST : MonoBehaviour
{
    [SerializeField] Text PlayerNameToDelete;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitDELETE()
    {
        var playerName = PlayerNameToDelete.text;
        if (playerName == "")
        {
            TextConfirmation.text = "Invalid name";
            return;
        }

        SimpleRestTest.Instance.DELETE_FROM_FIREBASE(PlayerNameToDelete.text,(deleteSucessfull)=> {
            if (deleteSucessfull)
                TextConfirmation.text = "Player sucessfull deleted";
            else
                TextConfirmation.text = "Fail to delete player - server error";
        });
    }
}
