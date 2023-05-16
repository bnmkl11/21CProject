using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    private Dictionary<kSFX, AudioClip> m_DicSound = new Dictionary<kSFX, AudioClip>();
    private Dictionary<kBGM, AudioClip> m_DicBGM = new Dictionary<kBGM, AudioClip>();

    private AudioSource m_BGMAudioSource;

    private AudioSource m_SFXAudioSource;

    private kBGM m_currentPlayBGM = kBGM.None;

    public override void InitManager()
    {
        m_BGMAudioSource = Common.GetOrAddComponent<AudioSource>(gameObject);
        m_SFXAudioSource = Common.GetOrAddComponent<AudioSource>(gameObject);

        AudioClip[] bgms = Resources.LoadAll<AudioClip>("Sounds/BGM");

        for (int i = 0; i < bgms.Length; i++)
        {
            string soundName = bgms[i].name;
            kBGM bgm = (kBGM)Enum.Parse(typeof(kBGM), soundName);
#if UNITY_EDITOR
            //Debug.Log(bgms[i].name + "Load Success");
#endif
            m_DicBGM.Add(bgm, bgms[i]);
        }

        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds/SFX");

        for (int i = 0; i < sounds.Length; i++)
        {
            string soundName = sounds[i].name;
            kSFX sfx = (kSFX)Enum.Parse(typeof(kSFX), soundName);
#if UNITY_EDITOR
            //Debug.Log(sounds[i].name + "Load Success");
#endif
            m_DicSound.Add(sfx, sounds[i]);
        }

        return;
    }

    public void StopBGM()
    {
        m_BGMAudioSource.Stop();
        m_SFXAudioSource.Stop();
        m_currentPlayBGM = kBGM.None;
    }

    public void SetBGMVolume(float volume)
    {
        m_BGMAudioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        m_SFXAudioSource.volume = volume;
    }

    public void PlayBGM(kBGM bgm)
    {
        if (m_currentPlayBGM == bgm)
            return;

        AudioClip clip;
        if (m_DicBGM.TryGetValue(bgm, out clip))
        {
            m_BGMAudioSource.loop = true;
            m_BGMAudioSource.clip = clip;
            m_BGMAudioSource.Play();
            m_currentPlayBGM = bgm;
        }
        else
        {
            StopBGM();
        }
    }

    public void PlaySFX(kSFX sfx)
    {
        AudioClip clip;
        if (m_DicSound.TryGetValue(sfx, out clip))
        {
            m_SFXAudioSource.PlayOneShot(clip);
        }
    }
}