
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PhoneDialogManager : MemorySceneManagerParent
{
    PhoneMessageManager phoneMessageManager;
    [SerializeField]
    RectTransform phoneParentRect;
    [SerializeField]
    RectTransform wholeChatParentRect;
    [SerializeField]
    GameObject otherChatPrefab;
    [SerializeField]
    GameObject playerChatPrefab;
    Vector2 nowPlayerSpawnPos;
    Vector2 nowOtherSpawnPos;
    string nowChattingCharacter;

    protected override void Start()
    {
        base.Start();
        nowPlayerSpawnPos = Vector2.zero;
        nowOtherSpawnPos = Vector2.zero;
        phoneMessageManager = PhoneManager.singleTon.phoneMessageManager;
    }

    protected override void NextDialog()
    {
        if (isPhoneOn)
        {
            ChatDialog();
        }
        else
        {
            base.NextDialog();
        }
    }

    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        base.OverrideAction(keywordList, parameterList);

        if (keywordList.Contains(ActionKeyword.PhoneOn))
        {
            isPhoneOn = true;
            
            int childCount = wholeChatParentRect.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(wholeChatParentRect.GetChild(i).gameObject);
            }
            Debug.Log(nowCharacter);
            TextFrameToggle(false);
            nowChattingCharacter = CharacterEnumToString.Changer(nowCharacter);
            phoneMessageManager.SetCharacter(nowCharacter);
            StartCoroutine(moduleManager.VolumeModule(blurVolume, true, 1));
            StartCoroutine(moduleManager.MoveModuleRect_Linear(phoneParentRect.gameObject, new Vector3(0,0,0), 0.5f));
            isStopActionable = false;
            StartCoroutine(InvokerCoroutine(1,SetDialogStopFalse));
        }

        if (keywordList.Contains(ActionKeyword.PhoneOff))
        {
            isPhoneOn = false;
            StartCoroutine(moduleManager.VolumeModule(blurVolume, false, 1));
            StartCoroutine(moduleManager.MoveModuleRect_Linear(phoneParentRect.gameObject, new Vector3(0,1070,0), 0.5f));
            isStopActionable = false;
            StartCoroutine(InvokerCoroutine(1,SetDialogStopFalse));

        }
    }

    void ChatDialog()
    {


        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];

        Text nowText = dialogText;
        if (nowDialog.characterEnum != nowCharacter && nowDialog.characterEnum != Character.NotAllocated)
        {
            nowCharacter = nowDialog.characterEnum;
        }
        isDialogStopping = true;
        phoneMessageManager.AddMessage(nowDialog, nowCharacter);
        if(nowDialog.dialog != null)
        {
            phoneArchiveManager.AddTalkBackLog(nowScene, BackLogType.Talk, nowCharacter, nowDialogIndex);
            switch (nowCharacter)
            {
                case Character.Player:
                    StartCoroutine(SpawnPlayerChat());
                    break;
                default:
                    StartCoroutine(SpawnOtherChat());
                    //일단 암것도 하지말아봐.
                    break;
            }
        }
       
        if (nowDialog.actionKeyword != null)
        {
            nowActionList = dialogBundle.dialogList[nowDialogIndex].actionList;
            isDialogStopping = true;
            StartCoroutine(InvokerCoroutine(1, SetStopActionableTrue));
        }
        else
        {
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
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

    IEnumerator SpawnPlayerChat()
    {
        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];
        
        GameObject chatInst = Instantiate(playerChatPrefab, wholeChatParentRect);
        RectTransform chatRect = chatInst.GetComponent<RectTransform>();
       // chatRect.anchoredPosition = new Vector3(10000, 10000);
        Text chatText = chatInst.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        chatText.text = nowDialog.dialog;
        Text timeText = chatInst.transform.GetChild(0).GetComponent<Text>(); ;
        timeText.text = DateTime.Now.ToString("hh : mm");
        LayoutRebuilder.ForceRebuildLayoutImmediate(wholeChatParentRect);
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(wholeChatParentRect);
    }

    IEnumerator SpawnOtherChat()
    {
        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];
        GameObject chatInst = Instantiate(otherChatPrefab, wholeChatParentRect);
        RectTransform chatRect = chatInst.GetComponent<RectTransform>();
      //  chatRect.anchoredPosition = new Vector3(10000, 10000);
        Text chatText = chatInst.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>(); 
        chatText.text = nowDialog.dialog;
        Text profileText = chatInst.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        profileText.text = nowChattingCharacter;
        Text timeText = chatInst.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        timeText.text = DateTime.Now.ToString("hh : mm");
        Image profileImage = chatInst.transform.GetChild(0).GetComponent<Image>();
        profileImage.sprite = CharacterEnumToSprite.Changer(nowCharacter);

        RectTransform layoutRect = chatInst.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
        layoutRect = chatInst.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
       layoutRect = chatInst.transform.GetChild(1).GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);

        
        LayoutRebuilder.ForceRebuildLayoutImmediate(wholeChatParentRect);
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(wholeChatParentRect);
    }


}
