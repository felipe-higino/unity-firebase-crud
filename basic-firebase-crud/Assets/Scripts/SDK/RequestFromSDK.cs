using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestFromSDK : Singleton<RequestFromSDK>
{
    [SerializeField] TextAsset dbURL;
    DatabaseReference database;

    void Awake()
    {
        AwakeSingleton(this);
    }

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbURL.text);
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void POST(PlayerData player)
    {
        var json = JsonUtility.ToJson(player);
        database.Child("Players").Child(player.Name).SetRawJsonValueAsync(json);
    }

    
}
