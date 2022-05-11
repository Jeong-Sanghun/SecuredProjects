using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataClass
{
    public SceneName savedScene;
    public int dialogIndex;
    public int healthGauge;
    public int moneyGauge;
    public int eighthMemoryLeftTime;

    public MessageBundle messageBundle;

    public BackLogBundle backLogBundle;

    public SaveDataClass()
    {
        savedScene = SceneName.Intro;
        dialogIndex = 0;
        healthGauge = 9;
        moneyGauge = 0;
        eighthMemoryLeftTime = 2;
        messageBundle = null;
        backLogBundle = null;
    }

}
