using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject goFadeOut;

    private void Start()
    {
        GamePlayManager.Instance.isTitleOn = true;
    }

    void GotoScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_Dream");
    }

    public void PressStart()
    {
        goFadeOut.SetActive(true);

        Invoke("GotoScene", 3f);
    }
}
