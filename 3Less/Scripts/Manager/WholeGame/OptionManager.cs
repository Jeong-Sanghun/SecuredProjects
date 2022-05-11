using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager singleton;
    

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    GameObject optionParent;
    [SerializeField]
    GameObject creditParent;
    [SerializeField]
    Sprite soundOnSprite;
    [SerializeField]
    Sprite soundOffSprite;
    [SerializeField]
    Image soundButtonImage;
    
    bool nowSoundOn;
    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Mute"))
        {
            PlayerPrefs.SetInt("Mute", 1);
        }

        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            soundManager.SoundActive(false);
            nowSoundOn = false;
            soundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            soundManager.SoundActive(true);
            nowSoundOn = true;
            soundButtonImage.sprite = soundOnSprite;
        }
    }

    public void SoundButtonToggle()
    {
        if (nowSoundOn)
        {
            soundManager.SoundActive(false);
            PlayerPrefs.SetInt("Mute", 1);
            nowSoundOn = false;
            soundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            soundManager.SoundActive(true);
            PlayerPrefs.SetInt("Mute", 0);
            nowSoundOn = true;
            soundButtonImage.sprite = soundOnSprite;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CreditActive(bool active)
    {
        OptionParentActive(!active);
        creditParent.SetActive(active);
    }

    public void OptionParentActive(bool active)
    {
        
        optionParent.SetActive(active);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
