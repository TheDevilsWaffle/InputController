﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadAnalogSticks.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadAnalogSticks : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction leftAnalogStick;
    [SerializeField]
    InputStatusAction l3;
    [Space]

    [SerializeField]
    InputStatusAction rightAnalogStick;
    [SerializeField]
    InputStatusAction r3;
    #endregion
    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// initialize references/connections/functions in this script and other scripts
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //correct player value for list index
        --player;
        SetSubscriptions();
    }
    #endregion
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);
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
        if (_event.player == player)
        {
            #region LEFT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.ls.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftAnalogStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftAnalogStick.onReleased.Activate(_event.xInputdata.ls);
                }
            }
            //HELD
            if (_event.xInputdata.ls.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftAnalogStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftAnalogStick.onHeld.Activate(_event.xInputdata.ls);
                }
            }
            //PRESSED
            if (_event.xInputdata.ls.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftAnalogStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftAnalogStick.onPressed.Activate(_event.xInputdata.ls);
                }
            }
            #endregion
            #region L3
            //RELEASED
            if (_event.xInputdata.l3.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onReleased.Activate(_event.xInputdata.l3);
                }
            }
            //HELD
            if (_event.xInputdata.l3.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onHeld.Activate(_event.xInputdata.l3);
                }
            }
            //PRESSED
            if (_event.xInputdata.l3.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onPressed.Activate(_event.xInputdata.l3);
                }
            }
            #endregion
            #region RIGHT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.rs.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightAnalogStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightAnalogStick.onReleased.Activate(_event.xInputdata.rs);
                }
            }
            //HELD
            if (_event.xInputdata.rs.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightAnalogStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightAnalogStick.onHeld.Activate(_event.xInputdata.rs);
                }
            }
            //PRESSED
            if (_event.xInputdata.rs.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightAnalogStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightAnalogStick.onPressed.Activate(_event.xInputdata.rs);
                }
            }
            #endregion
            #region R3
            //RELEASED
            if (_event.xInputdata.r3.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onReleased.Activate(_event.xInputdata.r3);
                }
            }
            //HELD
            if (_event.xInputdata.r3.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onHeld.Activate(_event.xInputdata.r3);
                }
            }
            //PRESSED
            if (_event.xInputdata.r3.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onPressed.Activate(_event.xInputdata.r3);
                }
            }
            #endregion
        }
    }
    #endregion
    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// called when the gameobject with this script is destroyed
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}