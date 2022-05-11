using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;

public class Chapter2SceneManager : SceneManagerParent
{
    [SerializeField]
    GameObject[] routeButtonParentArray;
    GameObject nowRouteButtonParent;
    bool isRouteButtonAble;
    Dialog routeDialog;
    bool isRouting;
    ActionKeyword nowChoosedRoute;
    [SerializeField]
    protected PostProcessVolume blurVolume;


    protected override void Start()
    {
        base.Start();

    }

    protected override void NextDialog()
    {
  
        if (dialogEnd == true || isRouting == true)
        {
            return;
        }
        if (nowDialogIndex >= dialogBundle.dialogList.Count)
        {
            return;
        }
        
        Dialog nowDialog = dialogBundle.dialogList[nowDialogIndex];

        if (nowChoosedRoute != ActionKeyword.Null && nowChoosedRoute != ActionKeyword.Route)
        {
            if (nowDialog.actionKeyword != null)
            {
                List<ActionClass> actionClassList = dialogBundle.dialogList[nowDialogIndex].actionList;
                for (int i = 0; i < actionClassList.Count; i++)
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
                            break;
                        }
                        else
                        {
                            int traceStartIndex = nowDialogIndex;
                            for (int k = traceStartIndex; k < dialogBundle.dialogList.Count; k++)
                            {
                                Dialog dialog = dialogBundle.dialogList[k];
                                List<ActionClass> traceActionList = dialog.actionList;
                                if (traceActionList == null)
                                {
                                    continue;
                                }
                                for (int m = 0; m < traceActionList.Count; m++)
                                {
                                    ActionClass action = traceActionList[m];
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
        base.NextDialog();

        if (nowDialog.routeList != null)
        {
            nowChoosedRoute = ActionKeyword.Null;
            isRouting = true;
            isStartOfWrapper = true;
            routeDialog = nowDialog;
            StartCoroutine(CheckRoutePointTextEnd());

        }
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

    void RouteButtonActive()
    {
        List<string> routeList = routeDialog.routeList;

        nowRouteButtonParent = routeButtonParentArray[routeList.Count - 2];
        routeButtonParentArray[routeList.Count - 2].SetActive(true);

        List<Text> routeTextList = new List<Text>();
        isRouteButtonAble = false;
        SaveUserData();

        StartCoroutine(moduleManager.VolumeModule(blurVolume, true, 1));
        for (int i = 0; i < routeList.Count; i++)
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
        StartCoroutine(InvokerCoroutine(1, RouteButtonAbleTrue));


    }

    protected override void SaveUserData()
    {
        GameManager.singleton.saveData.savedScene = nowScene;
        GameManager.singleton.saveData.dialogIndex = nowDialogIndex -1;
        gameManager.SaveSaveData();
    }

    void RouteButtonAbleTrue()
    {
        isRouteButtonAble = true;
    }


    public void OnRouteButton(int index)
    {

        if (isRouteButtonAble == true)
        {
            StartCoroutine(ButtonAnimCoroutine(index));
        }
        isRouteButtonAble = false;


    }

    IEnumerator ButtonAnimCoroutine(int index)
    {
        Transform[] childArray = new Transform[nowRouteButtonParent.transform.childCount];
        for (int i = 0; i < childArray.Length; i++)
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
            timer += Time.deltaTime * 6;
            childArray[index].localScale = Vector3.Lerp(originSize, targetSize, timer);
            yield return null;
        }
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 6;
            childArray[index].localScale = Vector3.Lerp(targetSize, originSize, timer);
            yield return null;
        }
        childArray[index].localScale = originSize;
        yield return new WaitForSeconds(0.1f);


    

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

}
