using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwentyNinethMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject bossObject;
    [SerializeField]
    GameObject brotherObject;


    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter18");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        bossObject.SetActive(true);
        bossObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        brotherObject.SetActive(true);
        brotherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryStore4;
        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));

        if (nowDialogIndex != 0)
        {
            BrotherMove();
        }
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
        if (keywordList.Contains(ActionKeyword.FishMove))
        {
            if (keywordList.Contains(ActionKeyword.First))
            {
                BrotherMove();
            }
            if (keywordList.Contains(ActionKeyword.Second))
            {
                BossMove();
            }
        }

    }

    void BrotherMove()
    {
        isDialogStopping = true;
        TextFrameToggle(false);
        StartCoroutine(moduleManager.MoveModule_Linear(brotherObject, brotherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(brotherObject, 0, 1, 1));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }

    void BossMove()
    {
        isDialogStopping = true;
        TextFrameToggle(false);
        StartCoroutine(moduleManager.MoveModule_Linear(bossObject, bossObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(bossObject, 0, 1, 1));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }


    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryDarkStreet2));
        }
    }
}
