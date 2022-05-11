using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnumToString
{
    //Player,
    //Narator,
    //System,
    //Message,
    //Fish,
    //Mushroom,
    //Mother,
    //Father,
    //FriendFather,
    //Brother,
    //FriendGirl,
    //RoomFriend,
    //GrandMother,
    //Friend1,
    //Friend2,
    //StoreBoss,
    //RooftopFriend,
    //TreeMonster,
    //Police,
    //CouncilTeacher,
    //HomeRoomTeacher,
    //DrunkenPerson1,
    //DrunkenPerson2,
    //YoungMan,
    public static string Changer(Character character)
    {
        switch (character)
        {
            case Character.Brother:
                return "동생";
            case Character.Father:
                return "아빠";
            case Character.Fish:
                return "금붕어";
            case Character.RoomFriend:
                return "자취방 친구";
            case Character.Friend1:
            case Character.Friend2:
            case Character.FriendGirl:
                return "친구";
            case Character.Message:
                return "메시지";
            case Character.Mother:
                return "엄마";
            case Character.Narator:
                return "내레이션";
            case Character.Player:
                return "주인공";
            case Character.System:
                return "시스템";
            case Character.Police:
                return "경찰";
            case Character.CouncilTeacher:
                return "상담선생님";
            case Character.HomeRoomTeacher:
                return "담임선생님";
            case Character.RooftopFriend:
                return "옥상친구";
            case Character.GrandMother:
                return "할머니";
            case Character.FriendFather:
                return "친구 아버지";
            case Character.JustBoss:
            case Character.StoreBoss:
                return "편의점 사장님";
            default:
                return "";


        }
    }
}
