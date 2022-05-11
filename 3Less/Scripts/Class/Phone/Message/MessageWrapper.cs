using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageWrapper
{
    public Character character;
    public List<OneMessage> messageList;

    [System.NonSerialized]
    public GameObject messageCanvas;
    [System.NonSerialized]
    public GameObject canvasOpenButton;

    public MessageWrapper()
    {
        character = Character.Brother;
        messageList = new List<OneMessage>();
    }

    public void SetObject(GameObject canvas, GameObject button)
    {
        messageCanvas = canvas;
        canvasOpenButton = button;
    }

    public void CanvasOpen()
    {
        messageCanvas.SetActive(true);
    }
    public void CanvasClose()
    {
        messageCanvas.SetActive(false);
    }

}
