using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System;

public class LoginFunctions : Singleton<LoginFunctions>
{
    FirebaseAuth auth;

    void Awake()
    {
        AwakeSingleton(this);
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public enum AuthStatus
    {
        CANCELED,
        FAULTED,
        SUCCESS
    }

    public async void CreateUserAccount(string email, string password, Action<AuthStatus, FirebaseUser> callback)
    {
        var status = new AuthStatus();
        FirebaseUser newUser = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password)
        .ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Create User was canceled.");
                status = AuthStatus.CANCELED;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError($"Create User encountered an error: {task.Exception}");
                status = AuthStatus.FAULTED;
                return;
            }

            // Firebase user has been created.
            newUser = task.Result;
            Debug.Log($"Firebase user created successfully: {newUser.DisplayName} ({newUser.UserId})");
            status = AuthStatus.SUCCESS;
        });
        callback(status, newUser);
    }

    public async void LoginWithAccount(string email, string password, Action<AuthStatus, FirebaseUser> callback)
    {
        var status = new AuthStatus();
        FirebaseUser newUser = null;
        await auth.SignInWithEmailAndPasswordAsync(email, password)
        .ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Sign In was canceled.");
                status = AuthStatus.CANCELED;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError($"SignIn encountered an error: {task.Exception}");
                status = AuthStatus.FAULTED;
                return;
            }

            newUser = task.Result;
            Debug.Log($"User signed in successfully: {newUser.DisplayName} ({newUser.UserId})");
            status = AuthStatus.SUCCESS;
        });
        callback(status, newUser);
    }

    public void Logout(Action<string> callback)
    {
        var name = auth.CurrentUser?.Email;
        if (name == null)
            return;
        auth.SignOut();
        Debug.Log($"account with email {auth.CurrentUser.Email} logged out");
        callback(name);
    }

    #region tooling
    [ContextMenu("Loged in account")]
    public void ListLogedInUsers()
    {
        //var list = "";
        Debug.Log("user: " + auth.CurrentUser?.Email);
    }
    #endregion

}
