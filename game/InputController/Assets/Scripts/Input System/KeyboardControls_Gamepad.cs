///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardControls_Gamepad.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardControls_Gamepad : MonoBehaviour
{
    #region FIELDS
    [Header("Keyboard Mapped Like A Gamepad")]
    [Header("Player Number")]
    [Range(1, 4)]
    [SerializeField]
    int player = 1;
    [Space]
    [Header("Analog Sticks")]
    public KeyCode up = KeyCode.W;
    [SerializeField]
    InputStatusAction upAction;
    [Space]
    public KeyCode right = KeyCode.D;
    [SerializeField]
    InputStatusAction rightAction;
    [Space]
    public KeyCode down = KeyCode.S;
    [SerializeField]
    InputStatusAction downAction;
    [Space]
    public KeyCode left = KeyCode.A;
    [SerializeField]
    InputStatusAction leftAction;

    [Header("DPad")]
    public KeyCode DPadUp = KeyCode.UpArrow;
    [SerializeField]
    InputStatusAction DPadUpAction;
    [Space]
    public KeyCode DPadRight = KeyCode.RightArrow;
    [SerializeField]
    InputStatusAction DPadRightAction;
    [Space]
    public KeyCode DPadDown = KeyCode.DownArrow;
    [SerializeField]
    InputStatusAction DPadDownAction;
    [Space]
    public KeyCode DPadLeft = KeyCode.LeftArrow;
    [SerializeField]
    InputStatusAction DPadLeftAction;
    [Space]

    [Header("Buttons")]
    public KeyCode y = KeyCode.LeftControl;
    [SerializeField]
    InputStatusAction yAction;
    [Space]
    public KeyCode b = KeyCode.Q;
    [SerializeField]
    InputStatusAction bAction;
    [Space]
    public KeyCode a = KeyCode.Space;
    [SerializeField]
    InputStatusAction aAction;
    [Space]
    public KeyCode x = KeyCode.E;
    [SerializeField]
    InputStatusAction xAction;
    [Space]

    [Header("Misc Buttons")]
    public KeyCode select = KeyCode.Tab;
    [SerializeField]
    InputStatusAction selectAction;
    [Space]
    public KeyCode start = KeyCode.Return;
    [SerializeField]
    InputStatusAction startAction;
    [Space]
    public KeyCode l3 = KeyCode.O;
    [SerializeField]
    InputStatusAction l3Action;
    [Space]
    public KeyCode r3 = KeyCode.P;
    [SerializeField]
    InputStatusAction r3Action;
    [Space]

    [Header("Bumpers")]
    public KeyCode lb = KeyCode.LeftAlt;
    [SerializeField]
    InputStatusAction lbAction;
    [Space]
    public KeyCode rb = KeyCode.LeftShift;
    [SerializeField]
    InputStatusAction rbAction;
    [Space]

    [Header("Triggers")]
    public KeyCode lt = KeyCode.RightAlt;
    [SerializeField]
    InputStatusAction ltAction;
    [Space]
    public KeyCode rt = KeyCode.RightShift;
    [SerializeField]
    InputStatusAction rtAction;
    [Space]

    bool enableKeyboard = false;
    Dictionary<KeyCode, InputStatus> keys;
    List<KeyCode> keysList;
    List<InputStatusAction> inputStatusActions;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //create keys dictionary and list (list needed to traverse dictionary and update dictionary value(cannot update value by reference))
        keys = new Dictionary<KeyCode, InputStatus>();
        inputStatusActions = new List<InputStatusAction>();

        //check for assigned keys and store them in keys
        AddAssignedKeys();

        SetSubscriptions();
    }
    #endregion
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_DISABLE_ALL>(DisableKeyboardInput);
        Events.instance.AddListener<EVENT_INPUT_DISABLE_PLAYER>(DisableKeyboardInput);
        Events.instance.AddListener<EVENT_INPUT_ENABLE_ALL>(EnableKeyboardInput);
        Events.instance.AddListener<EVENT_INPUT_ENABLE_PLAYER>(EnableKeyboardInput);
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
        Events.instance.RemoveListener<EVENT_INPUT_DISABLE_PLAYER>(DisableKeyboardInput);
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_ALL>(EnableKeyboardInput);
        Events.instance.RemoveListener<EVENT_INPUT_ENABLE_PLAYER>(EnableKeyboardInput);
        Events.instance.RemoveListener<EVENT_INPUT_INITIALIZE_KEYBOARD>(EnableKeyboardInput);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(enableKeyboard)
        {
            //go through the keyboard input update loop
            UpdateKeyboardInput();
            
            //DEBUG
            //print out to the console the contents of keys
            //foreach(KeyValuePair<KeyCode, InputStatus> key in keys)
            //{
            //    Debug.Log("key = " + key.Key + ", and its status is = " + key.Value);
            //}
        }
    }
    #endregion

    #region PRIVATE METHODS
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
    void EnableKeyboardInput(EVENT_INPUT_ENABLE_PLAYER _event)
    {
        if(player == _event.playerNumber)
        {
            enableKeyboard = true;
        }
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void DisableKeyboardInput(EVENT_INPUT_DISABLE_PLAYER _event)
    {
        if(player == _event.playerNumber)
        {
            enableKeyboard = false;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Adds only the assigned buttons to the keys list
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void AddAssignedKeys()
   {
        //checking analog stick keys
        if (up != KeyCode.None)
        {
           keys.Add(up, InputStatus.INACTIVE);
           inputStatusActions.Add(upAction);
        }
        if (right != KeyCode.None)
        {
           keys.Add(right, InputStatus.INACTIVE);
           inputStatusActions.Add(rightAction);
        }
        if (down != KeyCode.None)
        {
           keys.Add(down, InputStatus.INACTIVE);
           inputStatusActions.Add(downAction);
        }
        if (left != KeyCode.None)
        {
           keys.Add(left, InputStatus.INACTIVE);
           inputStatusActions.Add(leftAction);
        }

        //add dpad keys
        if  (DPadUp != KeyCode.None)
        {
            keys.Add(DPadUp, InputStatus.INACTIVE);
            inputStatusActions.Add(DPadUpAction);
        }
        if (DPadRight != KeyCode.None)
        {
            keys.Add(DPadRight, InputStatus.INACTIVE);
            inputStatusActions.Add(DPadRightAction);
        }
        if (DPadDown != KeyCode.None)
        {
            keys.Add(DPadDown, InputStatus.INACTIVE);
            inputStatusActions.Add(DPadDownAction);
        }
        if (DPadLeft != KeyCode.None)
        {
            keys.Add(DPadLeft, InputStatus.INACTIVE);
            inputStatusActions.Add(DPadLeftAction);
        }

        //checking button/keys
        if (y != KeyCode.None)
        {
            keys.Add(y, InputStatus.INACTIVE);
            inputStatusActions.Add(yAction);
        }
        if (b != KeyCode.None)
        {
            keys.Add(b, InputStatus.INACTIVE);
            inputStatusActions.Add(bAction);
        }
        if (a != KeyCode.None)
        {
            keys.Add(a, InputStatus.INACTIVE);
            inputStatusActions.Add(aAction);
        }
        if (x != KeyCode.None)
        {
            keys.Add(x, InputStatus.INACTIVE);
            inputStatusActions.Add(xAction);
        }

        //checking shoulder button/keys
        if (lb != KeyCode.None)
        {
            keys.Add(lb, InputStatus.INACTIVE);
            inputStatusActions.Add(lbAction);
        }
        if (rb != KeyCode.None)
        {
            keys.Add(rb, InputStatus.INACTIVE);
            inputStatusActions.Add(rbAction);
        }
        if (lt != KeyCode.None)
        {
            keys.Add(lt, InputStatus.INACTIVE);
            inputStatusActions.Add(ltAction);
        }
        if (rt != KeyCode.None)
        {
            keys.Add(rt, InputStatus.INACTIVE);
            inputStatusActions.Add(rtAction);
        }

        //checking analog button/keys
        if (l3 != KeyCode.None)
        {
            keys.Add(l3, InputStatus.INACTIVE);
            inputStatusActions.Add(l3Action);
        }
        if (r3 != KeyCode.None)
        {
            keys.Add(r3, InputStatus.INACTIVE);
            inputStatusActions.Add(r3Action);
        }

        //checking misc button/keys
        if (select != KeyCode.None)
        {
            keys.Add(select, InputStatus.INACTIVE);
            inputStatusActions.Add(selectAction);
        }
        if (start != KeyCode.None)
        {
            keys.Add(start, InputStatus.INACTIVE);
            inputStatusActions.Add(startAction);
        }

        //add only the keys needed to the list
        keysList = new List<KeyCode>(keys.Keys);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// cycles through keys and updates their status
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateKeyboardInput()
    {
        foreach (KeyCode key in keysList)
        {
            //released
            if (Input.GetKeyUp(key) && keys[key] == InputStatus.HELD)
            {
                keys[key] = InputStatus.RELEASED;
                
                //DEBUG — check which key is released and print its status
                //print("key released: " + key + " status = " + keys[key]);
            }
            //held
            else if (Input.GetKey(key) && keys[key] == InputStatus.PRESSED || keys[key] == InputStatus.HELD)
            {
                keys[key] = InputStatus.HELD;

                //DEBUG — check which key is held and print its status
                //print("key held: " + key + " status = " + keys[key]);
            }
            //pressed
            else if (Input.GetKeyDown(key) && keys[key] == InputStatus.INACTIVE)
            {
                keys[key] = InputStatus.PRESSED;

                //DEBUG — check which key is pressed and print its status
                //print("key pressed: " + key + " status = " + keys[key]);
            }
            //inactive
            else
            {
                keys[key] = InputStatus.INACTIVE;

                //DEBUG — check which key is inactive and print its status
                //print("key inactive: " + key + " status = " + keys[key]);
            }
        }
    }
    #endregion
    #region ONDESTROY
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}