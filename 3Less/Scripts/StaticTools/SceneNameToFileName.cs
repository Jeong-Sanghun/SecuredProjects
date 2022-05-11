using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneNameToFileName
{

        static Sprite[] archiveButtonSpriteArray;
    static Sprite[] archiveBackgroundSpriteArray;
    public static Sprite GetButtonImage(SceneName scene)
    {
        if(archiveButtonSpriteArray == null)
        {
            archiveButtonSpriteArray = new Sprite[Enum.GetValues(typeof(SceneName)).Length];
        }
        if(archiveButtonSpriteArray[(int)scene] == null)
        {
            archiveButtonSpriteArray[(int)scene] = Resources.Load<Sprite>("Image/ArchiveButtonImage/" + Changer(scene));
        }
        return archiveButtonSpriteArray[(int)scene];
    }

    public static Sprite GetBackgroundImage(SceneName scene)
    {
        if (archiveBackgroundSpriteArray == null)
        {
            archiveBackgroundSpriteArray = new Sprite[Enum.GetValues(typeof(SceneName)).Length];
        }
        if (archiveBackgroundSpriteArray[(int)scene] == null)
        {
            archiveBackgroundSpriteArray[(int)scene] = Resources.Load<Sprite>("Image/ArchiveBackgroundImage/" + Changer(scene));
        }
        return archiveBackgroundSpriteArray[(int)scene];
    }
    public static string Changer(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Bright:
                return "밝은 세계";
            case SceneName.Dark:
                return "어두운 세계";
            case SceneName.MemoryHallway1:
            case SceneName.MemoryHallway2:
            case SceneName.MemoryHallway3:
            case SceneName.MemoryHallway4:
                return "복도";
            case SceneName.MemoryHome1:
            case SceneName.MemoryHome2:
            case SceneName.MemoryHome3:
            case SceneName.MemoryHome4:
            
                return "집";
            case SceneName.MemoryMyRoom:
                return "무명방";
            case SceneName.MemoryRestaurant:
                return "고깃집";
            case SceneName.MemoryRooftop1:
            
                return "옥상";
            case SceneName.MemorySchool1:
            case SceneName.MemorySchool2:
            case SceneName.MemorySchool3:
            case SceneName.MemorySchool4:
            case SceneName.MemorySchool5:
            
                return "학교";
            case SceneName.MemoryDarkStreet1:
            case SceneName.MemoryDarkStreet2:
            
                return "밤거리";
            case SceneName.MemoryBrightStreet1:
            case SceneName.MemoryBrightStreet2:
                return "저녁거리";
            case SceneName.MemoryBrightStreet3:
                return "낮거리";
            case SceneName.MemoryFriendRoom1:
            case SceneName.MemoryFriendRoom2:
            case SceneName.MemoryFriendRoom3:
            case SceneName.MemoryFriendRoom4:
            case SceneName.MemoryFriendRoom5:
            case SceneName.MemoryFriendRoom6:
            case SceneName.MemoryFriendRoom7:
            case SceneName.MemoryFriendRoom8:
            case SceneName.MemoryFriendRoom9:
                return "자취방";
            case SceneName.MemoryStore1:
            case SceneName.MemoryStore2:
            case SceneName.MemoryStore3:
            case SceneName.MemoryStore4:
            case SceneName.MemoryStore5:
            case SceneName.MemoryStore6:
                return "편의점";
            default:
                return "밤거리";


        }
    }
}
