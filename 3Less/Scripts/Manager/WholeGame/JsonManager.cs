using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonManager    //SH
{
    public static List<BackLogDialogPair> backLogDialogPairList;


    public DialogBundle GetDialog(SceneName scene)
    {
        if(backLogDialogPairList == null)
        {
            backLogDialogPairList = new List<BackLogDialogPair>();
        }
        for(int i = 0; i < backLogDialogPairList.Count; i++)
        {
            if(backLogDialogPairList[i].sceneName == scene)
            {
                return backLogDialogPairList[i].dialogBundle;
            }
        }
        string fileName = SceneNameToNovel.Changer(scene);
        if (fileName == null)
        {
            return null;
        }
        DialogBundle bundle = ResourceDataLoad<DialogBundle>(fileName);
        bundle.SetCharacterEnum();
        BackLogDialogPair pair = new BackLogDialogPair(scene,bundle);
        backLogDialogPairList.Add(pair);
        return bundle;
    }

    public BackLogDialogPair GetDialogPair(SceneName scene)
    {
        if (backLogDialogPairList == null)
        {
            backLogDialogPairList = new List<BackLogDialogPair>();
        }
        for (int i = 0; i < backLogDialogPairList.Count; i++)
        {
            if (backLogDialogPairList[i].sceneName == scene)
            {
                return backLogDialogPairList[i];
            }
        }
        string fileName = SceneNameToNovel.Changer(scene);
        if (fileName == null)
        {
            return null;
        }
        DialogBundle bundle = ResourceDataLoad<DialogBundle>(fileName);
        bundle.SetCharacterEnum();
        BackLogDialogPair pair = new BackLogDialogPair(scene, bundle);
        backLogDialogPairList.Add(pair);
        return pair;
    }

    //세이브데이터를 세이브해줌.
    public void SaveJson(SaveDataClass saveData)
    {
        string jsonText;


        //안드로이드에서의 저장 위치를 다르게 해주어야 한다
        //Application.dataPath를 이용하면 어디로 가는지는 구글링 해보길 바란다
        //안드로이드의 경우에는 데이터조작을 막기위해 2진데이터로 변환을 해야한다

        string savePath = Application.dataPath;
        string appender = "/userData/";
        string nameString = "SaveData";
        string dotJson = ".json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
            //이거나중에 살려야됨
            savePath = Application.persistentDataPath;
        
#endif
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(nameString);
        builder.Append(dotJson);

        jsonText = JsonUtility.ToJson(saveData, true);
        //이러면은 일단 데이터가 텍스트로 변환이 된다
        //jsonUtility를 이용하여 data인 WholeGameData를 json형식의 text로 바꾸어준다

        //파일스트림을 이렇게 지정해주고 저장해주면된당 끗
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public T ResourceDataLoad<T>(string name)
    {
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        //만약 저장한 데이터가 없다면? 이걸 실행 안하고 튜토리얼을 실행하면 그만이다. 그 작업은 씬로더에서 해준다
        T gameData;
        string directory = "JsonData/";

        string appender1 = name;
        //        string appender2 = ".json";
        StringBuilder builder = new StringBuilder(directory);
        builder.Append(appender1);
        //      builder.Append(appender2);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        TextAsset jsonString = Resources.Load<TextAsset>(builder.ToString());
        gameData = JsonUtility.FromJson<T>(jsonString.ToString());

        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }

   
    //로딩, 게임매니저에서 호출
    public SaveDataClass LoadSaveData()
    {
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        //만약 저장한 데이터가 없다면? 이걸 실행 안하고 튜토리얼을 실행하면 그만이다. 그 작업은 씬로더에서 해준다
        SaveDataClass gameData;
        string loadPath = Application.dataPath;
        string directory = "/userData";
        string appender = "/SaveData";

        string dotJson = ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
            loadPath = Application.persistentDataPath;
        

#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //세이브 파일이 있는경우

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //텍스트를 string으로 바꾼다음에 FromJson에 넣어주면은 우리가 쓸 수 있는 객체로 바꿀 수 있다
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            gameData = new SaveDataClass();
        }
        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }

    public SaveDataClass LoadEndContents()
    {
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        //만약 저장한 데이터가 없다면? 이걸 실행 안하고 튜토리얼을 실행하면 그만이다. 그 작업은 씬로더에서 해준다
        SaveDataClass gameData;
        string loadPath = Application.dataPath;
        string directory = "/userData";

        string appender = "/EndContents";

        string dotJson = ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
            loadPath = Application.persistentDataPath;
        Debug.Log(loadPath);

#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //세이브 파일이 있는경우

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //텍스트를 string으로 바꾼다음에 FromJson에 넣어주면은 우리가 쓸 수 있는 객체로 바꿀 수 있다
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            return null;
        }
        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }

    public bool CheckEndContents()
    {
        string loadPath = Application.dataPath;
        string directory = "/userData";

        string appender = "/EndContents";

        string dotJson = ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
            loadPath = Application.persistentDataPath;
        

#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            return true;
        }
        else
        {
            //세이브파일이 없는경우
            return false;
        }
    }

    //세이브데이터를 세이브해줌.
    public void SaveEndContents(SaveDataClass saveData)
    {
        string jsonText;


        //안드로이드에서의 저장 위치를 다르게 해주어야 한다
        //Application.dataPath를 이용하면 어디로 가는지는 구글링 해보길 바란다
        //안드로이드의 경우에는 데이터조작을 막기위해 2진데이터로 변환을 해야한다

        string savePath = Application.dataPath;
        string appender = "/userData";
        string nameString = "/EndContents";
        string saveDataPath = "/SaveData";
        string dotJson = ".json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
            //이거나중에 살려야됨
            savePath = Application.persistentDataPath;
        Debug.Log(savePath);
#endif
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(saveDataPath);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //세이브 파일이 있는경우

            File.Delete(builder.ToString());
        }

        builder = new StringBuilder(savePath);
        builder.Append(appender);
        builder.Append(nameString);
        builder.Append(dotJson);

        jsonText = JsonUtility.ToJson(saveData, true);
        //이러면은 일단 데이터가 텍스트로 변환이 된다
        //jsonUtility를 이용하여 data인 WholeGameData를 json형식의 text로 바꾸어준다

        //파일스트림을 이렇게 지정해주고 저장해주면된당 끗
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }
}

