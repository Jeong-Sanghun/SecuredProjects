using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnumToColor
{
    public static Color Changer(Character character)
    {
        Color color;
        /*
         * 옥친(핑) #F4E4F4
엄마(초) #CEDDD3
아빠(보) #C3BDC9
동생(빨) #E5CACA
선생님(파) #BCCFD8
자친(갈) #CCBDB4
예비용(하) #C1D5D6
모브(회) #9B9B9B*/
        switch (character)
        {
            case Character.Brother:
                ColorUtility.TryParseHtmlString("#E5CACA", out color);
                break;
            case Character.Father:
                ColorUtility.TryParseHtmlString("#C3BDC9", out color);
                break;
            case Character.RoomFriend:
                ColorUtility.TryParseHtmlString("#CCBDB4", out color);
                break;
            case Character.FriendGirl:
                ColorUtility.TryParseHtmlString("#F4E4F4", out color);
                break;
            case Character.Mother:
                ColorUtility.TryParseHtmlString("#CEDDD3", out color);
                break;
            default:
                color = Color.white;
                break;


        }
        return color;
    }
}
