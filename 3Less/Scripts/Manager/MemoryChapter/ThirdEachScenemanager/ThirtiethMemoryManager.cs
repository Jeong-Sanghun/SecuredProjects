using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirtiethMemoryManager : MemorySceneManagerParent
{

    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter19");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        nowScene = SceneName.MemoryDarkStreet2;

        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }


    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryFriendRoom7));
        }
    }
}