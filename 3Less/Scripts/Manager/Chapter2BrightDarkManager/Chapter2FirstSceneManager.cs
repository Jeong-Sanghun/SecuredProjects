using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FirstSceneManager : Chapter2SceneManager
{
  



    protected override void Start()
    {
        base.Start();
        dialogBundle = jsonManager.ResourceDataLoad<DialogBundle>("ThirdChapter1");
        dialogBundle.SetCharacterEnum();
        
        Debug.Log(dialogBundle.dialogList[0].dialog);
        cameraRightBound = 67.8f;
        nowScene = SceneName.Chapter2Bright;
        SaveUserData();
        StartCoroutine(CameraFollowCoroutine());
        StartCoroutine(InvokerCoroutine(1, NextDialog));
    }






    protected override void OverrideAction(List<ActionKeyword> keywordList, List<float> parameterList)
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
        if (keywordList.Contains(ActionKeyword.PlayerMove))
        {
            player.isPlayPossible = true;
        }
        if (keywordList.Contains(ActionKeyword.StopSeconds))
        {
            int index = keywordList.IndexOf(ActionKeyword.StopSeconds);
            StartCoroutine(InvokerCoroutine(parameterList[index], NextDialog));
        }
        if (keywordList.Contains(ActionKeyword.FishMove))
        {
            FishFirstComingStopPoint();
        }
        if(keywordList.Contains(ActionKeyword.Scene) && keywordList.Contains(ActionKeyword.End))
        {
            StartCoroutine(SceneEndCoroutine());
        }

    }


    void FishFirstComingStopPoint()
    {
        player.SetAnim(PlayController.AnimState.Idle);
        fishState.GotoNextTarget(0, false, true);
    }



    public override void TriggerEnter(string triggerName)
    {
        for (int i = 0; i < nowActionList.Count; i++)
        {
            List<ActionKeyword> keywordList = nowActionList[i].actionList;
            if (triggerName.Contains("Trigger1") && keywordList.Contains(ActionKeyword.PlayerMove) && keywordList.Contains(ActionKeyword.First))
            {
                isDialogStopping = false;
                SoundManager.singleton.BGMPlay(BGM.BrightChange);
                player.SetAnim(PlayController.AnimState.Idle);
                NextDialog();
                player.isPlayPossible = false;
            }
        }
    }

    IEnumerator SceneEndCoroutine()
    {
        player.SetAnim(PlayController.AnimState.Idle);
        player.isPlayPossible = false;
        cameraFollowing = false;
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        yield return new WaitForSeconds(1f);

        gameManager.LoadScene(SceneName.Chapter2Dark);
    }

}
