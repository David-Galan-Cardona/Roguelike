using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip music;
    public AudioClip playerDamage;
    public AudioClip enemyDamage;
    public AudioClip doorOpen;

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
