using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ritual_Music_Controller : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource fluteMusic;
    public AudioSource ominousMusic;
    public AudioSource horrorMusic;
    public static int setMood;

    void Start()
    {
        fluteMusic = GetComponent<AudioSource>();
        ominousMusic = GetComponent<AudioSource>();
        horrorMusic = GetComponent<AudioSource>();
        setMood = Hearth_Stats.mood;
        SetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (setMood != Hearth_Stats.mood)
        {
            setMood = Hearth_Stats.mood;
            SetMusic();
        }
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetMusic()
    {
        StopMusic();
        if (setMood == 4) { audioSource = fluteMusic; PlayMusic(); }
        else if (setMood == 3) { audioSource = ominousMusic; PlayMusic(); }
        else if (setMood == 2) { audioSource = horrorMusic; PlayMusic(); }
    }
}
