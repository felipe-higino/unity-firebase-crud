using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestTypeSelection : Singleton<RequestTypeSelection>
{
    void Awake() => AwakeSingleton(this);

    private enum RequestType { SDK, REST }
    [SerializeField]
    [Header("Request method")]
    RequestType requestType;
}
