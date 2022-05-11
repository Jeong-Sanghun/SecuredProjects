using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEnumToSprite
{
    static Sprite[] spriteArray;
    public static Sprite Changer(Character character)
    {
        if(spriteArray == null)
        {
            spriteArray = new Sprite[Enum.GetValues(typeof(Character)).Length];
        }
        if(spriteArray[(int)character] == null)
        {
            spriteArray[(int)character] = Resources.Load<Sprite>("Image/Profile/" + character);
        }
        return spriteArray[(int)character];
    }
}
