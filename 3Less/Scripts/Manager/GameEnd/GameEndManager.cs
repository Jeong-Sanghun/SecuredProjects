using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndManager : MonoBehaviour
{
    GameManager gameManager;
    PhoneManager phoneManager;
    [SerializeField]
    ModuleManager moduleManager;
    [SerializeField]
    Image fadeObject;
    [SerializeField]
    GameObject backGround;
    [SerializeField]
    GameObject logoObject;
    [SerializeField]
    GameObject logoTarget;
    [SerializeField]
    Text categoryText;
    [SerializeField]
    Text nameText;
    [SerializeField]
    GameObject categoryTextTarget;
    [SerializeField]
    GameObject nameTextTarget;
    
    Vector3 categoryOriginPos;
    Vector3 nameOriginPos;

    bool isLoadingScene;
    float doubleClickTimer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.singleton;
        phoneManager = PhoneManager.singleTon;
        doubleClickTimer = 2;
        StartCoroutine(StartingCoroutine());
        phoneManager.PhoneMainCanvasActive(false);
        isLoadingScene = false;
        categoryOriginPos = categoryText.transform.position;
        nameOriginPos = nameText.transform.position;
    }


    void Update()
    {
        Vector3 pos = backGround.transform.position;
        pos.x -= Time.deltaTime/2;
        if (pos.x <= -33)
        {
            pos.x = 33;
        }
        backGround.transform.position = pos;

        doubleClickTimer += Time.deltaTime;
    }

    public void ScreenTouchEvent()
    {
        if (doubleClickTimer < 0.5f && isLoadingScene == false)
        {
            isLoadingScene = true;
            gameManager.LoadScene(SceneName.MainMenu);
        }
        else
        {
            doubleClickTimer = 0;
        }
    }

    IEnumerator StartingCoroutine()
    {
        Image logoImage = logoObject.GetComponent<Image>();
        logoImage.color = new Color(1, 1, 1, 0);

        StartCoroutine(moduleManager.FadeModule_Image(fadeObject, 1, 0, 1));
        
        yield return new WaitForSeconds(1f);

        StartCoroutine(moduleManager.FadeModule_Image(logoImage, 0, 1, 1));
        yield return new WaitForSeconds(1f);
        StartCoroutine(moduleManager.MoveModule_Linear(logoObject, logoTarget.transform.position, 2));
        yield return new WaitForSeconds(2);
        categoryText.text = "Director";
        nameText.text = "±Ë¡§¿Ã";
        TextMove();
        yield return new WaitForSeconds(3);
        TextFadeOut();
        yield return new WaitForSeconds(2);
        categoryText.text = "Scenario Writer";
        nameText.text = "√§»Òº± ∞≠øπ∫Û";
        TextMove();
        yield return new WaitForSeconds(3);
        TextFadeOut();
        yield return new WaitForSeconds(2);
        categoryText.text = "Designer";
        nameText.text = "¿Ã¿Ø¡¯ ±Ë«˝ªÛ";
        TextMove();
        yield return new WaitForSeconds(3);
        TextFadeOut();
        yield return new WaitForSeconds(2);
        categoryText.text = "Programmer";
        nameText.text = "¡§ªÛ»∆";
        TextMove();
        yield return new WaitForSeconds(3);
        TextFadeOut();
        yield return new WaitForSeconds(1);
        StartCoroutine(moduleManager.FadeModule_Image(fadeObject, 0, 1, 1));
        yield return new WaitForSeconds(1);

        if (isLoadingScene == false)
        {
            isLoadingScene = true;
            gameManager.LoadScene(SceneName.MainMenu);
        }
        
    }

    void TextMove()
    {

        StartCoroutine(moduleManager.FadeModule_Text(nameText, 0, 1, 1));
        StartCoroutine(moduleManager.FadeModule_Text(categoryText, 0, 1, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(categoryText.gameObject, categoryTextTarget.transform.position, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(nameText.gameObject, nameTextTarget.transform.position, 1));
    }

    void TextFadeOut()
    {
        StartCoroutine(moduleManager.FadeModule_Text(nameText, 1, 0, 1));
        StartCoroutine(moduleManager.FadeModule_Text(categoryText, 1, 0, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(categoryText.gameObject, categoryOriginPos, 1));
        StartCoroutine(moduleManager.MoveModule_Linear(nameText.gameObject,nameOriginPos, 1));

    }
}
