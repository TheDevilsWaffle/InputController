///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — KeyboardInput.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region ENUMS
public enum KeyboardKeyStatus
{
    INACTIVE,
    HELD,
    PRESSED, 
    RELEASED
}
#endregion

public class KeyboardInput : MonoBehaviour
{
    #region FIELDS
    [Header("Player")]
    [Range(0, 4)]
    public int player = 1;
    [Header("Keyboard Controls")]
    [Header("Movement/Directions")]
    public KeyCode up = KeyCode.W;
    public KeyCode right = KeyCode.D;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    [Header("Standard Buttons")]
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
    public static Dictionary<KeyCode, KeyboardKeyStatus> keys;
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
       keys = new Dictionary<KeyCode, KeyboardKeyStatus>();
        
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
        //    foreach(KeyValuePair<KeyCode, KeyboardKeyStatus> key in keys)
        //    {
        //        print("key = " + key.Key + ", and its status is = " + key.Value);
        //    }
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
       //checking movement keys
       if (up != KeyCode.None)
           keys.Add(up, KeyboardKeyStatus.INACTIVE);
       if (right != KeyCode.None)
           keys.Add(right, KeyboardKeyStatus.INACTIVE);
       if (down != KeyCode.None)
           keys.Add(down, KeyboardKeyStatus.INACTIVE);
       if (left != KeyCode.None)
           keys.Add(left, KeyboardKeyStatus.INACTIVE);

       //checking standard button/keys
       if (y != KeyCode.None)
           keys.Add(y, KeyboardKeyStatus.INACTIVE);
       if (b != KeyCode.None)
           keys.Add(b, KeyboardKeyStatus.INACTIVE);
       if (a != KeyCode.None)
           keys.Add(a, KeyboardKeyStatus.INACTIVE);
       if (x != KeyCode.None)
           keys.Add(x, KeyboardKeyStatus.INACTIVE);

       //checking shoulder button/keys
       if (l1 != KeyCode.None)
           keys.Add(l1, KeyboardKeyStatus.INACTIVE);
       if (l2 != KeyCode.None)
           keys.Add(l2, KeyboardKeyStatus.INACTIVE);
       if (r1 != KeyCode.None)
           keys.Add(r1, KeyboardKeyStatus.INACTIVE);
       if (r2 != KeyCode.None)
           keys.Add(r2, KeyboardKeyStatus.INACTIVE);

       //checking analog button/keys
       if (l3 != KeyCode.None)
           keys.Add(l3, KeyboardKeyStatus.INACTIVE);
       if (r3 != KeyCode.None)
           keys.Add(r3, KeyboardKeyStatus.INACTIVE);

       //checking misc button/keys
       if (select != KeyCode.None)
           keys.Add(select, KeyboardKeyStatus.INACTIVE);
       if (start != KeyCode.None)
           keys.Add(start, KeyboardKeyStatus.INACTIVE);

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
            if (Input.GetKeyUp(key) && keys[key] == KeyboardKeyStatus.HELD)
            {
                keys[key] = KeyboardKeyStatus.RELEASED;

                //DEBUG — check which key is released and print its status
                //print("key released: " + key + " status = " + keys[key]);
            }

            //held
            else if (Input.GetKey(key) && keys[key] == KeyboardKeyStatus.PRESSED || keys[key] == KeyboardKeyStatus.HELD)
            {
                keys[key] = KeyboardKeyStatus.HELD;

                //DEBUG — check which key is held and print its status
                //print("key held: " + key + " status = " + keys[key]);
            }

            //pressed
            else if (Input.GetKeyDown(key) && keys[key] == KeyboardKeyStatus.INACTIVE)
            {
                keys[key] = KeyboardKeyStatus.PRESSED;

                //DEBUG — check which key is pressed and print its status
                //print("key pressed: " + key + " status = " + keys[key]);
            }

            //key is not pressed
            else
            {
                keys[key] = KeyboardKeyStatus.INACTIVE;

                //DEBUG — check which key is inactive and print its status
                //print("key inactive: " + key + " status = " + keys[key]);
            }
        }
    }
    #endregion
}