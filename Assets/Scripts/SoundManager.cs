using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource BackgroundMusicSource, AudioSource;
    [SerializeField] AudioClip[] RoundSounds;

    private void Start()
    {
        BackgroundMusicSource.Play();
        AddEvents();
    }

    public void PlayRoundMusic() 
    {
        AudioSource.clip = RoundSounds[DataHolder.round - 1];
        AudioSource.Play();
    }

    public void PlaySoundToConvert(AudioClip clip) 
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    private void AddEvents() 
    {
        GlobalEventManager.RoundSoundEvent.AddListener(PlayRoundMusic);
        ConvertTextToSpeach.Instance.OnSuccessfullyConvertTextToAudioAction += PlaySoundToConvert;     
    }
}
