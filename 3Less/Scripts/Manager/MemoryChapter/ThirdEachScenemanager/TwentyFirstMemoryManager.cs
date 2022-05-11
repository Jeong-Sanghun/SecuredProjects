using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwentyFirstMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject bossObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter10");
        dialogBundle.SetCharacterEnum();

        nowScene = SceneName.MemoryStore2;
        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        bossObject.SetActive(true);
        bossObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(bossObject, bossObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(bossObject, 0, 1, 1f));

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
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryFriendRoom4));
            }
        }
    }

}