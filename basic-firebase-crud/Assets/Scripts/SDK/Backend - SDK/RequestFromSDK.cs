using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
public class RequestFromSDK : Singleton<RequestFromSDK>
{
    [SerializeField] TextAsset dbURL;
    DatabaseReference database;
    const string accounts = "Accounts";

    private void Awake()
    {
        AwakeSingleton(this);    
    }

    private void Start()
    {
        //firebase presets
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbURL.text);
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// Auxiliates Firebase's SDK response queries status and callbacks 
    /// </summary>
    public enum ResponseStatus { SUCCESS, ERROR }

    /// <summary>
    /// Creates new player in databaase
    /// </summary>
    public async void CREATE_PLAYER(PlayerData player, Action<ResponseStatus> callback)
    {
        var userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
        if (userID == null)
        {
            Debug.LogError("user not logged in");
            return;
        }

        player.Score = 1;
        var json = JsonUtility.ToJson(player);
        //generation of unique ID
        //var playerId = database.Child("Players").Push().Key;
        //setting player data
        ResponseStatus status = new ResponseStatus();
        await database.Child(accounts).Child(userID).SetRawJsonValueAsync(json)
        .ContinueWith( task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError($"Something went wrong. Status:{task.Status}");
                status = ResponseStatus.ERROR;
            }
            else if (task.IsCompleted)
            {
                // Do something with snapshot...
                Debug.Log($"Done right! Status:{task.Status}");
                status = ResponseStatus.SUCCESS;
            }
        });
        callback(status);
    }

    public async void GET_PLAYER_BY_NAME(string name, Action<ResponseStatus,PlayerData> callback)
    {
        ResponseStatus status = new ResponseStatus();
        PlayerData player = null;
        await FirebaseDatabase.DefaultInstance
        .GetReference($"Players/{name}")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError($"Something went wrong. Status:{task.Status}");
                status = ResponseStatus.ERROR;
            }
            else if (task.IsCompleted)
            {
                var json = task.Result.GetRawJsonValue();
                player = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log($"Done right! Status:{task.Status}");
                status = ResponseStatus.SUCCESS;
            }
        });
        callback(status, player);
    }

    public async void UPDATE_PLAYER_NAME(string oldName, string newName, Action<ResponseStatus> callback)
    {
        ResponseStatus status = new ResponseStatus();
        await database.Child("Players").Child(oldName).Child("Name").SetValueAsync(newName)
        .ContinueWith(task=> {
            if (task.IsFaulted)
            {
                Debug.LogError($"Something went wrong. Status:{task.Status}");
                status = ResponseStatus.ERROR;
            }
            else if (task.IsCompleted)
            {
                Debug.Log($"Done right! Status:{task.Status}");
                status = ResponseStatus.SUCCESS;
            }
        });
        callback(status);
    }

    public async void DELETE_ACCOUNT(string login, Action<ResponseStatus> callback)
    {
        ResponseStatus status = new ResponseStatus();
        await database.Child("Players").Child(login).RemoveValueAsync()
        .ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Something went wrong. Status:{task.Status}");
                status = ResponseStatus.ERROR;
            }
            else if (task.IsCompleted)
            {
                Debug.Log($"Done right! Status:{task.Status}");
                status = ResponseStatus.SUCCESS;
            }
        });
        callback(status);
    }

}
