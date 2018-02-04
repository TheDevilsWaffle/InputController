﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputController.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region EVENTS
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
public class EVENT_INPUT_ENABLE_ALL : GameEvent
{
    public EVENT_INPUT_ENABLE_ALL() { }
}
public class EVENT_INPUT_DISABLE_PLAYER : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_DISABLE_PLAYER(int _playerNumber) 
    {
        playerNumber = _playerNumber;
    }
}
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
    /// initialize references/connections/functions in this script and other scripts
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        players = numberOfPlayers;
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// called on first frame when script is enabled before Update()
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
    #endregion
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// listen to specific GameEvents
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
    /// end listening to specific GameEvents
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_ALL>(EnableAllPlayers);        
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_ALL>(DisableAllPlayers);
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_PLAYER>(EnablePlayerInput);
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_PLAYER>(DisablePlayerInput);
    }
    #endregion
    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// enable input for all players
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EnableAllPlayers(EVENT_INPUT_ENABLE_ALL _event)
    {
        enableInput = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// disable input for all players
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisableAllPlayers(EVENT_INPUT_DISABLE_ALL _event)
    {
        enableInput = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// enable input for a specific player
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void EnablePlayerInput(EVENT_INPUT_ENABLE_PLAYER _event)
    {
        //function needed here
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// disable input for a specific player
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DisablePlayerInput(EVENT_INPUT_DISABLE_PLAYER _event)
    {
        //function needed here
    }

    #endregion
    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// start input for enabled inputs
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
        if(keyboardSupport)
        {
            //function needed here
        }
        if(mouseSupport)
        {
            //function needed here
        }
    }
    #endregion
    #region ON DESTORY
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