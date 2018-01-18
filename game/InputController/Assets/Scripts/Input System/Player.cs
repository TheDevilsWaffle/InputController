﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Player.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

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
public class EVENT_XINPUT_STATUS_UPDATE : GameEvent
{
    public int playerNumber;
    public bool status;
    public EVENT_XINPUT_STATUS_UPDATE(int _playerNumber, bool _status)
    {
        playerNumber = _playerNumber;
        status = _status;
    }
}
public class EVENT_KEYBOARD_STATUS_UPDATE : GameEvent
{
    public int playerNumber;
    public bool status;
    public EVENT_KEYBOARD_STATUS_UPDATE(int _playerNumber, bool _status)
    {
        playerNumber = _playerNumber;
        status = _status;
    }
}
public class EVENT_MOUSE_STATUS_UPDATE : GameEvent
{
    public int playerNumber;
    public bool status;
    public EVENT_MOUSE_STATUS_UPDATE(int _playerNumber, bool _status)
    {
        playerNumber = _playerNumber;
        status = _status;
    }
}
#endregion

public class Player : MonoBehaviour
{
    #region FIELDS
    [Header("Player Number")]
    [SerializeField]
    [Range(1,4)]
    int playerNumber;

    [Header("Gamepad")]
    [SerializeField]
    protected bool enableXInput;
    [SerializeField]
    XInputController xInputControllerScript;

    [Header("Keyboard")]
    [SerializeField]
    protected bool enableKBInput;
    [SerializeField]
    KeyboardInput kbInput;

    // [Header("Mouse")]
    //[SerializeField]
    //protected bool enableMouseInput;
    // [SerializeField]
    // MouseInput mInput
    #endregion

    #region INITIALIZATION
    public virtual void Awake()
    {
        --playerNumber;

        InitializeXInput();
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DisableInput
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EnablePlayerInput()
    {
        //gamepad
        if(xInputControllerScript != null)
        {
            xInputControllerScript.EnableInput();
        }
        //keyboard
        if(kbInput != null)
        {
            //kbInput.EnableInput();
        }
        //mouse
        // if(mouseInput != null)
        // {
        //     mouseInput.EnableInput();
        // }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DisableInput
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisablePlayerInput()
    {
        //gamepad
        if(xInputControllerScript != null)
        {
            xInputControllerScript.DisableInput();
        }
        //keyboard
        if(kbInput != null)
        {
            //kbInput.DisableInput();
        }
        //mouse
        // if(mouseInput != null)
        // {
        //     mouseInput.DisableInput();
        // }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DisableInput
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void InitializePlayer()
    {
        //xinput
        if(xInputControllerScript != null)
        {
            xInputControllerScript.InitializeGamepad(playerNumber);
        }

        //keyboard
        if(kbInput != null)
        {
            //kbInput.InitializeKeyboard();
        }

        // if(mouseInput != null)
        // {
        //     mouseInput.InitializeMouse();
        // }

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DisableInput
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetInputStatus(InputTypes _inputTypes, bool _status)
    {
        switch (_inputTypes)
        {
            case InputTypes.XINPUT:
                enableXInput = _status;
                //raise event
                Events.instance.Raise(new EVENT_XINPUT_STATUS_UPDATE(playerNumber, enableXInput));
                break;
            case InputTypes.KEYBOARD:
                enableKBInput = _status;
                //raise event
                Events.instance.Raise(new EVENT_KEYBOARD_STATUS_UPDATE(playerNumber, enableKBInput));
                break;
            // case InputTypes.MOUSE:
                // enableMouseInput = _status;
                //raise event
                // Events.instance.Raise(new EVENT_MOUSE_STATUS_UPDATE(playerNumber, enableMouseInput));
                // break;
            default:
                Debug.Log("ERROR! Must pass parameter of type 'InputTypes' and/or bool status");
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// GetInputStatus
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetInputStatus(InputTypes _inputTypes)
    {
        switch (_inputTypes)
        {
            case InputTypes.XINPUT:
                return enableXInput;
            case InputTypes.KEYBOARD:
                return enableKBInput;
            // case InputTypes.MOUSE:
            //     return enableMouseInput;
            default:
                Debug.Log("ERROR! Must pass parameter of type 'InputTypes'");
                return false;
        }
    }
    #endregion
    #region PRIVATE METHODS
    void InitializeXInput()
    {
        if(xInputControllerScript != null)
            xInputControllerScript.InitializeGamepad(playerNumber);
    }
    #endregion
}