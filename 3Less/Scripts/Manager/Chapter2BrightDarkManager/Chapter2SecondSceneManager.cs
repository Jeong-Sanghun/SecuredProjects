using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Chapter2SecondSceneManager : Chapter2SceneManager
{
    SoundManager soundManager;
    [SerializeField]
    RectTransform panzaRect;

    [SerializeField]
    Image panzaUIImage;
    [SerializeField]
    RectTransform panzaMiddlePos;
    [SerializeField]
    RectTransform panzaEndPos;
    [SerializeField]
    GameObject firstPanzaObject;


    [SerializeField]
    GameObject bubbleObjectParent;

    [SerializeField]
    PostProcessVolume postProcessVolume;
    [SerializeField]
    PostProcessVolume grainVolume;

    [SerializeField]
    SpriteRenderer[] flashBackSpriteArray;
    AudioSource bgmSource;

    [SerializeField]
    GameObject banchanObject;
    [SerializeField]
    GameObject firstBlockCollider;
    [SerializeField]
    GameObject secondBlockCollider;


    [SerializeField]
    GameObject panzaItemObject;
    [SerializeField]
    GameObject panzaBackGroundObject;
    [SerializeField]
    SpriteRenderer panzaDropSprite;
    [SerializeField]
    GameObject panzaDropCollider;

    Vector3 panzaItemOriginPos;
    
    bool isPanzaClicked = false;
    bool isBubbleClicked = false;
    bool isBanchanClicked = false;
    bool isDraggingPanza = false;
    SpriteRenderer nowSprite;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        soundManager = SoundManager.singleton;
        soundManager.BGMPlay(BGM.Dark);
        nowScene = SceneName.Chapter2Dark;
        SaveUserData();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter2");
        dialogBundle.SetCharacterEnum();
        bgmSource = soundManager.bgmSource;
        SaveUserData();
        cameraRightBound = 19.8f;
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
        StartCoroutine(VolumeUpCoroutine());
        StartCoroutine(InvokerCoroutine(1, NextDialog));
        StartCoroutine(PanzaAnimCoroutine());
        StartCoroutine(BubbleAnimCoroutine());
        StartCoroutine(BanchanAnimCoroutine());
        fishState.SetStartLookingRight(true);
        panzaItemOriginPos = panzaItemObject.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SceneEndCoroutine());
        }
    }


    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
    {
        base.OverrideAction(keywordList, parameterList);
        for (int j = 0; j < keywordList.Count; j++)
        {
            Debug.Log(keywordList[j]);
        }

        if (keywordList.Contains(ActionKeyword.FadeOut))
        {
            StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }
        if (keywordList.Contains(ActionKeyword.PlayerMove))
        {
            player.isPlayPossible = true;
        }
        if (keywordList.Contains(ActionKeyword.StopSeconds))
        {
            int index = keywordList.IndexOf(ActionKeyword.StopSeconds);
            StartCoroutine(InvokerCoroutine(parameterList[index], NextDialog));
        }
        //if (keywordList.Contains(ActionKeyword.ImgFlashback))
        //{
        //    if (keywordList.Contains(ActionKeyword.First))
        //    {
        //        StartCoroutine(ImageFlashBackCoroutine(0));
        //    }
        //    if (keywordList.Contains(ActionKeyword.Second))
        //    {
        //        StartCoroutine(ImageFlashBackCoroutine(1));
        //    }
        //}
        if (keywordList.Contains(ActionKeyword.ImgFalse))
        {
            StartCoroutine(ImageFalseCoroutine());
        }
        if (keywordList.Contains(ActionKeyword.FishMove))
        {
            if (keywordList.Contains(ActionKeyword.First))
            {
                fishState.GotoNextTarget(0, true, false);
            }
            if (keywordList.Contains(ActionKeyword.Second))
            {
                fishState.GotoNextTarget(1, true, false);
            }
            if (keywordList.Contains(ActionKeyword.Third))
            {
                fishState.GotoNextTarget(2, true, false);
            }
            if (keywordList.Contains(ActionKeyword.Fourth))
            {
                fishState.GotoNextTarget(3, true, false);
            }
        }
        if (keywordList.Contains(ActionKeyword.Panza))
        {
            if (keywordList.Contains(ActionKeyword.Drag))
            {
                StartCoroutine(PanzaDragCoroutine());
            }
            else if (keywordList.Contains(ActionKeyword.Drop))
            {
                StartCoroutine(PanzaDropCoroutine());
            }
        }
        if (keywordList.Contains(ActionKeyword.ImgFlashback))
        {
            if (keywordList.Contains(ActionKeyword.First))
            {
                StartCoroutine(ImageFlashBackCoroutine(0));
            }
        }
        if (keywordList.Contains(ActionKeyword.BrokenSound))
        {
            //soundManager.EffectPlay(SFX.BrokenSound);
        }

    }


    public override void TriggerEnter(string triggerName)
    {
        base.TriggerEnter(triggerName);
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (triggerName.Contains("Trigger1") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.First))
            {
                isDialogStopping = false;
                player.SetAnim(PlayController.AnimState.Idle);
                NextDialog();
                player.isPlayPossible = false;
            }
            else if (triggerName.Contains("Trigger2") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.Fourth))
            {
                isDialogStopping = false;
                player.SetAnim(PlayController.AnimState.Idle);
                NextDialog();
                player.isPlayPossible = false;
            }
            else if (triggerName.Contains("Trigger3") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.Seventh))
            {
                isDialogStopping = false;
                player.SetAnim(PlayController.AnimState.Idle);
                NextDialog();
                player.isPlayPossible = false;
            }
        }

    }


    public void PanzaTouch()
    {
  
        if (isDialogStopping == false || nowActionList == null || isPanzaClicked == true)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (keywordList.Contains(ActionKeyword.Panza) && keywordList.Contains(ActionKeyword.Touch))
            {
                TextFrameToggle(false);
                player.SetAnim(PlayController.AnimState.Idle);
                player.isPlayPossible = false;
                firstBlockCollider.SetActive(false);
                isPanzaClicked = true;
                break;
            }
        }
    }

    void BubbleTouch()
    {

        if (isDialogStopping == false || nowActionList == null || isBubbleClicked == true)
        {
            return;
        }
        Debug.Log(Vector3.SqrMagnitude(bubbleObjectParent.transform.position - player.transform.position));
        if (Vector3.SqrMagnitude(bubbleObjectParent.transform.position - player.transform.position) > 40)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (keywordList.Contains(ActionKeyword.Bubble) && keywordList.Contains(ActionKeyword.Touch))
            {

                player.SetAnim(PlayController.AnimState.Idle);
                player.isPlayPossible = false;
                NextDialog();
                //StartCoroutine(ImageFlashBackCoroutine(0));
                isBubbleClicked = true;
                break;
            }
        }
    }

    void BanchanTouch()
    {
        if (isDialogStopping == false || nowActionList == null || isBanchanClicked == true)
        {
            return;
        }
        Debug.Log(Vector3.SqrMagnitude(banchanObject.transform.position - player.transform.position));
        if (Vector3.SqrMagnitude(banchanObject.transform.position - player.transform.position) > 60)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (keywordList.Contains(ActionKeyword.Banchan) && keywordList.Contains(ActionKeyword.Touch))
            {
                player.SetAnim(PlayController.AnimState.Idle);
                player.isPlayPossible = false;
                //NextDialog();

                StartCoroutine(ImageFlashBackCoroutine(1));
                isBanchanClicked = true;
                break;
            }
        }
    }



    IEnumerator PanzaAnimCoroutine()
    {
        Vector3 angle = panzaRect.localEulerAngles;
        while (isPanzaClicked == false)
        {
            yield return null;
        }
        panzaUIImage.color = Color.white;
        firstPanzaObject.SetActive(false);
        player.SetAnim(PlayController.AnimState.Idle);
        player.isPlayPossible = false;
        StartCoroutine(moduleManager.VolumeModule(blurVolume, true, 1));
        float timer = 0;
        Vector3 originPos = panzaRect.anchoredPosition;
        Vector3 originScale = panzaRect.localScale;
        Vector3 originRot = panzaRect.localEulerAngles - new Vector3(0, 0, 360);
        while (timer < 1)
        {
            timer += Time.deltaTime;
            panzaRect.anchoredPosition = Vector3.Lerp(originPos, panzaMiddlePos.anchoredPosition, timer);
            panzaRect.localScale = Vector3.Lerp(originScale, panzaMiddlePos.localScale, timer);
            panzaRect.localEulerAngles = Vector3.Lerp(originRot, panzaMiddlePos.localEulerAngles, timer);
            player.isPlayPossible = false;
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        timer = 0;
        originPos = panzaRect.anchoredPosition;
        originScale = panzaRect.localScale;
        originRot = panzaRect.localEulerAngles + new Vector3(0, 0, 360);
        //if (originRot.x < 180)
        //{
        //    originRot.x += 360;
        //}
        //if (originRot.y < 180)
        //{
        //    originRot.y += 360;
        //}
        //if (originRot.z > 180)
        //{
        //    originRot.z -= 360;
        //}
        StartCoroutine(moduleManager.VolumeModule(blurVolume, false, 1));
        panzaBackGroundObject.SetActive(true);
        panzaBackGroundObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        StartCoroutine(moduleManager.FadeModule_Image(panzaBackGroundObject.GetComponent<Image>(), 0, 1, 1));
        while (timer < 1)
        {
            timer += Time.deltaTime;
            panzaRect.anchoredPosition = Vector3.Lerp(originPos, panzaEndPos.anchoredPosition, timer);
            panzaRect.localScale = Vector3.Lerp(originScale, panzaEndPos.localScale, timer);
            panzaRect.localEulerAngles = Vector3.Lerp(originRot, panzaEndPos.localEulerAngles, timer);
            yield return null;
        }


        panzaRect.anchoredPosition = panzaEndPos.anchoredPosition;
        panzaRect.localScale = panzaEndPos.localScale;
        panzaRect.localEulerAngles = panzaEndPos.localEulerAngles;
        panzaRect.gameObject.SetActive(false);
        panzaItemObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        //roadObject.SetActive(true);
        //roadBlockObject.SetActive(false);
        //SpriteRenderer roadSprite = roadObject.GetComponent<SpriteRenderer>();

        //timer = 0;
        //Color originColor = new Color(1, 1, 1, 0);
        //roadSprite.color = originColor;
        //while (timer < 1)
        //{
        //    timer += Time.deltaTime;
        //    originColor.a = timer;
        //    roadSprite.color = originColor;
        //    yield return null;
        //}
        //roadSprite.color = originColor;
        //panzaRect.gameObject.SetActive(false);


        NextDialog();

        //timer = 0;
        //originPos = cam.transform.position;
        //Vector3 endPos = player.transform.position + new Vector3(-0.3f, 4.16f, -10.0f);
        //while (timer < 0.5f)
        //{
        //    timer += Time.deltaTime*2;
        //    cam.transform.position = Vector3.Lerp(originPos, endPos, timer);
        //    yield return null;
        //}


        StartCoroutine(CameraFollowCoroutine());
    }


    IEnumerator BubbleAnimCoroutine()
    {
        while (!isBubbleClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject touchedObject;               //터치한 오브젝트
                RaycastHit2D hit;                         //터치를 위한 raycastHit
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {
                    touchedObject = hit.collider.gameObject;

                    //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                    if (touchedObject.name.Contains("bubble"))
                    {
                        Debug.Log(touchedObject.name);
                        BubbleTouch();
                    }
                }
            }
            yield return null;

        }
    }


    IEnumerator BanchanAnimCoroutine()
    {
        while (isBanchanClicked == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject touchedObject;               //터치한 오브젝트
                RaycastHit2D hit;                         //터치를 위한 raycastHit
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {
                    touchedObject = hit.collider.gameObject;
                    Debug.Log(touchedObject.name);
                    //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                    if (touchedObject.name.Contains("Banchan"))
                    {
                        BanchanTouch();
                    }
                }
            }
            yield return null;

        }
    }

    IEnumerator ImageFlashBackCoroutine(int seq)
    {
        float postProcessTimer = 0;

        nowSprite = flashBackSpriteArray[seq];
        nowSprite.gameObject.SetActive(true);
        nowSprite.color = new Color(1, 1, 1, 0);
        panzaItemObject.SetActive(false);
        panzaBackGroundObject.SetActive(false);
        Color col = new Color(1, 1, 1, 0);
        while (postProcessTimer < 1)
        {
            postProcessTimer += Time.deltaTime;
            postProcessVolume.weight = postProcessTimer;
            col.a = postProcessTimer;
            nowSprite.color = col;
            yield return null;
        }
        nowSprite.color = new Color(1, 1, 1, 1);
        while (postProcessTimer > 0)
        {
            postProcessTimer -= Time.deltaTime;
            postProcessVolume.weight = postProcessTimer;
            yield return null;
        }
        if (seq == 0)
        {
            bubbleObjectParent.SetActive(false);
        }
        //if(seq == 1)
        //{
        //    medalObject.SetActive(false);
        //}

        NextDialog();
    }

    IEnumerator ImageFalseCoroutine()
    {
        float timer = 0;

        nowSprite.color = new Color(1, 1, 1, 1);
        Color col = new Color(1, 1, 1, 1);
        while (timer < 1)
        {
            timer += Time.deltaTime;
            col.a -= Time.deltaTime;
            nowSprite.color = col;
            yield return null;
        }

        panzaItemObject.SetActive(true);
        panzaBackGroundObject.SetActive(true);
        nowSprite.gameObject.SetActive(true);
        NextDialog();
    }

    IEnumerator VolumeUpCoroutine()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / 3f;
            bgmSource.volume = timer;
            yield return null;
        }
        bgmSource.volume = 1;
    }

    IEnumerator PanzaDragCoroutine()
    {
        bool dragAndDrop = false;
        while (!dragAndDrop)
        {
            if (Input.GetMouseButtonUp(0) && isDraggingPanza &&
                Vector3.Distance(panzaDropSprite.transform.position, player.transform.position) < 10)
            {
                GameObject touchedObject;               //터치한 오브젝트
                RaycastHit2D hit;                         //터치를 위한 raycastHit
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지

                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {

                    touchedObject = hit.collider.gameObject;
                    Debug.Log(touchedObject.name);
                    //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                    if (touchedObject.name.Contains("SecondPanza"))
                    {
                        player.isPlayPossible = false;
                        player.SetAnim(PlayController.AnimState.Idle);
                        secondBlockCollider.SetActive(false);
                        panzaBackGroundObject.SetActive(false);
                        dragAndDrop = true;
                        panzaItemObject.SetActive(false);

                    }
                }
            }
            yield return null;
        }
        StartCoroutine(moduleManager.FadeModule_Sprite(panzaDropSprite.gameObject, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        NextDialog();
        //StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        //yield return new WaitForSeconds(1f);

        //gameManager.LoadScene(SceneName.Memory);

    }

    IEnumerator PanzaDropCoroutine()
    {

        yield return new WaitForSeconds(1f);
        float timer = 0;
        player.isPlayPossible = false;
        player.SetAnim(PlayController.AnimState.Idle);
        StartCoroutine(moduleManager.FadeModule_Sprite(panzaDropSprite.gameObject, 1, 0, 1));
        panzaDropCollider.SetActive(false);
        fishState.GotoNextTarget(4, true, false);

        while (timer < 1)
        {
            timer += Time.deltaTime;
            grainVolume.weight = timer;

            yield return null;
        }
        StartCoroutine(SceneEndCoroutine());
        //StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        //yield return new WaitForSeconds(1f);

        //gameManager.LoadScene(SceneName.Memory);

    }


    IEnumerator SceneEndCoroutine()
    {
        fadeInImage.gameObject.SetActive(true);
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        gameManager.LoadScene(SceneName.MemoryFriendRoom1);
    }



    public void PanzaDrag()
    {
        isDraggingPanza = true;
        panzaItemObject.transform.position = Input.mousePosition;
        panzaBackGroundObject.SetActive(false);
    }

    public void PanzaPointerUp()
    {
        StartCoroutine(InvokerCoroutine(0.05f, DraggingPanzaFalse));
        panzaItemObject.transform.position = panzaItemOriginPos;
        panzaBackGroundObject.SetActive(true);
    }

    void DraggingPanzaFalse()
    {
        isDraggingPanza = false;
    }

}
