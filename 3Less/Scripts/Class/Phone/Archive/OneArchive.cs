using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneArchive
{
    public SceneName sceneNameEnum;
    public string dialog;

    [System.NonSerialized]
    public GameObject archiveParent;

    public OneArchive()
    {
        sceneNameEnum = SceneName.Intro;
        dialog = null;
    }


}
