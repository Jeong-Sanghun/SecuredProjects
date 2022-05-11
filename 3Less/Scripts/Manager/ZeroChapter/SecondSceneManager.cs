using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SecondSceneManager : SceneManagerParent
{

    [SerializeField]
    RectTransform scissorRect;

    [SerializeField]
    RectTransform scissorMiddlePos;
    [SerializeField]
    RectTransform scissorEndPos;
    [SerializeField]
    GameObject roadObject;
    [SerializeField]
    GameObject roadBlockObject;

    [SerializeField]
    GameObject[] bubbleObjectArray;
    [SerializeField]
    GameObject bubbleObjectParent;

    [SerializeField]
    PostProcessVolume postProcessVolume;
    [SerializeField]
    PostProcessVolume grainVolume;
    [SerializeField]
    PostProcessVolume blurVolume;

    [SerializeField]
    SpriteRenderer[] flashBackSpriteArray;
    [SerializeField]
    AudioSource bgmSource;

    [SerializeField]
    GameObject medalObject;

    [SerializeField]
    GameObject scissorItemObject;
    [SerializeField]
    GameObject scissorBackGroundObject;
    
    Vector3 scissorItemOriginPos;
    [SerializeField]
    Animator flowerCutAnimator;

    bool isScissorClicked = false;
    bool isBubbleClicked = false;
    bool isMedalClicked = false;
    bool isDraggingScissor = false;
    SpriteRenderer nowSprite;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        nowScene = SceneName.Dark;
        textFrameImage.GetComponent<Image>().raycastTarget = false;
        SaveUserData();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("FirstChapter2");
        dialogBundle.SetCharacterEnum();
        cameraRightBound = 19.8f;
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
        StartCoroutine(InvokerCoroutine(1, NextDialog));
        StartCoroutine(ScissorAnimCoroutine());
        StartCoroutine(BubbleAnimCoroutine());
        StartCoroutine(MedalAnimCoroutine());
        fishState.SetStartLookingRight(false);
        scissorItemOriginPos = scissorItemObject.transform.position;
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
        if (keywordList.Contains(ActionKeyword.ImgFlashback))
        {
            if (keywordList.Contains(ActionKeyword.First))
            {
                StartCoroutine(ImageFlashBackCoroutine(0));
            }
            if (keywordList.Contains(ActionKeyword.Second))
            {
                StartCoroutine(ImageFlashBackCoroutine(1));
            }
        }
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
        if(keywordList.Contains(ActionKeyword.Scissors) && keywordList.Contains(ActionKeyword.Drag))
        {
            StartCoroutine(ScissorsDragCoroutine());
        }
        if (keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
        {
            StartCoroutine(SceneEndCoroutine());
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
            else if (triggerName.Contains("Trigger2") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.Second))
            {
                isDialogStopping = false;
                player.SetAnim(PlayController.AnimState.Idle);
                NextDialog();
                player.isPlayPossible = false;
            }
            else if (triggerName.Contains("Trigger5") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.Third))
            {
                NextDialog();
                isDialogStopping = true;
            }
        }

    }


    public void ScissorTouch()
    {

        if (isDialogStopping == false || nowActionList == null || isScissorClicked == true)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (keywordList.Contains(ActionKeyword.Scissors) && keywordList.Contains(ActionKeyword.Touch))
            {

                player.SetAnim(PlayController.AnimState.Idle);
                player.isPlayPossible = false;
                isScissorClicked = true;
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
                isBubbleClicked = true;
                break;
            }
        }
    }

    void MedalTouch()
    {
        if (isDialogStopping == false || nowActionList == null || isMedalClicked == true)
        {
            return;
        }
        Debug.Log(Vector3.SqrMagnitude(medalObject.transform.position - player.transform.position));
        if (Vector3.SqrMagnitude(medalObject.transform.position - player.transform.position) > 40)
        {
            return;
        }
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (keywordList.Contains(ActionKeyword.Medal) && keywordList.Contains(ActionKeyword.Touch))
            {
                player.SetAnim(PlayController.AnimState.Idle);
                player.isPlayPossible = false;
                NextDialog();
                isMedalClicked = true;
                break;
            }
        }
    }



    IEnumerator ScissorAnimCoroutine()
    {
        Vector3 angle = scissorRect.localEulerAngles;
        bool isPlus = true;
        while(isScissorClicked == false)
        {
            bool lastIsPlus = isPlus;

            float middle = 25f;
            if(angle.z > 180)
            {
                angle.z -= 360;
            }
            if (isPlus && angle.z >40)
            {
                isPlus = false;
            }
            else if(!isPlus && angle.z<10)
            {
                isPlus = true;
            }
            float changingAngle;

            if ((isPlus && angle.z > 35f && angle.z < 40f) || (!isPlus && angle.z < 15f && angle.z > 10f))
            {
                changingAngle = Time.deltaTime * (18 - Mathf.Abs(angle.z - middle)) *3;
            }
            else
            {
                changingAngle = Time.deltaTime * (15)*3;
            }

            if (isPlus)
            {
                angle.z += changingAngle;
            }
            else
            {
                angle.z -= changingAngle;
            }

            scissorRect.localEulerAngles = angle;

            yield return null;
        }
        player.SetAnim(PlayController.AnimState.Idle);
        StartCoroutine(moduleManager.VolumeModule(blurVolume, true, 1));
        float timer = 0;
        Vector3 originPos = scissorRect.transform.position;
        originPos = scissorRect.anchoredPosition;
        Vector3 originScale = scissorRect.transform.localScale;
        Vector3 originRot = scissorRect.transform.localEulerAngles;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            scissorRect.anchoredPosition = Vector3.Lerp(originPos, scissorMiddlePos.anchoredPosition, timer);
            scissorRect.transform.localScale = Vector3.Lerp(originScale, scissorMiddlePos.localScale,timer);
            scissorRect.transform.localEulerAngles = Vector3.Lerp(originRot, scissorMiddlePos.localEulerAngles,timer);
            player.isPlayPossible = false;
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        timer = 0;
        originPos = scissorRect.anchoredPosition;
        originScale = scissorRect.transform.localScale;
        originRot = scissorRect.transform.localEulerAngles;
        //if (originRot.x < 180)
        //{
        //    originRot.x += 360;
        //}
        //if (originRot.y < 180)
        //{
        //    originRot.y += 360;
        //}
        if (originRot.z < 180)
        {
            originRot.z += 360;
        }
        StartCoroutine(moduleManager.VolumeModule(blurVolume, false, 1));
        scissorBackGroundObject.SetActive(true);
        scissorBackGroundObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        StartCoroutine(moduleManager.FadeModule_Image(scissorBackGroundObject.GetComponent<Image>(), 0, 1, 1));
        while (timer < 1)
        {
            timer += Time.deltaTime;
            scissorRect.anchoredPosition = Vector3.Lerp(originPos, scissorEndPos.anchoredPosition, timer);
            scissorRect.transform.localScale = Vector3.Lerp(originScale, scissorEndPos.localScale, timer);
            scissorRect.transform.localEulerAngles = Vector3.Lerp(originRot, scissorEndPos.localEulerAngles, timer);
            yield return null;
        }

        
        scissorRect.anchoredPosition = scissorEndPos.anchoredPosition;
        scissorRect.transform.localScale = scissorEndPos.transform.localScale;
        scissorRect.transform.localEulerAngles = scissorEndPos.transform.localEulerAngles;
        scissorRect.gameObject.SetActive(false);
        scissorItemObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        roadObject.SetActive(true);
        roadBlockObject.SetActive(false);
        SpriteRenderer roadSprite = roadObject.GetComponent<SpriteRenderer>();

        timer = 0;
        Color originColor = new Color(1, 1, 1, 0);
        roadSprite.color = originColor;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            originColor.a = timer;
            roadSprite.color = originColor;
            yield return null;
        }
        roadSprite.color = originColor;
        scissorRect.gameObject.SetActive(false);


        player.isPlayPossible = true;

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
        while(!isBubbleClicked)
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


    IEnumerator MedalAnimCoroutine()
    {
        while (isMedalClicked == false)
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
                    if (touchedObject.name.Contains("Medal"))
                    {
                        
                        MedalTouch();
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
        scissorItemObject.SetActive(false);
        scissorBackGroundObject.SetActive(false);
        Color col = new Color(1, 1, 1, 0);
        while (postProcessTimer<1)
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
        if(seq == 0)
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

        scissorItemObject.SetActive(true);
        scissorBackGroundObject.SetActive(true);
        nowSprite.gameObject.SetActive(true);
        NextDialog();
    }


    IEnumerator ScissorsDragCoroutine()
    {
        bool dragAndDrop = false;
        while (!dragAndDrop)
        {
            if (Input.GetMouseButtonUp(0)&&isDraggingScissor && 
                Vector3.Distance(flowerCutAnimator.transform.position,player.transform.position) < 10)
            {
                GameObject touchedObject;               //터치한 오브젝트
                RaycastHit2D hit;                         //터치를 위한 raycastHit
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
                
                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {
 
                    touchedObject = hit.collider.gameObject;
                    Debug.Log(touchedObject.name);
                    //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                    if (touchedObject.name.Contains("Flower"))
                    {
                        scissorBackGroundObject.SetActive(false);
                        dragAndDrop = true;
                        scissorItemObject.SetActive(false);
                        flowerCutAnimator.SetBool("IsCut", true);
                        
                    }
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        float timer = 0;
        fishState.GotoNextTarget(3, true, false);

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
        gameManager.LoadScene(SceneName.MemoryHome1);
    }

    

    public void ScissorDrag()
    {
        isDraggingScissor = true;
        scissorItemObject.transform.position = Input.mousePosition;
        scissorBackGroundObject.SetActive(false);
    }

    public void ScissorPointerUp()
    {
        StartCoroutine(InvokerCoroutine(0.05f, DraggingScissorFalse));
        scissorItemObject.transform.position = scissorItemOriginPos;
        scissorBackGroundObject.SetActive(true);
    }

    void DraggingScissorFalse()
    {
        isDraggingScissor = false;
    }

}
