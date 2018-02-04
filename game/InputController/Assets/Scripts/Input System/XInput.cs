﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInput.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using XInputDotNetPure;

#region EVENTS
public class EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_LOST : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_LOST(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
public class EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_ACQUIRED : GameEvent
{
    public int playerNumber;
    public EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_ACQUIRED(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
public class EVENT_INPUT_XINPUT_UPDATE : GameEvent
{
    public int player;
    public XInputData xInputdata;
    public EVENT_INPUT_XINPUT_UPDATE(int _playerNumber, XInputData _xInputData)
    {
        player = _playerNumber;
        xInputdata = _xInputData;
    }
}
#endregion

public class XInput : MonoBehaviour
{
    #region FIELDS
    [Header("DEAD ZONES")]
    [Range(0, 1)]
    [SerializeField]
    float analogStickDeadZone = 0.2f;
    [Range(0, 1)]
    [SerializeField]
    float triggerDeadZone = 0.2f;

    [Header("SETTINGS")]
    [SerializeField]
    float maxHeldDuration = 3f;
    [SerializeField]
    float maxInactiveDuration = 3f;
    [SerializeField]
    int maxButtonsCombo = 10;

    bool enableXInput = false;
    public static XInputData data;
    GamePadState previous;
    GamePadState current;

    //arcade axis values
    float up = -90f;
    float up_right = -45f;
    float right = 0f;
    float down_right = 45f;
    float down = 90f;
    float down_left = 135f;
    float left = -180f;
    float up_left = -135f;
    float axisLimit = 22.5f;

    #endregion
    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// validation/update in inspector
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //initial values
        data = new XInputData();
        up = -90f;
        up_right = -45f;
        right = 0f;
        down_right = 45f;
        down = 90f;
        down_left = 135f;
        left = -180f;
        up_left = -135f;
        axisLimit = 22.5f;
        enableXInput = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //set subscriptions to specifc events
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// subscribe to events
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_INPUT_INITIALIZE_XINPUT>(InitializeXInput);
    }
    #endregion
    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// update loop
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(enableXInput)
        {
            for (int _player = 0; _player < InputSystem.players; ++_player)
            {
                GamePadState _testState = GamePad.GetState((PlayerIndex)_player);
                if (_testState.IsConnected)
                {
                    //update the previous and currentstate
                    previous = current;
                    current = GamePad.GetState((PlayerIndex)_player);

                    //check current gamepad dpad/sticks/bumpers/triggers for input
                    UpdateSticks();
                    UpdateButtons();
                    UpdateTriggers();
                    UpdateBumpers();
                    UpdateDPad();

                    //broadcast event with updated gamepad information for current gamepad
                    Broadcast(_player, data);
                }
                //gamepad is not detected, broadcast gamepad detection lost event
                else
                {
                    CheckForGamepad(_player);
                    Events.instance.Raise(new EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_LOST(_player));
                }
            }
        }
    }
    #endregion
    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summy>
    /// checks to make sure that the controller is still connected
    /// <param name="_event">input initialize xinput event</param>
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeXInput(EVENT_INPUT_INITIALIZE_XINPUT _event)
    {
        //DEBUG
        //Debug.Log("InitializeXInput() for "+_event.players+" number of players");
        
        enableXInput = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summy>
    /// checks to make sure that the controller is still connected
    /// <param name="_status">is the stick currently being used</param>
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CheckForGamepad(int _player)
    {
        //DEBUG
        //Debug.Log("CheckForGamepad() using gamepad index("+playerNumber+")");

        //ensure that this gamepad is still connected
        GamePadState _testState = GamePad.GetState((PlayerIndex)_player);
        if (_testState.IsConnected)
        {
            //DEBUG
            //Debug.Log("CheckForGamepad() using gamepad index("+playerNumber+")");

            //raise event, gamepad acquired
            Events.instance.Raise(new EVENT_INPUT_XINPUT_GAMEPAD_DETECTION_ACQUIRED(_player));
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the DPad
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateDPad()
    {
        #region DPAD UP
        //RELEASED
        if (previous.DPad.Up == ButtonState.Pressed && current.DPad.Up == ButtonState.Released)
        {
            data.dp_up.SetStatus(InputStatus.RELEASED);
            data.dp_up.SetXYValue(0f, 0f);
            data.dp_up.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.DPad.Up == ButtonState.Pressed && current.DPad.Up == ButtonState.Pressed)
        {
            data.dp_up.SetStatus(InputStatus.HELD);
            data.dp_up.SetXYValue(1f, 1f);
            data.dp_up.SetHeldDuration(Time.deltaTime);
            data.dp_up.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.DPad.Up == ButtonState.Released && current.DPad.Up == ButtonState.Pressed)
        {
            data.dp_up.SetStatus(InputStatus.PRESSED);
            data.dp_up.SetXYValue(1f, 1f);
            data.dp_up.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.dp_up);
        }
        //INACTIVE
        else
        {
            data.dp_up.SetStatus(InputStatus.INACTIVE);
            data.dp_up.SetXYValue(0f, 0f);
            data.dp_up.SetHeldDuration(0f);
            data.dp_up.SetInactiveDuration(Time.deltaTime);
        }


        #endregion
        #region DPAD RIGHT
        //RELEASED
        if (previous.DPad.Right == ButtonState.Pressed && current.DPad.Right == ButtonState.Released)
        {
            data.dp_right.SetStatus(InputStatus.RELEASED);
            data.dp_right.SetXYValue(0f, 0f);
            data.dp_right.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.DPad.Right == ButtonState.Pressed && current.DPad.Right == ButtonState.Pressed)
        {
            data.dp_right.SetStatus(InputStatus.HELD);
            data.dp_right.SetXYValue(1f, 1f);
            data.dp_right.SetHeldDuration(Time.deltaTime);
            data.dp_right.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.DPad.Right == ButtonState.Released && current.DPad.Right == ButtonState.Pressed)
        {
            data.dp_right.SetStatus(InputStatus.PRESSED);
            data.dp_right.SetXYValue(1f, 1f);
            data.dp_right.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.dp_right);
        }
        //INACTIVE
        else
        {
            data.dp_right.SetStatus(InputStatus.INACTIVE);
            data.dp_right.SetXYValue(0f, 0f);
            data.dp_right.SetHeldDuration(0f);
            data.dp_right.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD DOWN
        //RELEASED
        if (previous.DPad.Down == ButtonState.Pressed && current.DPad.Down == ButtonState.Released)
        {
            data.dp_down.SetStatus(InputStatus.RELEASED);
            data.dp_down.SetXYValue(0f, 0f);
            data.dp_down.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.DPad.Down == ButtonState.Pressed && current.DPad.Down == ButtonState.Pressed)
        {
            data.dp_down.SetStatus(InputStatus.HELD);
            data.dp_down.SetXYValue(1f, 1f);
            data.dp_down.SetHeldDuration(Time.deltaTime);
            data.dp_down.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.DPad.Down == ButtonState.Released && current.DPad.Down == ButtonState.Pressed)
        {
            data.dp_down.SetStatus(InputStatus.PRESSED);
            data.dp_down.SetXYValue(1f, 1f);
            data.dp_down.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.dp_down);
        }
        //INACTIVE
        else
        {
            data.dp_down.SetStatus(InputStatus.INACTIVE);
            data.dp_down.SetXYValue(0f, 0f);
            data.dp_down.SetHeldDuration(0f);
            data.dp_down.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD LEFT
        //RELEASED
        if (previous.DPad.Left == ButtonState.Pressed && current.DPad.Left == ButtonState.Released)
        {
            data.dp_left.SetStatus(InputStatus.RELEASED);
            data.dp_left.SetXYValue(0f, 0f);
            data.dp_left.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.DPad.Left == ButtonState.Pressed && current.DPad.Left == ButtonState.Pressed)
        {
            data.dp_left.SetStatus(InputStatus.HELD);
            data.dp_left.SetXYValue(1f, 1f);
            data.dp_left.SetHeldDuration(Time.deltaTime);
            data.dp_left.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.DPad.Left == ButtonState.Released && current.DPad.Left == ButtonState.Pressed)
        {
            data.dp_left.SetStatus(InputStatus.PRESSED);
            data.dp_left.SetXYValue(1f, 1f);
            data.dp_left.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.dp_left);
        }
        //INACTIVE
        else
        {
            data.dp_left.SetStatus(InputStatus.INACTIVE);
            data.dp_left.SetXYValue(0f, 0f);
            data.dp_left.SetHeldDuration(0f);
            data.dp_left.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the left/right analog sticks (including l3/r3)
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateSticks()
    {
        //(UP is 90° RIGHT = 0°, DOWN = -90°, and LEFT = 180°)
        #region LEFT ANALOG STICK

        //store these values from the stick no matter what
        data.ls.SetXYValue(new Vector3(current.ThumbSticks.Left.X, current.ThumbSticks.Left.Y));
        data.ls.SetXYRawValue(new Vector3(Mathf.Ceil(current.ThumbSticks.Left.X), Mathf.Ceil(current.ThumbSticks.Left.Y)));
        data.ls.SetAngle(Mathf.Ceil(Mathf.Atan2(-current.ThumbSticks.Left.Y, current.ThumbSticks.Left.X) * Mathf.Rad2Deg));
        data.ls.SetArcadeAxis(DetermineArcadeAxis(data.ls.Status, data.ls.Angle));

        //check to see if the value of x and y is outside tolerance deadzone
        if (current.ThumbSticks.Left.X < -analogStickDeadZone ||
            current.ThumbSticks.Left.X > analogStickDeadZone ||
            current.ThumbSticks.Left.Y < -analogStickDeadZone ||
            current.ThumbSticks.Left.Y > analogStickDeadZone)
        {
            if (data.ls.Status == InputStatus.INACTIVE)
            {
                data.ls.SetStatus(InputStatus.PRESSED);
            }
            else
            {
                data.ls.SetStatus(InputStatus.HELD);
            }
        }

        //else, we're inside the deadzone, update status
        else if (current.ThumbSticks.Left.X > -analogStickDeadZone ||
                 current.ThumbSticks.Left.X < analogStickDeadZone ||
                 current.ThumbSticks.Left.Y > -analogStickDeadZone ||
                 current.ThumbSticks.Left.Y < analogStickDeadZone)
        {
            if (data.ls.Status == InputStatus.HELD)
            {
                data.ls.SetStatus(InputStatus.RELEASED);
            }
            else
            {
                data.ls.SetStatus(InputStatus.INACTIVE);
            }
        }

        //RELEASED
        if (previous.Buttons.LeftStick == ButtonState.Pressed && current.Buttons.LeftStick == ButtonState.Released)
        {
            data.l3.SetStatus(InputStatus.RELEASED);
            data.l3.SetXYValue(0f, 0f);
            data.l3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.LeftStick == ButtonState.Pressed && current.Buttons.LeftStick == ButtonState.Pressed)
        {
            data.l3.SetStatus(InputStatus.HELD);
            data.l3.SetXYValue(1f, 1f);
            data.l3.SetHeldDuration(Time.deltaTime);
            data.l3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.LeftStick == ButtonState.Released && current.Buttons.LeftStick == ButtonState.Pressed)
        {
            data.l3.SetStatus(InputStatus.PRESSED);
            data.l3.SetXYValue(1f, 1f);
            data.l3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.l3);
        }
        //INACTIVE
        else
        {
            data.l3.SetStatus(InputStatus.INACTIVE);
            data.l3.SetXYValue(0f, 0f);
            data.l3.SetHeldDuration(0f);
            data.l3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RIGHT ANALOG STICK

        //store these values from the stick no matter what
        data.rs.SetXYValue(new Vector3(current.ThumbSticks.Right.X, current.ThumbSticks.Right.Y));
        data.rs.SetXYRawValue(new Vector3(Mathf.Ceil(current.ThumbSticks.Right.X), Mathf.Ceil(current.ThumbSticks.Right.Y)));
        data.rs.SetAngle(Mathf.Ceil(Mathf.Atan2(-current.ThumbSticks.Right.Y, current.ThumbSticks.Right.X) * Mathf.Rad2Deg));
        data.rs.SetArcadeAxis(DetermineArcadeAxis(data.rs.Status, data.rs.Angle));

        //check to see if the value of x and y is inside tolerance deadzone
        if (current.ThumbSticks.Right.X < -analogStickDeadZone ||
            current.ThumbSticks.Right.X > analogStickDeadZone ||
            current.ThumbSticks.Right.Y < -analogStickDeadZone ||
            current.ThumbSticks.Right.Y > analogStickDeadZone)
        {
            if (data.rs.Status == InputStatus.INACTIVE)
            {
                data.rs.SetStatus(InputStatus.PRESSED);
            }
            else
            {
                data.rs.SetStatus(InputStatus.HELD);
            }
        }

        //else, we're outside the deadzone, update status
        else if (current.ThumbSticks.Right.X > -analogStickDeadZone ||
                 current.ThumbSticks.Right.X < analogStickDeadZone ||
                 current.ThumbSticks.Right.Y > -analogStickDeadZone ||
                 current.ThumbSticks.Right.Y < analogStickDeadZone)
        {
            if (data.rs.Status == InputStatus.HELD)
            {
                data.rs.SetStatus(InputStatus.RELEASED);
            }
            else
            {
                data.rs.SetStatus(InputStatus.INACTIVE);
            }
        }

        //RELEASED
        if (previous.Buttons.RightStick == ButtonState.Pressed && current.Buttons.RightStick == ButtonState.Released)
        {
            data.r3.SetStatus(InputStatus.RELEASED);
            data.r3.SetXYValue(0f, 0f);
            data.r3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.RightStick == ButtonState.Pressed && current.Buttons.RightStick == ButtonState.Pressed)
        {
            data.r3.SetStatus(InputStatus.HELD);
            data.r3.SetXYValue(1f, 1f);
            data.r3.SetHeldDuration(Time.deltaTime);
            data.r3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.RightStick == ButtonState.Released && current.Buttons.RightStick == ButtonState.Pressed)
        {
            data.r3.SetStatus(InputStatus.PRESSED);
            data.r3.SetXYValue(1f, 1f);
            data.r3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.r3);
        }
        //INACTIVE
        else
        {
            data.r3.SetStatus(InputStatus.INACTIVE);
            data.r3.SetXYValue(0f, 0f);
            data.r3.SetHeldDuration(0f);
            data.r3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the buttons
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateButtons()
    {
        #region Y BUTTON
        //RELEASED
        if (previous.Buttons.Y == ButtonState.Pressed && current.Buttons.Y == ButtonState.Released)
        {
            data.y.SetStatus(InputStatus.RELEASED);
            data.y.SetXYValue(0f, 0f);
            data.y.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.Y == ButtonState.Pressed && current.Buttons.Y == ButtonState.Pressed)
        {
            data.y.SetStatus(InputStatus.HELD);
            data.y.SetHeldDuration(Time.deltaTime);
            data.y.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.Y == ButtonState.Released && current.Buttons.Y == ButtonState.Pressed)
        {
            data.y.SetStatus(InputStatus.PRESSED);
            data.y.SetXYValue(1f, 1f);
            data.y.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.y);
        }
        //INACTIVE
        else
        {
            data.y.SetStatus(InputStatus.INACTIVE);
            data.y.SetXYValue(0f, 0f);
            data.y.SetHeldDuration(0f);
            data.y.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region B BUTTON
        //RELEASED
        if (previous.Buttons.B == ButtonState.Pressed && current.Buttons.B == ButtonState.Released)
        {
            data.b.SetStatus(InputStatus.RELEASED);
            data.b.SetXYValue(0f, 0f);
            data.b.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.B == ButtonState.Pressed && current.Buttons.B == ButtonState.Pressed)
        {
            data.b.SetStatus(InputStatus.HELD);
            data.b.SetHeldDuration(Time.deltaTime);
            data.b.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.B == ButtonState.Released && current.Buttons.B == ButtonState.Pressed)
        {
            data.b.SetStatus(InputStatus.PRESSED);
            data.b.SetXYValue(1f, 1f);
            data.b.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.b);
        }
        //INACTIVE
        else
        {
            data.b.SetStatus(InputStatus.INACTIVE);
            data.b.SetXYValue(0f, 0f);
            data.b.SetHeldDuration(0f);
            data.b.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region A BUTTON
        //RELEASED
        if (previous.Buttons.A == ButtonState.Pressed && current.Buttons.A == ButtonState.Released)
        {
            data.a.SetStatus(InputStatus.RELEASED);
            data.a.SetXYValue(0f, 0f);
            data.a.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.A == ButtonState.Pressed && current.Buttons.A == ButtonState.Pressed)
        {
            data.a.SetStatus(InputStatus.HELD);
            data.a.SetHeldDuration(Time.deltaTime);
            data.a.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.A == ButtonState.Released && current.Buttons.A == ButtonState.Pressed)
        {
            data.a.SetStatus(InputStatus.PRESSED);
            data.a.SetXYValue(1f, 1f);
            data.a.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.a);
        }
        //INACTIVE
        else
        {
            data.a.SetStatus(InputStatus.INACTIVE);
            data.a.SetXYValue(0f, 0f);
            data.a.SetHeldDuration(0f);
            data.a.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region X BUTTON
        //RELEASED
        if (previous.Buttons.X == ButtonState.Pressed && current.Buttons.X == ButtonState.Released)
        {
            data.x.SetStatus(InputStatus.RELEASED);
            data.x.SetXYValue(0f, 0f);
            data.x.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.X == ButtonState.Pressed && current.Buttons.X == ButtonState.Pressed)
        {
            data.x.SetStatus(InputStatus.HELD);
            data.x.SetHeldDuration(Time.deltaTime);
            data.x.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.X == ButtonState.Released && current.Buttons.X == ButtonState.Pressed)
        {
            data.x.SetStatus(InputStatus.PRESSED);
            data.x.SetXYValue(1f, 1f);
            data.x.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.x);
        }
        //INACTIVE
        else
        {
            data.x.SetStatus(InputStatus.INACTIVE);
            data.x.SetXYValue(0f, 0f);
            data.x.SetHeldDuration(0f);
            data.x.SetInactiveDuration(Time.deltaTime);
        }
        #endregion

        #region VIEW BUTTON
        //RELEASED
        if (previous.Buttons.Back == ButtonState.Pressed && current.Buttons.Back == ButtonState.Released)
        {
            data.view.SetStatus(InputStatus.RELEASED);
            data.view.SetXYValue(0f, 0f);
            data.view.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.Back == ButtonState.Pressed && current.Buttons.Back == ButtonState.Pressed)
        {
            data.view.SetStatus(InputStatus.HELD);
            data.view.SetHeldDuration(Time.deltaTime);
            data.view.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.Back == ButtonState.Released && current.Buttons.Back == ButtonState.Pressed)
        {
            data.view.SetStatus(InputStatus.PRESSED);
            data.view.SetXYValue(1f, 1f);
            data.view.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.view);
        }
        //INACTIVE
        else
        {
            data.view.SetStatus(InputStatus.INACTIVE);
            data.view.SetXYValue(0f, 0f);
            data.view.SetHeldDuration(0f);
            data.view.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region MENU BUTTON
        //RELEASED
        if (previous.Buttons.Start == ButtonState.Pressed && current.Buttons.Start == ButtonState.Released)
        {
            data.menu.SetStatus(InputStatus.RELEASED);
            data.menu.SetXYValue(0f, 0f);
            data.menu.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.Start == ButtonState.Pressed && current.Buttons.Start == ButtonState.Pressed)
        {
            data.menu.SetStatus(InputStatus.HELD);
            data.menu.SetHeldDuration(Time.deltaTime);
            data.menu.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.Start == ButtonState.Released && current.Buttons.Start == ButtonState.Pressed)
        {
            data.menu.SetStatus(InputStatus.PRESSED);
            data.menu.SetXYValue(1f, 1f);
            data.menu.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.menu);
        }
        //INACTIVE
        else
        {
            data.menu.SetStatus(InputStatus.INACTIVE);
            data.menu.SetXYValue(0f, 0f);
            data.menu.SetHeldDuration(0f);
            data.menu.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the bumpers
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateBumpers()
    {
        #region LB
        //RELEASED
        if (previous.Buttons.LeftShoulder == ButtonState.Pressed && current.Buttons.LeftShoulder == ButtonState.Released)
        {
            data.lb.SetStatus(InputStatus.RELEASED);
            data.lb.SetXYValue(0f, 0f);
            data.lb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.LeftShoulder == ButtonState.Pressed && current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            data.lb.SetStatus(InputStatus.HELD);
            data.lb.SetHeldDuration(Time.deltaTime);
            data.lb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.LeftShoulder == ButtonState.Released && current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            data.lb.SetStatus(InputStatus.PRESSED);
            data.lb.SetXYValue(1f, 1f);
            data.lb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.lb);
        }
        //INACTIVE
        else
        {
            data.lb.SetStatus(InputStatus.INACTIVE);
            data.lb.SetXYValue(0f, 0f);
            data.lb.SetHeldDuration(0f);
            data.lb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RB
        //RELEASED
        if (previous.Buttons.RightShoulder == ButtonState.Pressed && current.Buttons.RightShoulder == ButtonState.Released)
        {
            data.rb.SetStatus(InputStatus.RELEASED);
            data.rb.SetXYValue(0f, 0f);
            data.rb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (previous.Buttons.RightShoulder == ButtonState.Pressed && current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            data.rb.SetStatus(InputStatus.HELD);
            data.rb.SetHeldDuration(Time.deltaTime);
            data.rb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (previous.Buttons.RightShoulder == ButtonState.Released && current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            data.rb.SetStatus(InputStatus.PRESSED);
            data.rb.SetXYValue(1f, 1f);
            data.rb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.rb);
        }
        //INACTIVE
        else
        {
            data.rb.SetStatus(InputStatus.INACTIVE);
            data.rb.SetXYValue(0f, 0f);
            data.rb.SetHeldDuration(0f);
            data.rb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the triggers
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTriggers()
    {
        #region LT
        //RELEASED
        if (data.lt.Status == InputStatus.PRESSED && current.Triggers.Left < triggerDeadZone)
        {
            data.lt.SetStatus(InputStatus.RELEASED);
            data.lt.SetXYValue(current.Triggers.Left, current.Triggers.Left);
            data.lt.SetXYRawValue(Mathf.Ceil(current.Triggers.Left), Mathf.Ceil(current.Triggers.Left));
            data.lt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (data.lt.Status == InputStatus.PRESSED || data.lt.Status == InputStatus.HELD && current.Triggers.Left > triggerDeadZone)
        {
            data.lt.SetStatus(InputStatus.HELD);
            data.lt.SetXYValue(current.Triggers.Left, current.Triggers.Left);
            data.lt.SetXYRawValue(Mathf.Ceil(current.Triggers.Left), Mathf.Ceil(current.Triggers.Left));
            data.lt.SetHeldDuration(Time.deltaTime);
            data.lt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (data.lt.Status == InputStatus.INACTIVE && current.Triggers.Left > triggerDeadZone)
        {
            data.lt.SetStatus(InputStatus.PRESSED);
            data.lt.SetXYValue(current.Triggers.Left, current.Triggers.Left);
            data.lt.SetXYRawValue(Mathf.Ceil(current.Triggers.Left), Mathf.Ceil(current.Triggers.Left));
            data.lt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.lt);
        }
        //INACTIVE
        else
        {
            data.lt.SetStatus(InputStatus.INACTIVE);
            data.lt.SetXYValue(current.Triggers.Left, current.Triggers.Left);
            data.lt.SetXYRawValue(Mathf.Ceil(current.Triggers.Left), Mathf.Ceil(current.Triggers.Left));
            data.lt.SetHeldDuration(0f);
            data.lt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RT
        //RELEASED
        if (data.rt.Status == InputStatus.PRESSED && current.Triggers.Right < triggerDeadZone)
        {
            data.rt.SetStatus(InputStatus.RELEASED);
            data.rt.SetXYValue(current.Triggers.Right, current.Triggers.Right);
            data.rt.SetXYRawValue(Mathf.Ceil(current.Triggers.Right), Mathf.Ceil(current.Triggers.Right));
            data.rt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (data.rt.Status == InputStatus.PRESSED || data.rt.Status == InputStatus.HELD && current.Triggers.Right > triggerDeadZone)
        {
            data.rt.SetStatus(InputStatus.HELD);
            data.rt.SetXYValue(current.Triggers.Right, current.Triggers.Right);
            data.rt.SetXYRawValue(Mathf.Ceil(current.Triggers.Right), Mathf.Ceil(current.Triggers.Right));
            data.rt.SetHeldDuration(Time.deltaTime);
            data.rt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (data.rt.Status == InputStatus.INACTIVE && current.Triggers.Right > triggerDeadZone)
        {
            data.rt.SetStatus(InputStatus.PRESSED);
            data.rt.SetXYValue(current.Triggers.Right, current.Triggers.Right);
            data.rt.SetXYRawValue(Mathf.Ceil(current.Triggers.Right), Mathf.Ceil(current.Triggers.Right));
            data.rt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(data.rt);
        }
        //INACTIVE
        else
        {
            data.rt.SetStatus(InputStatus.INACTIVE);
            data.rt.SetXYValue(current.Triggers.Right, current.Triggers.Right);
            data.rt.SetXYRawValue(Mathf.Ceil(current.Triggers.Right), Mathf.Ceil(current.Triggers.Right));
            data.rt.SetHeldDuration(0f);
            data.rt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// tracks the last 'x-number' of buttons pressed from the current player
    /// <param name = "_button" >current button pressed</param>
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void UpdateCombo(InputData _button)
    {
        //add in the new button
        data.comboTracker.Enqueue(_button);

        //dequeue the oldest button if we're at max rememberence
        if (data.comboTracker.Count > maxButtonsCombo)
        {
            data.comboTracker.Dequeue();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates ArcadeAxis enum using the angle of the stick for 8-axis style controls
    /// </summary>
    /// <param name="_status">is the stick currently being used</param>
    /// <param name="_angle">angle of the stick</param>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public ArcadeAxis DetermineArcadeAxis(InputStatus _status, float _angle)
    {
        //is the stick active?
        if (_status == InputStatus.INACTIVE)
        {
            return ArcadeAxis.INACTIVE;
        }

        //if so, continue below
        //up
        else if (_angle == up || (_angle > (up + -axisLimit) && _angle < (up + axisLimit)))
            return ArcadeAxis.UP;
        //right
        else if (_angle == right || (_angle > (right + -axisLimit) && _angle < (right + axisLimit)))
            return ArcadeAxis.RIGHT;
        //up_right
        else if (_angle == up_right || (_angle > (up_right + -axisLimit) && _angle < (up_right + axisLimit)))
            return ArcadeAxis.UP_RIGHT;
        //down
        else if (_angle == down || (_angle > (down + -axisLimit) && _angle < (down + axisLimit)))
            return ArcadeAxis.DOWN;
        //down_right
        else if (_angle == down_right || (_angle > (down_right + -axisLimit) && _angle < (down_right + axisLimit)))
            return ArcadeAxis.DOWN_RIGHT;
        //left
        else if (_angle == left || (_angle > (left + -axisLimit) && _angle < (left + axisLimit)))
            return ArcadeAxis.LEFT;
        //up_left
        else if (_angle == up_left || (_angle > (up_left + -axisLimit) && _angle < (up_left + axisLimit)))
            return ArcadeAxis.UP_LEFT;
        //down_left
        else if (_angle == down_left || (_angle > (down_left + -axisLimit) && _angle < (down_left + axisLimit)))
            return ArcadeAxis.DOWN_LEFT;
        //default case
        else
            return ArcadeAxis.INACTIVE;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// creates the game event for this gamepad
    /// <param name="_player">current player (xInput index)</param>
    /// <param name="_data">InputData class holding all of this player's data</param>
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Broadcast(int _player, XInputData _data)
    {
        //DEBUG - Event broadcast
        //Debug.Log("TEST - Event Broadcast(PLAYER("+_player +"): a button = "+_data.a.Status);

       Events.instance.Raise(new EVENT_INPUT_XINPUT_UPDATE(_player, data));
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// unsubscribe to events
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        Events.instance.RemoveListener<EVENT_INPUT_INITIALIZE_XINPUT>(InitializeXInput);
    }
    #endregion
}