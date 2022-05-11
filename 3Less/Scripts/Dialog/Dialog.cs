using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Character
{
    Player,
    Narator,
    System,
    Message,
    Fish,
    Mushroom,
    Mother,
    Father,
    FriendFather,
    Brother,
    FriendGirl,
    RoomFriend,
    GrandMother,
    Friend1,
    Friend2,
    StoreBoss,
    JustBoss,
    RooftopFriend,
    TreeMonster,
    Police,
    CouncilTeacher,
    HomeRoomTeacher,
    DrunkenPerson1,
    DrunkenPerson2,
    YoungMan,
    NotAllocated
}


[System.Serializable]
public class Dialog
{

    public string dialog;
    public string actionKeyword;
    public string character;
    public string routeFirst;
    public string routeSecond;
    public string routeThird;
    public string routeFourth;
    public string routeFifth;

    [System.NonSerialized]
    public List<string> routeList;


    [System.NonSerialized]
    public List<ActionClass> actionList;

    [System.NonSerialized]
    public Character characterEnum;

    public Dialog()
    {
        dialog = null;
        character = null;
        routeFirst = null;
        routeSecond = null;
        routeThird = null;
        routeFourth = null;
        routeFifth = null;
        routeList = null;
        characterEnum = Character.NotAllocated;
    }

