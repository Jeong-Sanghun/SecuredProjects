using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnumToColor
{
    public static Color Changer(Character character)
    {
        Color color;
        /*
         * ��ģ(��) #F4E4F4
����(��) #CEDDD3
�ƺ�(��) #C3BDC9
����(��) #E5CACA
������(��) #BCCFD8
��ģ(��) #CCBDB4
�����(��) #C1D5D6
���(ȸ) #9B9B9B*/
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
