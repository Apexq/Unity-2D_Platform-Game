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

    private void Start()
    {
        // AudioSource bile�enlerini kontrol et ve devre d���ysa aktif et
        if (musicSource != null && !musicSource.enabled)
        {
            musicSource.enabled = true;
        }

        if (sfxSource != null && !sfxSource.enabled)
        {
            sfxSource.enabled = true;
        }

        // M�zik �almaya ba�la
        if (musicSource != null)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
    }

    public void playSfx(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void playButtonTouch()
    {
        playSfx(buttonClickSound);
    }
}
