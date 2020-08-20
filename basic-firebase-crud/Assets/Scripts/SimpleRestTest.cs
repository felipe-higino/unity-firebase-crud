using Proyecto26;
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
        //RestClient.Post($"{URL.text}.json", data)
        .Then(response => {
            confirmationCallback();
        })
        .Catch(err=> {
            Debug.LogError(err);
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

    public void EDIT_PLAYER_FROM_FIREBASE(String oldName, String newName, Action confirmationCallback)
    {
        GET_FROM_FIREBASE(oldName, (playerData)=> {
            var data = playerData;
            //delete
            RestClient.Delete($"{URL.text}{data.Name}.json")
            .Then(_=> {
                //changing name and URL
                data.Name = newName;
                RestClient.Put($"{URL.text}{data.Name}.json", data)
                .Then(response=> {
                    confirmationCallback();
                })
                .Catch(err=> {
                    Debug.LogError(err);
                });
            });
        });
    }

    public void DELETE_FROM_FIREBASE(String playerName, Action<bool> confirmationCallback)
    {
        RestClient.Delete($"{URL.text}{playerName}.json")
        .Then(response=> {
            confirmationCallback(true);
        })
        .Catch(response=> {
            confirmationCallback(false);
        });
    }
}
