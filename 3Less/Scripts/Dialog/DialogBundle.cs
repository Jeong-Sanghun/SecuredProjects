using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogBundle
{
    public string bundleName;
    public List<Dialog> dialogList;

    public void SetCharacterEnum()
    {
        for(int i = 0; i < dialogList.Count; i++)
        {
            dialogList[i].SetCharacterEnum();
        }
    }
    public DialogBundle()
    {
        dialogList = new List<Dialog>();
    }
}
