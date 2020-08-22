﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUT_Widget_SDK : MonoBehaviour
{
    [SerializeField] InputField PlayerNameToEdit;
    [SerializeField] InputField NewNameOfPlayer;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitPut()
    {
        var oldName = PlayerNameToEdit.text; //-- cache text
        var newName = NewNameOfPlayer.text; //--- cache text
        if (oldName == "")
        {
            TextConfirmation.text = "Invalid name input";
            return;
        }

        PlayerNameToEdit.text = ""; //-------- reset text
        NewNameOfPlayer.text = ""; //--------- reset text
        TextConfirmation.text = "submitted...";

        //RequestFromSDK.Instance
    }
}
