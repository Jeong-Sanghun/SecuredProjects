using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EighteenthMemoryManager : PhoneDialogManager
{
    [SerializeField]
    GameObject friendBoyObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter7");
        dialogBundle.SetCharacterEnum();

        nowScene = SceneName.MemoryFriendRoom3;
        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        friendBoyObject.SetActive(true);
        friendBoyObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(friendBoyObject, friendBoyObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(friendBoyObject, 0, 1, 1f));

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
                StartCoroutine(SceneEndCoroutine(SceneName.MemorySchool3));
            }
        }
    }

}
