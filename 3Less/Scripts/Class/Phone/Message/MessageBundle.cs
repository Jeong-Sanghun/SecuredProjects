using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageBundle
{
    public List<MessageWrapper> messageWrapperList;

    public MessageBundle()
    {
        messageWrapperList = new List<MessageWrapper>();
    }
}
