using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameToString
{
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
                return "방";
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
                return "밤 거리";
            case SceneName.MemoryBrightStreet1:
            case SceneName.MemoryBrightStreet2:
                return "저녁 거리";
            case SceneName.MemoryBrightStreet3:
                return "낮 거리";
            case SceneName.MemoryStore1:
            case SceneName.MemoryStore2:
            case SceneName.MemoryStore3:
            case SceneName.MemoryStore4:
            case SceneName.MemoryStore5:
            case SceneName.MemoryStore6:
                return "편의점";
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
            case SceneName.MemoryTeacherRoom1:
            case SceneName.MemoryTeacherRoom2:
            case SceneName.MemoryTeacherRoom3:
            case SceneName.MemoryTeacherRoom4:
            case SceneName.MemoryTeacherRoom5:
                return "교무실";

            default:
                return "";


        }
    }
}
