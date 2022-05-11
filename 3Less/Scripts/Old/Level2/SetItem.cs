using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    public GameObject goPanelOwn;
    public StageManager2 stageManager2;
    public int index;

    public void PanelOff()
    {
        goPanelOwn.SetActive(false);
    }

    public void PanelOn()
    {
        goPanelOwn.SetActive(true);
        stageManager2.SetItem(index);
    }

    
}
