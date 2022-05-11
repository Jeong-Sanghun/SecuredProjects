using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWorldStep1 : MonoBehaviour
{
    public GameObject goPlayerText;
    public GameObject goDad;
    public GameObject goDadText;
    public GameObject goDadText2;
    public GameObject goDadText3;
    public GameObject goDadText4;

    // Start is called before the first frame update
    void Start()
    {
        GamePlayManager.Instance.isTitleOn = false;
        StartCoroutine(RealWorldStep1Process());
    }

    IEnumerator RealWorldStep1Process()
    {
        yield return new WaitForSeconds(2f);
        goPlayerText.SetActive(true);

        yield return new WaitForSeconds(4f);
        goDad.SetActive(true);

        yield return new WaitForSeconds(2f);
        goDadText.SetActive(true);

        yield return new WaitForSeconds(5f);
        goDadText2.SetActive(true);

        yield return new WaitForSeconds(5f);
        goDadText3.SetActive(true);

        yield return new WaitForSeconds(5f);
        goDadText4.SetActive(true);




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
