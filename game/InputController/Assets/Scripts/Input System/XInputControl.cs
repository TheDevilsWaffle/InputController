///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInputControl.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEditor;
using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

#region ENUMS
//public enum EnumStatus
//{
//
//};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion


[System.Serializable]
public class InputStatusAction
{
    [SerializeField]
    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being pressed")]
    public InputAction onPressed;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being held")]
    [SerializeField]
    public InputAction onHeld;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being released")]
    [SerializeField]
    public InputAction onReleased;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate when inactive")]
    [SerializeField]
    public InputAction onInactive;
}

public class XInputControl : MonoBehaviour
{
    #region FIELDS
    [Header("GAMEPAD/CONTROLLER")]
    [Header("PLAYER")]
    [Range(1,4)]
    [SerializeField]
    int player = 1;

    [Header("Analog Sticks")]
    [SerializeField]
    InputStatusAction leftStick;
    [SerializeField]
    InputStatusAction l3;
    [SerializeField]
    InputStatusAction rightStick;
    [SerializeField]
    InputStatusAction r3;

    [Header("D-Pad")]
    [SerializeField]
    InputStatusAction up;
    [SerializeField]
    InputStatusAction right;
    [SerializeField]
    InputStatusAction down;
    [SerializeField]
    InputStatusAction left;

    [Header("Buttons")]
    [SerializeField]
    InputStatusAction a;
    [SerializeField]
    InputStatusAction x;
    [SerializeField]
    InputStatusAction y;
    [SerializeField]
    InputStatusAction b;

    [Header("Misc. Buttons")]
    [SerializeField]
    InputStatusAction view;
    [SerializeField]
    InputStatusAction menu;

    [Header("Bumpers")]
    [SerializeField]
    InputStatusAction leftBumper;
    [SerializeField]
    InputStatusAction rightBumper;

    [Header("Triggers")]
    [SerializeField]
    InputStatusAction leftTrigger;
    [SerializeField]
    InputStatusAction rightTrigger;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //initial values
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //correct player value for list index

       SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);
    }
   
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateInputActions(EVENT_INPUT_XINPUT_UPDATE _event)
    {
        Debug.Log("PLAYER("+_event.player+") a = "+ _event.xInputdata.a.Status);
        //check if this is the right player
        if(_event.player == --player)
        {
            //buttons
            #region A BUTTON
            //RELEASED
            if(_event.xInputdata.a.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onReleased != null)
                {
                    print("released");
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onReleased.Activate(_event.xInputdata.a);
                }
            }
            //HELD
            if(_event.xInputdata.a.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onHeld != null)
                {
                    print("held");
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onHeld.Activate(_event.xInputdata.a);
                }
            }
            //PRESSED
            if(_event.xInputdata.a.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onPressed != null)
                {
                    print("pressed");
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onPressed.Activate(_event.xInputdata.a);
                }
            }
            //INACTIVE
            if(_event.xInputdata.a.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onInactive != null)
                {
                    print("inactive");
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onInactive.Activate(_event.xInputdata.a);
                }
            }
            #endregion
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);
    }
    #endregion
}