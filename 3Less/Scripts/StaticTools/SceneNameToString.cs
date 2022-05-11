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
                return "���� ����";
            case SceneName.Dark:
                return "��ο� ����";

            case SceneName.MemoryHallway1:
            case SceneName.MemoryHallway2:
            case SceneName.MemoryHallway3:
            case SceneName.MemoryHallway4:
                return "����";
            case SceneName.MemoryHome1:
            case SceneName.MemoryHome2:
            case SceneName.MemoryHome3:
            case SceneName.MemoryHome4:
                return "��";
            case SceneName.MemoryMyRoom:
                return "��";
            case SceneName.MemoryRestaurant:
                return "�����";
            case SceneName.MemoryRooftop1:
                return "����";
            case SceneName.MemorySchool1:
            case SceneName.MemorySchool2:
            case SceneName.MemorySchool3:
            case SceneName.MemorySchool4:
            case SceneName.MemorySchool5:
                return "�б�";
            case SceneName.MemoryDarkStreet1:
            case SceneName.MemoryDarkStreet2:
                return "�� �Ÿ�";
            case SceneName.MemoryBrightStreet1:
            case SceneName.MemoryBrightStreet2:
                return "���� �Ÿ�";
            case SceneName.MemoryBrightStreet3:
                return "�� �Ÿ�";
            case SceneName.MemoryStore1:
            case SceneName.MemoryStore2:
            case SceneName.MemoryStore3:
            case SceneName.MemoryStore4:
            case SceneName.MemoryStore5:
            case SceneName.MemoryStore6:
                return "������";
            case SceneName.MemoryFriendRoom1:
            case SceneName.MemoryFriendRoom2:
            case SceneName.MemoryFriendRoom3:
            case SceneName.MemoryFriendRoom4:
            case SceneName.MemoryFriendRoom5:
            case SceneName.MemoryFriendRoom6:
            case SceneName.MemoryFriendRoom7:
            case SceneName.MemoryFriendRoom8:
            case SceneName.MemoryFriendRoom9:
                return "�����";
            case SceneName.MemoryTeacherRoom1:
            case SceneName.MemoryTeacherRoom2:
            case SceneName.MemoryTeacherRoom3:
            case SceneName.MemoryTeacherRoom4:
            case SceneName.MemoryTeacherRoom5:
                return "������";

            default:
                return "";


        }
    }
}
