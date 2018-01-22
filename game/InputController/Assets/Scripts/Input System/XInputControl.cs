///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInputControl.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEditor;
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


[System.Serializable]
public class InputAction
{
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

}

public class XInputControl : MonoBehaviour
{
    #region FIELDS
    [Header("XINPUT CONTROLS")]
    [Header("Buttons")]
    public InputAction a;
    public InputAction x;
    public InputAction y;
    public InputAction b;

    [Header("Analog Sticks")]
    public InputAction ls;
    public InputAction l3;
    public InputAction rs;
    public InputAction r3;

    #endregion

    //#region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //refs

        //initial values
        //onHeldTimer = ResetTimer();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //void Awake()
    //{
    //    SetSubscriptions();
    //}
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //void SetSubscriptions()
    //{
        //correct player number to the right index
    //     --player;

    //     switch (player)
    //     {
    //         case 0:
    //             Events.instance.AddListener<EVENT_XINPUT_P1>(P1);
    //             break;
    //         case 1:
    //             Events.instance.AddListener<EVENT_XINPUT_P2>(P2);
    //             break;
    //         case 2:
    //             Events.instance.AddListener<EVENT_XINPUT_P3>(P3);
    //             break;
    //         case 3:
    //             Events.instance.AddListener<EVENT_XINPUT_P4>(P4);
    //             break;
    //     }
    // }
    //#endregion

    //#region PRIVATE METHODS
    
