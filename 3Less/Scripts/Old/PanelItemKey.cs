using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelItemKey : MonoBehaviour
{
    public GameObject goPanelItemKey;
    public GameObject btnShowInventory;

    public StageManager1 stageManager1;

    public void GotoNextScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("3_Real");
        stageManager1.StartFadeOutReal();
    }

    public void ClosePanel()
    {
        btnShowInventory.SetActive(true);
        goPanelItemKey.SetActive(false);
    }

}
