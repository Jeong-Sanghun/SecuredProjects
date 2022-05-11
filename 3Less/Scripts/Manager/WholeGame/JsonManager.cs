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

    //���̺굥���͸� ���̺�����.
    public void SaveJson(SaveDataClass saveData)
    {
        string jsonText;


        //�ȵ���̵忡���� ���� ��ġ�� �ٸ��� ���־�� �Ѵ�
        //Application.dataPath�� �̿��ϸ� ���� �������� ���۸� �غ��� �ٶ���
        //�ȵ���̵��� ��쿡�� ������������ �������� 2�������ͷ� ��ȯ�� �ؾ��Ѵ�

        string savePath = Application.dataPath;
        string appender = "/userData/";
        string nameString = "SaveData";
        string dotJson = ".json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
            //�̰ų��߿� ����ߵ�
            savePath = Application.persistentDataPath;
        
#endif
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(nameString);
        builder.Append(dotJson);

        jsonText = JsonUtility.ToJson(saveData, true);
        //�̷����� �ϴ� �����Ͱ� �ؽ�Ʈ�� ��ȯ�� �ȴ�
        //jsonUtility�� �̿��Ͽ� data�� WholeGameData�� json������ text�� �ٲپ��ش�

        //���Ͻ�Ʈ���� �̷��� �������ְ� �������ָ�ȴ� ��
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public T ResourceDataLoad<T>(string name)
    {
        //���� �츮�� ������ �����ߴ� �����͸� �������Ѵ�
        //���� ������ �����Ͱ� ���ٸ�? �̰� ���� ���ϰ� Ʃ�丮���� �����ϸ� �׸��̴�. �� �۾��� ���δ����� ���ش�
        T gameData;
        string directory = "JsonData/";

        string appender1 = name;
        //        string appender2 = ".json";
        StringBuilder builder = new StringBuilder(directory);
        builder.Append(appender1);
        //      builder.Append(appender2);
        //�������� ���̺�� �Ȱ���
        //���Ͻ�Ʈ���� ������ش�. ���ϸ�带 open���� �ؼ� �����ش�. �� ���۸��̴�
        TextAsset jsonString = Resources.Load<TextAsset>(builder.ToString());
        gameData = JsonUtility.FromJson<T>(jsonString.ToString());

        return gameData;
        //�� ������ ���ӸŴ�����, �ε����� �Ѱ��ִ� ���̴�
    }

   
    //�ε�, ���ӸŴ������� ȣ��
    public SaveDataClass LoadSaveData()
    {
        //���� �츮�� ������ �����ߴ� �����͸� �������Ѵ�
        //���� ������ �����Ͱ� ���ٸ�? �̰� ���� ���ϰ� Ʃ�丮���� �����ϸ� �׸��̴�. �� �۾��� ���δ����� ���ش�
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
        //�������� ���̺�� �Ȱ���
        //���Ͻ�Ʈ���� ������ش�. ���ϸ�带 open���� �ؼ� �����ش�. �� ���۸��̴�
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //���̺� ������ �ִ°��

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //�ؽ�Ʈ�� string���� �ٲ۴����� FromJson�� �־��ָ��� �츮�� �� �� �ִ� ��ü�� �ٲ� �� �ִ�
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            //���̺������� ���°��
            gameData = new SaveDataClass();
        }
        return gameData;
        //�� ������ ���ӸŴ�����, �ε����� �Ѱ��ִ� ���̴�
    }

    public SaveDataClass LoadEndContents()
    {
        //���� �츮�� ������ �����ߴ� �����͸� �������Ѵ�
        //���� ������ �����Ͱ� ���ٸ�? �̰� ���� ���ϰ� Ʃ�丮���� �����ϸ� �׸��̴�. �� �۾��� ���δ����� ���ش�
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
        //�������� ���̺�� �Ȱ���
        //���Ͻ�Ʈ���� ������ش�. ���ϸ�带 open���� �ؼ� �����ش�. �� ���۸��̴�
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //���̺� ������ �ִ°��

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //�ؽ�Ʈ�� string���� �ٲ۴����� FromJson�� �־��ָ��� �츮�� �� �� �ִ� ��ü�� �ٲ� �� �ִ�
            gameData = JsonUtility.FromJson<SaveDataClass>(jsonData);
        }
        else
        {
            //���̺������� ���°��
            return null;
        }
        return gameData;
        //�� ������ ���ӸŴ�����, �ε����� �Ѱ��ִ� ���̴�
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
        //�������� ���̺�� �Ȱ���
        //���Ͻ�Ʈ���� ������ش�. ���ϸ�带 open���� �ؼ� �����ش�. �� ���۸��̴�
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //���丮�� ���°�� ������ش�
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
            //���̺������� ���°��
            return false;
        }
    }

    //���̺굥���͸� ���̺�����.
    public void SaveEndContents(SaveDataClass saveData)
    {
        string jsonText;


        //�ȵ���̵忡���� ���� ��ġ�� �ٸ��� ���־�� �Ѵ�
        //Application.dataPath�� �̿��ϸ� ���� �������� ���۸� �غ��� �ٶ���
        //�ȵ���̵��� ��쿡�� ������������ �������� 2�������ͷ� ��ȯ�� �ؾ��Ѵ�

        string savePath = Application.dataPath;
        string appender = "/userData";
        string nameString = "/EndContents";
        string saveDataPath = "/SaveData";
        string dotJson = ".json";

#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
            //�̰ų��߿� ����ߵ�
            savePath = Application.persistentDataPath;
        Debug.Log(savePath);
#endif
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //���丮�� ���°�� ������ش�
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(saveDataPath);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            //���̺� ������ �ִ°��

            File.Delete(builder.ToString());
        }

        builder = new StringBuilder(savePath);
        builder.Append(appender);
        builder.Append(nameString);
        builder.Append(dotJson);

        jsonText = JsonUtility.ToJson(saveData, true);
        //�̷����� �ϴ� �����Ͱ� �ؽ�Ʈ�� ��ȯ�� �ȴ�
        //jsonUtility�� �̿��Ͽ� data�� WholeGameData�� json������ text�� �ٲپ��ش�

        //���Ͻ�Ʈ���� �̷��� �������ְ� �������ָ�ȴ� ��
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }
}

