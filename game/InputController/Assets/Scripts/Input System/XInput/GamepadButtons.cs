﻿﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadButtons.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadButtons : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction a;
    [Space]

    [SerializeField]
    InputStatusAction x;
    [Space]

    [SerializeField]
    InputStatusAction y;
    [Space]

    [SerializeField]
    InputStatusAction b;
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
        //Debug.Log("PLAYER("+_event.player+") a = "+ _event.xInputData.a.Status);

        //check if this is the right player
        if (_event.player == player)
        {
            //buttons
            #region A BUTTON
            //RELEASED
            if (_event.xInputData.a.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (a.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onReleased.Activate(_event.xInputData.a);
                }
            }
            //HELD
            if (_event.xInputData.a.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (a.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onHeld.Activate(_event.xInputData.a);
                }
            }
            //PRESSED
            if (_event.xInputData.a.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (a.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onPressed.Activate(_event.xInputData.a);
                }
            }
            #endregion
            #region B BUTTON
            //RELEASED
            if (_event.xInputData.b.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onReleased.Activate(_event.xInputData.b);
                }
            }
            //HELD
            if (_event.xInputData.b.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onHeld.Activate(_event.xInputData.b);
                }
            }
            //PRESSED
            if (_event.xInputData.b.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onPressed.Activate(_event.xInputData.b);
                }
            }
            #endregion
            #region Y BUTTON
            //RELEASED
            if (_event.xInputData.y.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onReleased.Activate(_event.xInputData.y);
                }
            }
            //HELD
            if (_event.xInputData.y.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onHeld.Activate(_event.xInputData.y);
                }
            }
            //PRESSED
            if (_event.xInputData.y.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onPressed.Activate(_event.xInputData.y);
                }
            }
            #endregion
            #region X BUTTON
            //RELEASED
            if (_event.xInputData.x.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onReleased.Activate(_event.xInputData.x);
                }
            }
            //HELD
            if (_event.xInputData.x.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onHeld.Activate(_event.xInputData.x);
                }
            }
            //PRESSED
            if (_event.xInputData.x.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onPressed.Activate(_event.xInputData.x);
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