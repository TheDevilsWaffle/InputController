///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Template_InputAction.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

class Example_UI_Update : MonoBehaviour
{
    [Header("CURRENT BUTTON")]
	[SerializeField]
	Image icon;
    [SerializeField]
    Text text;

    [Header("DETAILS")]
    [SerializeField]
    Text heldDuration;
    [SerializeField]
    Text status;
    [SerializeField]
    Text value;


    InputData currentInput;
    void Awake()
    {
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateCurrentInput);
    }

    void UpdateCurrentInput(EVENT_INPUT_XINPUT_UPDATE _event)
    {
        if(_event.xInputData.comboTracker.Count > 0)
        {
            currentInput = _event.xInputData.comboTracker.Last();
            UpdateInformation(currentInput.ID, currentInput.IconXBox);
            UpdateDetails();
        }
    }

    void UpdateInformation(string _id, Sprite _icon)
    {
        text.text = _id;
        icon.sprite = _icon;
    }

    void UpdateDetails()
    {
        heldDuration.text = Truncate(currentInput.HeldDuration.ToString(), 5);
        status.text = currentInput.Status.ToString();
        value.text = "( "+ Truncate(currentInput.XYValues.x.ToString(), 5) + "," + Truncate(currentInput.XYValues.y.ToString(), 5) + ")";
    }

    string Truncate(string value, int maxChars)
    {
        return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
    }
}