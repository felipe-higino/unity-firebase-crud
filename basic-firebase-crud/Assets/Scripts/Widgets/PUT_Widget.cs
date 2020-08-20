using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUT_Widget : MonoBehaviour
{
    [SerializeField] Text PlayerNameToEdit;
    [SerializeField] Text NewNameOfPlayer;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }
}
