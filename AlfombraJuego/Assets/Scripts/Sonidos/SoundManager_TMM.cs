using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager THIS;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource musicSource;

    
    public AudioClip[] sounds;
    public AudioClip musicClip;


    void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
            //DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        ///audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        musicSource.loop = true;
        musicSource.Play();
    }




    /// <summary>
    /// Reproduce un hilo musical
    /// </summary>
    /// <param name="index"> Asigna el valor del indice asociado con el array Sounds </param>
    public void PlaySoundByIndex(int index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }
}
