using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackLogBundle
{
    public List<BackLogWrapper> backLogWrapperList;

    public BackLogBundle()
    {
        backLogWrapperList = new List<BackLogWrapper>();
    }
}
