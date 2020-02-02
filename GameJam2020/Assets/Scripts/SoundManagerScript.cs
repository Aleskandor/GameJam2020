using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    private static AudioSource song0src, song1src, song2src;
    private AudioSource[] audios;

    void Start()
    {
        audios = GetComponents<AudioSource>();
        song0src = audios[0];
        song1src = audios[1];
        song2src = audios[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlaySound(0);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound(2);
        }
    }

    public static void PlaySound(int songID)
    {
        switch (songID)
        {
            case 0:
                song0src.Stop();
                song1src.Stop();
                song2src.Stop();
                //song0src.loop = true;
                song0src.Play();
                break;
            case 1:
                song0src.Stop();
                song1src.Stop();
                song2src.Stop();
                //song1src.loop = true;
                song1src.Play();
                break;
            case 2:
                song0src.Stop();
                song1src.Stop();
                song2src.Stop();
                //song2src.loop = true;
                song2src.Play();
                break;
        }       
    }
}
