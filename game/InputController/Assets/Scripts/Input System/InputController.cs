﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputController.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#region ENUMS

#endregion

#region EVENTS
//disable all input
public class EVENT_INPUT_INITIALIZE_XINPUT : GameEvent
{
    public int players;
    public EVENT_INPUT_INITIALIZE_XINPUT(int _players)
    {
        players = _players;
    }
}
public class EVENT_INPUT_DISABLE_ALL : GameEvent
{
    public EVENT_INPUT_DISABLE_ALL() { }
}
//enable all input
public class EVENT_INPUT_ENABLE_ALL : GameEvent
{
    public EVENT_INPUT_ENABLE_ALL() { }
}
//disable specific player input
public class EVENT_INPUT_DISABLE_PLAYER : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_DISABLE_PLAYER(int _playerNumber) 
    {
        playerNumber = _playerNumber;
    }
}
//enable specific player input
public class EVENT_INPUT_ENABLE_PLAYER : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_ENABLE_PLAYER(int _playerNumber) 
    {
        playerNumber = _playerNumber;
    }
}
#endregion

[RequireComponent(typeof(XInput))]

public class InputController : MonoBehaviour
{
    #region FIELDS

    [Header("Enable/Disable")]
    [SerializeField]
    bool enableInput = true;
    [Header("Players")]
    [SerializeField]
    [Range(1,4)]
    int numberOfPlayers;
    public static int players = 1;
    [SerializeField]
    bool gamepadSupport;
    [SerializeField]
    bool keyboardSupport;
    [SerializeField]
    bool mouseSupport;

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //refs


        //initial values

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        players = numberOfPlayers;
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if(enableInput)
        {
            Events.instance.Raise(new EVENT_INPUT_ENABLE_ALL());
            InitializeSupportedInputs();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_ENABLE_ALL>(EnableAllPlayers);        
        Events.instance.AddListener<EVENT_INPUT_DISABLE_ALL>(DisableAllPlayers);
        Events.instance.AddListener<EVENT_INPUT_ENABLE_PLAYER>(EnablePlayerInput);
        Events.instance.AddListener<EVENT_INPUT_DISABLE_PLAYER>(DisablePlayerInput);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_ALL>(EnableAllPlayers);        
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_ALL>(DisableAllPlayers);
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_PLAYER>(EnablePlayerInput);
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_PLAYER>(DisablePlayerInput);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// InitializeSupportedInputs
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeSupportedInputs()
    {
        if(gamepadSupport)
        {
            //DEBUG
            //Debug.Log("InitializeSupportedInputs() xinput for "+ players +" number of players");
            
            Events.instance.Raise(new EVENT_INPUT_INITIALIZE_XINPUT(players));
        }
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {

    #if false
        UpdateTesting();
    #endif
    
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EnableAllPlayers(EVENT_INPUT_ENABLE_ALL _event)
    {
        enableInput = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisableAllPlayers(EVENT_INPUT_DISABLE_ALL _event)
    {
        enableInput = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EnablePlayerInput(EVENT_INPUT_ENABLE_PLAYER _event)
    {
        //players[_event.playerNumber].EnablePlayerInput();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisablePlayerInput(EVENT_INPUT_DISABLE_PLAYER _event)
    {
        //players[_event.playerNumber].DisablePlayerInput();
    }

    #endregion

    #region PRIVATE METHODS
    
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}