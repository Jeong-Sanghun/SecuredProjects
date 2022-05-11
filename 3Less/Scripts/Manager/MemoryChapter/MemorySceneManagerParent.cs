using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;

public class MemorySceneManagerParent : MonoBehaviour
{
    
    protected GameManager gameManager;
    protected JsonManager jsonManager;
    protected SoundManager soundManager;
    protected DialogBundle dialogBundle;
    protected SaveDataClass saveData;
    protected PhoneArchiveManager phoneArchiveManager;
    [SerializeField]
    protected GaugeManager gaugeManager;
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
    GameObject messageParent;
    [SerializeField]
    Text messageText;
    [SerializeField]
    GameObject[] routeButtonParentArray;
    GameObject nowRouteButtonParent;
    [SerializeField]
    protected PostProcessVolume blurVolume;

    [SerializeField]
    protected GameObject playerObject;
    protected MemoryPlayer memoryPlayer;

    [SerializeField]
    protected Camera cam;

    [SerializeField]
    protected List<GameObject> ballonList;

    protected SceneName nowScene;

    protected int nowDialogIndex;
    protected bool isDialogStopping;
    bool isTalkingSystem;
    protected bool isStartOfWrapper;
    protected bool isStopActionable;
    bool isRouteButtonAble;
    Dialog routeDialog;
    bool isMultiRouting;
    bool isRouting;
    int nowMultiRouteCount;
    List<int> choosedMultiRouteList;
    List<int> multiRouteEndedDialogIndex;
    ActionKeyword nowChoosedRoute;

    bool textFrameTransparent;
    bool isStarted;
    protected bool dialogEnd;
    protected bool isPhoneOn;
    protected Character nowCharacter;
    protected List<ActionClass> nowActionList;

    protected string triggerName;
    protected bool isTrigger;
    protected bool cameraFollowing;
    protected float cameraRightBound;
    protected float cameraLeftBound;
    bool loadedToRoute;

    protected bool isNewGame;
    bool isDeadPoint;

