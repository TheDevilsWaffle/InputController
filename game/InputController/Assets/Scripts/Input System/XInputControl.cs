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
        --player;
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
        //DEBUG
        //Debug.Log("PLAYER("+_event.player+") a = "+ _event.xInputdata.a.Status);

        //check if this is the right player
        if(_event.player == player)
        {
            //buttons
            #region A BUTTON
            //RELEASED
            if(_event.xInputdata.a.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onReleased != null)
                {
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
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onInactive.Activate(_event.xInputdata.a);
                }
            }
            #endregion
            #region B BUTTON
            //RELEASED
            if (_event.xInputdata.b.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onReleased.Activate(_event.xInputdata.b);
                }
            }
            //HELD
            if (_event.xInputdata.b.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onHeld.Activate(_event.xInputdata.b);
                }
            }
            //PRESSED
            if (_event.xInputdata.b.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onPressed.Activate(_event.xInputdata.b);
                }
            }
            //INACTIVE
            if (_event.xInputdata.b.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onInactive.Activate(_event.xInputdata.b);
                }
            }
            #endregion
            #region Y BUTTON
            //RELEASED
            if (_event.xInputdata.y.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onReleased.Activate(_event.xInputdata.y);
                }
            }
            //HELD
            if (_event.xInputdata.y.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onHeld.Activate(_event.xInputdata.y);
                }
            }
            //PRESSED
            if (_event.xInputdata.y.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onPressed.Activate(_event.xInputdata.y);
                }
            }
            //INACTIVE
            if (_event.xInputdata.y.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onInactive.Activate(_event.xInputdata.y);
                }
            }
            #endregion
            #region X BUTTON
            //RELEASED
            if (_event.xInputdata.x.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onReleased.Activate(_event.xInputdata.x);
                }
            }
            //HELD
            if (_event.xInputdata.x.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onHeld.Activate(_event.xInputdata.x);
                }
            }
            //PRESSED
            if (_event.xInputdata.x.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onPressed.Activate(_event.xInputdata.x);
                }
            }
            //INACTIVE
            if (_event.xInputdata.x.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onInactive.Activate(_event.xInputdata.x);
                }
            }
            #endregion

            //analog sticks
            #region LEFT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.ls.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onReleased.Activate(_event.xInputdata.ls);
                }
            }
            //HELD
            if (_event.xInputdata.ls.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onHeld.Activate(_event.xInputdata.ls);
                }
            }
            //PRESSED
            if (_event.xInputdata.ls.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onPressed.Activate(_event.xInputdata.ls);
                }
            }
            //INACTIVE
            if (_event.xInputdata.ls.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onInactive.Activate(_event.xInputdata.ls);
                }
            }
            #endregion
            #region RIGHT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.rs.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onReleased.Activate(_event.xInputdata.rs);
                }
            }
            //HELD
            if (_event.xInputdata.rs.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onHeld.Activate(_event.xInputdata.rs);
                }
            }
            //PRESSED
            if (_event.xInputdata.rs.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onPressed.Activate(_event.xInputdata.rs);
                }
            }
            //INACTIVE
            if (_event.xInputdata.rs.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onInactive.Activate(_event.xInputdata.rs);
                }
            }
            #endregion

            //dpads
            #region DPAD UP
            //RELEASED
            if (_event.xInputdata.dp_up.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onReleased.Activate(_event.xInputdata.dp_up);
                }
            }
            //HELD
            if (_event.xInputdata.dp_up.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onHeld.Activate(_event.xInputdata.dp_up);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_up.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onPressed.Activate(_event.xInputdata.dp_up);
                }
            }
            //INACTIVE
            if (_event.xInputdata.dp_up.Status == InputStatus.INACTIVE)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onInactive != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onInactive.Activate(_event.xInputdata.dp_up);
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