using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneManagerParent : MonoBehaviour
{
    protected GameManager gameManager;
    protected JsonManager jsonManager;
    protected DialogBundle dialogBundle;
    [SerializeField]
    protected ModuleManager moduleManager;
    [SerializeField]
    protected Image fadeInImage;
    [SerializeField]
    protected Image textFrameImage;
    [SerializeField]
    protected Text dialogText;
    [SerializeField]
    protected Text systemText;

    [SerializeField]
    protected PlayController player;
    [SerializeField]
    protected FishState fishState;


    [SerializeField]
    protected Camera cam;

    [SerializeField]
    protected List<GameObject> ballonList;

    protected int nowDialogIndex;
    protected bool isDialogStopping;
    protected bool isStartOfWrapper;
    protected bool isStopActionable;

    bool textFrameTransparent;
    bool isStarted;
    protected bool dialogEnd;
    Character nowCharacter;
    protected List<ActionClass> nowActionList;

    protected string triggerName;
    protected bool isTrigger;
    protected bool cameraFollowing;
    protected float cameraRightBound;

    protected SceneName nowScene;

    protected virtual void Start()
    {
        gameManager = GameManager.singleton;
        jsonManager = new JsonManager();
        isStartOfWrapper = true;
        fadeInImage.gameObject.SetActive(true);
        fadeInImage.color = new Color(0, 0, 0, 1);
        textFrameImage.color = new Color(1, 1, 1, 0);
        dialogText.text = "";
        systemText.text = "";
        textFrameTransparent = true;
        textFrameImage.GetComponent<Image>().raycastTarget = false;
        for (int i = 0; i < ballonList.Count; i++)
        {
            ballonList[i].SetActive(false);
        }
        nowDialogIndex = 0;
        dialogEnd = false;
        isStopActionable = false;
        isDialogStopping = true;
        isStarted = false;
    }


    // Update is called once per frame

    public void ScreenTouchEvent()
    {
        if (isStarted == false || moduleManager.nowTexting)
        {
            return;
        }
        if (isDialogStopping == false)
        {
            NextDialog();
        }
        else if (isStopActionable == true)
        {
            OnActionKeyword();
        }
    }



    protected virtual void NextDialog()
    {
        if(dialogEnd == true)
        {
            return;
        }
        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];
        bool isNewCharacter = false;
        Text nowText = dialogText;
        bool lastTextFrameTransparent = textFrameTransparent;
        if (isStartOfWrapper)
        {
            nowCharacter = nowDialog.characterEnum;
            isNewCharacter = true;
            isStartOfWrapper = false;
        }
        else if (nowDialog.characterEnum != nowCharacter && nowDialog.characterEnum != Character.NotAllocated)
        {
            nowCharacter = nowDialog.characterEnum;
            isNewCharacter = true;
        }

 
        if (isNewCharacter)
        {
            for (int i = 0; i < ballonList.Count; i++)
            {
                ballonList[i].SetActive(false);
            }
            switch (nowCharacter)
            {
                case Character.Player:
                    TextFrameToggle(true);

                    ballonList[0].SetActive(true);
 
                    break;
                case Character.Fish:
                    TextFrameToggle(true);
                    ballonList[1].SetActive(true);
                    break;
                case Character.Narator:
                    TextFrameToggle(true);
                    break;
                case Character.Mushroom:
                case Character.TreeMonster:
                    TextFrameToggle(true);
                    ballonList[2].SetActive(true);
                    break;
                case Character.System:
                    TextFrameToggle(false);
                    systemText.gameObject.SetActive(true);
                    systemText.text = nowDialog.dialog;
                    systemText.color = new Color(systemText.color.r, systemText.color.g, systemText.color.b, 0);
                    StartCoroutine(moduleManager.FadeModule_Text(systemText, 0, 1, 1));
                    StartCoroutine(moduleManager.AfterRunCoroutine(3, moduleManager.FadeModule_Text(systemText, 1, 0, 1)));
                    break;
                case Character.NotAllocated:
                    //이거도 위랑 연속적인거여서 아무것도 안해도됨.
                    break;
                default:
                    //일단 암것도 하지말아봐.
                    break;
            }
        }

        if (nowDialog.dialog != null)
        {
            if (lastTextFrameTransparent != textFrameTransparent)
            {
                moduleManager.nowTexting = true;
                StartCoroutine(moduleManager.
              AfterRunCoroutine(0.8f, moduleManager.LoadTextOneByOne(nowDialog.dialog, dialogText)));

            }
            else
            {
                StartCoroutine(moduleManager.LoadTextOneByOne(nowDialog.dialog, dialogText));

            }
        }
        isDialogStopping = false;
        if (nowDialog.actionKeyword != null)
        {
            isStartOfWrapper = true;
            StartCoroutine(CheckStopPointTextEnd());
            nowActionList = dialogBundle.dialogList[nowDialogIndex].actionList;
        }


        if (nowDialogIndex == dialogBundle.dialogList.Count)
        {
            isDialogStopping = true;
            dialogEnd = true;
        }
        else
        {
            nowDialogIndex++;
        }

    }


    protected virtual void OnActionKeyword()
    {
        Debug.Log("스탑포인트");
        List<ActionClass> actionClassList = nowActionList;
        bool immediateStart = false;
        bool isPlaying = player.isPlayPossible;
        player.isPlayPossible = false;
        for (int i = 0; i < actionClassList.Count; i++)
        {
            ActionClass nowAction = actionClassList[i];
            List<ActionKeyword> keywordList = nowAction.actionList;
            List<float> parameterList = nowAction.parameterList;
            if (keywordList.Contains(ActionKeyword.ImmediateDialog) || keywordList.Contains(ActionKeyword.Route))
            {
                immediateStart = true;
            }
            OverrideAction(keywordList, parameterList);
        }
        if (immediateStart)
        {
            player.isPlayPossible = isPlaying;
            isDialogStopping = false;
            NextDialog();
            return;
        }
        isStopActionable = false;
        for (int i = 0; i < ballonList.Count; i++)
        {
            ballonList[i].SetActive(false);
        }

        TextFrameToggle(false);
    }

    protected virtual void OverrideAction(List<ActionKeyword> keywordList,List<float> parameterList)
    {
        for (int i = 0; i < ballonList.Count; i++)
        {
            ballonList[i].SetActive(false);
        }

    }



    //트루면 메인프레임이 켜짐.
    protected virtual void TextFrameToggle(bool mainActive)
    {
        if (!mainActive == textFrameTransparent)
        {
            return;
        }
        textFrameTransparent = !mainActive;
        if (textFrameTransparent)
        {
            StartCoroutine(moduleManager.FadeModule_Image(textFrameImage, 1, 0, 0.7f));
            dialogText.gameObject.SetActive(false);
        }
        else
        {
            systemText.gameObject.SetActive(false);
            dialogText.gameObject.SetActive(true);
            dialogText.text = "";
            StartCoroutine(moduleManager.FadeModule_Image(textFrameImage, 0, 1, 0.7f));

        }
    }


    protected IEnumerator InvokerCoroutine(float time, Action method)
    {
        Debug.Log("인보크" + method.Method.Name);
        yield return new WaitForSeconds(time);
        isStarted = true;
        method();
    }

    protected void SetSystemTextFalse()
    {
        systemText.text = "";
        systemText.gameObject.SetActive(false);
    }

    protected void SetStopActionableTrue()
    {
        isStopActionable = true;
    }

    protected void SetDialogStopFalse()
    {
        isDialogStopping = false;
    }

    protected IEnumerator CheckStopPointTextEnd()
    {
        while (moduleManager.nowTexting)
        {
            yield return null;
        }
        isDialogStopping = true;
        isStopActionable = true;
    }


    protected IEnumerator CameraFollowCoroutine()
    {
        Transform playerTransform = player.transform;
        Vector3 delta = cam.transform.position - playerTransform.position;
        float originY = cam.transform.position.y;
        cameraFollowing = true;
        while (cameraFollowing == true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 pos = new Vector3((playerTransform.position + delta).x, originY, -10);
            if (pos.x >= 0.71 && pos.x<=cameraRightBound)
            {
                cam.transform.position = pos;
            }
                

        }
    }

    public virtual void TriggerEnter(string triggerName)
    {

    }


    protected virtual void SaveUserData()
    {
        GameManager.singleton.saveData.savedScene = nowScene;
        GameManager.singleton.saveData.dialogIndex = 0;
        gameManager.SaveSaveData();
    }
}
