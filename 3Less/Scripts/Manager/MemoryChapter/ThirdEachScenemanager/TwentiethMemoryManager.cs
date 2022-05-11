using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwentiethMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject friend1Object;
    [SerializeField]
    GameObject friend2Object;



    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter9");
        dialogBundle.SetCharacterEnum();



        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friend1Object.SetActive(true);
        friend1Object.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friend2Object.SetActive(true);
        friend2Object.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        nowScene = SceneName.MemoryHallway2;
        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(friend1Object, friend1Object.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(friend1Object, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(friend2Object, friend2Object.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(friend2Object, 0, 1, 1));


        StartCoroutine(InvokerCoroutine(1f, NextDialog));
    }



    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryStore2));
        }
    }
}