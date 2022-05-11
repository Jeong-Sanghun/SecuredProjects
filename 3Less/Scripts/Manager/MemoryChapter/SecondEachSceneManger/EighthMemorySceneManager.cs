using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EighthMemorySceneManager : MemorySceneManagerParent
{
    [SerializeField]
    Image eighthFadeImage;
    [SerializeField]
    Text lessonEndText;


    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter8");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        nowScene = SceneName.MemoryBrightStreet1;

        if(saveData.eighthMemoryLeftTime == 2)
        {
            StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
            StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
            StartCoroutine(InvokerCoroutine(1f, NextDialog));
        }
        else
        {
            nowDialogIndex = 1;
            eighthFadeImage.gameObject.SetActive(true);
            eighthFadeImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(moduleManager.FadeModule_Text(lessonEndText, 0, 1, 1));
            StartCoroutine(moduleManager.AfterRunCoroutine(2,moduleManager.FadeModule_Image(eighthFadeImage, 1, 0, 1)));
            StartCoroutine(moduleManager.AfterRunCoroutine(1,moduleManager.FadeModule_Text(lessonEndText, 1, 0, 1)));

            StartCoroutine(moduleManager.AfterRunCoroutine(3, moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f)));
            StartCoroutine(moduleManager.AfterRunCoroutine(3, moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f)));
            StartCoroutine(InvokerCoroutine(4f, NextDialog));
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
            Debug.Log("¿Ö¾ÈµÅ");
            return;
        }
        if (keywordList.Contains(ActionKeyword.FadeIn))
        { 
            eighthFadeImage.gameObject.SetActive(true);
            eighthFadeImage.color = new Color(0, 0, 0, 0);
            StartCoroutine(moduleManager.FadeModule_Image(eighthFadeImage, 0, 1, 1));
        }
        if (keywordList.Contains(ActionKeyword.FadeOut))
        {
            //if (eighthFadeImage.gameObject.activeSelf == true)
            //{
            //    eighthFadeImage.color = new Color(0, 0, 0, 1);
            //    StartCoroutine(moduleManager.FadeModule_Image(eighthFadeImage, 1, 0, 1));
            //}
            eighthFadeImage.gameObject.SetActive(true);
            eighthFadeImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(moduleManager.FadeModule_Image(eighthFadeImage, 1, 0, 1));
        }
        if (keywordList.Contains(ActionKeyword.ConditionalJump))
        {
            if(saveData.eighthMemoryLeftTime == 0)
            {
                nowDialogIndex += (int)parameterList[0];
                Debug.Log("ÀÏ¾î³µÀ½"+ (int)parameterList[0]);
            }
        }
    }

    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            if (saveData.eighthMemoryLeftTime == 0)
            {
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryHome3));
            }
            else
            {
                saveData.eighthMemoryLeftTime--;
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryBrightStreet1));
            }
        }
    }

}
