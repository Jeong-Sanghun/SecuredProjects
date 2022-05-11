using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OnePost
{
    public string appearingScene;
    public SceneName appearingSceneEnum;
    public string fileName;
    public string profileName;
    public string accountName;
    public string timeText;

    public string dialog;

    [System.NonSerialized]
    public Sprite loadedSprite;

    [System.NonSerialized]
    public GameObject snsObject;

    public OnePost()
    {
        appearingScene = null;
        fileName = null;
        appearingSceneEnum = SceneName.Intro;
        profileName = null;
        accountName = null;
        timeText = null;
        dialog = null;
    }

    public void Parse()
    {
        appearingSceneEnum = (SceneName)int.Parse(appearingScene);
    }


    public Sprite GetSprite()
    {
        if (fileName == null)
        {
            return null;
        }
        if(loadedSprite == null)
        {
            loadedSprite = Resources.Load<Sprite>("Image/Twitter/" + fileName);
        }
        return loadedSprite;
    }
}
