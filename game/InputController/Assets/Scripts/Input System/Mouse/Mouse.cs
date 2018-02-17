﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Mouse.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    bool overrideDefaultCursor;
    [Header("CUSTOM MOUSE CURSOR")]
    [SerializeField]
    Texture2D defaultCursorTexture2D;
    [SerializeField]
    Vector2 defaultCursorOffset = new Vector2(0f, 0f);

    [Header("ATTRIBUTES")]
    [SerializeField]
    [Range(0f, 0.5f)]
    float mouseDirectionSensitivity = 0.2f;
    [SerializeField]
    float delayBeforeDeclaredInactive = 3f;
    [SerializeField]
    bool hideMouseCursor = false;

    public static MouseData mouseInput = new MouseData();

    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        SetSubscriptions();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (overrideDefaultCursor)
        {
            SetCustomMouseCursor();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void RemoveSubscriptions()
    {
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
        UpdateMouseButtons();
        UpdateMousePosition();
        UpdateMouseDirection();
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///  updates the staus of the left-click mouse button
    /// </summary>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMouseButtons()
    {
        for (int _index = 0; _index < 2; ++_index)
        {
            //released
            if (Input.GetMouseButtonUp(_index))
            {
                mouseInput.SetMouseActive(true);
                mouseInput.SetMouseButtonStatus(_index, InputStatus.RELEASED);
            }
            //pressed
            else if (Input.GetMouseButtonDown(_index))
            {
                mouseInput.SetMouseActive(true);
                mouseInput.SetMouseButtonStatus(_index, InputStatus.PRESSED);
            }
            //held
            else if (Input.GetMouseButton(_index))
            {
                mouseInput.SetMouseActive(true);
                mouseInput.SetMouseButtonStatus(_index, InputStatus.HELD);
            }
            //inactive
            else
            {
                mouseInput.SetMouseButtonStatus(_index, InputStatus.INACTIVE);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMousePosition()
    {
        mouseInput.PixelPosition = Input.mousePosition;
        mouseInput.PercentagePosition = new Vector3((mouseInput.PixelPosition.x / Screen.width),
                                                   (mouseInput.PixelPosition.y / Screen.height),
                                                   mouseInput.PixelPosition.z);
        mouseInput.PercentagePositionCorrected = new Vector3(Mathf.Clamp(((mouseInput.PercentagePosition.x * 2) - 1), -1f, 1f),
                                                            Mathf.Clamp(((mouseInput.PercentagePosition.y * 2) - 1), -1f, 1f),
                                                            mouseInput.PixelPosition.z);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Updates the value/enum of the mouse's current direction
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateMouseDirection()
    {
        mouseInput.MouseCurrentDirectionValue = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //mouse is going left
        if (mouseInput.MouseCurrentDirectionValue.x < -mouseDirectionSensitivity)
        {
            mouseInput.SetMouseActive(true);

            if (mouseInput.MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.UP_LEFT;
            else if (mouseInput.MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.DOWN_LEFT;
            else
                mouseInput.MouseCurrentDirection = ArcadeAxis.LEFT;
        }
        //mouse is going right
        else if (mouseInput.MouseCurrentDirectionValue.x > mouseDirectionSensitivity)
        {
            mouseInput.SetMouseActive(true);

            if (mouseInput.MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.UP_RIGHT;
            else if (mouseInput.MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.DOWN_RIGHT;
            else
                mouseInput.MouseCurrentDirection = ArcadeAxis.RIGHT;
        }
        //mouse is going up
        else if (mouseInput.MouseCurrentDirectionValue.y > mouseDirectionSensitivity)
        {
            mouseInput.SetMouseActive(true);

            if (mouseInput.MouseCurrentDirectionValue.x < mouseDirectionSensitivity && mouseInput.MouseCurrentDirectionValue.x > -mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.UP;
        }
        //mouse is going down
        else if (mouseInput.MouseCurrentDirectionValue.y < -mouseDirectionSensitivity)
        {
            mouseInput.SetMouseActive(true);

            if (mouseInput.MouseCurrentDirectionValue.x < mouseDirectionSensitivity && mouseInput.MouseCurrentDirectionValue.x > -mouseDirectionSensitivity)
                mouseInput.MouseCurrentDirection = ArcadeAxis.DOWN;
        }
        else
        {
            mouseInput.MouseCurrentDirection = ArcadeAxis.INACTIVE;
        }
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void HideMouseCursor()
    {
        Cursor.visible = false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_texture2D"></param>
    /// <param name="_offset"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCustomMouseCursorSprite(Texture2D _texture2D, Vector2 _offset)
    {
        ShowMouseCursor();
        Cursor.SetCursor(_texture2D, _offset, CursorMode.Auto);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCustomMouseCursor()
    {
        ShowMouseCursor();
        if (defaultCursorTexture2D != null)
        {
            Cursor.SetCursor(defaultCursorTexture2D, defaultCursorOffset, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    #endregion

    #region ONDESTROY
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