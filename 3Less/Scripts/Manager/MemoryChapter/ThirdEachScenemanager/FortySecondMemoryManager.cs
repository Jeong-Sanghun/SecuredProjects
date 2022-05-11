using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortySecondMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject friendObject;
    [SerializeField]
    GameObject friendFatherObject;

    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter30");
        dialogBundle.SetCharacterEnum();


        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friendObject.SetActive(true);
        friendObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friendFatherObject.SetActive(true);
        friendFatherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryFriendRoom9;

        StartCoroutine(moduleManager.MoveModule_Linear(friendFatherObject, friendFatherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(friendFatherObject, 0, 1, 1));


        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(friendObject, friendObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(friendObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));




    }



    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            gameManager.SaveEndContents();
            StartCoroutine(SceneEndCoroutine(SceneName.GameEnd));
        }
    }


}
