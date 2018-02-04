///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInput_Gamepad.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;
using System.Collections;

public class XInput_Gamepad : MonoBehaviour
{
    #region FIELDS
    [Header("Player")]
    [Range(1,4)]
    [SerializeField]
    int player = 1;

    [Header("Analog Sticks")]
    [SerializeField]
    InputStatusAction leftStick;
    [SerializeField]
    InputStatusAction l3;
    [SerializeField]
    InputStatusAction rightStick;
    [SerializeField]
    InputStatusAction r3;

    [Header("D-Pad")]
    [SerializeField]
    InputStatusAction up;
    [SerializeField]
    InputStatusAction right;
    [SerializeField]
    InputStatusAction down;
    [SerializeField]
    InputStatusAction left;

    [Header("Buttons")]
    [SerializeField]
    InputStatusAction a;
    [SerializeField]
    InputStatusAction x;
    [SerializeField]
    InputStatusAction y;
    [SerializeField]
    InputStatusAction b;

    [Header("Misc. Buttons")]
    [SerializeField]
    InputStatusAction view;
    [SerializeField]
    InputStatusAction menu;

    [Header("Bumpers")]
    [SerializeField]
    InputStatusAction leftBumper;
    [SerializeField]
    InputStatusAction rightBumper;

    [Header("Triggers")]
    [SerializeField]
    InputStatusAction leftTrigger;
    [SerializeField]
    InputStatusAction rightTrigger;
    #endregion
    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //initial values
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //correct player value for list index
        --player;
       SetSubscriptions();
    }
    #endregion
    #region SUBSCRIPTIONS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// RemoveSubscriptions()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
        Events.instance.RemoveListener<EVENT_INPUT_XINPUT_UPDATE>(UpdateInputActions);      
    }
    #endregion
    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateInputActions(EVENT_INPUT_XINPUT_UPDATE _event)
    {
        //DEBUG
        //Debug.Log("PLAYER("+_event.player+") a = "+ _event.xInputdata.a.Status);

        //check if this is the right player
        if(_event.player == player)
        {
            //buttons
            #region A BUTTON
            //RELEASED
            if(_event.xInputdata.a.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onReleased.Activate(_event.xInputdata.a);
                }
            }
            //HELD
            if(_event.xInputdata.a.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onHeld.Activate(_event.xInputdata.a);
                }
            }
            //PRESSED
            if(_event.xInputdata.a.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(a.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    a.onPressed.Activate(_event.xInputdata.a);
                }
            }
            #endregion
            #region B BUTTON
            //RELEASED
            if (_event.xInputdata.b.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onReleased.Activate(_event.xInputdata.b);
                }
            }
            //HELD
            if (_event.xInputdata.b.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onHeld.Activate(_event.xInputdata.b);
                }
            }
            //PRESSED
            if (_event.xInputdata.b.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (b.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    b.onPressed.Activate(_event.xInputdata.b);
                }
            }
            #endregion
            #region Y BUTTON
            //RELEASED
            if (_event.xInputdata.y.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onReleased.Activate(_event.xInputdata.y);
                }
            }
            //HELD
            if (_event.xInputdata.y.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onHeld.Activate(_event.xInputdata.y);
                }
            }
            //PRESSED
            if (_event.xInputdata.y.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (y.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    y.onPressed.Activate(_event.xInputdata.y);
                }
            }
            #endregion
            #region X BUTTON
            //RELEASED
            if (_event.xInputdata.x.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onReleased.Activate(_event.xInputdata.x);
                }
            }
            //HELD
            if (_event.xInputdata.x.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onHeld.Activate(_event.xInputdata.x);
                }
            }
            //PRESSED
            if (_event.xInputdata.x.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (x.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    x.onPressed.Activate(_event.xInputdata.x);
                }
            }
            #endregion

            //bumpers
            #region LEFT BUMPER
            //RELEASED
            if(_event.xInputdata.lb.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftBumper.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onReleased.Activate(_event.xInputdata.lb);
                }
            }
            //HELD
            if(_event.xInputdata.lb.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftBumper.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onHeld.Activate(_event.xInputdata.lb);
                }
            }
            //PRESSED
            if(_event.xInputdata.lb.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftBumper.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftBumper.onPressed.Activate(_event.xInputdata.lb);
                }
            }
            #endregion
            #region RIGHT BUMPER
            //RELEASED
            if(_event.xInputdata.rb.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightBumper.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onReleased.Activate(_event.xInputdata.rb);
                }
            }
            //HELD
            if(_event.xInputdata.rb.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightBumper.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onHeld.Activate(_event.xInputdata.rb);
                }
            }
            //PRESSED
            if(_event.xInputdata.rb.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightBumper.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightBumper.onPressed.Activate(_event.xInputdata.rb);
                }
            }
            #endregion

            //triggers
            #region LEFT TRIGGER
            //RELEASED
            if(_event.xInputdata.lt.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftTrigger.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onReleased.Activate(_event.xInputdata.lt);
                }
            }
            //HELD
            if(_event.xInputdata.lt.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftTrigger.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onHeld.Activate(_event.xInputdata.lt);
                }
            }
            //PRESSED
            if(_event.xInputdata.lt.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(leftTrigger.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftTrigger.onPressed.Activate(_event.xInputdata.lt);
                }
            }
            #endregion
            #region RIGHT TRIGGER
            //RELEASED
            if(_event.xInputdata.rt.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightTrigger.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onReleased.Activate(_event.xInputdata.rt);
                }
            }
            //HELD
            if(_event.xInputdata.rt.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightTrigger.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onHeld.Activate(_event.xInputdata.rt);
                }
            }
            //PRESSED
            if(_event.xInputdata.rt.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if(rightTrigger.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightTrigger.onPressed.Activate(_event.xInputdata.rt);
                }
            }
            #endregion

            //analog sticks
            #region LEFT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.ls.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onReleased.Activate(_event.xInputdata.ls);
                }
            }
            //HELD
            if (_event.xInputdata.ls.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onHeld.Activate(_event.xInputdata.ls);
                }
            }
            //PRESSED
            if (_event.xInputdata.ls.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (leftStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    leftStick.onPressed.Activate(_event.xInputdata.ls);
                }
            }
            #endregion
            #region RIGHT ANALOG STICK
            //RELEASED
            if (_event.xInputdata.rs.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onReleased.Activate(_event.xInputdata.rs);
                }
            }
            //HELD
            if (_event.xInputdata.rs.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onHeld.Activate(_event.xInputdata.rs);
                }
            }
            //PRESSED
            if (_event.xInputdata.rs.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (rightStick.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    rightStick.onPressed.Activate(_event.xInputdata.rs);
                }
            }
            #endregion

            //dpads
            #region DPAD UP
            //RELEASED
            if (_event.xInputdata.dp_up.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onReleased.Activate(_event.xInputdata.dp_up);
                }
            }
            //HELD
            if (_event.xInputdata.dp_up.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onHeld.Activate(_event.xInputdata.dp_up);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_up.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (up.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    up.onPressed.Activate(_event.xInputdata.dp_up);
                }
            }
            #endregion
            #region DPAD RIGHT
            //RELEASED
            if (_event.xInputdata.dp_right.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (right.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    right.onReleased.Activate(_event.xInputdata.dp_right);
                }
            }
            //HELD
            if (_event.xInputdata.dp_right.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (right.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    right.onHeld.Activate(_event.xInputdata.dp_right);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_right.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (right.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    right.onPressed.Activate(_event.xInputdata.dp_right);
                }
            }
            #endregion
            #region DPAD DOWN
            //RELEASED
            if (_event.xInputdata.dp_down.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (down.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    down.onReleased.Activate(_event.xInputdata.dp_down);
                }
            }
            //HELD
            if (_event.xInputdata.dp_down.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (down.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    down.onHeld.Activate(_event.xInputdata.dp_down);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_down.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (down.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    down.onPressed.Activate(_event.xInputdata.dp_down);
                }
            }
            #endregion
            #region DPAD LEFT
            //RELEASED
            if (_event.xInputdata.dp_left.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (left.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    left.onReleased.Activate(_event.xInputdata.dp_left);
                }
            }
            //HELD
            if (_event.xInputdata.dp_left.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (left.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    left.onHeld.Activate(_event.xInputdata.dp_left);
                }
            }
            //PRESSED
            if (_event.xInputdata.dp_left.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (left.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    left.onPressed.Activate(_event.xInputdata.dp_left);
                }
            }
            #endregion

            //misc buttons
            #region VIEW
            //RELEASED
            if (_event.xInputdata.view.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onReleased.Activate(_event.xInputdata.view);
                }
            }
            //HELD
            if (_event.xInputdata.view.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onHeld.Activate(_event.xInputdata.view);
                }
            }
            //PRESSED
            if (_event.xInputdata.view.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (view.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    view.onPressed.Activate(_event.xInputdata.view);
                }
            }
            #endregion
            #region MENU
            //RELEASED
            if (_event.xInputdata.menu.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onReleased.Activate(_event.xInputdata.menu);
                }
            }
            //HELD
            if (_event.xInputdata.menu.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onHeld.Activate(_event.xInputdata.menu);
                }
            }
            //PRESSED
            if (_event.xInputdata.menu.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (menu.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    menu.onPressed.Activate(_event.xInputdata.menu);
                }
            }
            #endregion
            #region L3
            //RELEASED
            if (_event.xInputdata.l3.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onReleased.Activate(_event.xInputdata.l3);
                }
            }
            //HELD
            if (_event.xInputdata.l3.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onHeld.Activate(_event.xInputdata.l3);
                }
            }
            //PRESSED
            if (_event.xInputdata.l3.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (l3.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    l3.onPressed.Activate(_event.xInputdata.l3);
                }
            }
            #endregion
            #region R3
            //RELEASED
            if (_event.xInputdata.r3.Status == InputStatus.RELEASED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onReleased != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onReleased.Activate(_event.xInputdata.r3);
                }
            }
            //HELD
            if (_event.xInputdata.r3.Status == InputStatus.HELD)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onHeld != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onHeld.Activate(_event.xInputdata.r3);
                }
            }
            //PRESSED
            if (_event.xInputdata.r3.Status == InputStatus.PRESSED)
            {
                //safety check to see if we have an InputAction associated with this button/state combo
                if (r3.onPressed != null)
                {
                    //fire off input action and pass data for this button/stick/etc. as a parameter
                    r3.onPressed.Activate(_event.xInputdata.r3);
                }
            }
            #endregion
        }
    }
    #endregion
    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        RemoveSubscriptions();
    }
    #endregion
}