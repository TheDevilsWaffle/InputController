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

class Jump : InputAction  // inherits from InputAction
{
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
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is held
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Held(InputData _data)
    {
        //your code here
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// receives InputData when a gamepad/keyboard/mouse input is pressed
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Pressed(InputData _data)
    {
        //your code here

        Debug.Log("Player 1 has jumped!");
    }
}