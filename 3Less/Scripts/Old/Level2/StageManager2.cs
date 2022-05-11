using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager2 : MonoBehaviour
{
    public bool[] isGetItem;
    public GameObject goFadeOut;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        GamePlayManager.Instance.isPlayQuiz = true;
        GamePlayManager.Instance.isTitleOn = false;
    }

    public void SetItem(int _index)
    {
        isGetItem[_index] = true;

        if(_index == 0)
        {
            GamePlayManager.Instance.isGetItem[0] = true;
        }
    }

    public void CheckAllItem()
    {
        int cnt = 0;

        for (int i = 0; i < isGetItem.Length; i++)
        {
            if (isGetItem[i] == true)
            {
                cnt++;
            }
        }

        if (cnt == isGetItem.Length)
        {
            Debug.Log("Done 1111111111111");

            StartCoroutine(EndProcess());
        }
    }

    IEnumerator EndProcess()
    {
        //yield return new WaitForSeconds(1f);

        goFadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("1_Dream");
    }

    public void GotoDreamScene()
    {
        StartCoroutine(EndProcess());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            StartCoroutine(EndProcess());
        }
    }
}
