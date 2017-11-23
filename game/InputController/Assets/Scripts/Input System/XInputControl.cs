﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInputControl.cs
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
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class XInputControl : MonoBehaviour
{
    #region FIELDS
    [Header("Player")]
    [Range(1, 4)]
    [SerializeField]
    int player = 0;

    [Header("Input Type")]
    [SerializeField]
    XBoxButtons control = XBoxButtons.A;

    [Header("Pressed")]
    [SerializeField]
    InputActionBase onPressed;

    [Header("Held")]
    [SerializeField]
    InputActionBase onHeld;
    [SerializeField]
    float heldTimeThreshold = 0f;
    float onHeldTimer = 0f;
    [SerializeField]
    bool repeatHeldAction = false;

    [Header("Released")]
    [SerializeField]
    InputActionBase onReleased;

    [Header("Inactive")]
    [SerializeField]
    InputActionBase onInactive;

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
        onHeldTimer = ResetTimer();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
    
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        --player;
        switch (player)
        {
            case 0:
                Events.instance.AddListener<EVENT_XINPUT_P1>(P1);
                break;
            // case 1:
            //     Events.instance.AddListener<EVENT_XINPUT_P2>(P2);
            //     break;
            // case 2:
            //     Events.instance.AddListener<EVENT_XINPUT_P3>(P3);
            //     break;
            // case 3:
            //     Events.instance.AddListener<EVENT_XINPUT_P4>(P4);
            //     break;
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

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void P1(EVENT_XINPUT_P1 _event)
    {
        switch (control)
        {
            //BUTTONS

            //A
            case XBoxButtons.A:
            //pressed
            if(_event.gamepad.a.Status == InputStatus.PRESSED)
            {
                onPressed.Activate();
            }
            //held
            if(_event.gamepad.a.Status == InputStatus.HELD)
            {
                onHeld.Activate();
            }
            //released
            if(_event.gamepad.a.Status == InputStatus.RELEASED)
            {
                onReleased.Activate();
            }
            //inactive
            if(_event.gamepad.a.Status == InputStatus.INACTIVE)
            {
                onInactive.Activate();
            }
            break;
            //B
            case XBoxButtons.B:
            //pressed
            if(_event.gamepad.b.Status == InputStatus.PRESSED)
            {
                onPressed.Activate();
            }
            //held
            if(_event.gamepad.b.Status == InputStatus.HELD)
            {
                onHeld.Activate();
            }
            //released
            if(_event.gamepad.b.Status == InputStatus.RELEASED)
            {
                onReleased.Activate();
            }
            //inactive
            if(_event.gamepad.b.Status == InputStatus.INACTIVE)
            {
                onInactive.Activate();
            }
            break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    float ResetTimer()
    {
        return 0;
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        switch (player)
        {
            case 0:
                Events.instance.RemoveListener<EVENT_XINPUT_P1>(P1);
                break;
            // case 1:
            //     Events.instance.RemoveListener(EVENT_XINPUT_P2);
            //     break;
            // case 2:
            //     Events.instance.RemoveListener(EVENT_XINPUT_P3);
            //     break;
            // case 3:
            //     Events.instance.RemoveListener(EVENT_XINPUT_P4);
            //     break;
        }
    }
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