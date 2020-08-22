using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RequestFromSDK : Singleton<RequestFromSDK>
{
    [SerializeField] TextAsset dbURL;
    DatabaseReference database;

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

    public enum ResponseStatus { SUCCESS, ERROR }

    /// <summary>
    /// Creates new player in databaase
    /// </summary>
    public async void CREATE_PLAYER(PlayerData player, Action<ResponseStatus> callback)
    {
        var json = JsonUtility.ToJson(player);
        //generation of unique ID
        //var playerId = database.Child("Players").Push().Key;
        //setting player data
        ResponseStatus status = new ResponseStatus();
        await database.Child("Players").Child(player.Name).SetRawJsonValueAsync(json)
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

    //public async void UPDATE()
    //{

    //}

    //public async void DELETE()
    //{

    //}

}
