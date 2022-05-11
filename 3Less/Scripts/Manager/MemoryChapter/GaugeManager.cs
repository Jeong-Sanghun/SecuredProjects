using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    public int nowHealthGauge;
    public int nowMoneyGauge;
    GameManager gameManager;
    SaveDataClass saveData;
    [SerializeField]
    Image moneyEffectImage;
    [SerializeField]
    Image healthEffectImage;
    
    Image moneyImage;
    
    Image healthImage;

    Sprite[] effectSpriteArray;
    [SerializeField]
    Sprite[] moneySpriteArray;
    [SerializeField]
    Sprite[] healthSpriteArray;
    public bool isGameOver = false;
    

    

    // Start is called before the first frame update
    void Awake()
    {
        moneySpriteArray = Resources.LoadAll<Sprite>("Image/Money");
        healthSpriteArray = Resources.LoadAll<Sprite>("Image/Health");
        effectSpriteArray = Resources.LoadAll<Sprite>("Image/GaugeEffect");
        moneyImage = moneyEffectImage.transform.GetChild(0).GetComponent<Image>();
        healthImage = healthEffectImage.transform.GetChild(0).GetComponent<Image>();
        moneyEffectImage.sprite = effectSpriteArray[0];
        healthEffectImage.sprite = effectSpriteArray[0];
        gameManager = GameManager.singleton;
        saveData = gameManager.saveData;
    }

    public void SetGauge(int money, int health)
    {
        nowHealthGauge = health;
        nowMoneyGauge = money;
        moneyImage.sprite = moneySpriteArray[money];
        healthImage.sprite = healthSpriteArray[health];
    }

    public void ChangeMoneyGauge(int number)
    {
        SoundManager.singleton.EffectPlay(SFX.Gauge);
        StartCoroutine(GaugeImageChangeCoroutine(true, number));
    }

    public void ChangeHealthGauge(int number)
    {
        SoundManager.singleton.EffectPlay(SFX.Gauge);
        StartCoroutine(GaugeImageChangeCoroutine(false, number));
    }

    IEnumerator GaugeImageChangeCoroutine(bool isMoney,int number)
    {
        Image nowImage;
        Image nowEffectImage;
        Sprite[] nowSpriteArray;
        int lastGaugeNumber;
        int afterGaugeNumber;
        int changingNumber = number;
        if (isMoney)
        {
            nowImage = moneyImage;
            nowEffectImage = moneyEffectImage;
            lastGaugeNumber = nowMoneyGauge;
            afterGaugeNumber = nowMoneyGauge + number;
            nowSpriteArray = moneySpriteArray;
            if (afterGaugeNumber < 0)
            {
                changingNumber = -lastGaugeNumber;
            }
            else if (afterGaugeNumber >= nowSpriteArray.Length - 1)
            {
                changingNumber = nowSpriteArray.Length - 1 - lastGaugeNumber;
            }
            nowMoneyGauge += changingNumber;            
            saveData.moneyGauge = nowMoneyGauge;
        }
        else
        {
            nowImage = healthImage;
            nowEffectImage = healthEffectImage;
            lastGaugeNumber = nowHealthGauge;
            afterGaugeNumber = nowHealthGauge + number;
            nowSpriteArray = healthSpriteArray;
            if (afterGaugeNumber < 0)
            {
                changingNumber = -lastGaugeNumber;
            }
            else if (afterGaugeNumber >= nowSpriteArray.Length - 1)
            {
                changingNumber = nowSpriteArray.Length - 1 - lastGaugeNumber;
            }
            nowHealthGauge += changingNumber;
            saveData.healthGauge = nowHealthGauge;
        }
        if (afterGaugeNumber < 0)
        {
            isGameOver = true;
        }

        float timer = 0;
        int effectIndex = 0;
        for(int i = 0; i < effectSpriteArray.Length / 2; i++)
        {
            nowEffectImage.sprite = effectSpriteArray[effectIndex];
            effectIndex++;
            yield return new WaitForSeconds(0.07f);
        }


        if(changingNumber != 0)
        {
            int absNumber = Mathf.Abs(changingNumber);
            int one = absNumber / changingNumber;

            for (int i = 0; i < absNumber; i++)
            {

                yield return new WaitForSeconds(1.0f / (float)absNumber);
                
                lastGaugeNumber += one;
                nowImage.sprite = nowSpriteArray[lastGaugeNumber];
            }
        }
        for (int i = effectIndex; i < effectSpriteArray.Length; i++)
        {
            nowEffectImage.sprite = effectSpriteArray[effectIndex];
            effectIndex++;
            yield return new WaitForSeconds(0.07f);
        }

        moneyEffectImage.sprite = effectSpriteArray[0];
        healthEffectImage.sprite = effectSpriteArray[0];


        if (afterGaugeNumber < 0)
        {
            isGameOver = true;
            Debug.Log("게이지다까짐");
            GameManager.singleton.GameOver();
        }

    }

}
