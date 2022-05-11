using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMemorySceneManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject motherObject;


    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter1");
        dialogBundle.SetCharacterEnum();

        motherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        memoryPlayer.ToggleToSprite();
        motherObject.SetActive(true);

        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position+Vector3.right/2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
        nowScene = SceneName.MemoryHome1;
        SaveUserData();
        cameraLeftBound = -7.7f;
        cameraRightBound = 7.7f;

    }


    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        base.OverrideAction(keywordList, parameterList);
        for (int j = 0; j < keywordList.Count; j++)
        {
            Debug.Log(keywordList[j]);
        }

    }

    public override void TriggerEnter(string triggerName)
    {
        if(nowActionList == null)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (triggerName.Contains("Target1") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.First))
            {
                PhoneManager.singleTon.PhoneMainCanvasActive(false);
                StartCoroutine(PlayerMoveCoroutine());

            }
            if (triggerName.Contains("Target2") && keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
            {
                memoryPlayer.isPlayPossible = false;
                memoryPlayer.ToggleToSprite();
                PhoneManager.singleTon.PhoneMainCanvasActive(false);
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryRestaurant));
            }
        }
    }

    IEnumerator PlayerMoveCoroutine()
    {
        memoryPlayer.ToggleToSprite();
        memoryPlayer.isPlayPossible = false;
        isDialogStopping = true;
        isStopActionable = false;
        StartCoroutine(moduleManager.MoveModule_Linear(motherObject, motherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(motherObject, 0, 1, 1));
        yield return new WaitForSeconds(1.05f);
        NextDialog();
    }



}