using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;//the array list of Sound script which will contain audio files
    [HideInInspector] public bool mute = false;
    private static AudioManager instance;
    public static AudioManager Instance
    {
      get
        {
            if (instance == null)
            {
                Debug.LogWarning("There is no PlayerPrefs in the hieararchy");
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
            s.source.loop = s.loop;
        }
        Play("guitar");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void Mute()
    {
        if (mute)
        {
            //Play("backgroundMusic");
        }

        else
        {
            foreach (Sound s in sounds)
            {
                s.source.Pause();
            }
        }
    }

}
