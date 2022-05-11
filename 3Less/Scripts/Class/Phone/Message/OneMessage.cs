using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneMessage
{
    public bool isPlayer;
    public string dialog;
    public string time;

    [System.NonSerialized]
    public GameObject messageObject;

    public OneMessage()
    {
        isPlayer = true;
        dialog = null;
        time = null;
        messageObject = null;
    }

    public OneMessage(bool _isPlayer, string _dialog, string _time)
    {
        isPlayer = _isPlayer;
        dialog = _dialog;
        time = _time;
    }

    public void SetObject(GameObject obj)
    {
        messageObject = obj;
    }

}