    protected virtual void Start()
    {
        gameManager = GameManager.singleton;
        phoneArchiveManager = PhoneManager.singleTon.phoneArchiveManager;
        soundManager = SoundManager.singleton;
        jsonManager = new JsonManager();
        isStartOfWrapper = true;
        fadeInImage.gameObject.SetActive(true);
        fadeInImage.color = new Color(0, 0, 0, 1);
        textFrameImage.color = new Color(1, 1, 1, 0);
        dialogText.text = "";
        systemText.text = "";
        textFrameTransparent = true;
        for (int i = 0; i < ballonList.Count; i++)
        {
            ballonList[i].SetActive(false);
        }
        nowChoosedRoute = ActionKeyword.Null;
        dialogEnd = false;
        isStopActionable = false;
        isDialogStopping = true;
        isStarted = false;
        isRouteButtonAble = false;
        routeDialog = null;
        isMultiRouting = false;
        isRouting = false;
        nowMultiRouteCount = 0;
        multiRouteEndedDialogIndex = new List<int>();
        saveData = gameManager.saveData;
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 0.5f));
        textFrameImage.GetComponent<Image>().raycastTarget = false;
        loadedToRoute = false;
        if (playerObject != null)
        {
            memoryPlayer = playerObject.GetComponent<MemoryPlayer>();
        }
        else
        {
            memoryPlayer = null;
        }
        isNewGame = gameManager.isNewGame;

        if(gameManager.isNewGame == false)
        {
            nowDialogIndex = saveData.dialogIndex;
            gameManager.isNewGame = true;
            loadedToRoute = true;
        }
        gaugeManager.SetGauge(saveData.moneyGauge, saveData.healthGauge);
    }

   




    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ScreenTouchEvent();
        }
    }



    public void ScreenTouchEvent()
    {
        if (isStarted == false || moduleManager.nowTexting || isRouting == true)
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
        if (dialogEnd == true || isRouting == true ||gaugeManager.isGameOver == true)
        {
            return;
        }
        if(nowDialogIndex >= dialogBundle.dialogList.Count)
        {
            return;
        }


        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];

        if (nowChoosedRoute != ActionKeyword.Null && nowChoosedRoute != ActionKeyword.Route)
        {
            if (nowDialog.actionKeyword != null)
            {
                List<ActionClass> actionClassList = dialogBundle.dialogList[nowDialogIndex].actionList;
                for(int i = 0; i < actionClassList.Count; i++)
                {
                    List<ActionKeyword> actionList = actionClassList[i].actionList;
                    if (actionList.Contains(ActionKeyword.Route))
                    {
                        if (actionList.Contains(nowChoosedRoute))
                        {
                            break;
                        }
                        else if (actionList.Contains(ActionKeyword.End))
                        {
                            nowChoosedRoute = ActionKeyword.Route;
                            if(isMultiRouting == true)
                            {
                                isMultiRouting = false;
                                nowMultiRouteCount = 0;
                                choosedMultiRouteList = null;
                            }
                            break;
                        }
                        else
                        {
                            int traceStartIndex = nowDialogIndex;
                            if(isMultiRouting == true)
                            {
                                traceStartIndex = 0;
                            }
                            for (int k = traceStartIndex; k < dialogBundle.dialogList.Count; k++)
                            {
                                Dialog dialog = dialogBundle.dialogList[k];
                                List<ActionClass> traceActionList = dialog.actionList;
                                if(traceActionList == null)
                                {
                                    continue;
                                }
                                for (int m = 0; m < traceActionList.Count; m++)
                                {
                                    ActionClass action = traceActionList[m];
                                    if (isMultiRouting == true)
                                    {
                                        if (action.actionList.Contains(ActionKeyword.MultiRoute))
                                        {
                                            nowChoosedRoute = ActionKeyword.Null;
                                            nowDialogIndex = k;
                                            NextDialog();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (action.actionList.Contains(ActionKeyword.Route)
                                        && action.actionList.Contains(ActionKeyword.End))
                                        {
                                            nowChoosedRoute = ActionKeyword.Null;
                                            nowDialogIndex = k + 1;
                                            if (nowDialogIndex >= dialogBundle.dialogList.Count)
                                            {
                                                nowDialogIndex = dialogBundle.dialogList.Count;
                                                isStartOfWrapper = true;
                                                dialogEnd = true;
                                                StartCoroutine(CheckStopPointTextEnd());
                                                nowActionList = dialogBundle.dialogList[nowDialogIndex - 1].actionList;
                                                return;
                                            }
                                            NextDialog();
                                            return;
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
        if (isTalkingSystem == true)
        {
            return;
        }
        

        bool isNewCharacter = false;
        Text nowText = dialogText;
        bool lastTextFrameTransparent = textFrameTransparent;
        if (isStartOfWrapper)
        {
            if(nowDialog.characterEnum != Character.NotAllocated)
                nowCharacter = nowDialog.characterEnum;
            isNewCharacter = true;
            isStartOfWrapper = false;
        }
        else if (nowDialog.characterEnum != nowCharacter && nowDialog.characterEnum != Character.NotAllocated)
        {
            nowCharacter = nowDialog.characterEnum;
            isNewCharacter = true;
        }
        else if(nowCharacter == Character.System || nowCharacter == Character.Message)
        {
            isNewCharacter = true;
        }
        isDialogStopping = false;
        if(nowDialog.dialog != null)
        {
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
                    case Character.RooftopFriend:
                    case Character.FriendGirl:
                    case Character.Mother:
                    case Character.RoomFriend:
                    case Character.Police:
                    case Character.JustBoss:
                    case Character.StoreBoss:
                    case Character.CouncilTeacher:
                    case Character.DrunkenPerson1:
                        TextFrameToggle(true);
                        ballonList[1].SetActive(true);
                        break;
                    case Character.Father:
                    case Character.FriendFather:
                    case Character.Friend1:
                    case Character.HomeRoomTeacher:
                    case Character.DrunkenPerson2:
                        TextFrameToggle(true);
                        ballonList[2].SetActive(true);
                        break;
                    case Character.Friend2:
                    case Character.Brother:
                    case Character.YoungMan:
                        TextFrameToggle(true);
                        ballonList[3].SetActive(true);
                        break;
                    case Character.Narator:
                        TextFrameToggle(true);
                        break;
                    case Character.System:
                        TextFrameToggle(false);
                        systemText.gameObject.SetActive(true);
                        systemText.text = nowDialog.dialog;
                        systemText.color = new Color(systemText.color.r, systemText.color.g, systemText.color.b, 0);
                        StartCoroutine(moduleManager.FadeModule_Text(systemText, 0, 1, 1));
                        StartCoroutine(moduleManager.AfterRunCoroutine(2, moduleManager.FadeModule_Text(systemText, 1, 0, 1)));
                        isTalkingSystem = true;
                        StartCoroutine(InvokerCoroutine(3, SetTalkingSystemFalse));
                        if (nowDialog.actionKeyword != null)
                        {
                            isStartOfWrapper = true;
                            isDialogStopping = true;
                            StartCoroutine(InvokerCoroutine(3f, SetStopActionableTrue));
                            nowActionList = dialogBundle.dialogList[nowDialogIndex].actionList;
                        }
                        break;
                    case Character.Message:
                        TextFrameToggle(false);
                        messageParent.SetActive(true);
                        messageText.gameObject.SetActive(true);
                        messageText.text = nowDialog.dialog;
                        StartCoroutine(moduleManager.MoveModuleRect_Linear(messageParent, new Vector3(0, -140, 0), 1));
                        StartCoroutine(moduleManager.AfterRunCoroutine(3, moduleManager.MoveModuleRect_Linear(messageParent, new Vector3(0, 0, 0), 1)));
                        isTalkingSystem = true;
                        StartCoroutine(InvokerCoroutine(4, SetTalkingSystemFalse));
                        if (nowDialog.actionKeyword != null)
                        {
                            isStartOfWrapper = true;
                            isDialogStopping = true;
                            StartCoroutine(InvokerCoroutine(4, SetStopActionableTrue));
                            nowActionList = dialogBundle.dialogList[nowDialogIndex].actionList;
                        }
                        break;
                    case Character.NotAllocated:
                        //이거도 위랑 연속적인거여서 아무것도 안해도됨.
                        break;
                    default:
                        //일단 암것도 하지말아봐.
                        break;
                }
            }
        }


        if (nowDialog.dialog != null && textFrameTransparent == false)
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

        if (nowDialog.dialog != null)
        {
            if(loadedToRoute == false)
            {
                if(nowChoosedRoute != ActionKeyword.Null)
                {
                    if(nowChoosedRoute == ActionKeyword.Route)
                    {
                        nowChoosedRoute = ActionKeyword.Null;
                    }
                    phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.Talk, nowCharacter, nowDialogIndex,-1,-1,true);
                }
                else
                {
                    phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.Talk, nowCharacter, nowDialogIndex);
                }
                
            }
            else if(nowDialog.routeList != null)
            {
                loadedToRoute = false;
            }
            
        }

        
        if (nowDialog.actionKeyword != null && nowCharacter != Character.System && nowCharacter != Character.Message)
        {
        
            isStartOfWrapper = true;
            StartCoroutine(CheckStopPointTextEnd());
            nowActionList = dialogBundle.dialogList[nowDialogIndex].actionList;
        }

        if (nowDialog.routeList != null)
        {
            nowChoosedRoute = ActionKeyword.Null;

            isRouting = true;
            isStartOfWrapper = true;
            routeDialog = nowDialog;
            StartCoroutine(CheckRoutePointTextEnd());
            if(isMultiRouting == false && nowDialog.actionKeyword != null
                && !multiRouteEndedDialogIndex.Contains(nowDialogIndex))
            {
               
                for (int i = 0; i < nowActionList.Count; i++)
                {
                    if (nowActionList[i].actionList.Contains(ActionKeyword.MultiRoute))
                    {
                        multiRouteEndedDialogIndex.Add(nowDialogIndex);
                        isMultiRouting = true;
                        nowMultiRouteCount = (int)nowActionList[i].parameterList[0];
                        choosedMultiRouteList = new List<int>();
                        break;
                    }
                }
            }
            IsDeadPoint();

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
        List<ActionClass> actionClassList = nowActionList;
        bool immediateStart = false;
        for (int i = 0; i < actionClassList.Count; i++)
        {
            ActionClass nowAction = actionClassList[i];
            List<ActionKeyword> keywordList = nowAction.actionList;
            List<float> parameterList = nowAction.parameterList;
            if (keywordList.Contains(ActionKeyword.ImmediateDialog) || (keywordList.Contains(ActionKeyword.Route) ))//&& !keywordList.Contains(ActionKeyword.End)))
            {
                immediateStart = true;
            }
            OverrideAction(keywordList, parameterList);
        }
        if (immediateStart)
        {
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

    protected virtual void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        for (int i = 0; i < ballonList.Count; i++)
        {
            ballonList[i].SetActive(false);
        }
        if (keywordList.Contains(ActionKeyword.PlayerMove))
        {
            StartCoroutine(CameraFollowCoroutine());
            PhoneManager.singleTon.PhoneMainCanvasActive(true);
            memoryPlayer.isPlayPossible = true;
            isDialogStopping = true;
            isStopActionable = false;
            TextFrameToggle(false);
        }
        if (keywordList.Contains(ActionKeyword.HealthGauge))
        {
            gaugeManager.ChangeHealthGauge((int)parameterList[0]);
            phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.HealthGauge, nowCharacter,-1,(int)parameterList[0]);
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }
        if (keywordList.Contains(ActionKeyword.MoneyGauge))
        {
            gaugeManager.ChangeMoneyGauge((int)parameterList[0]);
            phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.MoneyGauge, nowCharacter, -1,(int)parameterList[0]);
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }
        if (keywordList.Contains(ActionKeyword.SoundMessageAlarm))
        {
            soundManager.EffectPlay(SFX.SoundMessageAlarm);
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }
        if (keywordList.Contains(ActionKeyword.DoorSound))
        {
            //soundManager.EffectPlay(SFX.DoorSound);
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }



    }



    //트루면 메인프레임이 켜짐.
    protected virtual void TextFrameToggle(bool mainActive)
    {
        if (!mainActive == textFrameTransparent)
        {
            return;
        }
        if(mainActive == false)
        {
            Debug.Log("어디서일어남?");
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
        if (memoryPlayer == null)
        {
            isDialogStopping = false;
        }
        else if(memoryPlayer.isPlayPossible == false)
        {
            isDialogStopping = false;
        }
        
            
    }

    protected void SetTalkingSystemFalse()
    {
        isTalkingSystem = false;

    }


    IEnumerator CheckStopPointTextEnd()
    {
        while (moduleManager.nowTexting)
        {
            yield return null;
        }
        isDialogStopping = true;
        isStopActionable = true;
    }


    IEnumerator CheckRoutePointTextEnd()
    {
        isDialogStopping = true;
        isStopActionable = false;
        while (moduleManager.nowTexting)
        {
            yield return null;
        }
        yield return null;
        isDialogStopping = true;
        isStopActionable = false;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }

        TextFrameToggle(false);
        yield return new WaitForSeconds(0.8f);
        RouteButtonActive();

    }


    protected IEnumerator CameraFollowCoroutine()
    {
        if(cameraFollowing == false)
        {
            Transform playerTransform = playerObject.transform;
            Vector3 delta = cam.transform.position - playerTransform.position;
            float originY = cam.transform.position.y;
            cameraFollowing = true;
            while (cameraFollowing == true)
            {
                yield return new WaitForFixedUpdate();
                Vector3 pos = new Vector3((playerTransform.position + delta).x, originY, -10);
                if (pos.x >= cameraLeftBound && pos.x <= cameraRightBound)
                {
                    cam.transform.position = pos;
                }


            }
        }
 
    }

    public virtual void TriggerEnter(string triggerName)
    {

    }

    protected IEnumerator SceneEndCoroutine(SceneName scene)
    {
        fadeInImage.gameObject.SetActive(true);
        //SaveUserData();
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        gameManager.LoadScene(scene);
    }

    void RouteButtonActive()
    {
        List<string> routeList = routeDialog.routeList;
        
        if (isMultiRouting==true)
        {
            routeList = new List<string>();
            
            for(int i = 0; i < routeDialog.routeList.Count; i++)
            {
                routeList.Add(routeDialog.routeList[i]);
            }
            choosedMultiRouteList.Sort(CompareInt);
            for (int i = 0; i < choosedMultiRouteList.Count; i++)
            {
                
                routeList.RemoveAt(choosedMultiRouteList[i]);
            }
            nowRouteButtonParent = routeButtonParentArray[routeList.Count - 2 ];
            routeButtonParentArray[routeList.Count - 2].SetActive(true);

        }
        else
        {
            nowRouteButtonParent = routeButtonParentArray[routeList.Count - 2];
            routeButtonParentArray[routeList.Count - 2].SetActive(true);

        }

        List<Text> routeTextList = new List<Text>();
        isRouteButtonAble = false;
        if(isDeadPoint == false)
        {
            if (isMultiRouting == false)
            {
                SaveUserData();
            }
            else if (choosedMultiRouteList.Count == 0)
            {
                SaveUserData();
            }
        }
        isDeadPoint = false;
        
        
        StartCoroutine(moduleManager.VolumeModule(blurVolume, true, 1));
        for(int i = 0; i < routeList.Count; i++)
        {
            GameObject txtObj = nowRouteButtonParent.transform.GetChild(i).GetChild(0).gameObject;
            GameObject imgObj = nowRouteButtonParent.transform.GetChild(i).gameObject;
            Text txt = txtObj.GetComponent<Text>();
            Image img = imgObj.GetComponent<Image>();
            img.color = new Color(1, 1, 1, 0);
            txt.color = new Color(1, 1, 1, 0);
            txt.text = routeList[i];
            StartCoroutine(moduleManager.FadeModule_Image(img, 0, 1, 1));
            StartCoroutine(moduleManager.FadeModule_Text(txt, 0, 1, 1));
        }
        StartCoroutine(InvokerCoroutine(1,RouteButtonAbleTrue));


    }


    void IsDeadPoint()
    {
        if(isMultiRouting == true)
        {
            return;
        }
        isDeadPoint = false;
        //true on Dead
        int nowTracingIndex = nowDialogIndex;
        int nowHealthGauge = gaugeManager.nowHealthGauge;
        int nowMoneyGauge = gaugeManager.nowMoneyGauge;
        float[] healthGaugeArray = new float[5] ;
        float[] moneyGaugeArray = new float[5];
        int nowIndex = 0;
        for(int i = 0; i < 5; i++)
        {
            healthGaugeArray[i] = 0;
            moneyGaugeArray[i] = 0;
        }
        while (true)
        {
            bool breaking = false;
            if(nowTracingIndex >= dialogBundle.dialogList.Count)
            {

                break;
            }
            if(dialogBundle.dialogList[nowTracingIndex].actionList == null)
            {
                nowTracingIndex++;
                continue;
            }
            
            List<ActionClass> actionClassList = dialogBundle.dialogList[nowTracingIndex].actionList;
            for(int i = 0; i < actionClassList.Count; i++)
            {
                if(actionClassList[i].actionList.Contains(ActionKeyword.Route) && actionClassList[i].actionList.Contains(ActionKeyword.End))
                {
                    breaking = true;
                    break;
                }
                if (actionClassList[i].actionList.Contains(ActionKeyword.Route))
                {
                    if (actionClassList[i].actionList.Contains(ActionKeyword.First))
                    {
                        nowIndex = 0;
                    }
                    if (actionClassList[i].actionList.Contains(ActionKeyword.Second))
                    {
                        nowIndex = 1;
                    }
                    if (actionClassList[i].actionList.Contains(ActionKeyword.Third))
                    {
                        nowIndex = 2;
                    }
                    if (actionClassList[i].actionList.Contains(ActionKeyword.Fifth))
                    {
                        nowIndex = 3;
                    }
                    if (actionClassList[i].actionList.Contains(ActionKeyword.Fourth))
                    {
                        nowIndex = 4;
                    }
                }

                if (actionClassList[i].actionList.Contains(ActionKeyword.HealthGauge))
                {
                    List<ActionKeyword> actionKeywordList = actionClassList[i].actionList;
                    for (int j = 0; j < actionKeywordList.Count; j++)
                    {
                        if (actionKeywordList[j] == ActionKeyword.HealthGauge)
                        {
                            healthGaugeArray[nowIndex] +=(actionClassList[i].parameterList[j]);
                            break;
                        }
                    }

                }
                if (actionClassList[i].actionList.Contains(ActionKeyword.MoneyGauge))
                {
                    List<ActionKeyword> actionKeywordList = actionClassList[i].actionList;
                    for (int j = 0; j < actionKeywordList.Count; j++)
                    {
                        if (actionKeywordList[j] == ActionKeyword.MoneyGauge)
                        {
                            moneyGaugeArray[nowIndex] +=(actionClassList[i].parameterList[j]);
                            break;
                        }
                    }

                }

            }
            if (breaking)
            {
                break;
            }
            nowTracingIndex++;
        }
        bool isHealthDead = true;
        if (healthGaugeArray.Length == 0)
        {
            isHealthDead = false;
        }
        for (int i = 0; i < healthGaugeArray.Length; i++)
        {
            if (nowHealthGauge + healthGaugeArray[i] >= 0)
            {
                isHealthDead = false;
                break;
            }
        }
        bool isMoneyDead = true;
        if (moneyGaugeArray.Length == 0)
        {
            isMoneyDead = false;
        }
        for (int i = 0; i < moneyGaugeArray.Length; i++)
        {
            if (nowMoneyGauge + moneyGaugeArray[i] >= 0)
            {
                isMoneyDead = false;
                break;
            }
        }
        isDeadPoint =  isHealthDead || isMoneyDead;


    }

    int CompareInt(int x, int y)
    {
        if (x < y)
        {
            return 1;
        }
        if (x == y)
        {
            return 0;
        }
        if (x > y)
        {
            return -1;
        }
        return 1;
    }


    public void OnRouteButton(int index)
    {

        if(isRouteButtonAble == true)
        {
            StartCoroutine(ButtonAnimCoroutine(index));
        }
        isRouteButtonAble = false;


    }

    IEnumerator ButtonAnimCoroutine(int index)
    {
        Transform[] childArray = new Transform[nowRouteButtonParent.transform.childCount];
        for(int i = 0; i < childArray.Length; i++)
        {
            childArray[i] = nowRouteButtonParent.transform.GetChild(i);
            Text txt = childArray[i].GetChild(0).GetComponent<Text>();
            if (i != index)
            {
                StartCoroutine(moduleManager.FadeModule_Image(childArray[i].GetComponent<Image>(), 1, 0, 1));
                StartCoroutine(moduleManager.FadeModule_Text(txt, 1, 0, 1));
            }
        }

        Vector3 targetSize = new Vector3(1.05f, 1.05f, 1);
        Vector3 originSize = Vector3.one;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime*6;
            childArray[index].localScale = Vector3.Lerp(originSize, targetSize, timer);
            yield return null;
        }
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime*6;
            childArray[index].localScale = Vector3.Lerp( targetSize, originSize, timer);
            yield return null;
        }
        childArray[index].localScale = originSize;
        yield return new WaitForSeconds(0.1f);


        if (isMultiRouting)
        {
            List<int> indexList = new List<int>();
            for(int i = 0; i < routeDialog.routeList.Count; i++)
            {
                indexList.Add(i);
            }
            
            for (int i = 0; i < choosedMultiRouteList.Count; i++)
            {
                indexList.Remove(choosedMultiRouteList[i]);
            }
            index = indexList[index];
            nowMultiRouteCount--;

            if (nowMultiRouteCount == 0)
            {
                
                isMultiRouting = false;
                choosedMultiRouteList = null;
            }
            else
            {
                choosedMultiRouteList.Add(index);
            }
        }


        phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.Route, Character.NotAllocated,nowDialogIndex -1,-1,index);

        isDialogStopping = false;
        nowRouteButtonParent.SetActive(false);
        nowRouteButtonParent = null;
        StartCoroutine(moduleManager.VolumeModule(blurVolume, false, 1));
        ActionKeyword nowSeqence = ActionKeyword.First;
        switch (index)
        {
            case 0:
                nowSeqence = ActionKeyword.First;
                break;
            case 1:
                nowSeqence = ActionKeyword.Second;
                break;
            case 2:
                nowSeqence = ActionKeyword.Third;
                break;
            case 3:
                nowSeqence = ActionKeyword.Fourth;
                break;
            case 4:
                nowSeqence = ActionKeyword.Fifth;
                break;
            default:
                break;
        }
        nowChoosedRoute = nowSeqence;
        for (int i = nowDialogIndex; i < dialogBundle.dialogList.Count; i++)
        {
            bool found = false;
            Dialog dialog = dialogBundle.dialogList[i];
            if (dialog.actionList == null)
            {
                continue;
            }
            List<ActionClass> actionClassList = dialog.actionList;
            for (int j = 0; j < actionClassList.Count; j++)
            {
                ActionClass action = actionClassList[j];
                if (action.actionList.Contains(ActionKeyword.Route) && action.actionList.Contains(nowSeqence))
                {
                    found = true;
                    nowDialogIndex = i;
                    break;
                }
            }
            if (found == true)
            {
                break;
            }

        }
        isRouting = false;
        
        NextDialog();

    }

    void RouteButtonAbleTrue()
    {
        isRouteButtonAble = true;
    }

    protected void SaveUserData()
    {
        saveData.savedScene = nowScene;
        if(nowDialogIndex <= 0)
        {
            saveData.dialogIndex = 0;
        }
        else
        {
            saveData.dialogIndex = nowDialogIndex - 1;
        }
        
        saveData.healthGauge = gaugeManager.nowHealthGauge;
        saveData.moneyGauge = gaugeManager.nowMoneyGauge;
        gameManager.SaveSaveData();
    }

    
}
