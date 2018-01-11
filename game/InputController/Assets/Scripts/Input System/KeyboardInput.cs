///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardInput.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardInput : MonoBehaviour
{
    #region FIELDS
    [Header("Player")]
    [Range(0, 4)]
    public int player = 1;
    [Header("Keyboard Controls")]
    [Header("Analog Sticks")]
    public KeyCode up = KeyCode.W;
    public KeyCode right = KeyCode.D;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    [Header("DPad")]
    public KeyCode dpad_up = KeyCode.UpArrow;
    public KeyCode dpad_right = KeyCode.RightArrow;
    public KeyCode dpad_down = KeyCode.DownArrow;
    public KeyCode dpad_left = KeyCode.LeftArrow;
    [Header("Buttons")]
    public KeyCode y = KeyCode.LeftControl;
    public KeyCode b = KeyCode.Q;
    public KeyCode a = KeyCode.Space;
    public KeyCode x = KeyCode.E;
    [Header("Shoulder Buttons")]
    public KeyCode l1 = KeyCode.LeftAlt;
    public KeyCode l2 = KeyCode.LeftShift;
    public KeyCode r1 = KeyCode.RightAlt;
    public KeyCode r2 = KeyCode.RightShift;
    [Header("Analog Stick Buttons")]
    public KeyCode l3 = KeyCode.O;
    public KeyCode r3 = KeyCode.P;
    [Header("Misc")]
    public KeyCode select = KeyCode.Tab;
    public KeyCode start = KeyCode.Return;
    [HideInInspector]
    public static Dictionary<KeyCode, InputStatus> keys;
    private List<KeyCode> keysList;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //correct player number to use as index for gamepads
        --player;

       //create keys dictionary and list (list needed to traverse dictionary and update dictionary value(cannot update value by reference))
       keys = new Dictionary<KeyCode, InputStatus>();
        
       //check for assigned keys and store them in keys
       AddAssignedKeys();
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
        //go through the keyboard input update loop
        UpdateKeyboardInput();

        //DEBUG — print out the contents of keys
        //    foreach(KeyValuePair<KeyCode, InputStatus> key in keys)
        //    {
        //        print("key = " + key.Key + ", and its status is = " + key.Value);
        //    }

        //DEBUG KEYBOARD -> GAMEPAD INPUT CHECK
        //Debug.Log("DPAD_UP = " + XInput.gamepads[player].dp_up.Status);
        //Debug.Log("DPAD_RIGHT = " + XInput.gamepads[player].dp_right.Status);
        //Debug.Log("DPAD_DOWN = " + XInput.gamepads[player].dp_down.Status);
        //Debug.Log("DPAD_LEFT = " + XInput.gamepads[player].dp_left.Status);
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Adds only the assigned buttons to the keys list
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void AddAssignedKeys()
   {
       //checking analog stick keys
       if (up != KeyCode.None)
           keys.Add(up, InputStatus.INACTIVE);
       if (right != KeyCode.None)
           keys.Add(right, InputStatus.INACTIVE);
       if (down != KeyCode.None)
           keys.Add(down, InputStatus.INACTIVE);
       if (left != KeyCode.None)
           keys.Add(left, InputStatus.INACTIVE);

        //add dpad keys
        if (dpad_up != KeyCode.None)
            keys.Add(dpad_up, InputStatus.INACTIVE);
        if (dpad_right != KeyCode.None)
            keys.Add(dpad_right, InputStatus.INACTIVE);
        if (dpad_down != KeyCode.None)
            keys.Add(dpad_down, InputStatus.INACTIVE);
        if (dpad_left != KeyCode.None)
            keys.Add(dpad_left, InputStatus.INACTIVE);

       //checking button/keys
       if (y != KeyCode.None)
           keys.Add(y, InputStatus.INACTIVE);
       if (b != KeyCode.None)
           keys.Add(b, InputStatus.INACTIVE);
       if (a != KeyCode.None)
           keys.Add(a, InputStatus.INACTIVE);
       if (x != KeyCode.None)
           keys.Add(x, InputStatus.INACTIVE);

       //checking shoulder button/keys
       if (l1 != KeyCode.None)
           keys.Add(l1, InputStatus.INACTIVE);
       if (l2 != KeyCode.None)
           keys.Add(l2, InputStatus.INACTIVE);
       if (r1 != KeyCode.None)
           keys.Add(r1, InputStatus.INACTIVE);
       if (r2 != KeyCode.None)
           keys.Add(r2, InputStatus.INACTIVE);

       //checking analog button/keys
       if (l3 != KeyCode.None)
           keys.Add(l3, InputStatus.INACTIVE);
       if (r3 != KeyCode.None)
           keys.Add(r3, InputStatus.INACTIVE);

       //checking misc button/keys
       if (select != KeyCode.None)
           keys.Add(select, InputStatus.INACTIVE);
       if (start != KeyCode.None)
           keys.Add(start, InputStatus.INACTIVE);

       keysList = new List<KeyCode>(keys.Keys);

       //DEBUG — CHECK KEYS STORED
       /*
       print("the contents of keys are: ");
       foreach(KeyCode key in keys)
       {
           print(key + ", ");
       }
       */
   }

   // ---------------IMPORTANT > LOOK AT THIS! this won't work, need to do individual ones per key.
   
    // void SetStatus(XInputData _button, InputStatus _status, int _x, int _y, float _duration)
    // {
    //     XInput.gamepads[player].
    //     gamepads[_index].dp_up.SetStatus(InputStatus.RELEASED);
    //     gamepads[_index].dp_up.SetXYValue(0f, 0f);
    //     gamepads[_index].dp_up.SetInactiveDuration(Time.deltaTime);
    // }
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

            //key is not pressed
            else
            {
                keys[key] = InputStatus.INACTIVE;

                //DEBUG — check which key is inactive and print its status
                //print("key inactive: " + key + " status = " + keys[key]);
            }

            UpdateGamepads(key, keys[key]);
        }
    }

    void UpdateGamepads(KeyCode _key, InputStatus _status)
    {
        #region DPADS
        //DPAD UP
        if(_key == dpad_up)
        {
            //do not override the gamepad itself
            if(XInput.gamepads[player].dp_up.Status == InputStatus.INACTIVE)
            {
                //first set status, then update data based upon status
                XInput.gamepads[player].dp_up.SetStatus(_status);
                //RELEASED
                if (_status == InputStatus.RELEASED)
                {
                    XInput.gamepads[player].dp_up.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_up.SetInactiveDuration(Time.deltaTime);

                }
                //HELD
                else if(_status == InputStatus.HELD)
                {
                    XInput.gamepads[player].dp_up.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_up.SetHeldDuration(Time.deltaTime);
                    XInput.gamepads[player].dp_up.SetInactiveDuration(0f);
                }
                //PRESSED
                else if (_status == InputStatus.PRESSED)
                {
                    XInput.gamepads[player].dp_up.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_up.SetHeldDuration(Time.deltaTime);

                    //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                    //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                }
                //INACTIVE
                else
                {
                    XInput.gamepads[player].dp_up.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_up.SetHeldDuration(0f);
                    XInput.gamepads[player].dp_up.SetInactiveDuration(0f);
                }
            }
        }
        //DPAD RIGHT
        if (_key == dpad_right)
        {
            //do not override the gamepad itself
            if (XInput.gamepads[player].dp_right.Status == InputStatus.INACTIVE)
            {
                //first set status, then update data based upon status
                XInput.gamepads[player].dp_right.SetStatus(_status);
                //RELEASED
                if (_status == InputStatus.RELEASED)
                {
                    XInput.gamepads[player].dp_right.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_right.SetInactiveDuration(Time.deltaTime);

                }
                //HELD
                else if (_status == InputStatus.HELD)
                {
                    XInput.gamepads[player].dp_right.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_right.SetHeldDuration(Time.deltaTime);
                    XInput.gamepads[player].dp_right.SetInactiveDuration(0f);
                }
                //PRESSED
                else if (_status == InputStatus.PRESSED)
                {
                    XInput.gamepads[player].dp_right.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_right.SetHeldDuration(Time.deltaTime);

                    //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                    //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                }
                //INACTIVE
                else
                {
                    XInput.gamepads[player].dp_right.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_right.SetHeldDuration(0f);
                    XInput.gamepads[player].dp_right.SetInactiveDuration(0f);
                }
            }
        }
        //DPAD DOWN
        if (_key == dpad_down)
        {
            //do not override the gamepad itself
            if (XInput.gamepads[player].dp_down.Status == InputStatus.INACTIVE)
            {
                //first set status, then update data based upon status
                XInput.gamepads[player].dp_down.SetStatus(_status);
                //RELEASED
                if (_status == InputStatus.RELEASED)
                {
                    XInput.gamepads[player].dp_down.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_down.SetInactiveDuration(Time.deltaTime);

                }
                //HELD
                else if (_status == InputStatus.HELD)
                {
                    XInput.gamepads[player].dp_down.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_down.SetHeldDuration(Time.deltaTime);
                    XInput.gamepads[player].dp_down.SetInactiveDuration(0f);
                }
                //PRESSED
                else if (_status == InputStatus.PRESSED)
                {
                    XInput.gamepads[player].dp_down.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_down.SetHeldDuration(Time.deltaTime);

                    //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                    //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                }
                //INACTIVE
                else
                {
                    XInput.gamepads[player].dp_down.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_down.SetHeldDuration(0f);
                    XInput.gamepads[player].dp_down.SetInactiveDuration(0f);
                }
            }
        }
        //DPAD LEFT
        if (_key == dpad_left)
        {
            //do not override the gamepad itself
            if (XInput.gamepads[player].dp_left.Status == InputStatus.INACTIVE)
            {
                //first set status, then update data based upon status
                XInput.gamepads[player].dp_left.SetStatus(_status);
                //RELEASED
                if (_status == InputStatus.RELEASED)
                {
                    XInput.gamepads[player].dp_left.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_left.SetInactiveDuration(Time.deltaTime);

                }
                //HELD
                else if (_status == InputStatus.HELD)
                {
                    XInput.gamepads[player].dp_left.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_left.SetHeldDuration(Time.deltaTime);
                    XInput.gamepads[player].dp_left.SetInactiveDuration(0f);
                }
                //PRESSED
                else if (_status == InputStatus.PRESSED)
                {
                    XInput.gamepads[player].dp_left.SetXYValue(1f, 1f);
                    XInput.gamepads[player].dp_left.SetHeldDuration(Time.deltaTime);

                    //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                    //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                }
                //INACTIVE
                else
                {
                    XInput.gamepads[player].dp_left.SetXYValue(0f, 0f);
                    XInput.gamepads[player].dp_left.SetHeldDuration(0f);
                    XInput.gamepads[player].dp_left.SetInactiveDuration(0f);
                }
            }
        }
        #endregion
        #region ANALOG STICKS
        //LEFT KEY
        if (_key == left)
        {
            //do not override the gamepad itself
            if (XInput.gamepads[player].ls.Status == InputStatus.INACTIVE)
            {
                //first set status, then update data based upon status
                XInput.gamepads[player].ls.SetStatus(_status);

                //RELEASED
                if (_status == InputStatus.RELEASED)
                {
                    XInput.gamepads[player].ls.SetXYValue(0f, 0f);
                    XInput.gamepads[player].ls.SetInactiveDuration(Time.deltaTime);

                }
                //HELD
                else if (_status == InputStatus.HELD)
                {
                    //IMPORTANT! --> this will need to be set after up,left,right,down are checked.
                    XInput.gamepads[player].ls.SetXYValue(-1f, 0f);
                    XInput.gamepads[player].ls.SetHeldDuration(Time.deltaTime);
                    XInput.gamepads[player].ls.SetInactiveDuration(0f);

                    //this might need to be checked/updated AFTER all directions (up, right, down, left) are checked
                    XInput.gamepads[player].ls.SetArcadeAxis(ArcadeAxis.LEFT);
                    XInput.gamepads[player].ls.SetAngle(0f);
                }
                //PRESSED
                else if (_status == InputStatus.PRESSED)
                {
                    XInput.gamepads[player].ls.SetXYValue(1f, 1f);
                    XInput.gamepads[player].ls.SetHeldDuration(Time.deltaTime);

                    //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                    //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                }
                //INACTIVE
                else
                {
                    XInput.gamepads[player].ls.SetXYValue(0f, 0f);
                    XInput.gamepads[player].ls.SetHeldDuration(0f);
                    XInput.gamepads[player].ls.SetInactiveDuration(0f);
                }
            }
            //RIGHT KEY
            else if (_key == right)
            {
                //do not override the gamepad itself
                if (XInput.gamepads[player].ls.Status == InputStatus.INACTIVE)
                {
                    //first set status, then update data based upon status
                    XInput.gamepads[player].ls.SetStatus(_status);

                    //RELEASED
                    if (_status == InputStatus.RELEASED)
                    {
                        XInput.gamepads[player].ls.SetXYValue(0f, 0f);
                        XInput.gamepads[player].ls.SetInactiveDuration(Time.deltaTime);

                    }
                    //HELD
                    else if (_status == InputStatus.HELD)
                    {
                        //IMPORTANT! --> this will need to be set after up,left,right,down are checked.
                        XInput.gamepads[player].ls.SetXYValue(1f, 0f);
                        XInput.gamepads[player].ls.SetHeldDuration(Time.deltaTime);
                        XInput.gamepads[player].ls.SetInactiveDuration(0f);

                        //this might need to be checked/updated AFTER all directions (up, right, down, left) are checked
                        XInput.gamepads[player].ls.SetArcadeAxis(ArcadeAxis.RIGHT);
                        XInput.gamepads[player].ls.SetAngle(0f);
                    }
                    //PRESSED
                    else if (_status == InputStatus.PRESSED)
                    {
                        XInput.gamepads[player].ls.SetXYValue(1f, 1f);
                        XInput.gamepads[player].ls.SetHeldDuration(Time.deltaTime);

                        //IMPORTANT - MAKE EVENT CALL FOR UPDATE COMBO AND MOVE STATUS UPDATES IN XINPUT TO ONLY 1 LINE PER BUTTON/STICK/ETC.
                        //XInput.UpdateCombo(player, XInput.gamepads[player].dp_up);
                    }
                    //INACTIVE
                    else
                    {
                        XInput.gamepads[player].ls.SetXYValue(0f, 0f);
                        XInput.gamepads[player].ls.SetHeldDuration(0f);
                        XInput.gamepads[player].ls.SetInactiveDuration(0f);
                    }
                }
            
        #endregion
    }
    #endregion
}