using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwentyThirdMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject drunkenMan1;
    [SerializeField]
    GameObject drunkenMan2;
    [SerializeField]
    GameObject youngMan;

    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter12");
        dialogBundle.SetCharacterEnum();


        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        drunkenMan1.SetActive(true);
        drunkenMan1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        drunkenMan2.SetActive(true);
        drunkenMan2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        youngMan.SetActive(true);
        youngMan.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryStore3;

        StartCoroutine(moduleManager.MoveModule_Linear(drunkenMan2, drunkenMan2.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(drunkenMan2, 0, 1, 1));


        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(drunkenMan1, drunkenMan1.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(drunkenMan1, 0, 1, 1f));
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
            BrotherMove();
        }

    }

    void BrotherMove()
    {
        isDialogStopping = true;
        TextFrameToggle(false);
        StartCoroutine(moduleManager.MoveModule_Linear(youngMan, youngMan.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(youngMan, 0, 1, 1));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }




    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryFriendRoom5));
        }
    }


}