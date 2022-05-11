using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapObject : MonoBehaviour
{
    public GameObject goDefault;
    public GameObject goShowObject;


    public void ShowObject()
    {
        goShowObject.SetActive(true);
        goDefault.SetActive(false);
    }

}
