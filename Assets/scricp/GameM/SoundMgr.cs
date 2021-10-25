using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class SoundMgr : MonoBehaviour
{
    public AudioSource bgmS;
    public static SoundMgr instance;
    [SerializeField]
    private List<AudioClip> bgso=new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> soundeff=new List<AudioClip>();
    private void Awake()
    {
        if (instance == null)
        {
            BgmSPlay(0);
            instance = this;
            DontDestroyOnLoad(instance);
            //SceneManager.sceneLoaded += OnSceanL;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void soundPlay(int id,string soundName)
    {
        GameObject name = new GameObject(soundName + "Sound");

        AudioSource audiosouse = name.AddComponent<AudioSource>();
        

        audiosouse.clip = soundeff[id];
        audiosouse.Play();

        Destroy(name, soundeff[id].length);
    }
    public void BgmSPlay(int id)
    {
        
        bgmS.clip = bgso[id];
        bgmS.loop = true;
        bgmS.volume = 0.1f;
        bgmS.Play();
       
    }
    public void BgmStop(int id)
    {
        bgmS.clip = bgso[id];
        bgmS.loop = false;
        bgmS.Stop();
    }
}
