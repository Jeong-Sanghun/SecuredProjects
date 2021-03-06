using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymptomBookManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject bookCanvas;
    [SerializeField] Transform pageBundleParent;
    [SerializeField] GameObject pageBundlePrefab;
    [SerializeField] GameObject onePagePrefab;
    [SerializeField] Text prefabTitleText;
    [SerializeField] Text prefabExplainText;
    [SerializeField] Image prefabImage;
    [SerializeField] GameObject bookMarkPrefab;
    [SerializeField] Sprite[] bookMarkSpriteArray;


    List<SymptomBookBundle> bookBundleList;
    SymptomBookBundle nowBundle;
    GameObject nowPageBundle;
    bool isNewPage;
    int nowPageIndex;
    int nowLeftBookMarkIndex;
    int nowRightBookMarkIndex;
    List<int> bookMarkIndexList;
    List<RectTransform> bookMarkRectList;
    List<GameObject> pageBundleList;
    // Start is called before the first frame update
    void Start()
    {
        int bookMarkNumber = 0;
        int nowPages = 0;
        bool bookMark = false;
        gameManager = GameManager.singleton;
        //연구할 때 이걸 받아와야함.
        isNewPage = true;
        nowPageIndex = 0;
        nowLeftBookMarkIndex = 0;
        nowRightBookMarkIndex = 1;
        pageBundleList = new List<GameObject>();
        bookBundleList = new List<SymptomBookBundle>();
        bookMarkRectList = new List<RectTransform>();
        bookMarkIndexList = new List<int>();
        for (int i = 0; i < gameManager.saveData.symptomBookList.Count; i++)
        {
            SymptomBookBundle bundle = gameManager.LoadSymptomBookBundle(gameManager.saveData.symptomBookList[i]);
            bookBundleList.Add(bundle);
        }
        for(int i  =0; i< gameManager.saveData.owningOtherToolList.Count; i++)
        {
            if (gameManager.saveData.owningOtherToolList[i].Contains("bookMark"))
            {
                bookMark = true;
                break;
            }
        }

        Symptom firstSymptom = Symptom.none;
        for(int j = 0; j < bookBundleList.Count; j++)
        {
            
            if (bookMark)
            {
                bookMarkIndexList.Add(pageBundleList.Count);
                GameObject inst = Instantiate(bookMarkPrefab, pageBundleParent);
                int dele = bookMarkNumber;
                inst.GetComponent<Button>().onClick.AddListener(() => BookMarkButton(dele));
                if (firstSymptom == bookBundleList[j].symptom)
                {
                    inst.GetComponentInChildren<Text>().text = "+";
                }
                else
                {
                    inst.GetComponentInChildren<Text>().text = "-";
                }

                inst.GetComponent<Image>().sprite = bookMarkSpriteArray[(int)bookBundleList[j].symptom];
                inst.SetActive(true);
                RectTransform rect = inst.GetComponent<RectTransform>();
                if (j == 0)
                {
                    rect.anchoredPosition = new Vector3(-980,500, 0);
                }
                else
                {
                    rect.localRotation = Quaternion.Euler(0, 180, 0);
                    rect.anchoredPosition = new Vector3(980, 500 - j*100, 0);
                }

                bookMarkRectList.Add(rect);
                bookMarkNumber++;
                firstSymptom = bookBundleList[j].symptom;
            }
            nowPages += bookBundleList[j].oneSymptomBookList.Count + bookBundleList[j].twoSymptomBookList.Count;
            nowBundle = bookBundleList[j];
            for (int i = 0; i < bookBundleList[j].oneSymptomBookList.Count; i++)
            {
                MakePages(bookBundleList[j].oneSymptomBookList[i]);
            }
            isNewPage = true;
            for (int i = 0; i < bookBundleList[j].twoSymptomBookList.Count; i++)
            {
                MakePages(bookBundleList[j].twoSymptomBookList[i]);
            }
            isNewPage = true;
        }

    }

    void MakePages(SymptomBook book)
    {

        GameObject pageBundleInst;
        if(isNewPage == true)
        {
            pageBundleInst = Instantiate(pageBundlePrefab, pageBundleParent);
            if(pageBundleList.Count == 0)
            {
                pageBundleInst.SetActive(true);
            }
            else
            {
                pageBundleInst.SetActive(false);
            }
            
            pageBundleList.Add(pageBundleInst);
            
            nowPageBundle = pageBundleInst;
           
        }
        else
        {
            pageBundleInst = nowPageBundle;
        }
        Transform leftPage = pageBundleInst.transform.GetChild(0);
        Transform rightPage = pageBundleInst.transform.GetChild(1);
        prefabTitleText.text = book.title;
        prefabExplainText.text = book.explain;
        prefabImage.sprite = book.LoadImage(nowBundle.symptomString);

        GameObject pageInst;
        if (isNewPage)
        {
            pageInst = Instantiate(onePagePrefab, leftPage);
            isNewPage = false;
        }
        else
        {
            pageInst = Instantiate(onePagePrefab, rightPage);
            isNewPage = true;
        }
        pageInst.SetActive(true);
        pageInst.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void ChangePage(bool left)
    {
        Debug.Log("페이지체인지 되냐");
        if (left)
        {
            if(nowPageIndex <= 0)
            {
                return;
            }
            pageBundleList[nowPageIndex].SetActive(false);
            pageBundleList[nowPageIndex - 1].SetActive(true);

            if (nowPageIndex <= bookMarkIndexList[nowLeftBookMarkIndex])
            {
                bookMarkRectList[nowLeftBookMarkIndex].localRotation = Quaternion.Euler(0, 180, 0);
                bookMarkRectList[nowLeftBookMarkIndex].anchoredPosition = new Vector3(980, bookMarkRectList[nowLeftBookMarkIndex].anchoredPosition.y, 0);
                nowLeftBookMarkIndex--;
                nowRightBookMarkIndex--;

            }
            nowPageIndex--;

        }
        else
        {
            if(nowPageIndex >= pageBundleList.Count-1)
            {
                return;
            }
            pageBundleList[nowPageIndex].SetActive(false);
            pageBundleList[nowPageIndex + 1].SetActive(true);


            nowPageIndex++;
            if (nowRightBookMarkIndex >= bookMarkIndexList.Count)
            {
                return;
            }
            if (nowPageIndex >= bookMarkIndexList[nowRightBookMarkIndex])
            {
                bookMarkRectList[nowRightBookMarkIndex].localRotation = Quaternion.Euler(0, 0, 0);
                bookMarkRectList[nowRightBookMarkIndex].anchoredPosition = new Vector3(-980, bookMarkRectList[nowRightBookMarkIndex].anchoredPosition.y, 0);
                
                nowRightBookMarkIndex++;
                nowLeftBookMarkIndex++;
            }

        }
    }

    public void BookMarkButton(int index)
    {
        pageBundleList[nowPageIndex].SetActive(false);
        nowPageIndex = bookMarkIndexList[index];
        pageBundleList[nowPageIndex].SetActive(true);
        nowRightBookMarkIndex = index + 1;
        nowLeftBookMarkIndex = index;

        for(int i = 0; i <= nowLeftBookMarkIndex; i++)
        {
            bookMarkRectList[i].anchoredPosition = new Vector3(-980, bookMarkRectList[i].anchoredPosition.y, 0);
        }
        for(int i = nowRightBookMarkIndex; i< bookMarkRectList.Count; i++)
        {
            bookMarkRectList[i].anchoredPosition = new Vector3(980, bookMarkRectList[i].anchoredPosition.y, 0);
        }

    }

    public void BookCanvasActive(bool active)
    {
        bookCanvas.SetActive(active);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
