using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM,Effect;
    public List<MyAudio> myAudios = new List<MyAudio>();
    public AudioManager()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayEffectAudio("Click");
        }
    }

    public void PlayEffectAudio(string key,bool isLong = false)
    {
        AudioClip clip = null;
        foreach (MyAudio myAudio in myAudios)
        {
            if (myAudio.key == key)
            {
                clip = myAudio.audioClip;
            }
        }
        if(clip == null)
        {
            Debug.LogError("未找到音频对应的key");
            return;
        }

        if (isLong)
        {
            Effect.clip = clip;
            Effect.Play();
        }
        else
        {
            Effect.PlayOneShot(clip);
        }

    }

    public void StopEffectAudio()
    {
        Effect.Stop();
    }
    [Serializable]
    public struct MyAudio
    {
        public string key;
        public AudioClip audioClip;
    }
}
