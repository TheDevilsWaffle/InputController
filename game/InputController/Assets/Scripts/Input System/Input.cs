﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputType.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;


public class InputType : MonoBehaviour
{
    [Header("Player")]
    [Range(1,4)]
    public int player = 1;

    protected virtual void Awake()
    {
        //correct player number for easier list use
        --player;
    }
}