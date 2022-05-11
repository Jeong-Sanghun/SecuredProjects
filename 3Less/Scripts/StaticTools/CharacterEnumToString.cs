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
                return "����";
            case Character.Father:
                return "�ƺ�";
            case Character.Fish:
                return "�ݺؾ�";
            case Character.RoomFriend:
                return "����� ģ��";
            case Character.Friend1:
            case Character.Friend2:
            case Character.FriendGirl:
                return "ģ��";
            case Character.Message:
                return "�޽���";
            case Character.Mother:
                return "����";
            case Character.Narator:
                return "�����̼�";
            case Character.Player:
                return "���ΰ�";
            case Character.System:
                return "�ý���";
            case Character.Police:
                return "����";
            case Character.CouncilTeacher:
                return "��㼱����";
            case Character.HomeRoomTeacher:
                return "���Ӽ�����";
            case Character.RooftopFriend:
                return "����ģ��";
            case Character.GrandMother:
                return "�ҸӴ�";
            case Character.FriendFather:
                return "ģ�� �ƹ���";
            case Character.JustBoss:
            case Character.StoreBoss:
                return "������ �����";
            default:
                return "";


        }
    }
}
