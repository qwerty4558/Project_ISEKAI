// ====================================
//
// Simple soundmodule for Unity
// v 1.1
//
// Public domiain dedication
// Copy or modify, commercial use are available for your project freely.
//
// Features : Easliy Play / Stop / Play Random pitch, volume or soundclips / FadeIn-Out
// 
// Notice! Only single sound could be played at once. When you play some sound while other one is playing, later one will override previous one.
// If you want to make more than two sound at once, Create enough number of Gameobjects and then apply a soundmodule each of them.
// Directly setting pitch or volume by AudioSource is not recommended.
//
// ship2042@gmail.com / @BOREALIS_FINCH (SIR_BAD_TOAST)
//
// ====================================

using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundModule : SerializedMonoBehaviour
{
    [SerializeField] private SoundItem[] soundItems;
    public SoundItem[] SoundItems { get => soundItems; }

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        try
        {
            var cur = SoundItem.GetSoundItem(soundItems, soundName);

            aud.clip = cur.audioClips[Random.Range(0, cur.audioClips.Length)];

            if (cur.randomPitch)
                aud.pitch = Random.Range(cur.pitchMin, cur.pitchMax);
            else
                aud.pitch = 1.0f;

            if (cur.randomVolume)
                aud.volume = Random.Range(cur.volumeMin, cur.volumeMax);
            else
                aud.volume = 1.0f;

            if(!aud.isPlaying)
                aud.Play();
        }
#pragma warning disable CS0168
        catch (NullReferenceException n)
        {
            Debug.LogError("SimpleSoundModule : Cannot found soundname \"" + soundName + "\" in Object \"" + gameObject.name + "\"");
        }
        catch (IndexOutOfRangeException i)
        {
            Debug.LogError("SimpleSoundModule : soundname \"" + soundName + "\" in Object \"" + gameObject.name + "\" : Audio is Empty!");
        }
#pragma warning restore CS0168
    }
    public void PlayGroup(string soundName_0, string soundName_1)
    {
        try
        {
            var cur1 = SoundItem.GetSoundItem(soundItems, soundName_0);
            var cur2 = SoundItem.GetSoundItem(soundItems, soundName_1);

            aud.clip = cur1.audioClips[Random.Range(0, cur1.audioClips.Length)];

            if (cur1.randomPitch)
                aud.pitch = Random.Range(cur1.pitchMin, cur1.pitchMax);
            else
                aud.pitch = 1.0f;

            if (cur1.randomVolume)
                aud.volume = Random.Range(cur1.volumeMin, cur1.volumeMax);
            else
                aud.volume = 1.0f;

            
            if (!aud.isPlaying)
                aud.Play();

            StartCoroutine(PlaySoundClip(cur2));
        }
#pragma warning disable CS0168
        catch (NullReferenceException n)
        {
            Debug.LogError("SimpleSoundModule : Cannot found soundname \"" + soundName_0 + "\" in Object \"" + gameObject.name + "\"");
        }
        catch (IndexOutOfRangeException i)
        {
            Debug.LogError("SimpleSoundModule : soundname \"" + soundName_0 + "\" in Object \"" + gameObject.name + "\" : Audio is Empty!");
        }
#pragma warning restore CS0168
    }

    public void PlayInteraction(string soundName_0, string soundName_1)
    {
        try
        {
            var cur1 = SoundItem.GetSoundItem(soundItems, soundName_0);
            var cur2 = SoundItem.GetSoundItem(soundItems, soundName_1);

            

            if (cur1.randomPitch)
                aud.pitch = Random.Range(cur1.pitchMin, cur1.pitchMax);
            else
                aud.pitch = 1.0f;

            if (cur1.randomVolume)
                aud.volume = Random.Range(cur1.volumeMin, cur1.volumeMax);
            else
                aud.volume = 1.0f;


            
            StartCoroutine(IPlayGroup(cur1, cur2));
        }
#pragma warning disable CS0168
        catch (NullReferenceException n)
        {
            Debug.LogError("SimpleSoundModule : Cannot found soundname \"" + soundName_0 + "\" in Object \"" + gameObject.name + "\"");
        }
        catch (IndexOutOfRangeException i)
        {
            Debug.LogError("SimpleSoundModule : soundname \"" + soundName_0 + "\" in Object \"" + gameObject.name + "\" : Audio is Empty!");
        }
#pragma warning restore CS0168
    }

    private IEnumerator IPlayGroup(SoundItem s1, SoundItem s2)
    {
        aud.clip = s1.audioClips[Random.Range(0, s1.audioClips.Length)];
        aud.Play();

        yield return new WaitWhile(() => aud.isPlaying);

        if (s2.randomPitch)
            aud.pitch = Random.Range(s2.pitchMin, s2.pitchMax);
        else
            aud.pitch = 1.0f;

        if (s2.randomVolume)
            aud.volume = Random.Range(s2.volumeMin, s2.volumeMax);
        else
            aud.volume = 1.0f;

        aud.clip = s2.audioClips[Random.Range(0, s2.audioClips.Length)];
        aud.Play();
    }


    private IEnumerator PlaySoundClip(SoundItem soundItem)
    {
        yield return new WaitUntil(() => !aud.isPlaying);

        aud.clip = soundItem.audioClips[Random.Range(0, soundItem.audioClips.Length)];

        if (soundItem.randomPitch)      
            aud.pitch = Random.Range(soundItem.pitchMin, soundItem.pitchMax);
        
        else aud.pitch = 1.0f;

        if (soundItem.randomVolume) 
            aud.volume = Random.Range(soundItem.volumeMin, soundItem.volumeMax);

        else aud.volume = 1.0f;

        if(!aud.isPlaying) 
            aud.Play();
    }

    public void Stop()
    {
        aud.Stop();
    }

    public void FadeIn(string soundName, float time)
    {
        StopAllCoroutines();
        Play(soundName);
        StartCoroutine(Cor_FadeIn(aud.volume, time));
    }

    public void FadeOut(float time)
    {
        if (aud.volume == 0f) return;

        StopAllCoroutines();
        StartCoroutine(Cor_FadeOut(aud.volume, time));
    }

    private IEnumerator Cor_FadeIn(float maxVol, float time)
    {
        aud.volume = 0f;
        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            aud.volume = i * maxVol;
            yield return null;
        }

        aud.volume = maxVol;
    }

    private IEnumerator Cor_FadeOut(float currentVol, float time)
    {
        for (float i = 1; i > 0; i -= Time.deltaTime / time)
        {
            aud.volume = i * currentVol;
            yield return null;
        }
        aud.volume = 0f;
    }
}

[Serializable]
public class SoundItem
{
    public string name;
    public AudioClip[] audioClips;


    [FoldoutGroup("Additional Settings")]
    public bool randomPitch;
    [Range(-3f, 3f), FoldoutGroup("Additional Settings")]
    public float pitchMin = 1.0f;
    [Range(-3f, 3f), FoldoutGroup("Additional Settings")]
    public float pitchMax = 1.0f;

    [FoldoutGroup("Additional Settings")]
    public bool randomVolume;
    [Range(0f, 2f), FoldoutGroup("Additional Settings")]
    public float volumeMin = 2.0f;
    [Range(0f, 2f), FoldoutGroup("Additional Settings")]
    public float volumeMax = 2.0f;

    public static SoundItem GetSoundItem(SoundItem[] soundItems, string key)
    {
        return Array.Find(soundItems, i => i.name == key);
    }
}