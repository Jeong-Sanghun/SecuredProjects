using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameToNovel
{
  
    public static string Changer(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Bright:
                return "FirstChapter1";
            case SceneName.Dark:
                return "FirstChapter2";
            case SceneName.MemoryHallway1:
                return "SecondChapter7";
            case SceneName.MemoryHome1:
                return "SecondChapter1";
            case SceneName.MemoryHome2:
                return "SecondChapter6";
            case SceneName.MemoryHome3:
                return "SecondChapter9";
            case SceneName.MemoryHome4:
                return "SecondChapter12";
            case SceneName.MemoryMyRoom:
                return "SecondChapter10";
            case SceneName.MemoryRestaurant:
                return "SecondChapter2";
            case SceneName.MemoryRooftop1:
                return "SecondChapter4";
            case SceneName.MemorySchool1:
                return "SecondChapter5";
            case SceneName.MemorySchool2:
                return "SecondChapter11";
            case SceneName.MemoryDarkStreet1:
                return "SecondChapter3";
            case SceneName.MemoryBrightStreet1:
                return "SecondChapter8";
            case SceneName.MemoryBrightStreet2:
                return "SecondChapter13";
            case SceneName.MemoryFriendRoom1:
                return "ThirdChapter3";
            case SceneName.MemoryBrightStreet3:
                return "ThirdChapter4";
            case SceneName.MemoryFriendRoom2:
                return "ThirdChapter5";
            case SceneName.MemoryStore1:
                return "ThirdChapter6";
            case SceneName.MemoryFriendRoom3:
                return "ThirdChapter7";
            case SceneName.MemorySchool3:
                return "ThirdChapter8";
            case SceneName.MemoryHallway2:
                return "ThirdChapter9";
            case SceneName.MemoryStore2:
                return "ThirdChapter10";
            case SceneName.MemoryFriendRoom4:
                return "ThirdChapter11";
            case SceneName.MemoryStore3:
                return "ThirdChapter12";
            case SceneName.MemoryFriendRoom5:
                return "ThirdChapter13";
            case SceneName.MemoryFriendRoom6:
                return "ThirdChapter14";
            case SceneName.MemorySchool4:
                return "ThirdChapter15";
            case SceneName.MemoryTeacherRoom1:
                return "ThirdChapter16";
            case SceneName.MemoryHallway3:
                return "ThirdChapter17";
            case SceneName.MemoryStore4:
                return "ThirdChapter18";
            case SceneName.MemoryDarkStreet2:
                return "ThirdChapter19";
            case SceneName.MemoryFriendRoom7:
                return "ThirdChapter20";
            case SceneName.MemoryTeacherRoom2:
                return "ThirdChapter21";
            case SceneName.MemoryStore5:
                return "ThirdChapter22";
            case SceneName.MemoryTeacherRoom3:
                return "ThirdChapter23";
            case SceneName.MemorySchool5:
                return "ThirdChapter24";
            case SceneName.MemoryFriendRoom8:
                return "ThirdChapter25";
            case SceneName.MemoryTeacherRoom4:
                return "ThirdChapter26";
            case SceneName.MemoryStore6:
                return "ThirdChapter27";
            case SceneName.MemoryHallway4:
                return "ThirdChapter28";
            case SceneName.MemoryTeacherRoom5:
                return "ThirdChapter29";
            case SceneName.MemoryFriendRoom9:
                return "ThirdChapter30";

            default:
                return "SecondChapter1";


        }
    }
}
