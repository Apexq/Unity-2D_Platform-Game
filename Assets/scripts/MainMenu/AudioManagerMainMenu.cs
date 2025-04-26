using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---------Audio Clips---------")]
    public AudioClip mainMenuMusic;
    public AudioClip buttonClickSound;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }
    public void playSfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void playButtonTouch()
    {
        playSfx(buttonClickSound);
    }




}
