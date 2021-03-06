using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTool : MeasureTool
{
    [SerializeField]
    GameObject crystalBallObject;
    [SerializeField]
    GameObject dustPrefab;

    GameObject touchedObject;               //터치한 오브젝트
    RaycastHit2D hit;                         //터치를 위한 raycastHit
    public Camera cam;                      //레이캐스트를 위한 카메라.

    GameObject[] dustObjectArray;

    [SerializeField]
    Text symptomText;

    
    int dustQuantity = 100;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dustObjectArray = new GameObject[dustQuantity];
        dustPrefab.SetActive(false);
        for (int i = 0; i < dustQuantity; i++)
        {
            GameObject dust = Instantiate(dustPrefab, crystalBallObject.transform);
            dust.SetActive(true);
            dustObjectArray[i] = dust;
        }
        ExplainLoad();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && measureEnd ==false && measureStarted == true)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;
                if (touchedObject.CompareTag("Dust"))
                {

                    touchedObject.SetActive(false);

                }
            }
            bool dustAllCollected = true;
            for (int i = 0; i < dustObjectArray.Length; i++)
            {
                if (dustObjectArray[i].activeSelf)
                {
                    dustAllCollected = false;
                    break;
                }
            }
            if (dustAllCollected == true)
            {
                MeasureEnd();
            }
        }
    }

    public override void ToolActive(bool active)
    {
        base.ToolActive(active);
        measureStarted = true;
        if (isAuto == true && measureEnd == false)
        {
            for (int i = 0; i < dustQuantity; i++)
            {
                dustObjectArray[i].SetActive(false);
            }
            MeasureEnd();
        }
        StartCoroutine(SceneManager.inst.MoveModule_Linear(toolObject, new Vector3(5,0,0), 0.5f));
    }

    

    public override void OnNewVisitor(int symptomNum,int index,bool auto)
    {
        base.OnNewVisitor(symptomNum,index,auto);

        if (!isAuto)
        {
            float x, y;
            for (int i = 0; i < dustQuantity; i++)
            {
                dustObjectArray[i].SetActive(true);
                x = Random.Range(-3.5f, 3.5f);
                y = Random.Range(1.995f - Mathf.Sqrt(12.25f - x * x), 1.995f + Mathf.Sqrt(12.25f - x * x));
                dustObjectArray[i].transform.localPosition = new Vector3(x, y, 0);
            }
        }
        else
        {
            for (int i = 0; i < dustQuantity; i++)
            {
                dustObjectArray[i].SetActive(false);
            }
        }
        symptomText.gameObject.SetActive(false);

    }

    protected override void ExplainLoad()
    {
        base.ExplainLoad();
        explainData = gameManager.jsonManager.ResourceDataLoad<MeasureToolExplain>("MeasureToolExplain/Fire");
        ExplainSet();
    }

    protected override void MeasureEnd()
    {
        base.MeasureEnd();
        symptomText.gameObject.SetActive(true);
        symptomText.text = symptomNumber.ToString();
    }

}
