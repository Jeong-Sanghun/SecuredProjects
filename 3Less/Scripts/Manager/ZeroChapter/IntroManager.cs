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
        "피곤한 목요일 밤, 금요일 정시퇴근을 사수하기 위해 늦게까지 일하고 있다.",    //0
        "집에 가고 싶다.",//1
        "로또만 당첨되면 회사 때려치고 만다. ...아니, 회사는 다니고 야근은 하지 말자.",   //2
        "근데 내일 갑자기 일 몰아주면서 야근하라고 하면 어떡하지?", //3
        "",//4
        "뻐근한 상체를 일으켜 이리저리 기지개를 켠다. 창밖을 보니 보름달이 밝아 창에서 엷은 빛이 내려온다.", //5
        "추석도 아닌데 달 엄청 크네.", //6
        "",//7
        "..어? 위에 뭐 있나?", //8
        "무엇인가 떨어지는...",//9
        "",//10
        "눈? 그럼 방금...",  //11
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
