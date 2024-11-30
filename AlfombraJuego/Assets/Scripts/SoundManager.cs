using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioSound
    {
        public string name;
        public AudioClip clip;
    }


    [SerializeField]
    List<AudioSound> sounds;
    [SerializeField]
    List<AudioSound> music;


    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource musicSource;

    void PlaySound(string name)
    {
        audioSource.Stop();
        audioSource.resource = sounds.Find();
        audioSource.Play();
    }

    void PlaySong(int musicId)
    {
        musicSource.Stop();
        musicSource.resource = music[musicId].clip;
        musicSource.Play();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaySound(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
