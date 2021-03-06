using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeterIngameComment
{
    public GameObject commentObject;

    public TreeterIngameComment()
    {

    }

    public void SetButton(TreeterCommentData data, GameObject prefab,Transform prefabParent)
    {
        commentObject = GameObject.Instantiate(prefab, prefabParent);
        commentObject.SetActive(true);
        Text profileNameText = commentObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Image profileImage = commentObject.transform.GetChild(0).GetComponent<Image>();
        Text dialogText = commentObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        profileNameText.text = TabletTreeterManager.profileWrapper.LoadIngameName(data.profileFileName);
        //data.profileIngameName;
        profileImage.sprite = TabletTreeterManager.profileWrapper.LoadSprite(data.profileFileName);
            //data.LoadSprite();
        dialogText.text = data.dialog;

    }


    public void SetTutorialButton(TreeterCommentData data, GameObject prefab, Transform prefabParent)
    {
        commentObject = GameObject.Instantiate(prefab, prefabParent);
        commentObject.SetActive(true);
        Text profileNameText = commentObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Image profileImage = commentObject.transform.GetChild(0).GetComponent<Image>();
        Text dialogText = commentObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        profileNameText.text = TutorialTreeterManager.profileWrapper.LoadIngameName(data.profileFileName);
        //data.profileIngameName;
        profileImage.sprite = TutorialTreeterManager.profileWrapper.LoadSprite(data.profileFileName);
        //data.LoadSprite();
        dialogText.text = data.dialog;

    }
}
