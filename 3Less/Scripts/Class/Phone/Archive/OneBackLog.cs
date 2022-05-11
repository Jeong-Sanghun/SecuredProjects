using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackLogType
{
    Route,Talk,HealthGauge,MoneyGauge
}
[System.Serializable]
public class OneBackLog
{
    public Character character;
    public BackLogType backLogType;
    public int dialogIndex;
    public int choosedRouteIndex;
    public bool isRouteTalk;
    public int change;

    [System.NonSerialized]
    public GameObject backLogBallonObject;
    [System.NonSerialized]
    public string dialog;

    public OneBackLog()
    {
        character = Character.Player;
        backLogType = BackLogType.Talk;
        dialogIndex = 0;
        choosedRouteIndex = -1;
    }

    public void SetObject(GameObject obj)
    {
        backLogBallonObject = obj;
    }

    public void SetBackLog(Character character, int index,bool isRouteTalk)
    {
        dialogIndex = index;
        this.character = character;
        backLogType = BackLogType.Talk;
        this.isRouteTalk = isRouteTalk;
    }


    public void SetRouteBackLog(int _dialogIndex, int routeIndex)
    {
        backLogType = BackLogType.Route;
        choosedRouteIndex = routeIndex;
        dialogIndex = _dialogIndex;
    }

    public void SetMoneyGaugeLog(int change)
    {
        backLogType = BackLogType.MoneyGauge;
        this.change = change;
        dialog = "µ· " + change.ToString();
    }

    public void SetHealthGaugeLog(int change)
    {
        backLogType = BackLogType.HealthGauge;
        this.change = change;
        dialog = "Á¤¼­ " + change.ToString();
    }

}
