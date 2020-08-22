using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUT_Widget_REST : MonoBehaviour
{
    [SerializeField] Text PlayerNameToEdit;
    [SerializeField] Text NewNameOfPlayer;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitEDIT()
    {
        var playerName = PlayerNameToEdit.text;
        if (playerName == "")
        {
            TextConfirmation.text = "Invalid name";
            return;
        }

        var old = PlayerNameToEdit.text;
        var new_ = NewNameOfPlayer.text;
        SimpleRestTest.Instance.EDIT_PLAYER_FROM_FIREBASE(old, new_,()=> {
            TextConfirmation.text = $"{old} changed to {new_}";
        });
    }
}
