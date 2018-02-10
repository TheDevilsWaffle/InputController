﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputSystem.cs
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
public class EVENT_INPUT_INITIALIZE_KEYBOARD : GameEvent
{
    public EVENT_INPUT_INITIALIZE_KEYBOARD() { }
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
public class EVENT_INPUT_KEYBOARD_TO_GAMEPAD : GameEvent
{
    public int playerNumber;
    public Dictionary<KeyCode, InputStatus> keys;
    public EVENT_INPUT_KEYBOARD_TO_GAMEPAD(int _playerNumber, Dictionary<KeyCode, InputStatus> _keys)
    {
        playerNumber = _playerNumber;
        keys = _keys;
    }
}
public class EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_LOST : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_LOST(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
public class EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_ACQUIRED : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_ACQUIRED(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
public class EVENT_INPUT_XINPUT_UPDATE : GameEvent
{
    public int player;
    public XInputData xInputdata;
    public EVENT_INPUT_XINPUT_UPDATE(int _playerNumber, XInputData _xInputData)
    {
        player = _playerNumber;
        xInputdata = _xInputData;
    }
}
#endregion

[RequireComponent(typeof(XInput))]

public class InputSystem : MonoBehaviour
{
    #region FIELDS
    [Header("Enable/Disable")]
    [SerializeField]
    bool enableInput = true;
    [Space]

    [Header("Players")]
    [SerializeField]
    [Range(1,4)]
    int numberOfPlayers;
    public static int players = 1;
    [Space]

    [Header("Inputs Supported")]
    [SerializeField]
    bool gamepadSupport;
    [SerializeField]
    bool keyboardSupport;
    public static Queue<InputData> keyboardComboTracker;
    [SerializeField]
    bool mouseSupport;
    [Space]

    [Header("Dead Zone Thresholds")]
    [Range(0, 1)]
    [SerializeField]
    float analogSticksDeadZone = 0.2f;
    public static float analogStickDeadZone;
    [Range(0, 1)]
    [SerializeField]
    float triggersDeadZone = 0.2f;
    public static float triggerDeadZone;
    [Space]

    [Header("Maximum Buttons Remembered")]
    [SerializeField]
    [Range(2, 20)]
    int maxButtonsCombo = 3;
    public static int maxButtonsRemembered;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// initialize references/connections/functions in this script and other scripts
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //set references/values/initialize data structures
        keyboardComboTracker = new Queue<InputData>();
        players = numberOfPlayers;
        analogStickDeadZone = analogSticksDeadZone;
        triggerDeadZone = triggersDeadZone;
        maxButtonsRemembered = maxButtonsCombo;
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
        Events.instance.AddListener<EVENT_INPUT_KEYBOARD_KEY_BROADCAST>(UpdateKeyboardCombo);
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
        Events.instance.RemoveListener<EVENT_INPUT_KEYBOARD_KEY_BROADCAST>(UpdateKeyboardCombo);
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
            Events.instance.Raise(new EVENT_INPUT_INITIALIZE_KEYBOARD());
        }
        if(mouseSupport)
        {
            //function needed here
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// tracks the last 'x-number' of buttons pressed from the keyboard
    /// <param name = "_event" >InputData of the key that was pressed</param>
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateKeyboardCombo(EVENT_INPUT_KEYBOARD_KEY_BROADCAST _event)
    {
        //add in the latest button pressed
        keyboardComboTracker.Enqueue(_event.keyInputData);

        //dequeue the oldest button if we're at max rememberence
        if (keyboardComboTracker.Count > maxButtonsRemembered)
        {
            keyboardComboTracker.Dequeue();
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