using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMemorySceneManager : MemorySceneManagerParent
{
    [SerializeField]
    GameObject playerTarget;
    [SerializeField]
    GameObject motherObject;
    [SerializeField]
    GameObject fatherObject;
    [SerializeField]
    GameObject brotherObject;
    bool firstCollided;
    bool secondCollided;


    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("SecondChapter3");
        dialogBundle.SetCharacterEnum();

        firstCollided = false;
        secondCollided = false;
        
        playerObject.SetActive(true);
        memoryPlayer.ToggleToSprite();
        memoryPlayer.spritePlayerObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        motherObject.SetActive(true);
        motherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        fatherObject.SetActive(true);
        fatherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        brotherObject.SetActive(true);
        brotherObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowScene = SceneName.MemoryDarkStreet1;
        if(nowDialogIndex != 0)
        {
            StartCoroutine(OnReEntryGame());
        }
        else
        {
            StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
            StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
            StartCoroutine(InvokerCoroutine(1f, NextDialog));
        }
        cameraLeftBound = -20.4f;
        cameraRightBound = 2.2f;



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
            if (triggerName.Contains("Target1")
                &&firstCollided == false&& keywordList.Contains(ActionKeyword.PlayerMove))
            {
                PhoneManager.singleTon.PhoneMainCanvasActive(false);
                StartCoroutine(PlayerMoveCoroutine());

            }
            if (triggerName.Contains("Target2")
                 && secondCollided == false && keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
            {
                secondCollided = true;
                memoryPlayer.isPlayPossible = false;
                memoryPlayer.ToggleToSprite();
                PhoneManager.singleTon.PhoneMainCanvasActive(false);
                StartCoroutine(SceneEndCoroutine(SceneName.MemoryRooftop1));
            }
        }
    }
    IEnumerator PlayerMoveCoroutine()
    {
        firstCollided = true;
        memoryPlayer.isPlayPossible = false;
        memoryPlayer.ToggleToSprite();
        StartCoroutine(moduleManager.MoveModule_Linear(motherObject, motherObject.transform.position + Vector3.right / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(motherObject, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(fatherObject, fatherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(fatherObject, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(brotherObject, brotherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(brotherObject, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        NextDialog();
    }

    IEnumerator OnReEntryGame()
    {
        firstCollided = true;
        isDialogStopping = true;
        isStopActionable = false;
        playerObject.transform.position = playerTarget.transform.position;
        Vector3 camTarget = new Vector3(playerTarget.transform.position.x + 5, 0, -10);
        cam.gameObject.transform.position = camTarget;
        StartCoroutine(moduleManager.MoveModule_Linear(playerObject, playerObject.transform.position + Vector3.right / 2f, 1f));
        StartCoroutine(moduleManager.FadeModule_Sprite(memoryPlayer.spritePlayerObject, 0, 1, 1f));
        StartCoroutine(moduleManager.MoveModule_Linear(motherObject, motherObject.transform.position + Vector3.right / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(motherObject, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(fatherObject, fatherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(fatherObject, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(brotherObject, brotherObject.transform.position + Vector3.left / 2, 1));
        StartCoroutine(moduleManager.FadeModule_Sprite(brotherObject, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        NextDialog();
    }
}
