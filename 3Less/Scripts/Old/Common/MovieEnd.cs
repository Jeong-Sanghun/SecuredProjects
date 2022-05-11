using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieEnd : MonoBehaviour
{
    // Start is called before the first frame update

    public float delayTime;

    void Start()
    {
        Invoke("GotoNextScene", delayTime);
    }

    void GotoNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
