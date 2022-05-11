using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortyFirstMemoryManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject homeRoomTeacherObject;
    [SerializeField]
    GameObject councilTeacherObject;

    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter29");
        dialogBundle.SetCharacterEnum();


        playerObject.SetActive(true);
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        homeRoomTeacherObject.SetActive(true);
        homeRoomTeacherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        councilTeacherObject.SetActive(true);
        councilTeacherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryTeacherRoom5;

        StartCoroutine(moduleManager.MoveModule_Linear(councilTeacherObject, councilTeacherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(councilTeacherObject, 0, 1, 1));


        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(homeRoomTeacherObject, homeRoomTeacherObject.transform.position + Vector3.left / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(homeRoomTeacherObject, 0, 1, 1f));
        StartCoroutine(InvokerCoroutine(1f, NextDialog));




    }



    public override void TriggerEnter(string triggerName)
    {
        if (triggerName.Contains("Target1"))
        {
            memoryPlayer.isPlayPossible = false;
            memoryPlayer.ToggleToSprite();
            PhoneManager.singleTon.PhoneMainCanvasActive(false);
            StartCoroutine(SceneEndCoroutine(SceneName.MemoryFriendRoom9));
        }
    }


}
