using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public enum ActionKeyword
{
    Null,VisitorUp,VisitorDown,VisitorBallonGlow,
    Delay,BookGlow,RightPageGlow,EffectIconGlow,
    ExitGlow,CounterSymptomChartGlow, WaterPlusGlow,
    CounterChartExitButtonGlow,Jump, ToRoomButtonGlow,
    RoomSymptomChartGlow, RoomChartExitButtonGlow,
    ItemForceChoose, FireIconGlow, WaterSubIconGlow,
    AddDesireGlow, AddFrostGlow, CookButtonClick, TrayGlow, GetCoin, SceneEnd,

    TabletButtonGlow,CariButtonGlow,TreeterButtonGlow,
    TreeterPostButtonGlow,TabletExitButtonGlow,

    WitchGraveMapiconGlow, WreckMapiconGlow




}


public class ActionClass
{
    public ActionKeyword action;
    public float parameter;

    public ActionClass()
    {
        
    }
}
