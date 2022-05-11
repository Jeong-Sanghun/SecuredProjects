using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapImage : MonoBehaviour
{
    public RawImage go2;

    public void Start()
    {
        go2.enabled = false;
    }

    public void OnMouseOver()
    {
        go2.enabled = true;
    }

    public void OnMouseExit()
    {
        go2.enabled = false;
    }
}
