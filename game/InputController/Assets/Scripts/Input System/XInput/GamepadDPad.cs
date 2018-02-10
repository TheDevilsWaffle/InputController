﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadDPad.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadDPad : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction dPadUp;
    [Space]

    [SerializeField]
    InputStatusAction dPadRight;
    [Space]

    [SerializeField]
    InputStatusAction dPadDown;
    [Space]

    [SerializeField]
    InputStatusAction dPadLeft;
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
            //buttons
            #region DPAD UP
            //RELEASED
            if (_event.xInputdata.dp_up.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadUp.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadUp.onReleased.Activate(_event.xInputdata.dp_up);
                }
            }
            //HELD
            if (_event.xInputdata.dp_up.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadUp.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadUp.onHeld.Activate(_event.xInputdata.dp_up);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_up.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadUp.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadUp.onPressed.Activate(_event.xInputdata.dp_up);
                }
            }
            #endregion
            #region DPAD RIGHT
            //RELEASED
            if (_event.xInputdata.dp_right.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadRight.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadRight.onReleased.Activate(_event.xInputdata.dp_right);
                }
            }
            //HELD
            if (_event.xInputdata.dp_right.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadRight.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadRight.onHeld.Activate(_event.xInputdata.dp_right);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_right.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadRight.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadRight.onPressed.Activate(_event.xInputdata.dp_right);
                }
            }
            #endregion
            #region DPAD DOWN
            //RELEASED
            if (_event.xInputdata.dp_down.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadDown.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadDown.onReleased.Activate(_event.xInputdata.dp_down);
                }
            }
            //HELD
            if (_event.xInputdata.dp_down.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadDown.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadDown.onHeld.Activate(_event.xInputdata.dp_down);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_down.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadDown.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadDown.onPressed.Activate(_event.xInputdata.dp_down);
                }
            }
            #endregion
            #region DPAD LEFT
            //RELEASED
            if (_event.xInputdata.dp_left.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadLeft.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadLeft.onReleased.Activate(_event.xInputdata.dp_left);
                }
            }
            //HELD
            if (_event.xInputdata.dp_left.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadLeft.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadLeft.onHeld.Activate(_event.xInputdata.dp_left);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_left.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (dPadLeft.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    dPadLeft.onPressed.Activate(_event.xInputdata.dp_left);
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