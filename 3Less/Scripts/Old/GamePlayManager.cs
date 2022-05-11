using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance;
    public static GamePlayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject userInfoManager = new GameObject("GamePlayManager");
                _instance = userInfoManager.AddComponent<GamePlayManager>();
            }

            return _instance;
        }
    }

    public bool[] isGetItem;
    public bool isPlayQuiz;
    public bool isTitleOn;

    public float time;

    void OnEnable()
    {
        // 씬이 변경되도 게임 오브젝트가 유지될 수 있도록 DontDestroyOnLoad를 호출. 
        DontDestroyOnLoad(this.gameObject);
    }


    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        for(int i = 0; i < isGetItem.Length; i++)
        {
            isGetItem[i] = false;
        }

        isPlayQuiz = false;
        isTitleOn = false;
    }

    public void SetUi()
    {
        if(isGetItem[0] == true)
        {
            if(GameObject.Find("StageManager1").GetComponent<StageManager1>() != null)
            {
                GameObject.Find("StageManager1").GetComponent<StageManager1>().ShowInventoryButton();
            }
        }

        if(isPlayQuiz == true)
        {
            if (GameObject.Find("goPlayer") != null)
            {
                Vector3 temp = GameObject.Find("goPlayer").transform.position;

                GameObject.Find("goPlayer").transform.position
                    = new Vector3(53f, temp.y, temp.z);
            }

            if(GameObject.Find("imageInfo") != null)
            {
                GameObject.Find("imageInfo").SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(isTitleOn == false)
        {
            if(Input.GetKey(KeyCode.A))
            {
                time = 0f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                time = 0f;
            }
            else
            {
                time += Time.deltaTime;

                if(time > 300f)
                {
                    time = 0f;
                    isTitleOn = true;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("0_Title");
                    Init();
                }
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                time = 0f;
                isTitleOn = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("0_Title");
                Init();
            }
        }
    }

}
