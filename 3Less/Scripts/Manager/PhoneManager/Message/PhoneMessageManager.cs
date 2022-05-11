using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PhoneMessageManager : MonoBehaviour
{
    PhoneManager phoneManager;
    GameManager gameManager;
    SaveDataClass saveData;
    MessageBundle messageBundle;
    [SerializeField]
    GameObject wholeMessageCanvas;
    [SerializeField]
    GameObject messageCanvasPrefab;
    [SerializeField]
    Transform messageCanvasParent;
    [SerializeField]
    GameObject otherMessagePrefab;
    [SerializeField]
    GameObject playerMessagePrefab;
    [SerializeField]
    GameObject messageListButtonPrefab;
    [SerializeField]
    Transform messageListButtonParent;
    [SerializeField]
    RectTransform wholeCanavsBackGroundRect;
    [SerializeField]
    EventTrigger handle;
    Character nowCharacter;

    GameObject nowOpenedCanvas;


    private void Start()
    {
        if(phoneManager != null)
        {
            return;
        }
        phoneManager = PhoneManager.singleTon;
        gameManager = GameManager.singleton;
        saveData = gameManager.saveData;


        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerUp;
        entry1.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, wholeCanavsBackGroundRect); });
        handle.triggers.Add(entry1);

        //버튼 이벤트
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.Drag;
        entry2.callback.AddListener((data) => { phoneManager.Swipe((PointerEventData)data, wholeCanavsBackGroundRect); });
        handle.triggers.Add(entry2);

        //EventTrigger.Entry entry3 = new EventTrigger.Entry();
        //entry3.eventID = EventTriggerType.PointerExit;
        //entry3.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, wholeCanavsBackGroundRect); });
        //handle.triggers.Add(entry3);

        SetMessage();

    }

    public void SetMessage()
    {
        
        if(messageBundle != null)
        {
            FlushMessage();
        }
        saveData = gameManager.saveData;
        messageBundle = saveData.messageBundle;
        if(messageBundle == null)
        {
            
            saveData.messageBundle = new MessageBundle();
            messageBundle = saveData.messageBundle;
        }
        for (int i = 0; i < messageBundle.messageWrapperList.Count; i++)
        {
            
            MessageWrapper wrapper = messageBundle.messageWrapperList[i];
            wrapper.canvasOpenButton = Instantiate(messageListButtonPrefab, messageListButtonParent);
            Button listButton = wrapper.canvasOpenButton.transform.GetChild(1).GetComponent<Button>();
            int deleIndex = i;
            listButton.onClick.AddListener(()=>OpenMessageCanvas(deleIndex));

            Text listText = listButton.transform.GetChild(0).GetComponent<Text>();
            listText.text = CharacterEnumToString.Changer(wrapper.character);

            Text previewText = listButton.transform.GetChild(1).GetComponent<Text>();
            int lastIndex = messageBundle.messageWrapperList[i].messageList.Count - 1;
            if(lastIndex>=0)
                previewText.text = messageBundle.messageWrapperList[i].messageList[lastIndex].dialog;

            Image profile = wrapper.canvasOpenButton.transform.GetChild(0).GetComponent<Image>();
            profile.sprite = CharacterEnumToSprite.Changer(wrapper.character);

            wrapper.messageCanvas = Instantiate(messageCanvasPrefab, messageCanvasParent);
            wrapper.messageCanvas.SetActive(false);
            Button getOutButton1 = wrapper.messageCanvas.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Button>();
            int dele = i;
            getOutButton1.onClick.AddListener(() => CloseMessageCanvas(dele));

            //Button getOutButton2 = wrapper.messageCanvas.transform.GetChild(0).GetChild(5).GetComponent<Button>();
            //getOutButton2.onClick.AddListener(() => CloseMessageCanvas(dele));

            Image canvasProfile = wrapper.messageCanvas.transform.GetChild(1).GetChild(3).GetComponent<Image>();
            canvasProfile.sprite = CharacterEnumToSprite.Changer(wrapper.character);

            Text name = wrapper.messageCanvas.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
            name.text = CharacterEnumToString.Changer(wrapper.character);

            RectTransform backGround = wrapper.messageCanvas.transform.GetChild(1).GetComponent<RectTransform>();

            EventTrigger swipeEvent = wrapper.messageCanvas.transform.GetChild(1).GetChild(0).GetComponent<EventTrigger>();

            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerUp;
            entry1.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, backGround); });
            swipeEvent.triggers.Add(entry1);

            //버튼 이벤트
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Drag;
            entry2.callback.AddListener((data) => { phoneManager.Swipe((PointerEventData)data, backGround); });
            swipeEvent.triggers.Add(entry2);

            //EventTrigger.Entry entry3 = new EventTrigger.Entry();
            //entry3.eventID = EventTriggerType.PointerExit;
            //entry3.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, backGround); });
            //swipeEvent.triggers.Add(entry3);

            RectTransform messageParent = wrapper.messageCanvas.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            for (int  j = 0; j < wrapper.messageList.Count; j++)
            {
                OneMessage message = wrapper.messageList[j];
                if (message.isPlayer == true)
                {
                    message.SetObject(SpawnPlayerChat(message, messageParent));
                }
                else
                {
                    message.SetObject(SpawnOtherChat(message, messageParent, wrapper));
                }
            }
        }
    }

    public void SetCharacter(Character character)
    {
        if(character == Character.Player)
        {
            Debug.LogError("나자신과의 대화");
        }
        nowCharacter = character;

    }

    public void AddMessage(Dialog dialog, Character character)
    {
        int wrapperIndex = -1;
        for(int i = 0; i < messageBundle.messageWrapperList.Count; i++)
        {
            MessageWrapper wrapper = messageBundle.messageWrapperList[i];
            if (wrapper.character == nowCharacter)
            {
                wrapperIndex = i;
                break;
            }
        }

        if(wrapperIndex == -1)
        {
            MessageWrapper wrapper = new MessageWrapper();
            messageBundle.messageWrapperList.Add(wrapper);
            wrapperIndex = messageBundle.messageWrapperList.Count - 1;
            wrapper.character = nowCharacter;
            wrapper.canvasOpenButton = Instantiate(messageListButtonPrefab, messageListButtonParent);
            Button listButton = wrapper.canvasOpenButton.transform.GetChild(1).GetComponent<Button>();
            int deleIndex = wrapperIndex;
            listButton.onClick.AddListener(()=>OpenMessageCanvas(deleIndex));

            Text listText = listButton.transform.GetChild(0).GetComponent<Text>();
            listText.text = CharacterEnumToString.Changer(nowCharacter);

            Image profile = wrapper.canvasOpenButton.transform.GetChild(0).GetComponent<Image>();
            profile.sprite = CharacterEnumToSprite.Changer(nowCharacter);

            wrapper.messageCanvas = Instantiate(messageCanvasPrefab, messageCanvasParent);
            wrapper.messageCanvas.SetActive(false);
            Button getOutButton1 = wrapper.messageCanvas.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Button>();
            int dele = wrapperIndex;
            getOutButton1.onClick.AddListener(()=>CloseMessageCanvas(dele));

            //Button getOutButton2 = wrapper.messageCanvas.transform.GetChild(0).GetChild(5).GetComponent<Button>();
            //getOutButton2.onClick.AddListener(() => CloseMessageCanvas(dele));

            Image canvasProfile = wrapper.messageCanvas.transform.GetChild(1).GetChild(3).GetComponent<Image>();
            canvasProfile.sprite = CharacterEnumToSprite.Changer(nowCharacter);

            Text name = wrapper.messageCanvas.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
            name.text = CharacterEnumToString.Changer(nowCharacter);

            RectTransform backGround = wrapper.messageCanvas.transform.GetChild(1).GetComponent<RectTransform>();

            EventTrigger swipeEvent = wrapper.messageCanvas.transform.GetChild(1).GetChild(0).GetComponent<EventTrigger>();


            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerUp;
            entry1.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, backGround); });
            swipeEvent.triggers.Add(entry1);

            //버튼 이벤트
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Drag;
            entry2.callback.AddListener((data) => { phoneManager.Swipe((PointerEventData)data, backGround); });
            swipeEvent.triggers.Add(entry2);

            //EventTrigger.Entry entry3 = new EventTrigger.Entry();
            //entry3.eventID = EventTriggerType.PointerExit;
            //entry3.callback.AddListener((data) => { phoneManager.PointerUp((PointerEventData)data, backGround); });
            //swipeEvent.triggers.Add(entry3);
        }

        MessageWrapper nowWrapper = messageBundle.messageWrapperList[wrapperIndex];
        RectTransform messageParent = nowWrapper.messageCanvas.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        OneMessage message;

        Text preview = nowWrapper.canvasOpenButton.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        preview.text = dialog.dialog;

        if (character == Character.Player)
        {
            message = new OneMessage(true,dialog.dialog,DateTime.Now.ToString("hh : mm"));
            message.SetObject(SpawnPlayerChat(message, messageParent));

            //RectTransform layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);

        }
        else
        {
            message = new OneMessage(false, dialog.dialog, DateTime.Now.ToString("hh : mm"));
            message.SetObject(SpawnOtherChat(message, messageParent, nowWrapper));
            //RectTransform layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<RectTransform>();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
            //layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
            //layoutRect = message.messageObject.transform.GetChild(1).GetComponent<RectTransform>();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
        }
        nowWrapper.messageList.Add(message);


    }

    void FlushMessage()
    {
        for(int i = 0; i < messageBundle.messageWrapperList.Count; i++)
        {
            MessageWrapper wrapper = messageBundle.messageWrapperList[i];
            Destroy(wrapper.messageCanvas);
            Destroy(wrapper.canvasOpenButton);
        }
    }

    GameObject SpawnPlayerChat(OneMessage message,RectTransform parentRect)
    {
        GameObject chatInst = Instantiate(playerMessagePrefab, parentRect);
        RectTransform chatRect = chatInst.GetComponent<RectTransform>();
        // chatRect.anchoredPosition = new Vector3(10000, 10000);
        Text timeText = chatInst.transform.GetChild(0).GetComponent<Text>();
        timeText.text = message.time;
        Text chatText = chatInst.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        chatText.text = message.dialog;
        return chatInst.gameObject;
    }

    GameObject SpawnOtherChat(OneMessage message, RectTransform parentRect,MessageWrapper wrapper)
    {
        GameObject chatInst = Instantiate(otherMessagePrefab, parentRect);
        RectTransform chatRect = chatInst.GetComponent<RectTransform>();
        //  chatRect.anchoredPosition = new Vector3(10000, 10000);
        Text chatText = chatInst.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        chatText.text = message.dialog;
        Text profileText = chatInst.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        profileText.text = CharacterEnumToString.Changer(wrapper.character);
        Text timeText = chatInst.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        timeText.text = message.time;
        Image profileImage = chatInst.transform.GetChild(0).GetComponent<Image>();
        profileImage.sprite = CharacterEnumToSprite.Changer(wrapper.character);
        return chatInst.gameObject;
    }

    void OpenMessageCanvas(int wrapperIndex)
    {
        MessageWrapper nowWrapper = messageBundle.messageWrapperList[wrapperIndex];
        nowWrapper.CanvasOpen();
        nowOpenedCanvas = nowWrapper.messageCanvas;
        for (int i = 0; i < nowWrapper.messageList.Count; i++)
        {
            OneMessage message = nowWrapper.messageList[i];
            if(message.isPlayer == true)
            {
                RectTransform layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
            }
            else
            {
                RectTransform layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
                layoutRect = message.messageObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
                layoutRect = message.messageObject.transform.GetChild(1).GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRect);
            }

        }
        RectTransform messageParent = nowWrapper.messageCanvas.transform.
            GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageParent);

        wholeMessageCanvas.SetActive(false);
    }

    void CloseMessageCanvas(int wrapperIndex)
    {
        
        nowOpenedCanvas = null;
        MessageWrapper nowWrapper = messageBundle.messageWrapperList[wrapperIndex];
        nowWrapper.CanvasClose();
        wholeMessageCanvas.SetActive(true);
    }


    public void ShutDown()
    {
        if (nowOpenedCanvas != null)
        {
            nowOpenedCanvas.SetActive(false);
        }
            

        wholeMessageCanvas.SetActive(false);
    }

}
