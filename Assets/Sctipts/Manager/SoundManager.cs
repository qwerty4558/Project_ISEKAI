using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameManager
{
    public AudioSource MusicSource;

    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
