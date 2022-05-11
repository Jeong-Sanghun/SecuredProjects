using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMemorySceneManager : MemorySceneManagerParent
{
    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter2");
        dialogBundle.SetCharacterEnum();

        nowScene = SceneName.MemoryRestaurant;

        StartCoroutine(InvokerCoroutine(1f, NextDialog));

    }

    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        base.OverrideAction(keywordList, parameterList);
        if(gaugeManager.isGameOver == true)
        {
            return;
        }
        for (int j = 0; j < keywordList.Count; j++)
        {
            Debug.Log(keywordList[j]);
        }
        if (keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
        {
            gameManager.isNewGame = true;
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryDarkStreet1));
        }
    }

}
