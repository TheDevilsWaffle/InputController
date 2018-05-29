﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — GamepadMiscButtons.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GamepadMiscButtons : MonoBehaviour
{
    #region FIELDS
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]

    [SerializeField]
    InputStatusAction view;
    [Space]

    [SerializeField]
    InputStatusAction menu;
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
            #region VIEW
            //RELEASED
            if (_event.xInputData.view.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onReleased.Activate(_event.xInputData.view);
                }
            }
            //HELD
            if (_event.xInputData.view.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onHeld.Activate(_event.xInputData.view);
                }
            }
            //PRESSED
            if (_event.xInputData.view.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onPressed.Activate(_event.xInputData.view);
                }
            }
            #endregion
            #region MENU
            //RELEASED
            if (_event.xInputData.menu.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onReleased.Activate(_event.xInputData.menu);
                }
            }
            //HELD
            if (_event.xInputData.menu.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onHeld.Activate(_event.xInputData.menu);
                }
            }
            //PRESSED
            if (_event.xInputData.menu.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onPressed.Activate(_event.xInputData.menu);
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