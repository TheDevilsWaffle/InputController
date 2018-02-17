﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — MouseData.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class MouseData
{
    #region FIELDS
    Vector3 pixelPosition;
    public Vector3 PixelPosition
    {
        get { return pixelPosition; }
        set { pixelPosition = value; }
    }
    Vector3 percentagePosition;
    public Vector3 PercentagePosition
    {
        get { return percentagePosition; }
        set { percentagePosition = value; }
    }
    Vector3 percentagePositionCorrected;
    public Vector3 PercentagePositionCorrected
    {
        get { return percentagePositionCorrected; }
        set { percentagePositionCorrected = value; }
    }
    bool isMouseActive;
    public bool IsMouseActive
    {
        get { return isMouseActive; }
        private set { isMouseActive = value; }
    }
    InputStatus m1;
    public InputStatus M1Status
    {
        get { return m1; }
        private set { m1 = value; }
    }
    InputStatus m2;
    public InputStatus M2Status
    {
        get { return m2; }
        private set { m2 = value; }
    }
    ArcadeAxis mouseCurrentDirection;
    public ArcadeAxis MouseCurrentDirection
    {
        get { return mouseCurrentDirection; }
        set { mouseCurrentDirection = value; }
    }
    Vector2 mouseCurrentDirectionValue;
    public Vector2 MouseCurrentDirectionValue
    {
        get { return mouseCurrentDirectionValue; }
        set { mouseCurrentDirectionValue = value; }
    }
    float inactivityTimer = 0f;
    #endregion
    #region PUBLIC METHODS
    public void SetMouseActive(bool _active)
    {
        IsMouseActive = _active;
    }
    public void SetMouseButtonStatus(int _button, InputStatus _status)
    {
        switch (_button)
        {
            case 0:
                M1Status = _status;
                break;
            case 1:
                M2Status = _status;
                break;
            default:
                break;
        }
    }
    #endregion
}