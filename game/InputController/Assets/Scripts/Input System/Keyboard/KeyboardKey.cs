﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardKey.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class KeyboardKey : MonoBehaviour
{
    #region FIELDS
    [Header("Single Keyboard Key")]
    [Space]
    
    [Header("Key")]
    public KeyCode key = KeyCode.Q;
    InputData keyInputData;
    [SerializeField]
    InputStatusAction keyInputStatusAction;

    bool enableKeyboard = false;
    #endregion
    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// initialize references/connections/functions in this script and other scripts
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        SetSubscriptions();
        SetAssignedKey();
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
        Events.instance.AddListener<EVENT_INPUT_DISABLE_ALL>(DisableKeyboardInput);
        Events.instance.AddListener<EVENT_INPUT_ENABLE_ALL>(EnableKeyboardInput);
        Events.instance.AddListener<EVENT_INPUT_INITIALIZE_KEYBOARD>(EnableKeyboardInput);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_ALL>(DisableKeyboardInput);
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_ALL>(EnableKeyboardInput);
        Events.instance.RemoveListener<EVENT_INPUT_INITIALIZE_KEYBOARD>(EnableKeyboardInput);
    }
    #endregion
    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// runs every frame
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (enableKeyboard)
        {
            UpdateKeyInput();
        }
    }
    #endregion
    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function desc.
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetAssignedKey()
    {
        if (key != KeyCode.None)
        {
            //set input data
            keyInputData = new InputData();
            keyInputData.SetName(key.ToString());
            keyInputData.SetKey(key);
            keyInputData.SetStatus(InputStatus.INACTIVE);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function desc.
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateKeyInput()
    {
        #region RELEASED
        if (Input.GetKeyUp(key))
        {
            //update data
            keyInputData.SetStatus(InputStatus.RELEASED);
            keyInputData.SetXYRawValue(Vector2.zero);
            keyInputData.AddXYValue(Vector2.zero);
            keyInputData.SetHeldDuration(0f);
            keyInputData.SetInactiveDuration(Time.deltaTime);

            //perform input action if there is one assigned
            if (keyInputStatusAction.onReleased != null)
            {
                keyInputStatusAction.onReleased.Activate(keyInputData);
            }

            //broadcast a message
            Events.instance.Raise(new EVENT_INPUT_KEYBOARD_KEY_BROADCAST(keyInputData));

            //DEBUG — check which key is released and print its status
            //Debug.Log("key released: " + key + " status = " + keyInputData.Status);
        }
        #endregion
        #region HELD
        if (Input.GetKey(key))
        {
            //update data
            keyInputData.SetStatus(InputStatus.HELD);
            keyInputData.SetXYRawValue(Vector2.one);
            keyInputData.AddXYValue(Vector2.one);
            keyInputData.SetHeldDuration(Time.deltaTime);
            keyInputData.SetInactiveDuration(0);

            //perform input action if there is one assigned
            if (keyInputStatusAction.onHeld != null)
            {
                keyInputStatusAction.onHeld.Activate(keyInputData);
            }

            //broadcast a message
            Events.instance.Raise(new EVENT_INPUT_KEYBOARD_KEY_BROADCAST(keyInputData));

            //DEBUG — check which key is held and print its status
            //Debug.Log("key held: " + key + " status = " + keyInputData.Status);
        }
        #endregion
        #region PRESSED
        if (Input.GetKeyDown(key))
        {
            //update data
            keyInputData.SetStatus(InputStatus.RELEASED);
            keyInputData.SetXYRawValue(Vector2.one);
            keyInputData.AddXYValue(Vector2.one);
            keyInputData.SetHeldDuration(Time.deltaTime);
            keyInputData.SetInactiveDuration(0f);

            //perform input action if there is one assigned
            if (keyInputStatusAction.onPressed != null)
            {
                keyInputStatusAction.onPressed.Activate(keyInputData);
            }

            //broadcast a message
            Events.instance.Raise(new EVENT_INPUT_KEYBOARD_KEY_BROADCAST(keyInputData));

            //DEBUG — check which key is pressed and print its status
            //Debug.Log("key pressed: " + key + " status = " + keyInputData.Status);
        }
        #endregion
        #region INACTIVE
        //inactive
        else
        {
            //update data
            keyInputData.SetStatus(InputStatus.INACTIVE);
            keyInputData.SetXYRawValue(Vector2.zero);
            keyInputData.AddXYValue(Vector2.zero);
            keyInputData.SetHeldDuration(0f);
            keyInputData.SetInactiveDuration(Time.deltaTime);

            //DEBUG — check which key is inactive and print its status
            //Debug.Log("key inactive: " + key + " status = " + keyInputData.Status);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void EnableKeyboardInput(EVENT_INPUT_INITIALIZE_KEYBOARD _event)
    {
        enableKeyboard = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void EnableKeyboardInput(EVENT_INPUT_ENABLE_ALL _event)
    {
        enableKeyboard = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void DisableKeyboardInput(EVENT_INPUT_DISABLE_ALL _event)
    {
        enableKeyboard = false;
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