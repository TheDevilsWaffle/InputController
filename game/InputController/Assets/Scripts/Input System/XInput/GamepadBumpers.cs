﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadBumpers.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadBumpers : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction leftBumper;
    [Space]

    [SerializeField]
    InputStatusAction rightBumper;
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
            #region LEFT BUMPER
            //RELEASED
            if (_event.xInputdata.lb.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftBumper.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onReleased.Activate(_event.xInputdata.lb);
                }
            }
            //HELD
            if (_event.xInputdata.lb.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftBumper.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onHeld.Activate(_event.xInputdata.lb);
                }
            }
            //PRESSED
            if (_event.xInputdata.lb.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftBumper.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onPressed.Activate(_event.xInputdata.lb);
                }
            }
            #endregion
            #region RIGHT BUMPER
            //RELEASED
            if (_event.xInputdata.rb.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightBumper.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onReleased.Activate(_event.xInputdata.rb);
                }
            }
            //HELD
            if (_event.xInputdata.rb.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightBumper.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onHeld.Activate(_event.xInputdata.rb);
                }
            }
            //PRESSED
            if (_event.xInputdata.rb.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightBumper.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onPressed.Activate(_event.xInputdata.rb);
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