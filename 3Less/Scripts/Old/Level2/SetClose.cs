using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClose : MonoBehaviour
{
    public StageManager2 stageManager2;

    public void PanelClose()
    {
        gameObject.SetActive(false);
        stageManager2.CheckAllItem();
    }
}
