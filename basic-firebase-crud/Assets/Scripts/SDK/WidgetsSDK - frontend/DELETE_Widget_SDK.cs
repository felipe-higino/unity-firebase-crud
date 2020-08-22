using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DELETE_Widget_SDK : MonoBehaviour
{
    [SerializeField] InputField loginToDelete;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitDelete()
    {
        var name = loginToDelete.text; //-- cache text
        if (name == "")
        {
            TextConfirmation.text = "Invalid name input";
            return;
        }

        loginToDelete.text = ""; //-------- reset text
        TextConfirmation.text = "submitted...";

        RequestFromSDK.Instance.DELETE_ACCOUNT(name,res=> {
            switch (res)
            {
                case RequestFromSDK.ResponseStatus.SUCCESS:
                    TextConfirmation.text = $"{name} deleted";
                    break;
                case RequestFromSDK.ResponseStatus.ERROR:
                    TextConfirmation.text = "Fail to delete account";
                    break;
                default:
                    break;
            }
        });
    }
}
