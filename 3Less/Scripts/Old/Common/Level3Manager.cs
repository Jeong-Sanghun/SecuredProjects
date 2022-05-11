using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public GameObject goFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProcessEnd());
    }

    IEnumerator ProcessEnd()
    {
        yield return new WaitForSeconds(2f);

        goFadeOut.SetActive(true);

        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
