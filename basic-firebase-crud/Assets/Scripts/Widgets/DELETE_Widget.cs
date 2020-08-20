using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DELETE_Widget : MonoBehaviour
{
    [SerializeField] Text PlayerNameToDelete;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }
}