    public void SetCharacterEnum()
    {
        
        if(character == null)
        {
            characterEnum = Character.NotAllocated;
        }
        else if (character.Contains("주인공"))
        {
            characterEnum = Character.Player;
        }
        else if (character.Contains("SYS"))
        {
            characterEnum = Character.System;
        }
        else if (character.Contains("NAR"))
        {
            characterEnum = Character.Narator;
        }
        else if (character.Contains("친구네 아버지"))
        {
            characterEnum = Character.FriendFather;
        }
        else if (character.Contains("금붕어"))
        {
            characterEnum = Character.Fish;
        }
        else if (character.Contains("버섯"))
        {
            characterEnum = Character.Mushroom;
        }
        else if (character.Contains("나무괴물"))
        {
            characterEnum = Character.TreeMonster;
        }
        else if (character.Contains("어머니"))
        {
            characterEnum = Character.Mother;
        }
        else if (character.Contains("할머니"))
        {
            characterEnum = Character.GrandMother;
        }
        else if (character.Contains("아버지"))
        {
            characterEnum = Character.Father;
        }
        else if (character.Contains("동생"))
        {
            characterEnum = Character.Brother;
        }
        else if (character.Contains("친구 1"))
        {
            characterEnum = Character.Friend1;
        }
        else if (character.Contains("친구 2"))
        {
            characterEnum = Character.Friend2;
        }
        else if (character.Contains("친구"))
        {
            characterEnum = Character.RoomFriend;
        }
        else if (character.Contains("B"))
        {
            characterEnum = Character.RooftopFriend;
        }
        else if (character.Contains("학생"))
        {
            characterEnum = Character.FriendGirl;
        }
        else if (character.Contains("편의점 사장"))
        {
            characterEnum = Character.StoreBoss;
        }
        else if (character.Contains("사장"))
        {
            characterEnum = Character.JustBoss;
        }
        else if (character.Contains("경찰"))
        {
            characterEnum = Character.Police;
        }
        else if (character.Contains("상담"))
        {
            characterEnum = Character.CouncilTeacher;
        }
        else if (character.Contains("담임"))
        {
            characterEnum = Character.HomeRoomTeacher;
        }
        else if (character.Contains("MES"))
        {
            characterEnum = Character.Message;
        }
        else if (character.Contains("취객 1"))
        {
            characterEnum = Character.DrunkenPerson1;
        }
        else if (character.Contains("취객 2"))
        {
            characterEnum = Character.DrunkenPerson2;
        }
        else if (character.Contains("젊은 남자"))
        {
            characterEnum = Character.YoungMan;
        }


        if (routeFirst != null)
        {
            routeList = new List<string>();
            routeList.Add(routeFirst);
        }
        if (routeSecond != null)
        {
            routeList.Add(routeSecond);

        }
        if (routeThird != null)
        {
            routeList.Add(routeThird);

        }
        if (routeFourth != null)
        {
            routeList.Add(routeFourth);

        }
        if (routeFifth != null)
        {
            routeList.Add(routeFifth);

        }


        if (actionKeyword == null)
        {
            actionList = null;
            return;
        }
        else
        {
            actionList = new List<ActionClass>();
        }

        string[] keywordArray = actionKeyword.Split(',');

        for(int i = 0; i < keywordArray.Length; i++)
        {
            ActionClass act = new ActionClass();
            actionList.Add(act);
            if (keywordArray[i].Contains("fishMove") || keywordArray[i].Contains("FishMove"))
            {
                act.actionList.Add(ActionKeyword.FishMove);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("playerMove") || keywordArray[i].Contains("PlayerMove"))
            {
                act.actionList.Add(ActionKeyword.PlayerMove);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("otherMove") || keywordArray[i].Contains("OtherMove"))
            {
                act.actionList.Add(ActionKeyword.OtherMove);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("phoneOn") || keywordArray[i].Contains("PhoneOn"))
            {
                act.actionList.Add(ActionKeyword.PhoneOn);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("phoneOff") || keywordArray[i].Contains("PhoneOff"))
            {
                act.actionList.Add(ActionKeyword.PhoneOff);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("soundMessageAlarm") || keywordArray[i].Contains("SoundMessageAlarm"))
            {
                act.actionList.Add(ActionKeyword.SoundMessageAlarm);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("doorSound") || keywordArray[i].Contains("DoorSound"))
            {
                act.actionList.Add(ActionKeyword.DoorSound);
                act.parameterList.Add(-1);
            }



            if (keywordArray[i].Contains("stop") || keywordArray[i].Contains("Stop"))
            {
                if (keywordArray[i].Contains("seconds") || keywordArray[i].Contains("Seconds"))
                {
                    act.actionList.Add(ActionKeyword.StopSeconds);
                    string bufferKeyword = keywordArray[i];
                    int secondsIndex;
                    bufferKeyword = bufferKeyword.Remove(0, 4);
                    if (bufferKeyword.Contains("Seconds"))
                    {
                        secondsIndex =bufferKeyword.IndexOf("Seconds");
                    }
                    else
                    {
                        secondsIndex =bufferKeyword.IndexOf("seconds");
                    }
                    bufferKeyword = bufferKeyword.Remove(secondsIndex, 7);
                    
                    act.parameterList.Add(float.Parse(bufferKeyword));

                }
            }

            if (keywordArray[i].Contains("conditionalJump") || keywordArray[i].Contains("ConditionalJump"))
            {
                act.actionList.Add(ActionKeyword.ConditionalJump);
                string bufferKeyword = keywordArray[i];
                bufferKeyword = bufferKeyword.Remove(0, "conditionalJump".Length);
                act.parameterList.Add(float.Parse(bufferKeyword));
            }

            //if (keywordArray[i].Contains("conditionalSceneEnd") || keywordArray[i].Contains("ConditionalSceneEnd"))
            //{
            //    act.actionList.Add(ActionKeyword.ConditionalSceneEnd);
            //    string bufferKeyword = keywordArray[i];
            //    bufferKeyword = bufferKeyword.Remove(0, "conditionalSceneEnd".Length);
            //    act.parameterList.Add(float.Parse(bufferKeyword));
            //    continue;
            //}


            if (keywordArray[i].Contains("healthGauge") || keywordArray[i].Contains("HealthGauge"))
            {
                act.actionList.Add(ActionKeyword.HealthGauge);
                string bufferKeyword = keywordArray[i];
                bufferKeyword = bufferKeyword.Remove(0, "healthGauge".Length);
                act.parameterList.Add(float.Parse(bufferKeyword));
            }

            if (keywordArray[i].Contains("moneyGauge") || keywordArray[i].Contains("moneyGauge"))
            {
                act.actionList.Add(ActionKeyword.MoneyGauge);
                string bufferKeyword = keywordArray[i];
                bufferKeyword = bufferKeyword.Remove(0, "moneyGauge".Length);
                act.parameterList.Add(float.Parse(bufferKeyword));
            }
            if (keywordArray[i].Contains("multiRoute") || keywordArray[i].Contains("MultiRoute"))
            {
                act.actionList.Add(ActionKeyword.MultiRoute);
                string bufferKeyword = keywordArray[i];
                bufferKeyword = bufferKeyword.Remove(0, "multiRoute".Length);
                act.parameterList.Add(float.Parse(bufferKeyword));
            }


            if (keywordArray[i].Contains("scene") || keywordArray[i].Contains("Scene"))
            {
                act.actionList.Add(ActionKeyword.Scene);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("zoomOut") || keywordArray[i].Contains("ZoomOut"))
            {
                act.actionList.Add(ActionKeyword.ZoomOut);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("fadeOut") || keywordArray[i].Contains("FadeOut"))
            {
                act.actionList.Add(ActionKeyword.FadeOut);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("fadeIn") || keywordArray[i].Contains("FadeIn"))
            {
                act.actionList.Add(ActionKeyword.FadeIn);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("scissor") || keywordArray[i].Contains("Scissor"))
            {
                act.actionList.Add(ActionKeyword.Scissors);
                act.parameterList.Add(-1);
            }


            if (keywordArray[i].Contains("bubble") || keywordArray[i].Contains("Bubble"))
            {
                act.actionList.Add(ActionKeyword.Bubble);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("tree") || keywordArray[i].Contains("Tree"))
            {
                act.actionList.Add(ActionKeyword.Tree);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("brokenSound") || keywordArray[i].Contains("BrokenSound"))
            {
                act.actionList.Add(ActionKeyword.BrokenSound);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("banchan") || keywordArray[i].Contains("Banchan"))
            {
                act.actionList.Add(ActionKeyword.Banchan);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("panza") || keywordArray[i].Contains("Panza"))
            {
                act.actionList.Add(ActionKeyword.Panza);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("drop") || keywordArray[i].Contains("Drop"))
            {
                act.actionList.Add(ActionKeyword.Drop);
                act.parameterList.Add(-1);
            }


            if (keywordArray[i].Contains("medal") || keywordArray[i].Contains("Medal"))
            {
                act.actionList.Add(ActionKeyword.Medal);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("touch") || keywordArray[i].Contains("Touch"))
            {
                act.actionList.Add(ActionKeyword.Touch);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("drag") || keywordArray[i].Contains("Drag"))
            {
                act.actionList.Add(ActionKeyword.Drag);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("immediateDialog") || keywordArray[i].Contains("ImmediateDialog"))
            {
                act.actionList.Add(ActionKeyword.ImmediateDialog);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("imgFlashback") || keywordArray[i].Contains("ImgFlashback"))
            {
                act.actionList.Add(ActionKeyword.ImgFlashback);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("imgFalse") || keywordArray[i].Contains("ImgFalse"))
            {
                act.actionList.Add(ActionKeyword.ImgFalse);
                act.parameterList.Add(-1);
            }

            if(keywordArray[i].Contains("route") || keywordArray[i].Contains("Route"))
            {
                act.actionList.Add(ActionKeyword.Route);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("end") || keywordArray[i].Contains("End"))
            {
                act.actionList.Add(ActionKeyword.End);
                act.parameterList.Add(-1);
            }


            if (keywordArray[i].Contains("first") || keywordArray[i].Contains("First"))
            {
                act.actionList.Add(ActionKeyword.First);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("second") || keywordArray[i].Contains("Second") && !(actionKeyword.Contains("seconds") || actionKeyword.Contains("Seconds")))
            {
                act.actionList.Add(ActionKeyword.Second);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("third") || keywordArray[i].Contains("Third"))
            {
                act.actionList.Add(ActionKeyword.Third);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("fourth") || keywordArray[i].Contains("Fourth"))
            {
                act.actionList.Add(ActionKeyword.Fourth);
                act.parameterList.Add(-1);
            }

            if (keywordArray[i].Contains("fifth") || keywordArray[i].Contains("Fifth"))
            {
                act.actionList.Add(ActionKeyword.Fifth);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("sixth") || keywordArray[i].Contains("Sixth"))
            {
                act.actionList.Add(ActionKeyword.Sixth);
                act.parameterList.Add(-1);
            }
            if (keywordArray[i].Contains("seventh") || keywordArray[i].Contains("Seventh"))
            {
                act.actionList.Add(ActionKeyword.Seventh);
                act.parameterList.Add(-1);
            }
        }



    }

}
