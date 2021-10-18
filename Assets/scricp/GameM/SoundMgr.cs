using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class SoundMgr : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgmS;
    public static SoundMgr instance;
    public AudioClip[] bgso;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceanL;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceanL(Scene arg0, LoadSceneMode arg1)
    {
       for(int i = 0; i < bgso.Length; i++)
        {
            if (arg0.name == bgso[i].name)
            {
                BgmSPlay(bgso[i]);
            }
        }
    }
   
   
    public void BgSoundV(float val)
    {
        mixer.SetFloat("BgSound", Mathf.Log10(val) * 20);

    }
    public void VFXV(float val)
    {
        mixer.SetFloat("VF", Mathf.Log10(val) * 20);
    }
    public void soundPlay(string soundName, AudioClip cilp)
    {
        GameObject name = new GameObject(soundName + "Sound");

        AudioSource audiosouse = name.AddComponent<AudioSource>();
        audiosouse.outputAudioMixerGroup = mixer.FindMatchingGroups("VFS")[0];

        audiosouse.clip = cilp;
        audiosouse.Play();

        Destroy(name, cilp.length);
    }
    public void BgmSPlay(AudioClip cilp)
    {
        bgmS.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
        bgmS.clip = cilp;
        bgmS.loop = true;
        bgmS.volume = 0.1f;
        bgmS.Play();
       
    }
    public void BgmStop(AudioClip cilp)
    {
        bgmS.clip = cilp;
        bgmS.loop = false;
        bgmS.Stop();
    }
}
