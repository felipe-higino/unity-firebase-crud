using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DELETE_Widget_SDK : MonoBehaviour
{
    [SerializeField] InputField PlayerNameToDelete;
    [SerializeField] Text TextConfirmation;

    private void Awake()
    {
        TextConfirmation.text = "";
    }

    public void OnSubmitDelete()
    {
        //TODO
    }
}
