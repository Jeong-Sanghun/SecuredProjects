using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;
public class FirstSceneManager : SceneManagerParent
{
    // Start is called before the first frame update


    [SerializeField]
    FishState1 fish;

    Vector3 camStartPos;
    Vector3 camZoomPos;

    [SerializeField]
    SpriteRenderer blackBoxSprite;
    [SerializeField]
    SpriteMask spriteMask;
    [SerializeField]
    Sprite[] maskSpriteArray;
    [SerializeField]
    PostProcessVolume postProcessVolume;
    [SerializeField]
    AudioSource bgmSource;
    



    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("FirstChapter1");
        dialogBundle.SetCharacterEnum();

        camStartPos = cam.transform.position;
        camZoomPos = new Vector3(2.43f, -0.02f, -10);
        nowScene = SceneName.Bright;
        SaveUserData();
        StartCoroutine(InvokerCoroutine(1, NextDialog));
        maskSpriteArray = Resources.LoadAll<Sprite>("curtain/");
        cameraRightBound = 67.8f;
        
    }






    protected override void OverrideAction(List<ActionKeyword> keywordList,List<float> parameterList)
    {
        for (int j = 0; j < keywordList.Count; j++)
        {
            Debug.Log(keywordList[j]);
        }

        if (keywordList.Contains(ActionKeyword.FadeOut))
        {
            StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
            StartCoroutine(InvokerCoroutine(1, SetDialogStopFalse));
        }
        if (keywordList.Contains(ActionKeyword.ZoomOut))
        {
            StartCoroutine(CameraZoomCoroutine());
        }
        if (keywordList.Contains(ActionKeyword.PlayerMove))
        {
            Debug.Log("이게실행되니?");
            player.isPlayPossible = true;
        }
        if (keywordList.Contains(ActionKeyword.StopSeconds))
        {
            int index = keywordList.IndexOf(ActionKeyword.StopSeconds);
            StartCoroutine(InvokerCoroutine(parameterList[index], NextDialog));
        }
        if (keywordList.Contains(ActionKeyword.FishMove))
        {
            if (keywordList.Contains(ActionKeyword.First))
            {
                FishFirstComingStopPoint();
            }
            if (keywordList.Contains(ActionKeyword.Second))
            {
                FishSecondComingStopPoint();
            }
            if (keywordList.Contains(ActionKeyword.Third))
            {
                fish.GotoTarget3();
            }
        }

    }


    void FishFirstComingStopPoint()
    {
        player.SetAnim(PlayController.AnimState.Idle);
        fish.GotoTarget1();
    }

    void FishSecondComingStopPoint()
    {
        player.SetAnim(PlayController.AnimState.Idle);
        fish.GotoTarget2();
        StartCoroutine(InvokerCoroutine(0.5f, NextDialog));

    }

    IEnumerator CameraZoomCoroutine()
    {
        float timer = 0;
        float startOrtho = cam.orthographicSize;
        float endOrtho = 5.4f;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            yield return null;
            cam.transform.position = Vector3.Lerp(camStartPos, camZoomPos, timer);
            cam.orthographicSize = Mathf.Lerp(startOrtho, endOrtho, timer);
        }

        
        cam.transform.localPosition = camZoomPos;
        StartCoroutine(CameraFollowCoroutine());
        cam.orthographicSize = endOrtho;
        isDialogStopping = false;
        TextFrameToggle(true);
        NextDialog();

    }

    public override void TriggerEnter(string triggerName)
    {
        for(int i = 0; i < nowActionList.Count; i++)
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
                SoundManager.singleton.BGMPlay(BGM.BrightChange);
                NextDialog();
                isDialogStopping = true;
            }
            else if(triggerName.Contains("Trigger6") && keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
            {
                isStopActionable = false;
                StartCoroutine(SceneEndCoroutine());
            }
        }
    }

    IEnumerator SceneEndCoroutine()
    {
        float timer = 0;
        player.SetAnim(PlayController.AnimState.Idle);
        player.isPlayPossible = false;
        cameraFollowing = false;
        


        blackBoxSprite.gameObject.SetActive(true);
        blackBoxSprite.color = new Color(0, 0, 0, 0);

        Color col = new Color(0, 0, 0, 0);
        while (timer < 0.3f)
        {
            timer += Time.deltaTime*1.5f;
            
            col.a = timer;
            blackBoxSprite.color = col;
            yield return null;
        }
        player.isPlayPossible = false;
        spriteMask.gameObject.SetActive(true);
        int spriteIndex = 0;
        while (spriteIndex < maskSpriteArray.Length)
        {
            spriteMask.sprite = maskSpriteArray[spriteIndex];
            spriteIndex++;
            yield return new WaitForFixedUpdate();
        }
        player.isPlayPossible = false;
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject touchedObject;               //터치한 오브젝트
                RaycastHit2D hit;                         //터치를 위한 raycastHit
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {
                    touchedObject = hit.collider.gameObject;

                    //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                    if (touchedObject.name.Contains("curtain"))
                    {
                        break;
                    }
                }
            }
        }
        player.isPlayPossible = false;
        timer = 0;
        float startOrtho = cam.orthographicSize;
        float endOrtho = 0.1f;
        Vector3 camEndPos = new Vector3(70, 0, -10);
        Vector3 camOriginPos = cam.transform.position;
        float accel = 0.6f;

        while (timer < 1)
        {
            accel += Time.deltaTime/2f;
            timer += accel *Time.deltaTime;
            bgmSource.volume = 1- timer;
            cam.transform.position = Vector3.Lerp(camOriginPos, camEndPos, timer);
            cam.orthographicSize = Mathf.Lerp(startOrtho, endOrtho, timer);
            postProcessVolume.weight = timer;
            blackBoxSprite.color = new Color(timer, timer, timer, 0.5f);
            yield return null;
        }
        blackBoxSprite.color = new Color(0, 0, 0, 1);
        blackBoxSprite.maskInteraction = SpriteMaskInteraction.None;

        gameManager.LoadScene(SceneName.Dark);




    }




}
