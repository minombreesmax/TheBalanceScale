using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] RoundSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GlobalEventManager.RoundSoundEvent.AddListener(PlayRoundMusic);
    }

    public void PlayRoundMusic() 
    {
        audioSource.clip = RoundSounds[DataHolder.round - 1];
        audioSource.Play();
    }
}
