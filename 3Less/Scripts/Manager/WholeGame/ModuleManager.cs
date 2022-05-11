using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Rendering.PostProcessing;
public class ModuleManager : MonoBehaviour
{
    // // Module // //
    public bool nowTexting;
    GameObject linearMovingObj;
    

    

    public IEnumerator LoadTextOneByOne(string inputTextString, Text inputTextUI, float eachTime = 0.05f, bool canClickSkip = true)
    {
        nowTexting = true;
        float miniTimer = 0f; //Ÿ�̸�
        float currentTargetNumber = 0f; // �ش� Time�� ����� ��ǥ�� �ϴ� �ּ� ���� ��
        int currentNumber = 0; // �ش� Time�� ������� ���� ��
        string displayedText = "";
        StringBuilder builder = new StringBuilder(displayedText);
        while (currentTargetNumber < inputTextString.Length)
        {
            while (currentNumber < currentTargetNumber)
            { // ��ǥ ���ڼ����� ���
                //displayedText += inputTextString.Substring(currentNumber,1);
                builder.Append(inputTextString.Substring(currentNumber, 1));
                currentNumber++;
            }
            //inputTextUI.text = displayedText;
            inputTextUI.text = builder.ToString();
            yield return null;
            miniTimer += Time.deltaTime;
            currentTargetNumber = miniTimer / eachTime;
            if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && canClickSkip)
            {
                break;
            }
        }
        while (currentNumber < inputTextString.Length)
        { // ��ǥ ���ڼ����� ���
            builder.Append(inputTextString.Substring(currentNumber, 1));
            currentNumber++;
        }
        inputTextUI.text = builder.ToString();
        yield return null;
        nowTexting = false;
    }
    public IEnumerator FadeModule_Sprite(GameObject i_Object, float i_Alpha_Initial, float i_Alpha_Final, float i_Time)
    {
        i_Object.SetActive(true);
        SpriteRenderer i_image = i_Object.GetComponent<SpriteRenderer>();

        float miniTimer = 0f;
        float newAlpha = i_Alpha_Initial;
        while (miniTimer < i_Time)
        {
            newAlpha = Mathf.Lerp(i_Alpha_Initial, i_Alpha_Final, miniTimer / i_Time);
            i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, newAlpha);
            yield return null;
            miniTimer += Time.deltaTime;
        }
        i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, i_Alpha_Final);
        yield return null;
    }

    public IEnumerator VolumeModule(PostProcessVolume volume, bool finalOne, float time)
    {
        float timer = 0;
        int one;
        if (finalOne)
        {
            timer = 0;
            while (timer < 1)
            {
                volume.weight = timer;
                timer += Time.deltaTime / time;
                yield return null;
            }
            volume.weight = 1;
        }
        else
        {
            timer = 1;
            while (timer > 0)
            {
                volume.weight = timer;
                timer -= Time.deltaTime / time;
                yield return null;
            }
            volume.weight = 0;
        }
        
    }

    public IEnumerator FadeModule_Image(Image i_image, float i_Alpha_Initial, float i_Alpha_Final, float i_Time)
    {
        i_image.gameObject.SetActive(true);
        float miniTimer = 0f;
        float newAlpha = i_Alpha_Initial;
        while (miniTimer < i_Time)
        {
            newAlpha = Mathf.Lerp(i_Alpha_Initial, i_Alpha_Final, miniTimer / i_Time);
            i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, newAlpha);
            yield return null;
            miniTimer += Time.deltaTime;
        }
        i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, i_Alpha_Final);
        yield return null;
    }

    public IEnumerator FadeModule_Image(Image i_image, float i_Alpha_Initial, float i_Alpha_Final, float i_Time, bool afterActive)
    {
        i_image.gameObject.SetActive(true);
        float miniTimer = 0f;
        float newAlpha = i_Alpha_Initial;
        while (miniTimer < i_Time)
        {
            newAlpha = Mathf.Lerp(i_Alpha_Initial, i_Alpha_Final, miniTimer / i_Time);
            i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, newAlpha);
            yield return null;
            miniTimer += Time.deltaTime;
        }
        i_image.color = new Color(i_image.color.r, i_image.color.g, i_image.color.b, i_Alpha_Final);
        yield return null;
        i_image.gameObject.SetActive(afterActive);
    }

    public IEnumerator FadeModule_Text(Text i_Object, float i_Alpha_Initial, float i_Alpha_Final, float i_Time)
    {
        //i_Object.SetActive(true);
        //Image i_image = i_Object.GetComponent<Image>();
        float miniTimer = 0f;
        float newAlpha = i_Alpha_Initial;
        while (miniTimer < i_Time)
        {
            newAlpha = Mathf.Lerp(i_Alpha_Initial, i_Alpha_Final, miniTimer / i_Time);
            i_Object.color = new Color(i_Object.color.r, i_Object.color.g, i_Object.color.b, newAlpha);
            yield return null;
            miniTimer += Time.deltaTime;
        }
        i_Object.color = new Color(i_Object.color.r, i_Object.color.g, i_Object.color.b, i_Alpha_Final);
        yield return null;
    }

    bool moveModuleLinearRunning = false;
    bool moveModuleInterrupt = false;
    public IEnumerator MoveModule_Linear(GameObject i_Object, Vector3 i_Vector, float i_Time)
    {
        float miniTimer = 0f;
        if (moveModuleLinearRunning == true && linearMovingObj == i_Object)
        {
            moveModuleInterrupt = true;
        }
        moveModuleLinearRunning = true;
        Vector3 origin = i_Object.transform.position;
        Vector3 newVector = new Vector3(0f, 0f, 0f);
        linearMovingObj = i_Object;
        float newX = 0f, newY = 0f, newZ = 0f;
        while (miniTimer < 1)
        {
            newX = Mathf.Lerp(origin.x, i_Vector.x, miniTimer);
            newY = Mathf.Lerp(origin.y, i_Vector.y, miniTimer);
            newZ = Mathf.Lerp(origin.z, i_Vector.z, miniTimer);
            newVector = new Vector3(newX, newY, newZ);
            i_Object.transform.position = newVector;
            yield return null;
            if (moveModuleInterrupt)
            {
                moveModuleInterrupt = false;
                break;
            }
            miniTimer += Time.deltaTime/i_Time;
        }
        if (linearMovingObj == i_Object)
        {
            i_Object.transform.position = i_Vector;
        }

        yield return null;
        moveModuleLinearRunning = false;
    }

    public IEnumerator MoveModuleRect_Linear(GameObject i_Object, Vector3 i_Vector, float i_Time)
    {
        float miniTimer = 0f;
        Vector2 newVector = new Vector2(0f, 0f);
        float newX = 0f, newY = 0f;
        RectTransform rect = i_Object.GetComponent<RectTransform>();
        float oldX = rect.anchoredPosition.x;
        float oldY = rect.anchoredPosition.y;
        while (miniTimer < i_Time)
        {
            newX = Mathf.Lerp(oldX, i_Vector.x, miniTimer / i_Time);
            newY = Mathf.Lerp(oldY, i_Vector.y, miniTimer / i_Time);
            //newZ = Mathf.Lerp(rect.anchoredPosition.z, i_Vector.z, miniTimer / i_Time);
            newVector = new Vector2(newX, newY);
            rect.anchoredPosition = newVector;
            yield return null;
            miniTimer += Time.deltaTime;
        }
        rect.anchoredPosition = i_Vector;
        yield return null;
    }


    // �����̵���� (������ ������Ʈ, ��ǥ���Ͱ�, �̵��ð�, �ʱⰡ�ӿ���, �ıⰡ�ӿ���, �̵�������)
    public IEnumerator MoveModule_Accel(GameObject i_Object, Vector3 i_Vector, float i_Time, bool i_firstAccel = true, bool i_lastAccel = true, bool i_afterDestroy = false)
    {
        float miniTimer = 0f;
        float miniTimer_Accel = 0f;
        Vector3 initialVector = new Vector3(i_Object.transform.localPosition.x, i_Object.transform.localPosition.y, i_Object.transform.localPosition.z);
        Vector3 newVector = new Vector3(0f, 0f, 0f);
        float newX = 0f, newY = 0f, newZ = 0f;
        float timeDivision = 1f;
        if (i_firstAccel && i_lastAccel)
            timeDivision = 2f;
        if (i_firstAccel)
        {
            while (miniTimer < i_Time)
            {
                miniTimer_Accel = (miniTimer / i_Time) * (miniTimer / i_Time); // ((miniTimer/i_Time)^2)
                newX = Mathf.Lerp(initialVector.x, i_Vector.x, miniTimer_Accel / timeDivision);
                newY = Mathf.Lerp(initialVector.y, i_Vector.y, miniTimer_Accel / timeDivision);
                newZ = Mathf.Lerp(initialVector.z, i_Vector.z, miniTimer_Accel / timeDivision);
                newVector = new Vector3(newX, newY, newZ);
                i_Object.transform.localPosition = newVector;
                yield return null;
                miniTimer += Time.deltaTime;
            }
        }
        miniTimer = i_Time;
        if (i_lastAccel)
        {
            while (miniTimer > 0f)
            {
                miniTimer_Accel = (miniTimer / i_Time) * (miniTimer / i_Time); // ((miniTimer/i_Time)^2)
                newX = Mathf.Lerp(initialVector.x, i_Vector.x, 1f - miniTimer_Accel / timeDivision);
                newY = Mathf.Lerp(initialVector.y, i_Vector.y, 1f - miniTimer_Accel / timeDivision);
                newZ = Mathf.Lerp(initialVector.z, i_Vector.z, 1f - miniTimer_Accel / timeDivision);
                newVector = new Vector3(newX, newY, newZ);
                i_Object.transform.localPosition = newVector;
                yield return null;
                miniTimer -= Time.deltaTime;
            }
        }
        i_Object.transform.localPosition = i_Vector;
        i_Object.SetActive(!i_afterDestroy);
        yield return null;
    }

    // �ε巯�� ���Ӹ��
    public IEnumerator MoveModule_Accel2(GameObject i_Object, Vector3 i_Vector, float i_Time)
    {
        float miniTimer = 0f;
        float miniTimer_Accel = 0f;
        Vector3 newVector = new Vector3(0f, 0f, 0f);
        float newX = 0f, newY = 0f, newZ = 0f;
        while (miniTimer < i_Time)
        {
            miniTimer_Accel = (miniTimer / i_Time) * (miniTimer / i_Time); // ((miniTimer/i_Time)^2)
            newX = Mathf.Lerp(i_Object.transform.position.x, i_Vector.x, miniTimer_Accel);
            newY = Mathf.Lerp(i_Object.transform.position.y, i_Vector.y, miniTimer_Accel);
            newZ = Mathf.Lerp(i_Object.transform.position.z, i_Vector.z, miniTimer_Accel);
            newVector = new Vector3(newX, newY, newZ);
            i_Object.transform.position = newVector;
            yield return null;
            miniTimer += Time.deltaTime;
        }
        i_Object.transform.position = i_Vector;
        yield return null;
    }

    public IEnumerator AfterRunCoroutine(float t, IEnumerator i)
    {
        yield return new WaitForSeconds(t);
        StartCoroutine(i);
    }
}
