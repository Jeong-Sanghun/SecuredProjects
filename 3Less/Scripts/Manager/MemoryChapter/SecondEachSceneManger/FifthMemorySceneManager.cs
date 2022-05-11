using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FifthMemorySceneManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject friendGirlObject;
    [SerializeField]
    Image fifthFadeImage;


    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter5");
        dialogBundle.SetCharacterEnum();
        fifthFadeImage.gameObject.SetActive(true);


        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friendGirlObject.SetActive(true);
        friendGirlObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        nowScene = SceneName.MemorySchool1;
        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(friendGirlObject, friendGirlObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(friendGirlObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
        if (!isNewGame)
        {
            StartCoroutine(moduleManager.FadeModule_Image(fifthFadeImage, 1, 0, 1));
        }



    }


    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        base.OverrideAction(keywordList, parameterList);
        for (int j = 0; j < keywordList.Count; j++)
        {
            Debug.Log(keywordList[j]);
        }
        if (gaugeManager.isGameOver == true)
        {
            return;
        }
        if (keywordList.Contains(ActionKeyword.FadeOut))
        {
            StartCoroutine(moduleManager.FadeModule_Image(fifthFadeImage, 1, 0, 1));
        }
        if (keywordList.Contains(ActionKeyword.FadeIn))
        {
            StartCoroutine(moduleManager.FadeModule_Image(fifthFadeImage, 0, 1, 1));
            StartCoroutine(InvokerCoroutine(1, NextDialog));
        }
        if (keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
        {
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryHome2));
        }
    }

    public override void TriggerEnter(string triggerName)
    {
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (triggerName.Contains("Target1"))
            {
                memoryPlayer.isPlayPossible = false;
                memoryPlayer.ToggleToSprite();
                PhoneManager.singleTon.PhoneMainCanvasActive(false);
                NextDialog();
            }
        }
    }
}
