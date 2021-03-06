﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zframework;
using System;
public class PanelTest1 : PanelBase
{
    [SerializeField]private Button mBtnClose = null;

    public override void OnLoad(object userData = null)
    {
        base.OnLoad(userData);

        mBtnClose.onClick.AddListener(()=>CloseSelf());
    }

    public override void OnOpen(object userData = null)
    {
        base.OnOpen(userData);
     
        Z.Debug.Log("Test1 OnOpen");
    }

    public override void OnClose(object userData = null)
    {
        base.OnClose(userData);
        Z.Debug.Log("Test1 OnClose");
    }

    public override void OnSwitch(object userData = null)
    {
        base.OnSwitch(userData);
        Z.Debug.Log("Test1 OnSwicth");
    }
}