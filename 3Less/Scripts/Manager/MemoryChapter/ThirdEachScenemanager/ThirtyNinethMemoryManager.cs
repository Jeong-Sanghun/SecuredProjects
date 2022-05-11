using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirtyNinethMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject teacherObject;



    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter28");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        teacherObject.SetActive(true);
        teacherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        nowScene = SceneName.MemoryHallway4;
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
        if (keywordList.Contains(ActionKeyword.FishMove))
        {
            TeacherMove();
        }

    }

    void TeacherMove()
    {
        isDialogStopping = true;
        TextFrameToggle(false);
        StartCoroutine(moduleManager.MoveModule_Linear(teacherObject, teacherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(teacherObject, 0, 1, 1));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }


    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryTeacherRoom5));
        }
    }
}


