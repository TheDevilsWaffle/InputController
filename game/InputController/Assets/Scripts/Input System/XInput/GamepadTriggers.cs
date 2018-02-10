﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadTriggers.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadTriggers : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction leftTrigger;
    [Space]

    [SerializeField]
    InputStatusAction rightTrigger;
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
            #region LEFT TRIGGER
            //RELEASED
            if (_event.xInputdata.lt.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftTrigger.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onReleased.Activate(_event.xInputdata.lt);
                }
            }
            //HELD
            if (_event.xInputdata.lt.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftTrigger.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onHeld.Activate(_event.xInputdata.lt);
                }
            }
            //PRESSED
            if (_event.xInputdata.lt.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftTrigger.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onPressed.Activate(_event.xInputdata.lt);
                }
            }
            #endregion
            #region RIGHT TRIGGER
            //RELEASED
            if (_event.xInputdata.rt.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightTrigger.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onReleased.Activate(_event.xInputdata.rt);
                }
            }
            //HELD
            if (_event.xInputdata.rt.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightTrigger.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onHeld.Activate(_event.xInputdata.rt);
                }
            }
            //PRESSED
            if (_event.xInputdata.rt.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightTrigger.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onPressed.Activate(_event.xInputdata.rt);
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