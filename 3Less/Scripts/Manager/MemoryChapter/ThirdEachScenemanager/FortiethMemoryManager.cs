using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortiethMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject teacherObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter28");
        dialogBundle.SetCharacterEnum();

        nowScene = SceneName.MemoryHallway4;
        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        teacherObject.SetActive(true);
        teacherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(teacherObject, teacherObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(teacherObject, 0, 1, 1f));

        StartCoroutine(InvokerCoroutine(1f, NextDialog));
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
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryTeacherRoom5));
            }
        }
    }

}