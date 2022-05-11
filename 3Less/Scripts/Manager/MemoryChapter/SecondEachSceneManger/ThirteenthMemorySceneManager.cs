using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirteenthMemorySceneManager : PhoneDialogManager
{
    [SerializeField]
    GameObject policeObject;
    [SerializeField]
    Image thirteenthFadeImage;
    protected override void Start()
    {

        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter13");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        policeObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryBrightStreet2;
            StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
            StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
            StartCoroutine(InvokerCoroutine(1f, NextDialog));

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
        if (keywordList.Contains(ActionKeyword.OtherMove))
        {
            PoliceMove();
        }
        if (keywordList.Contains(ActionKeyword.FadeIn))
        {
            StartCoroutine(moduleManager.FadeModule_Image(thirteenthFadeImage, 0, 1, 1));
        }
        if (keywordList.Contains(ActionKeyword.FadeOut))
        {
            StartCoroutine(moduleManager.FadeModule_Image(thirteenthFadeImage, 1, 0, 1));
        }
    }

    void PoliceMove()
    {
        StartCoroutine(moduleManager.MoveModule_Linear(policeObject, policeObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(policeObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }

    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.Chapter2Bright));
        }
    }
}
