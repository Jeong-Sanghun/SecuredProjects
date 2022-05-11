using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM {
    Intro, Bright,BrightChange, Dark,Memory1Base,Scene15,Scene16to19,Memory2Base,Scene45,Scene46
}

public enum SFX
{
    SoundMessageAlarm, DoorSound, BrokenSound,Siren,Koong,Gauge
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;
    
    public AudioSource bgmSource;
    
    public AudioSource effectSource;

    [SerializeField]
    AudioSource clickSource;

    [SerializeField]
    AudioClip[] bgmArray;
    [SerializeField]
    AudioClip[] effectArray;
    
    

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
    // Start is called before the first frame update
    void Start()
    {
        //bgmSource.clip = null;
    }

    public void SetBGMOnSceneStart(SceneName scene)
    {
        int sceneNum = (int)scene;
        if(sceneNum ==0 || sceneNum == 1 || sceneNum >= (int)SceneName.GameEnd)
        {
            BGMPlay(BGM.Intro);
        }
        else if(sceneNum == (int)SceneName.Bright || sceneNum == (int)SceneName.Chapter2Bright)
        {
            BGMPlay(BGM.Bright);
        }
        else if (sceneNum == (int)SceneName.Dark || sceneNum == (int)SceneName.Chapter2Dark)
        {
            BGMPlay(BGM.Dark);
        }
        else if (sceneNum == 15)
        {
            BGMPlay(BGM.Scene15);
        }
        else if (sceneNum == 16)
        {
            BGMPlay(BGM.Scene16to19);
        }
        else if (sceneNum == 45)
        {
            BGMPlay(BGM.Scene45);
        }
        else if (sceneNum == 46)
        {
            BGMPlay(BGM.Scene46);
        }
        else if (sceneNum >= (int)SceneName.MemoryHome1 && sceneNum <= (int)SceneName.MemoryBrightStreet2)
        {
            BGMPlay(BGM.Memory1Base);
        }
        else if (sceneNum >= (int)SceneName.MemoryFriendRoom1 && sceneNum <= (int)SceneName.MemoryFriendRoom9)
        {
            BGMPlay(BGM.Memory2Base);
        }
    }

    public void BGMPlay(BGM bgm)
    {
        if(bgmSource.clip!= null)
        {
            if(bgmSource.clip == bgmArray[(int)bgm])
            {
                return;
            }
        }
        
        StartCoroutine(BGMFade(bgm));
    }
    public void EffectPlay(SFX sfx)
    {
        effectSource.clip = effectArray[(int)sfx];
        effectSource.Play();
    }

    public void EffectStop()
    {
        effectSource.Stop();
    }

    public void SoundActive(bool active)
    {
        bgmSource.mute = active;
        effectSource.mute = active;
        clickSource.mute = active;
    }

    IEnumerator BGMFade(BGM bgm)
    {
        if (bgmSource.clip != null)
        {
            while (bgmSource.volume > 0)
            {
                bgmSource.volume -= Time.deltaTime;
                yield return null;
            }
        }
        
        bgmSource.volume = 0;
        bgmSource.clip = bgmArray[(int)bgm];
        bgmSource.Play();
        while (bgmSource.volume < 1)
        {
            bgmSource.volume += Time.deltaTime;
            yield return null;
        }
        
        bgmSource.volume = 1;
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        clickSource.Play();
    //    }
    //}


}
