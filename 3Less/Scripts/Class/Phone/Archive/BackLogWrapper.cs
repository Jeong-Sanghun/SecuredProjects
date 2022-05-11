using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackLogWrapper
{
    public SceneName sceneName;
    public List<OneBackLog> backLogList;

    [System.NonSerialized]
    public BackLogDialogPair backLogDialogPair;
    [System.NonSerialized]
    public Transform ballonParent;
    [System.NonSerialized]
    public GameObject archiveButtonObject;
    [System.NonSerialized]
    public GameObject backLogCanvas;


    public BackLogWrapper()
    {
        sceneName = SceneName.Intro;
        backLogList = new List<OneBackLog>();
    }

    public void SetBackLogPair(SceneName scene)
    {
        sceneName = scene;
        JsonManager json = new JsonManager();
        backLogDialogPair = json.GetDialogPair(scene);
        
    }
}
