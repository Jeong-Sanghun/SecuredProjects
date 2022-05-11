using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    ModuleManager moduleManager;
    GameManager gameManager;

    [SerializeField]
    GameObject cutSceneParent;
    [SerializeField]
    Image[] cutSceneArray;
    [SerializeField]
    Image fadeInImage;
    [SerializeField]
    Text dialogText;
    [SerializeField]
    Image textFrameImage;

    [SerializeField]
    Image startLogo;
    [SerializeField]
    Image explainingImage;
    [SerializeField]
    Image threeImage;
    [SerializeField]
    GameObject startLogoTarget;
    
    

    string[] dialogs =
    {
        "�ǰ��� ����� ��, �ݿ��� ��������� ����ϱ� ���� �ʰԱ��� ���ϰ� �ִ�.",    //0
        "���� ���� �ʹ�.",//1
        "�ζǸ� ��÷�Ǹ� ȸ�� ����ġ�� ����. ...�ƴ�, ȸ��� �ٴϰ� �߱��� ���� ����.",   //2
        "�ٵ� ���� ���ڱ� �� �����ָ鼭 �߱��϶�� �ϸ� �����?", //3
        "",//4
        "������ ��ü�� ������ �̸����� �������� �Ҵ�. â���� ���� �������� ��� â���� ���� ���� �����´�.", //5
        "�߼��� �ƴѵ� �� ��û ũ��.", //6
        "",//7
        "..��? ���� �� �ֳ�?", //8
        "�����ΰ� ��������...",//9
        "",//10
        "��? �׷� ���...",  //11
    };

    int dialogIndex;
    bool isChanging = false;
    int nowCutSceneIndex;    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.singleton;
        dialogIndex = 0;
        nowCutSceneIndex = 0;
        isChanging = true;
        StartCoroutine(StartingCoroutine());
    }

    // Update is called once per frame

    public void ScreenTouchEvent()
    {
        if (moduleManager.nowTexting == false && isChanging == false)
        {
            NextDialog();
        }
    }

    IEnumerator StartingCoroutine()
    {
        startLogo.color = new Color(1, 1, 1, 0);
        threeImage.color = new Color(1, 1, 1, 0);
        explainingImage.color = new Color(1, 1, 1, 0);

        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
        StartCoroutine(moduleManager.FadeModule_Image(threeImage, 0, 1, 1));
        StartCoroutine(moduleManager.FadeModule_Image(startLogo, 0, 1, 1));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(moduleManager.MoveModule_Linear(startLogo.gameObject, startLogoTarget.transform.position, 1));
        yield return new WaitForSeconds(1f);
        StartCoroutine(moduleManager.FadeModule_Image(threeImage, 1, 0, 1));
        yield return new WaitForSeconds(1f);
        StartCoroutine(moduleManager.FadeModule_Image(explainingImage, 0, 1, 1));
        yield return new WaitForSeconds(2f);
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 0, 1, 1));
        yield return new WaitForSeconds(1.5f);
        startLogo.gameObject.SetActive(false);
        explainingImage.gameObject.SetActive(false);
        threeImage.gameObject.SetActive(false);

        cutSceneParent.SetActive(true);
        StartCoroutine(moduleManager.FadeModule_Image(fadeInImage, 1, 0, 1));
        StartCoroutine(moduleManager.FadeModule_Image(textFrameImage, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        fadeInImage.gameObject.SetActive(false);
        isChanging = false;
        NextDialog();
    }

    void NextDialog()
    {

        if (dialogIndex == 12)
        {
            return;
        }
        if(dialogIndex == 10)
        {
            SoundManager.singleton.EffectPlay(SFX.Koong);
        }
        if(dialogIndex == 11)
        {
            SoundManager.singleton.EffectPlay(SFX.Siren);
        }
        switch (dialogIndex)
        {

            case 4:
            case 7:
            case 10:
                StartCoroutine(moduleManager.FadeModule_Image(cutSceneArray[nowCutSceneIndex], 1, 0, 1));
                StartCoroutine(moduleManager.FadeModule_Image(cutSceneArray[++nowCutSceneIndex], 0, 1, 1));
                StartCoroutine(moduleManager.FadeModule_Image(textFrameImage, 1, 0, 1));
                StartCoroutine(moduleManager.FadeModule_Text(dialogText, 1, 0, 1));
                isChanging = true;
                Invoke("SetChangeFalse", 1);

                dialogIndex++;
                return;
            case 5:
            case 8:
            case 11:
                StartCoroutine(moduleManager.FadeModule_Image(textFrameImage, 0, 1, 1));
                dialogText.text = "";
                StartCoroutine(moduleManager.FadeModule_Text(dialogText, 0, 1, 1));
                isChanging = true;
                
                Invoke("SetChangeFalse", 0.5f);
                break;
            default:
                break;

        }

        StartCoroutine(moduleManager.LoadTextOneByOne(dialogs[dialogIndex], dialogText));
        dialogIndex++;
        if (dialogIndex == 12)
        {
            Invoke("SceneEnd", 4);
        }

    }

    void SetChangeFalse()
    {
        isChanging = false;
    }

    void SceneEnd()
    {
        gameManager.LoadScene(SceneName.Bright);
    }
}
