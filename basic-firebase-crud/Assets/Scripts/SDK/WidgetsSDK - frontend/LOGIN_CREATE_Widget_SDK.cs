using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LOGIN_CREATE_Widget_SDK : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] Text TextConfirmation;

    private void Start()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitCreate()
    {
        LoginFunctions.Instance.CreateUserAccount(
        email.text, 
        password.text, 
        (status, user) => {
            switch (status)
            {
                case LoginFunctions.AuthStatus.CANCELED:
                    TextConfirmation.text = "canceled...";
                    break;
                case LoginFunctions.AuthStatus.FAULTED:
                    TextConfirmation.text = "fault...";
                    break;
                case LoginFunctions.AuthStatus.SUCCESS:
                    TextConfirmation.text = $"Created: {user.UserId} ";
                    break;
                default:
                    break;
            }
        });
    }

    public void OnSubmitLogin()
    {
        LoginFunctions.Instance.LoginWithAccount(
        email.text,
        password.text,
        (status,user)=> {
            switch (status)
            {
                case LoginFunctions.AuthStatus.CANCELED:
                    TextConfirmation.text = "canceled...";
                    break;
                case LoginFunctions.AuthStatus.FAULTED:
                    TextConfirmation.text = "fault...";
                    break;
                case LoginFunctions.AuthStatus.SUCCESS:
                    TextConfirmation.text = $"Logged in: {user.UserId}";
                    break;
                default:
                    break;
            }
        });
    }

    public void OnSubmitLogout()
    {
        LoginFunctions.Instance.Logout( (name) => {
            TextConfirmation.text = $"{name} logged out";
        });
    }
}
