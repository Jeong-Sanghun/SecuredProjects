using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public enum SceneName
{
    MainMenu,Intro,Bright,Dark,

    MemoryHome1,MemoryRestaurant,
    MemoryDarkStreet1,MemoryRooftop1,MemorySchool1,MemoryHome2,
    MemoryHallway1, MemoryBrightStreet1,MemoryHome3,MemoryMyRoom,
    MemorySchool2,MemoryHome4,MemoryBrightStreet2,

    Chapter2Bright,Chapter2Dark,

    MemoryFriendRoom1,MemoryBrightStreet3,MemoryFriendRoom2,MemoryStore1,MemoryFriendRoom3,
    MemorySchool3,MemoryHallway2,MemoryStore2, MemoryFriendRoom4, MemoryStore3, MemoryFriendRoom5, MemoryFriendRoom6,
    MemorySchool4,MemoryTeacherRoom1,MemoryHallway3,MemoryStore4,MemoryDarkStreet2,
    MemoryFriendRoom7,MemoryTeacherRoom2,MemoryStore5,MemoryTeacherRoom3,MemorySchool5,MemoryFriendRoom8,MemoryTeacherRoom4,
    MemoryStore6,MemoryHallway4,MemoryTeacherRoom5,MemoryFriendRoom9,

       
    GameEnd, EndContents

}

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public SaveDataClass saveData;
    [SerializeField]
    ModuleManager moduleManager;
    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    Image gameOverBackGround;
    [SerializeField]
    Image gameOverFish;
    [SerializeField]
    Text[] gameOverTextArray;

    JsonManager jsonManager;

    [HideInInspector]
    public int wholeSceneNumber;
    
    //SNS매니저에서 씀
    public SceneName nowScene;
    [HideInInspector]
    public bool isNewGame;
    [HideInInspector]
    public bool isGameOver;
    [SerializeField]
    InputField debugInputField;

    // Start is called before the first frame update
    void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        nowScene = SceneName.MainMenu;
        jsonManager = new JsonManager();
        isNewGame = true;
        isGameOver = false;
        wholeSceneNumber = SceneManager.sceneCountInBuildSettings;
        SoundManager.singleton.SetBGMOnSceneStart(SceneName.MainMenu);
        //디버그
        saveData = jsonManager.LoadSaveData();
    }

    public void LoadScene(SceneName scene)
    {
        //StartCoroutine(SceneLoadCoroutine(scene));
        SceneManager.LoadScene((int)scene);
        SoundManager.singleton.EffectStop();
        SoundManager.singleton.SetBGMOnSceneStart(scene);
        PhoneManager.singleTon.phoneInstagramManager.AddPost(scene);
        PhoneManager.singleTon.phoneTwitterManager.AddPost(scene);
        PhoneManager.singleTon.PhoneMainCanvasActive(false);
        //if ((int)scene >= 4)
        //{
        //    PhoneManager.singleTon.PhoneMainCanvasActive(true);

        //}
        //else
        //{
        //    PhoneManager.singleTon.PhoneMainCanvasActive(false);
        //}

    }

    IEnumerator SceneLoadCoroutine(SceneName scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;


    }

    public void LoadSaveData()
    {
        saveData = jsonManager.LoadSaveData();
        nowScene = saveData.savedScene;
        PhoneManager.singleTon.PhoneSetup();
    }

    public void SaveSaveData()
    {
        jsonManager.SaveJson(saveData);
    }

    public void SaveEndContents()
    {
        jsonManager.SaveEndContents(saveData);
    }

    public void StartLoadedGame()
    {
        isNewGame = false;
        LoadSaveData();
        LoadScene(saveData.savedScene);
    }

    public void StartEndContents()
    {
        saveData = jsonManager.LoadEndContents();
        nowScene = SceneName.EndContents;
        PhoneManager.singleTon.PhoneSetup();
        LoadScene(nowScene);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        for(int i = 0; i < gameOverTextArray.Length; i++)
        {
            gameOverTextArray[i].color = new Color(1, 1, 1, 0);
            StartCoroutine(moduleManager.FadeModule_Text(gameOverTextArray[i], 0, 1, 1));
        }
        gameOverBackGround.color = new Color(0, 0, 0, 0);
        StartCoroutine(moduleManager.FadeModule_Image(gameOverBackGround, 0, 0.93f, 1));
        gameOverFish.color = new Color(1, 1, 1, 0);
        StartCoroutine(moduleManager.FadeModule_Image(gameOverFish, 0, 1, 1));

    }

    public void GameOverBackButton()
    {
        
        StartLoadedGame();
        gameOverCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DebugSceneLoadButton()
    {
        int outing = -1;
        if(int.TryParse(debugInputField.text,out outing))
        {
            int sceneNum = int.Parse(debugInputField.text);
            if (sceneNum < 0 || sceneNum > Enum.GetValues(typeof(SceneName)).Length)
            {
                return;
            }
            LoadScene((SceneName)sceneNum);
        }
    }

}
