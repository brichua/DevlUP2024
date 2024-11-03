using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ritual_Music_Controller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fluteMusic;
    public AudioClip ominousMusic;
    public AudioClip horrorMusic;
    public static int setMood;

    void Start()
    {
        setMood = Hearth_Stats.mood;
        SetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (setMood != Hearth_Stats.mood)
        {
            Debug.Log("setMood is " + setMood + ", and HearthStats.Mood is " + Hearth_Stats.mood);
            setMood = Hearth_Stats.mood;
            Debug.Log("setMood is " + setMood + ", and HearthStats.Mood is " + Hearth_Stats.mood);
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
        if (setMood == 4) { audioSource.clip = fluteMusic; PlayMusic(); }
        else if (setMood == 3) { audioSource.clip = ominousMusic; PlayMusic(); }
        else if (setMood == 2) { audioSource.clip = horrorMusic; PlayMusic(); }
    }
}
