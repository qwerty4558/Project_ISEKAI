using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameManager
{
    [Header("Scene ���� �����")]
    public AudioSource titleAudio;
    public AudioSource mainSceneSound;
    public AudioSource shopSceneSound;
    public AudioSource forest_1_SceneSound;
    public AudioSource forest_2_SceneSound;
    public AudioSource mineSceneSound;
    public AudioSource bossSceneSound;

    [Space]
    [Header("SFX ����� Ŭ��")]
    [Header("�÷��̾�")]
    public AudioClip walk_On_Grass;
    public AudioClip walk_On_Wood;
    public AudioClip pick_Axe_SFX;
    public AudioClip glove_SFX;
    public AudioClip sord_Swing_SFX;
    public AudioClip puzzle_Insert;
    public AudioClip puzzle_Done;
    [Header("------------------------")]
    [Header("����")]
    public AudioClip monsterAttack;
    
    
    
    public void SetMusicVolume(float volume)
    {
        mainSceneSound.volume = volume;
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
