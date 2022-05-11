using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ActionKeyword
{
    Null,First, Second, Third,Fourth,Fifth,Sixth,Seventh,End,
    Tree,Banchan,Panza,Drop,
    StopSeconds, FishMove, PlayerMove,FadeOut,FadeIn, ZoomOut, ImgFlashback,ImgFalse,
    OtherMove,
    Bubble, Scissors,Medal,
    ImmediateDialog, Route,
    HealthGauge,MoneyGauge,
    MultiRoute,
    ConditionalJump,
    ConditionalSceneEnd,
    Touch, Drag,
    PhoneOn,PhoneOff,BrokenSound,
    SoundMessageAlarm,
    DoorSound,
    Scene
}


public class ActionClass
{
    public List<ActionKeyword> actionList;
    public List<float> parameterList;

    public ActionClass()
    {
        actionList = new List<ActionKeyword>();
        parameterList = new List<float>();
    }
}
