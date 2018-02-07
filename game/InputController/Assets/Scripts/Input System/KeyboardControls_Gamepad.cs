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
    [Header("Left Analog Stick")]
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

    [Header("Right Analog Stick")]
    public KeyCode upAlt = KeyCode.I;
    [SerializeField]
    InputStatusAction upAltAction;
    [Space]
    public KeyCode rightAlt = KeyCode.L;
    [SerializeField]
    InputStatusAction rightAltAction;
    [Space]
    public KeyCode downAlt = KeyCode.K;
    [SerializeField]
    InputStatusAction downAltAction;
    [Space]
    public KeyCode leftAlt = KeyCode.J;
    [SerializeField]
    InputStatusAction leftAltAction;

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
    public KeyCode view = KeyCode.Tab;
    [SerializeField]
    InputStatusAction viewAction;
    [Space]
    public KeyCode menu = KeyCode.Return;
    [SerializeField]
    InputStatusAction menuAction;
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
    List<InputData> keys;
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
        keys = new List<InputData>();
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
    /// disable all keyboard input
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
    /// Adds only the assigned buttons from Unity Inspector to keys
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void AddAssignedKeys()
   {
        #region LEFT ANALOG STICK
        if (up != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("up");
            _inputData.SetKey(up);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(upAction);
        }
        if (right != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("right");
            _inputData.SetKey(right);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(rightAction);
        }
        if (down != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("down");
            _inputData.SetKey(down);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(downAction);
        }
        if (left != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("left");
            _inputData.SetKey(left);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(leftAction);
        }
        #endregion
        #region RIGHT ANALOG STICK
        if (upAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("upAlt");
            _inputData.SetKey(upAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(upAltAction);
        }
        if (rightAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rightAlt");
            _inputData.SetKey(rightAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(rightAltAction);
        }
        if (downAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("downAlt");
            _inputData.SetKey(downAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(downAltAction);
        }
        if (leftAlt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("leftAlt");
            _inputData.SetKey(leftAlt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(leftAction);
        }
        #endregion
        #region DPAD
        if (DPadUp != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_up");
            _inputData.SetKey(DPadUp);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(DPadUpAction);
        }
        if (DPadRight != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_right");
            _inputData.SetKey(DPadRight);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(DPadRightAction);
        }
        if (DPadDown != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_down");
            _inputData.SetKey(DPadDown);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(DPadDownAction);
        }
        if (DPadLeft != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("dpad_left");
            _inputData.SetKey(DPadLeft);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(DPadLeftAction);
        }
        #endregion
        #region BUTTONS
        if (y != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("y");
            _inputData.SetKey(y);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(yAction);
        }
        if (b != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("b");
            _inputData.SetKey(b);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(bAction);
        }
        if (a != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("a");
            _inputData.SetKey(a);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(aAction);
        }
        if (x != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("x");
            _inputData.SetKey(x);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(xAction);
        }
        #endregion
        #region BUMPERS
        if (lb != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("lb");
            _inputData.SetKey(lb);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(lbAction);
        }
        if (rb != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rb");
            _inputData.SetKey(rb);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(rbAction);
        }
        #endregion
        #region TRIGGERS
        if (lt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("lt");
            _inputData.SetKey(lt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(ltAction);
        }
        if (rt != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("rt");
            _inputData.SetKey(rt);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(rtAction);
        }
        #endregion
        #region MISC. BUTTONS
        if (l3 != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("l3");
            _inputData.SetKey(l3);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(l3Action);
        }
        if (r3 != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("r3");
            _inputData.SetKey(r3);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(r3Action);
        }
        if (view != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("view");
            _inputData.SetKey(view);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(viewAction);
        }
        if (menu != KeyCode.None)
        {
            //set input data
            InputData _inputData = new InputData();
            _inputData.SetName("menu");
            _inputData.SetKey(menu);
            _inputData.SetStatus(InputStatus.INACTIVE);
            keys.Add(_inputData);
            //add actions
            inputStatusActions.Add(menuAction);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// cycles through each key in keys and updates their InputData
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateKeyboardInput()
    {
        //loop through each InputData in keys
        for(int _index = 0; _index < keys.Count; ++_index)
        {
            #region RELEASED
            if (Input.GetKeyUp(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.RELEASED);
                keys[_index].SetXYRawValue(Vector2.zero);
                keys[_index].AddXYValue(Vector2.zero);
                keys[_index].SetHeldDuration(0f);
                keys[_index].SetInactiveDuration(Time.deltaTime);

                //perform input action if there is one assigned
                if(inputStatusActions[_index].onReleased != null)
                {
                    inputStatusActions[_index].onReleased.Activate(keys[_index]);
                }

                //DEBUG — check which key is released and print its status
                //Debug.Log("key released: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region HELD
            if (Input.GetKey(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.HELD);
                keys[_index].SetXYRawValue(Vector2.one);
                keys[_index].AddXYValue(Vector2.one);
                keys[_index].SetHeldDuration(Time.deltaTime);
                keys[_index].SetInactiveDuration(0);

                //perform input action if there is one assigned
                if (inputStatusActions[_index].onHeld != null)
                {
                    inputStatusActions[_index].onHeld.Activate(keys[_index]);
                }

                //DEBUG — check which key is held and print its status
                //Debug.Log("key held: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region PRESSED
            if (Input.GetKeyDown(keys[_index].Key))
            {
                //update data
                keys[_index].SetStatus(InputStatus.RELEASED);
                keys[_index].SetXYRawValue(Vector2.one);
                keys[_index].AddXYValue(Vector2.one);
                keys[_index].SetHeldDuration(Time.deltaTime);
                keys[_index].SetInactiveDuration(0f);

                //perform input action if there is one assigned
                if (inputStatusActions[_index].onPressed != null)
                {
                    inputStatusActions[_index].onPressed.Activate(keys[_index]);
                }

                //DEBUG — check which key is pressed and print its status
                //Debug.Log("key pressed: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
            #region INACTIVE
            //inactive
            else
            {
                //update data
                keys[_index].SetStatus(InputStatus.INACTIVE);
                keys[_index].SetXYRawValue(Vector2.zero);
                keys[_index].AddXYValue(Vector2.zero);
                keys[_index].SetHeldDuration(0f);
                keys[_index].SetInactiveDuration(Time.deltaTime);

                //perform input action if there is one assigned
                keys[_index].SetStatus(InputStatus.INACTIVE);

                //DEBUG — check which key is inactive and print its status
                //Debug.Log("key inactive: " + keys[_index].Key + " status = " + keys[_index].Status);
            }
            #endregion
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