﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Player.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

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

public class Player : MonoBehaviour
{
    #region FIELDS
    [Header("Player Number")]
    [SerializeField]
    int playerNumber;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public Player(int _playerNumber)
    {
        playerNumber = _playerNumber;
    }
    #endregion
}