using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager1 : MonoBehaviour
{
    public GameObject goArrow;

    public GameObject goFadeOut;

    public GameObject goInventory;
    public GameObject btnInventory;

    public GameObject[] goText;
    public SwapImage swapImage;

    public GameObject goPanelOption;

    private bool isOpenPanelOption;

    //public AudioSource audioBGM;
    public GameObject goAudioIcon;

    private bool isAudioOn;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        isOpenPanelOption = false;

        isAudioOn = true;


        goAudioIcon.SetActive(false);
        btnInventory.SetActive(false);
        goPanelOption.SetActive(false);

        GamePlayManager.Instance.SetUi();

        GamePlayManager.Instance.isTitleOn = false;

        if (GamePlayManager.Instance.isPlayQuiz == false)
        {
            StartCoroutine(ShowText());
        }
    }

    public void OpenPanelOption()
    {
        isOpenPanelOption = true;
        goPanelOption.SetActive(true);
    }

    public void ClosePanelOption()
    {
        isOpenPanelOption = false;
        goPanelOption.SetActive(false);
    }

    public void SetAudio()
    {
        isAudioOn = !isAudioOn;

        if (isAudioOn)
        {
            AudioOn();
        }
        else
        {
            AudioOff();
        }
    }

    private void AudioOn()
    {
        goAudioIcon.SetActive(false);
        GamePlayManager.Instance.gameObject.GetComponent<AudioSource>().volume = 1f;
    }

    private void AudioOff()
    {
        goAudioIcon.SetActive(true);
        GamePlayManager.Instance.gameObject.GetComponent<AudioSource>().volume = 0f;
    }


    public void GameQuit()
    {
        Application.Quit();
    }


    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(7f);
        goText[0].SetActive(true);

        yield return new WaitForSeconds(6f);
        goText[1].SetActive(true);

        yield return new WaitForSeconds(6f);
        goText[2].SetActive(true);

        yield return new WaitForSeconds(6f);
        goText[3].SetActive(true);

        yield return new WaitForSeconds(6f);
        goText[4].SetActive(true);
    }

    public void ShowArrow()
    {
        goArrow.SetActive(true);
    }

    public void StartFadeOut()
    {
        goFadeOut.SetActive(true);
        Invoke("GotoScene", 3f);
    }

    public void StartFadeOutReal()
    {
        goFadeOut.SetActive(true);
        Invoke("GotoSceneReal", 3f);
    }

    public void ShowInventoryButton()
    {
        btnInventory.SetActive(true);
       
    }


    public void ShowInventory()
    {
        goInventory.SetActive(true);
        btnInventory.SetActive(false);
        swapImage.go2.enabled = false;
    }

    public void CloseInventory()
    {
        goInventory.SetActive(false);
        btnInventory.SetActive(true);
    }


    void GotoScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2_Game1");
    }


    void GotoSceneReal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3_Real");
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpenPanelOption)
            {
                ClosePanelOption();
            }
            else
            {
                OpenPanelOption();
            }
        }
    }
}