    // ///////////////////////////////////////////////////////////////////////////////////////////////
    // /// <summary>
    // /// function
    // /// </summary>
    // ///////////////////////////////////////////////////////////////////////////////////////////////
    // float ResetTimer()
    // {
    //     return 0;
    // }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
//     void P1(EVENT_XINPUT_P1 _event)
//     {
//         switch (control)
//         {
//             #region BUTTONS
//             //A
//             case XBoxButtons.A:
//             //pressed
//             if(_event.gamepad.a.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.a.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.a.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                 onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.a.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //B
//             case XBoxButtons.B:
//             //pressed
//             if(_event.gamepad.b.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.b.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.b.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.b.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //X
//             case XBoxButtons.X:
//             //pressed
//             if(_event.gamepad.x.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.x.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.x.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.x.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //Y
//             case XBoxButtons.Y:
//             //pressed
//             if(_event.gamepad.y.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.y.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.y.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.y.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //VIEW
//             case XBoxButtons.VIEW:
//             //pressed
//             if(_event.gamepad.view.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.view.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.view.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.view.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //MENU
//             case XBoxButtons.MENU:
//             //pressed
//             if(_event.gamepad.menu.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.menu.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.menu.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.menu.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region BUMPERS
//             //LB
//             case XBoxButtons.LB:
//             //pressed
//             if(_event.gamepad.lb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RB
//             case XBoxButtons.RB:
//             //pressed
//             if(_event.gamepad.rb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region TRIGGERS
//             //LT
//             case XBoxButtons.LT:
//             //pressed
//             if(_event.gamepad.lt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RT
//             case XBoxButtons.RT:
//             //pressed
//             if(_event.gamepad.rt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region DPAD
//              //DPAD UP
//             case XBoxButtons.DP_UP:
//             //pressed
//             if(_event.gamepad.dp_up.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_up.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_up.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_up.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD RIGHT
//             case XBoxButtons.DP_RIGHT:
//             //pressed
//             if(_event.gamepad.dp_right.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_right.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_right.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_right.Status == InputStatus.INACTIVE)
//             {
//                 if(onPressed != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD DOWN
//             case XBoxButtons.DP_DOWN:
//             //pressed
//             if(_event.gamepad.dp_down.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_down.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_down.Status == InputStatus.RELEASED)
//             {
//                 if(onPressed != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_down.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD LEFT
//             case XBoxButtons.DP_LEFT:
//             //pressed
//             if(_event.gamepad.dp_left.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_left.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_left.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_left.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region ANALOG STICKS
//             //L3
//             case XBoxButtons.L3:
//             //pressed
//             if(_event.gamepad.l3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.l3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.l3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.l3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //R3
//             case XBoxButtons.R3:
//             //pressed
//             if(_event.gamepad.r3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.r3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.r3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.r3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //LS
//             case XBoxButtons.LS:
//             //pressed
//             if(_event.gamepad.ls.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.ls.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.ls.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.ls.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RS
//             case XBoxButtons.RS:
//             //pressed
//             if(_event.gamepad.rs.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rs.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rs.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rs.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//         }
//     }
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     /// <summary>
//     /// function
//     /// </summary>
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     void P2(EVENT_XINPUT_P2 _event)
//     {
//         switch (control)
//         {
//             #region BUTTONS
//             //A
//             case XBoxButtons.A:
//             //pressed
//             if(_event.gamepad.a.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.a.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.a.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                 onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.a.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //B
//             case XBoxButtons.B:
//             //pressed
//             if(_event.gamepad.b.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.b.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.b.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.b.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //X
//             case XBoxButtons.X:
//             //pressed
//             if(_event.gamepad.x.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.x.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.x.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.x.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //Y
//             case XBoxButtons.Y:
//             //pressed
//             if(_event.gamepad.y.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.y.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.y.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.y.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //VIEW
//             case XBoxButtons.VIEW:
//             //pressed
//             if(_event.gamepad.view.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.view.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.view.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.view.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //MENU
//             case XBoxButtons.MENU:
//             //pressed
//             if(_event.gamepad.menu.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.menu.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.menu.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.menu.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region BUMPERS
//             //LB
//             case XBoxButtons.LB:
//             //pressed
//             if(_event.gamepad.lb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RB
//             case XBoxButtons.RB:
//             //pressed
//             if(_event.gamepad.rb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region TRIGGERS
//             //LT
//             case XBoxButtons.LT:
//             //pressed
//             if(_event.gamepad.lt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RT
//             case XBoxButtons.RT:
//             //pressed
//             if(_event.gamepad.rt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region DPAD
//              //DPAD UP
//             case XBoxButtons.DP_UP:
//             //pressed
//             if(_event.gamepad.dp_up.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_up.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_up.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_up.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD RIGHT
//             case XBoxButtons.DP_RIGHT:
//             //pressed
//             if(_event.gamepad.dp_right.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_right.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_right.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_right.Status == InputStatus.INACTIVE)
//             {
//                 if(onPressed != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD DOWN
//             case XBoxButtons.DP_DOWN:
//             //pressed
//             if(_event.gamepad.dp_down.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_down.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_down.Status == InputStatus.RELEASED)
//             {
//                 if(onPressed != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_down.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD LEFT
//             case XBoxButtons.DP_LEFT:
//             //pressed
//             if(_event.gamepad.dp_left.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_left.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_left.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_left.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region ANALOG STICKS
//             //L3
//             case XBoxButtons.L3:
//             //pressed
//             if(_event.gamepad.l3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.l3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.l3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.l3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //R3
//             case XBoxButtons.R3:
//             //pressed
//             if(_event.gamepad.r3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.r3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.r3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.r3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //LS
//             case XBoxButtons.LS:
//             //pressed
//             if(_event.gamepad.ls.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.ls.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.ls.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.ls.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RS
//             case XBoxButtons.RS:
//             //pressed
//             if(_event.gamepad.rs.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rs.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rs.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rs.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//         }
//     }
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     /// <summary>
//     /// function
//     /// </summary>
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     void P3(EVENT_XINPUT_P3 _event)
//     {
//         switch (control)
//         {
//             #region BUTTONS
//             //A
//             case XBoxButtons.A:
//             //pressed
//             if(_event.gamepad.a.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.a.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.a.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                 onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.a.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //B
//             case XBoxButtons.B:
//             //pressed
//             if(_event.gamepad.b.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.b.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.b.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.b.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //X
//             case XBoxButtons.X:
//             //pressed
//             if(_event.gamepad.x.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.x.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.x.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.x.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //Y
//             case XBoxButtons.Y:
//             //pressed
//             if(_event.gamepad.y.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.y.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.y.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.y.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //VIEW
//             case XBoxButtons.VIEW:
//             //pressed
//             if(_event.gamepad.view.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.view.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.view.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.view.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //MENU
//             case XBoxButtons.MENU:
//             //pressed
//             if(_event.gamepad.menu.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.menu.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.menu.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.menu.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region BUMPERS
//             //LB
//             case XBoxButtons.LB:
//             //pressed
//             if(_event.gamepad.lb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RB
//             case XBoxButtons.RB:
//             //pressed
//             if(_event.gamepad.rb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region TRIGGERS
//             //LT
//             case XBoxButtons.LT:
//             //pressed
//             if(_event.gamepad.lt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RT
//             case XBoxButtons.RT:
//             //pressed
//             if(_event.gamepad.rt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region DPAD
//              //DPAD UP
//             case XBoxButtons.DP_UP:
//             //pressed
//             if(_event.gamepad.dp_up.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_up.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_up.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_up.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD RIGHT
//             case XBoxButtons.DP_RIGHT:
//             //pressed
//             if(_event.gamepad.dp_right.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_right.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_right.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_right.Status == InputStatus.INACTIVE)
//             {
//                 if(onPressed != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD DOWN
//             case XBoxButtons.DP_DOWN:
//             //pressed
//             if(_event.gamepad.dp_down.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_down.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_down.Status == InputStatus.RELEASED)
//             {
//                 if(onPressed != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_down.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD LEFT
//             case XBoxButtons.DP_LEFT:
//             //pressed
//             if(_event.gamepad.dp_left.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_left.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_left.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_left.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region ANALOG STICKS
//             //L3
//             case XBoxButtons.L3:
//             //pressed
//             if(_event.gamepad.l3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.l3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.l3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.l3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //R3
//             case XBoxButtons.R3:
//             //pressed
//             if(_event.gamepad.r3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.r3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.r3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.r3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //LS
//             case XBoxButtons.LS:
//             //pressed
//             if(_event.gamepad.ls.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.ls.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.ls.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.ls.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RS
//             case XBoxButtons.RS:
//             //pressed
//             if(_event.gamepad.rs.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rs.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rs.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rs.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//         }
//     }
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     /// <summary>
//     /// function
//     /// </summary>
//     ///////////////////////////////////////////////////////////////////////////////////////////////
//     void P4(EVENT_XINPUT_P4 _event)
//     {
// switch (control)
//         {
//             #region BUTTONS
//             //A
//             case XBoxButtons.A:
//             //pressed
//             if(_event.gamepad.a.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.a.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.a.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                 onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.a.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //B
//             case XBoxButtons.B:
//             //pressed
//             if(_event.gamepad.b.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.b.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.b.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.b.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //X
//             case XBoxButtons.X:
//             //pressed
//             if(_event.gamepad.x.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.x.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.x.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.x.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //Y
//             case XBoxButtons.Y:
//             //pressed
//             if(_event.gamepad.y.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.y.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.y.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.y.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //VIEW
//             case XBoxButtons.VIEW:
//             //pressed
//             if(_event.gamepad.view.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.view.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.view.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.view.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //MENU
//             case XBoxButtons.MENU:
//             //pressed
//             if(_event.gamepad.menu.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.menu.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.menu.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.menu.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region BUMPERS
//             //LB
//             case XBoxButtons.LB:
//             //pressed
//             if(_event.gamepad.lb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RB
//             case XBoxButtons.RB:
//             //pressed
//             if(_event.gamepad.rb.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rb.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rb.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rb.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region TRIGGERS
//             //LT
//             case XBoxButtons.LT:
//             //pressed
//             if(_event.gamepad.lt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.lt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.lt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.lt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RT
//             case XBoxButtons.RT:
//             //pressed
//             if(_event.gamepad.rt.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rt.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rt.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rt.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region DPAD
//              //DPAD UP
//             case XBoxButtons.DP_UP:
//             //pressed
//             if(_event.gamepad.dp_up.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_up.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_up.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_up.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD RIGHT
//             case XBoxButtons.DP_RIGHT:
//             //pressed
//             if(_event.gamepad.dp_right.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_right.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_right.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_right.Status == InputStatus.INACTIVE)
//             {
//                 if(onPressed != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD DOWN
//             case XBoxButtons.DP_DOWN:
//             //pressed
//             if(_event.gamepad.dp_down.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_down.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_down.Status == InputStatus.RELEASED)
//             {
//                 if(onPressed != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_down.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //DPAD LEFT
//             case XBoxButtons.DP_LEFT:
//             //pressed
//             if(_event.gamepad.dp_left.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.dp_left.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.dp_left.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.dp_left.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//             #region ANALOG STICKS
//             //L3
//             case XBoxButtons.L3:
//             //pressed
//             if(_event.gamepad.l3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.l3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.l3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.l3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //R3
//             case XBoxButtons.R3:
//             //pressed
//             if(_event.gamepad.r3.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.r3.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.r3.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.r3.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //LS
//             case XBoxButtons.LS:
//             //pressed
//             if(_event.gamepad.ls.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.ls.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.ls.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.ls.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;

//             //RS
//             case XBoxButtons.RS:
//             //pressed
//             if(_event.gamepad.rs.Status == InputStatus.PRESSED)
//             {
//                 if(onPressed != null)
//                     onPressed.Activate();
//             }
//             //held
//             if(_event.gamepad.rs.Status == InputStatus.HELD)
//             {
//                 if(onHeld != null)
//                     onHeld.Activate();
//             }
//             //released
//             if(_event.gamepad.rs.Status == InputStatus.RELEASED)
//             {
//                 if(onReleased != null)
//                     onReleased.Activate();
//             }
//             //inactive
//             if(_event.gamepad.rs.Status == InputStatus.INACTIVE)
//             {
//                 if(onInactive != null)
//                     onInactive.Activate();
//             }
//             break;
//             #endregion
//         }
//     }
     //#endregion

    //#region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    // void OnDestroy()
    // {
    //     //remove listeners
    //     switch (player)
    //     {
    //         case 0:
    //             Events.instance.RemoveListener<EVENT_XINPUT_P1>(P1);
    //             break;
    //         case 1:
    //             Events.instance.RemoveListener<EVENT_XINPUT_P2>(P2);
    //             break;
    //         case 2:
    //             Events.instance.RemoveListener<EVENT_XINPUT_P3>(P3);
    //             break;
    //         case 3:
    //             Events.instance.RemoveListener<EVENT_XINPUT_P4>(P4);
    //             break;
    //     }
    // }
    //#endregion
}