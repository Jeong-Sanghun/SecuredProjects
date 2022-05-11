using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirtySecondMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject teacherObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter21");
        dialogBundle.SetCharacterEnum();

        nowScene = SceneName.MemoryTeacherRoom2;
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
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryStore5));
        }
    }

}