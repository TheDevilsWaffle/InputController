﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//BASED OFF OF TEMPLATE SCRIPT — Template_InputAction.cs
//
// HOW TO USE:
// Simply copy & paste this script and rename the class name to whatever you would like
//
// - or -
//
// When making your own script that will use gamepad/keyboard/mouse input it must have:
// * the class must inherit from InputAction
// * it must include public override void Activate() that takes InputData as a parameter
// * include at least one of the functions: Released(), Held(), and Pressed() that take InputData 
//   as a parameter
//
// NOTE: you do not need to make use of all 3 states (Released, Held, Pressed), but you can.
//
// ABOUT INPUT DATA:
// the parameter (_data) passed to Released(), Held(), and Pressed() contains information
// you might want when writing a behavior. The following is the data accessible:
//  
//  * _data.ID - string name value of the button/stick/key/etc.
//  * _data.Key - KeyCode of keyboard key pressed
//  * _data.Status - the InputStatus state of the button during this frame
//  * _data.XYValues - the Vector2 x & y values (float) during this frame
//  * _data.XYRawValues - the Vector2 x & y value (int) during this frame
//  * _data.Angle - the angle that the analog stick is being pressed/held
//  * _data.HeldDuration - time in seconds that the button/stick/key/etc. has been held
//  * _data.InactiveDuration - time in seconds that the button/stick/key/etc. has been inactive
//  * _data.ArcadeAxis - the ArcadeAxis enum value of the stick during this frame
//
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public enum FIREBALL_SIZE
{
    SMALL,
    MEDIUM,
    LARGE,
    HOLY_SHIT
};

class Fireball : InputAction  // inherits from InputAction
{
    [SerializeField]
    float small = 1f;

    [SerializeField]
    float medium = 3f;

    [SerializeField]
    float large = 5f;

    float timeHeld = 0f;
    FIREBALL_SIZE size;

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is released/held/pressed
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public override void Activate(InputData _data)
    {
        if (_data.Status == InputStatus.RELEASED)
        {
            Released(_data);
        }
        else if (_data.Status == InputStatus.HELD)
        {
            Held(_data);
        }
        else if (_data.Status == InputStatus.PRESSED)
        {
            Pressed(_data);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is released
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Released(InputData _data)
    {
        //your code here

        if(timeHeld < small)
        {
            size = FIREBALL_SIZE.SMALL;
        }
        else if(timeHeld > small && timeHeld < medium)
        {
            size = FIREBALL_SIZE.MEDIUM;
        }
        else if(timeHeld > medium && timeHeld < large)
        {
            size = FIREBALL_SIZE.LARGE;
        }
        else if(timeHeld > large)
        {
            size = FIREBALL_SIZE.HOLY_SHIT;
        }
        Debug.Log("Player 1 has cast a " + size + " sized fireball!");

        timeHeld = 0f;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is held
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Held(InputData _data)
    {
        //your code here
        timeHeld += Time.deltaTime;
        Debug.Log("Player 1 is charging up their fireball! (" + timeHeld +")");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is pressed
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Pressed(InputData _data)
    {
        //your code here
    }
}