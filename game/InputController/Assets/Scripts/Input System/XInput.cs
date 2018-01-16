///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XInput.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

#region ENUMS
#endregion

#region EVENTS
public class EVENT_XINPUT_P1 : GameEvent
{
    public XInputData gamepad;
    public EVENT_XINPUT_P1(XInputData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_XINPUT_P2 : GameEvent
{
    public XInputData gamepad;
    public EVENT_XINPUT_P2(XInputData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_XINPUT_P3 : GameEvent
{
    public XInputData gamepad;
    public EVENT_XINPUT_P3(XInputData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_XINPUT_P4 : GameEvent
{
    public XInputData gamepad;
    public EVENT_XINPUT_P4(XInputData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_XINPUT_GAMEPAD_DETECTION_LOST : GameEvent
{
    public int playerNumber;
    public EVENT_XINPUT_GAMEPAD_DETECTION_LOST(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
public class EVENT_XINPUT_GAMEPAD_DETECTION_ACQUIRED : GameEvent
{
    public int playerNumber;
    public EVENT_XINPUT_GAMEPAD_DETECTION_ACQUIRED(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
}
#endregion

public class XInput : InputType
{
    #region FIELDS
    [Header("ENABLE/DISABLE")]
    public bool isEnabled = true;
    bool isReady = false;

    [Header("DEAD ZONES")]
    [Range(0, 1)]
    [SerializeField]
    float triggerDeadZone = 0.2f;
    [Range(0, 1)]
    [SerializeField]
    float analogStickDeadZone = 0.2f;

    [Header("MAX")]
    [SerializeField]
    float maxHeldDuration = 3f;
    [SerializeField]
    float maxInactiveDuration = 3f;
    [SerializeField]
    int maxButtonsRemembered = 10;

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

    //gamepad states
    GamePadState currentState;
    GamePadState previousState;

    //gamepads/players
    [HideInInspector]
    public static List<XInputData> gamepads;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //set/check initial values
        up = -90f;
        up_right = -45f;
        right = 0f;
        down_right = 45f;
        down = 90f;
        down_left = 135f;
        left = -180f;
        up_left = -135f;
        axisLimit = 22.5f;

        isReady = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    protected override void Awake()
    {
        base.Awake();

        //create list to store gamepad data
        gamepads = new List<XInputData>();

        //if gamepads are enabled in inspector, initialize gamepads
        if (isEnabled)
        {
            InitializeGamepads();
        }
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        //gamepad enabled? go through the update loop
        if (isEnabled && isReady)
        {
            //ensure that this gamepad is still connected
            GamePadState _testState = GamePad.GetState((PlayerIndex)player);
            if (_testState.IsConnected)
            {
                //update the previous and currentstate
                previousState = currentState;
                currentState = GamePad.GetState((PlayerIndex)player);

                //check current gamepad dpad/sticks/bumpers/triggers for input
                UpdateDPad(player, previousState, currentState);
                UpdateSticks(player, previousState, currentState);
                UpdateButtons(player, previousState, currentState);
                UpdateBumpers(player, previousState, currentState);
                UpdateTriggers(player, previousState, currentState);

                //broadcast event with updated gamepad information for current gamepad
                Broadcast(player);
            }
            //something is wrong with gamepads, we are no longer ready (possibly unplugged gamepad)
            else
            {
                //stop looping through gamepad update
                isReady = false;

                //broadcast gamepad detection lost event
                Events.instance.Raise(new EVENT_XINPUT_GAMEPAD_DETECTION_LOST(player));

                //DEBUG
                //Debug.LogWarning("WARNING! Player(" + _index + ") no longer exists? ");
            }
        }
        else
        {
            //DEBUG
            //Debug.Log("attempting to repopulate gamepads");

            //attempt to repopulate gamepads
            InitializeGamepads();
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// populates gamepads list with gamepads based on inspector set players
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    void InitializeGamepads()
    {
        gamepads.Clear();

        GamePadState _testState = GamePad.GetState((PlayerIndex)player);
        if (_testState.IsConnected)
        {
            //broadcast gamepad acquired event
            Events.instance.Raise(new EVENT_XINPUT_GAMEPAD_DETECTION_ACQUIRED(player));

            //add a XInputData to list for this gamepad
            gamepads.Add(new XInputData());
            isReady = true;
            //DEBUG
            //Debug.Log("Added a gamepad to gamepads("+_index+")");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the DPad
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateDPad(int _index, GamePadState _previous, GamePadState _current)
    {
        #region DPAD UP
        //RELEASED
        if (_previous.DPad.Up == ButtonState.Pressed && _current.DPad.Up == ButtonState.Released)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_up.SetXYValue(0f, 0f);
            gamepads[_index].dp_up.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Up == ButtonState.Pressed && _current.DPad.Up == ButtonState.Pressed)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_up.SetXYValue(1f, 1f);
            gamepads[_index].dp_up.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_up.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Up == ButtonState.Released && _current.DPad.Up == ButtonState.Pressed)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_up.SetXYValue(1f, 1f);
            gamepads[_index].dp_up.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_up);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_up.SetXYValue(0f, 0f);
            gamepads[_index].dp_up.SetHeldDuration(0f);
            gamepads[_index].dp_up.SetInactiveDuration(Time.deltaTime);
        }

        
        #endregion
        #region DPAD RIGHT
        //RELEASED
        if (_previous.DPad.Right == ButtonState.Pressed && _current.DPad.Right == ButtonState.Released)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_right.SetXYValue(0f, 0f);
            gamepads[_index].dp_right.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Right == ButtonState.Pressed && _current.DPad.Right == ButtonState.Pressed)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_right.SetXYValue(1f, 1f);
            gamepads[_index].dp_right.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_right.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Right == ButtonState.Released && _current.DPad.Right == ButtonState.Pressed)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_right.SetXYValue(1f, 1f);
            gamepads[_index].dp_right.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_right);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_right.SetXYValue(0f, 0f);
            gamepads[_index].dp_right.SetHeldDuration(0f);
            gamepads[_index].dp_right.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD DOWN
        //RELEASED
        if (_previous.DPad.Down == ButtonState.Pressed && _current.DPad.Down == ButtonState.Released)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_down.SetXYValue(0f, 0f);
            gamepads[_index].dp_down.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Down == ButtonState.Pressed && _current.DPad.Down == ButtonState.Pressed)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_down.SetXYValue(1f, 1f);
            gamepads[_index].dp_down.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_down.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Down == ButtonState.Released && _current.DPad.Down == ButtonState.Pressed)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_down.SetXYValue(1f, 1f);
            gamepads[_index].dp_down.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_down);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_down.SetXYValue(0f, 0f);
            gamepads[_index].dp_down.SetHeldDuration(0f);
            gamepads[_index].dp_down.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD LEFT
        //RELEASED
        if (_previous.DPad.Left == ButtonState.Pressed && _current.DPad.Left == ButtonState.Released)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_left.SetXYValue(0f, 0f);
            gamepads[_index].dp_left.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Left == ButtonState.Pressed && _current.DPad.Left == ButtonState.Pressed)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_left.SetXYValue(1f, 1f);
            gamepads[_index].dp_left.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_left.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Left == ButtonState.Released && _current.DPad.Left == ButtonState.Pressed)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_left.SetXYValue(1f, 1f);
            gamepads[_index].dp_left.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_left);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_left.SetXYValue(0f, 0f);
            gamepads[_index].dp_left.SetHeldDuration(0f);
            gamepads[_index].dp_left.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the left/right analog sticks (including l3/r3)
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateSticks(int _index, GamePadState _previous, GamePadState _current)
    {
        //analog sticks !REMEMBER that angles are funky:
        //(UP is 90° RIGHT = 0°, DOWN = -90°, and LEFT = 180°)
        #region LEFT ANALOG STICK

        //store these values from the stick no matter what
        gamepads[_index].ls.SetXYValue(new Vector3(_current.ThumbSticks.Left.X, _current.ThumbSticks.Left.Y));
        gamepads[_index].ls.SetXYRawValue(new Vector3(Mathf.Ceil(_current.ThumbSticks.Left.X), Mathf.Ceil(_current.ThumbSticks.Left.Y)));
        gamepads[_index].ls.SetAngle(Mathf.Ceil(Mathf.Atan2(-_current.ThumbSticks.Left.Y, _current.ThumbSticks.Left.X) * Mathf.Rad2Deg));
        gamepads[_index].ls.SetArcadeAxis(DetermineArcadeAxis(gamepads[_index].ls.Status, gamepads[_index].ls.Angle));

        //check to see if the value of x and y is outside tolerance deadzone
        if (_current.ThumbSticks.Left.X < -analogStickDeadZone ||
            _current.ThumbSticks.Left.X > analogStickDeadZone ||
            _current.ThumbSticks.Left.Y < -analogStickDeadZone ||
            _current.ThumbSticks.Left.Y > analogStickDeadZone)
        {
            if(gamepads[_index].ls.Status == InputStatus.INACTIVE)
                gamepads[_index].ls.SetStatus(InputStatus.PRESSED);
            else
                gamepads[_index].ls.SetStatus(InputStatus.HELD);
        }

        //else, we're inside the deadzone, update status
        else if (_current.ThumbSticks.Left.X > -analogStickDeadZone ||
                _current.ThumbSticks.Left.X < analogStickDeadZone ||
                _current.ThumbSticks.Left.Y > -analogStickDeadZone ||
                _current.ThumbSticks.Left.Y < analogStickDeadZone)
        {
            if(gamepads[_index].ls.Status == InputStatus.HELD)
                gamepads[_index].ls.SetStatus(InputStatus.RELEASED);
            else
                gamepads[_index].ls.SetStatus(InputStatus.INACTIVE);
        }

        //RELEASED
        if (_previous.Buttons.LeftStick == ButtonState.Pressed && _current.Buttons.LeftStick == ButtonState.Released)
        {
            gamepads[_index].l3.SetStatus(InputStatus.RELEASED);
            gamepads[_index].l3.SetXYValue(0f, 0f);
            gamepads[_index].l3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.LeftStick == ButtonState.Pressed && _current.Buttons.LeftStick == ButtonState.Pressed)
        {
            gamepads[_index].l3.SetStatus(InputStatus.HELD);
            gamepads[_index].l3.SetXYValue(1f, 1f);
            gamepads[_index].l3.SetHeldDuration(Time.deltaTime);
            gamepads[_index].l3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.LeftStick == ButtonState.Released && _current.Buttons.LeftStick == ButtonState.Pressed)
        {
            gamepads[_index].l3.SetStatus(InputStatus.PRESSED);
            gamepads[_index].l3.SetXYValue(1f, 1f);
            gamepads[_index].l3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].l3);
        }
        //INACTIVE
        else
        {
            gamepads[_index].l3.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].l3.SetXYValue(0f, 0f);
            gamepads[_index].l3.SetHeldDuration(0f);
            gamepads[_index].l3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RIGHT ANALOG STICK

        //store these values from the stick no matter what
        gamepads[_index].rs.SetXYValue(new Vector3(_current.ThumbSticks.Right.X, _current.ThumbSticks.Right.Y));
        gamepads[_index].rs.SetXYRawValue(new Vector3(Mathf.Ceil(_current.ThumbSticks.Right.X), Mathf.Ceil(_current.ThumbSticks.Right.Y)));
        gamepads[_index].rs.SetAngle(Mathf.Ceil(Mathf.Atan2(-_current.ThumbSticks.Right.Y, _current.ThumbSticks.Right.X) * Mathf.Rad2Deg));
        gamepads[_index].rs.SetArcadeAxis(DetermineArcadeAxis(gamepads[_index].rs.Status, gamepads[_index].rs.Angle));

        //check to see if the value of x and y is inside tolerance deadzone
        if (_current.ThumbSticks.Right.X < -analogStickDeadZone ||
            _current.ThumbSticks.Right.X > analogStickDeadZone ||
            _current.ThumbSticks.Right.Y < -analogStickDeadZone ||
            _current.ThumbSticks.Right.Y > analogStickDeadZone)
        {
            if(gamepads[_index].rs.Status == InputStatus.INACTIVE)
                gamepads[_index].rs.SetStatus(InputStatus.PRESSED);
            else
                gamepads[_index].rs.SetStatus(InputStatus.HELD);
        }

        //else, we're outside the deadzone, update status
        else if (_current.ThumbSticks.Right.X > -analogStickDeadZone ||
                _current.ThumbSticks.Right.X < analogStickDeadZone ||
                _current.ThumbSticks.Right.Y > -analogStickDeadZone ||
                _current.ThumbSticks.Right.Y < analogStickDeadZone)
        {
            if(gamepads[_index].rs.Status == InputStatus.HELD)
                gamepads[_index].rs.SetStatus(InputStatus.RELEASED);
            else
                gamepads[_index].rs.SetStatus(InputStatus.INACTIVE);
        }

        //RELEASED
        if (_previous.Buttons.RightStick == ButtonState.Pressed && _current.Buttons.RightStick == ButtonState.Released)
        {
            gamepads[_index].r3.SetStatus(InputStatus.RELEASED);
            gamepads[_index].r3.SetXYValue(0f, 0f);
            gamepads[_index].r3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.RightStick == ButtonState.Pressed && _current.Buttons.RightStick == ButtonState.Pressed)
        {
            gamepads[_index].r3.SetStatus(InputStatus.HELD);
            gamepads[_index].r3.SetXYValue(1f, 1f);
            gamepads[_index].r3.SetHeldDuration(Time.deltaTime);
            gamepads[_index].r3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.RightStick == ButtonState.Released && _current.Buttons.RightStick == ButtonState.Pressed)
        {
            gamepads[_index].r3.SetStatus(InputStatus.PRESSED);
            gamepads[_index].r3.SetXYValue(1f, 1f);
            gamepads[_index].r3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].r3);
        }
        //INACTIVE
        else
        {
            gamepads[_index].r3.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].r3.SetXYValue(0f, 0f);
            gamepads[_index].r3.SetHeldDuration(0f);
            gamepads[_index].r3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the buttons
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateButtons(int _index, GamePadState _previous, GamePadState _current)
    {
        #region Y BUTTON
        //RELEASED
        if (_previous.Buttons.Y == ButtonState.Pressed && _current.Buttons.Y == ButtonState.Released)
        {
            gamepads[_index].y.SetStatus(InputStatus.RELEASED);
            gamepads[_index].y.SetXYValue(0f, 0f);
            gamepads[_index].y.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Y == ButtonState.Pressed && _current.Buttons.Y == ButtonState.Pressed)
        {
            gamepads[_index].y.SetStatus(InputStatus.HELD);
            gamepads[_index].y.SetHeldDuration(Time.deltaTime);
            gamepads[_index].y.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Y == ButtonState.Released && _current.Buttons.Y == ButtonState.Pressed)
        {
            gamepads[_index].y.SetStatus(InputStatus.PRESSED);
            gamepads[_index].y.SetXYValue(1f, 1f);
            gamepads[_index].y.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].y);
        }
        //INACTIVE
        else
        {
            gamepads[_index].y.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].y.SetXYValue(0f, 0f);
            gamepads[_index].y.SetHeldDuration(0f);
            gamepads[_index].y.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region B BUTTON
        //RELEASED
        if (_previous.Buttons.B == ButtonState.Pressed && _current.Buttons.B == ButtonState.Released)
        {
            gamepads[_index].b.SetStatus(InputStatus.RELEASED);
            gamepads[_index].b.SetXYValue(0f, 0f);
            gamepads[_index].b.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.B == ButtonState.Pressed && _current.Buttons.B == ButtonState.Pressed)
        {
            gamepads[_index].b.SetStatus(InputStatus.HELD);
            gamepads[_index].b.SetHeldDuration(Time.deltaTime);
            gamepads[_index].b.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.B == ButtonState.Released && _current.Buttons.B == ButtonState.Pressed)
        {
            gamepads[_index].b.SetStatus(InputStatus.PRESSED);
            gamepads[_index].b.SetXYValue(1f, 1f);
            gamepads[_index].b.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].b);
        }
        //INACTIVE
        else
        {
            gamepads[_index].b.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].b.SetXYValue(0f, 0f);
            gamepads[_index].b.SetHeldDuration(0f);
            gamepads[_index].b.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region A BUTTON
        //RELEASED
        if (_previous.Buttons.A == ButtonState.Pressed && _current.Buttons.A == ButtonState.Released)
        {
            gamepads[_index].a.SetStatus(InputStatus.RELEASED);
            gamepads[_index].a.SetXYValue(0f, 0f);
            gamepads[_index].a.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.A == ButtonState.Pressed && _current.Buttons.A == ButtonState.Pressed)
        {
            gamepads[_index].a.SetStatus(InputStatus.HELD);
            gamepads[_index].a.SetHeldDuration(Time.deltaTime);
            gamepads[_index].a.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.A == ButtonState.Released && _current.Buttons.A == ButtonState.Pressed)
        {
            gamepads[_index].a.SetStatus(InputStatus.PRESSED);
            gamepads[_index].a.SetXYValue(1f, 1f);
            gamepads[_index].a.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].a);
        }
        //INACTIVE
        else
        {
            gamepads[_index].a.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].a.SetXYValue(0f, 0f);
            gamepads[_index].a.SetHeldDuration(0f);
            gamepads[_index].a.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region X BUTTON
        //RELEASED
        if (_previous.Buttons.X == ButtonState.Pressed && _current.Buttons.X == ButtonState.Released)
        {
            gamepads[_index].x.SetStatus(InputStatus.RELEASED);
            gamepads[_index].x.SetXYValue(0f, 0f);
            gamepads[_index].x.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.X == ButtonState.Pressed && _current.Buttons.X == ButtonState.Pressed)
        {
            gamepads[_index].x.SetStatus(InputStatus.HELD);
            gamepads[_index].x.SetHeldDuration(Time.deltaTime);
            gamepads[_index].x.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.X == ButtonState.Released && _current.Buttons.X == ButtonState.Pressed)
        {
            gamepads[_index].x.SetStatus(InputStatus.PRESSED);
            gamepads[_index].x.SetXYValue(1f, 1f);
            gamepads[_index].x.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].x);
        }
        //INACTIVE
        else
        {
            gamepads[_index].x.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].x.SetXYValue(0f, 0f);
            gamepads[_index].x.SetHeldDuration(0f);
            gamepads[_index].x.SetInactiveDuration(Time.deltaTime);
        }
        #endregion

        #region VIEW BUTTON
        //RELEASED
        if (_previous.Buttons.Back == ButtonState.Pressed && _current.Buttons.Back == ButtonState.Released)
        {
            gamepads[_index].view.SetStatus(InputStatus.RELEASED);
            gamepads[_index].view.SetXYValue(0f, 0f);
            gamepads[_index].view.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Back == ButtonState.Pressed && _current.Buttons.Back == ButtonState.Pressed)
        {
            gamepads[_index].view.SetStatus(InputStatus.HELD);
            gamepads[_index].view.SetHeldDuration(Time.deltaTime);
            gamepads[_index].view.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Back == ButtonState.Released && _current.Buttons.Back == ButtonState.Pressed)
        {
            gamepads[_index].view.SetStatus(InputStatus.PRESSED);
            gamepads[_index].view.SetXYValue(1f, 1f);
            gamepads[_index].view.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].view);
        }
        //INACTIVE
        else
        {
            gamepads[_index].view.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].view.SetXYValue(0f, 0f);
            gamepads[_index].view.SetHeldDuration(0f);
            gamepads[_index].view.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region MENU BUTTON
        //RELEASED
        if (_previous.Buttons.Start == ButtonState.Pressed && _current.Buttons.Start == ButtonState.Released)
        {
            gamepads[_index].menu.SetStatus(InputStatus.RELEASED);
            gamepads[_index].menu.SetXYValue(0f, 0f);
            gamepads[_index].menu.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Start == ButtonState.Pressed && _current.Buttons.Start == ButtonState.Pressed)
        {
            gamepads[_index].menu.SetStatus(InputStatus.HELD);
            gamepads[_index].menu.SetHeldDuration(Time.deltaTime);
            gamepads[_index].menu.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Start == ButtonState.Released && _current.Buttons.Start == ButtonState.Pressed)
        {
            gamepads[_index].menu.SetStatus(InputStatus.PRESSED);
            gamepads[_index].menu.SetXYValue(1f, 1f);
            gamepads[_index].menu.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].menu);
        }
        //INACTIVE
        else
        {
            gamepads[_index].menu.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].menu.SetXYValue(0f, 0f);
            gamepads[_index].menu.SetHeldDuration(0f);
            gamepads[_index].menu.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the bumpers
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateBumpers(int _index, GamePadState _previous, GamePadState _current)
    {
        #region LB
        //RELEASED
        if (_previous.Buttons.LeftShoulder == ButtonState.Pressed && _current.Buttons.LeftShoulder == ButtonState.Released)
        {
            gamepads[_index].lb.SetStatus(InputStatus.RELEASED);
            gamepads[_index].lb.SetXYValue(0f, 0f);
            gamepads[_index].lb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.LeftShoulder == ButtonState.Pressed && _current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            gamepads[_index].lb.SetStatus(InputStatus.HELD);
            gamepads[_index].lb.SetHeldDuration(Time.deltaTime);
            gamepads[_index].lb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.LeftShoulder == ButtonState.Released && _current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            gamepads[_index].lb.SetStatus(InputStatus.PRESSED);
            gamepads[_index].lb.SetXYValue(1f, 1f);
            gamepads[_index].lb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].lb);
        }
        //INACTIVE
        else
        {
            gamepads[_index].lb.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].lb.SetXYValue(0f, 0f);
            gamepads[_index].lb.SetHeldDuration(0f);
            gamepads[_index].lb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RB
        //RELEASED
        if (_previous.Buttons.RightShoulder == ButtonState.Pressed && _current.Buttons.RightShoulder == ButtonState.Released)
        {
            gamepads[_index].rb.SetStatus(InputStatus.RELEASED);
            gamepads[_index].rb.SetXYValue(0f, 0f);
            gamepads[_index].rb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.RightShoulder == ButtonState.Pressed && _current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            gamepads[_index].rb.SetStatus(InputStatus.HELD);
            gamepads[_index].rb.SetHeldDuration(Time.deltaTime);
            gamepads[_index].rb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.RightShoulder == ButtonState.Released && _current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            gamepads[_index].rb.SetStatus(InputStatus.PRESSED);
            gamepads[_index].rb.SetXYValue(1f, 1f);
            gamepads[_index].rb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].rb);
        }
        //INACTIVE
        else
        {
            gamepads[_index].rb.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].rb.SetXYValue(0f, 0f);
            gamepads[_index].rb.SetHeldDuration(0f);
            gamepads[_index].rb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the triggers
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTriggers(int _index, GamePadState _previous, GamePadState _current)
    {
        #region LT
        //RELEASED
        if (gamepads[_index].lt.Status == InputStatus.PRESSED && _current.Triggers.Left < triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.RELEASED);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (gamepads[_index].lt.Status == InputStatus.PRESSED || gamepads[_index].lt.Status == InputStatus.HELD && _current.Triggers.Left > triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.HELD);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(Time.deltaTime);
            gamepads[_index].lt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (gamepads[_index].lt.Status == InputStatus.INACTIVE && _current.Triggers.Left > triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.PRESSED);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].lt);
        }
        //INACTIVE
        else
        {
            gamepads[_index].lt.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(0f);
            gamepads[_index].lt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RT
        //RELEASED
        if (gamepads[_index].rt.Status == InputStatus.PRESSED && _current.Triggers.Right < triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.RELEASED);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (gamepads[_index].rt.Status == InputStatus.PRESSED || gamepads[_index].rt.Status == InputStatus.HELD && _current.Triggers.Right > triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.HELD);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(Time.deltaTime);
            gamepads[_index].rt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (gamepads[_index].rt.Status == InputStatus.INACTIVE && _current.Triggers.Right > triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.PRESSED);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].rt);
        }
        //INACTIVE
        else
        {
            gamepads[_index].rt.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(0f);
            gamepads[_index].rt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// tracks the last 'x' buttons pressed from the current player's gamepad
    /// </summary>
    /// <param name="_index">current player index</param>
    /// <param name="_button">last button pressed</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void UpdateCombo(int _index, InputData _button)
    {
        //add in the new button
        gamepads[_index].comboTracker.Enqueue(_button);

        //dequeue the oldest button if we're at max rememberence
        if (gamepads[_index].comboTracker.Count > maxButtonsRemembered)
        {
            gamepads[_index].comboTracker.Dequeue();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ArcadeAxis and the angle of the stick are used to create a retro joystick feel
    /// </summary>
    /// <param name="_status">is the stick currently being used</param>
    /// <param name="_angle">angle of the stick</param>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public ArcadeAxis DetermineArcadeAxis(InputStatus _status, float _angle)
    {
        //is the stick active?
        if (_status == InputStatus.INACTIVE)
            return ArcadeAxis.INACTIVE;

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
    /// creates the game event for each gamepad connected (1 – 4 gamepads connected)
    /// </summary>
    /// <param name="_index">gamepad/player index (0 = p1, 1 = p2, etc.)</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Broadcast(int _index)
    {
        //TEST - Event broadcast
        //Debug.Log("TEST - Event Broadcast("+_index +")");

        switch (_index)
        {
            case 0:
                Events.instance.Raise(new EVENT_XINPUT_P1(gamepads[_index]));
                break;
            case 1:
                Events.instance.Raise(new EVENT_XINPUT_P2(gamepads[_index]));
                break;
            case 2:
                Events.instance.Raise(new EVENT_XINPUT_P3(gamepads[_index]));
                break;
            case 3:
                Events.instance.Raise(new EVENT_XINPUT_P4(gamepads[_index]));
                break;
            default:
                Debug.LogWarning("INCORRECT PLAYER INDEX DETECTED(PLAYER " + _index+" ?)!");
                break;
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove event listeners
    }
    #endregion

}