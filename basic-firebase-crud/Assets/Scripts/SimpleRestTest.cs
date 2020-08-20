﻿using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData
{
    public string Name;// { get; }
    public string CreationDate;// { get; }

    public PlayerData(string name)
    {
        Name = name;
        CreationDate = DateTime.Now.ToString();
    }
}

public class SimpleRestTest : MonoBehaviour
{
    #region singleton
    public static SimpleRestTest Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    [SerializeField] TextAsset URL;

    public void POST_IN_FIREBASE(PlayerData data, Action confirmationCallback)
    {
        RestClient.Put($"{URL.text}{data.Name}.json", data)
        .Then(response => {
            confirmationCallback();
        });
    }

    public void GET_FROM_FIREBASE(String playerName, Action<PlayerData> confirmationCallback)
    {
        RestClient.Get<PlayerData>($"{URL.text}{playerName}.json")
        .Then(response=> {
            confirmationCallback(response);
        }).Catch(err=> {
            confirmationCallback(null);
        });
    }

}
