﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputStatusAction.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[System.Serializable]
public class InputStatusAction
{
    #region FIELDS
    [SerializeField]
    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being pressed")]
    public InputAction onPressed;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being held")]
    [SerializeField]
    public InputAction onHeld;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate upon being released")]
    [SerializeField]
    public InputAction onReleased;

    [Tooltip("Drag-and-drop a script that inherits from the 'InputAction' class to activate when inactive")]
    [SerializeField]
    public InputAction onInactive;
    #endregion
}