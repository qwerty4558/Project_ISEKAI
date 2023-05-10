using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameManager
{
    [Header("Scene 관련 오디오")]
    public AudioSource titleAudio;
    public AudioSource mainSceneSound;
    public AudioSource shopSceneSound;
    public AudioSource forest_1_SceneSound;    
    public AudioSource forest_2_SceneSound;
    public AudioSource mineSceneSound;
    public AudioSource bossSceneSound;
    public AudioSource cutScenesound;

    [Space]
    [Header("SFX 오디오 클립")]
    [Header("플레이어")]
    public AudioClip walk_General_SFX;
    public AudioClip walk_On_Grass_SFX;
    public AudioClip walk_On_Wood_SFX;
    public AudioClip pick_Axe_SFX;
    public AudioClip glove_SFX;
    public AudioClip[] sord_Swing_SFX;
    public AudioClip pickup_Item_SFX;

    public AudioClip hit_by_Monster_SFX;
    public AudioClip player_Dead_SFX;
    [Space]
    [Header("------------------------")]
    [Header("UI")]
    [Space]

    [Header("인게임")]
    public AudioClip swap_Tool_SFX;
    public AudioClip ui_Popup_SFX;
    public AudioClip ui_Close_SFX;
    public AudioClip click_SFX;

    [Space]

    [Header("공방 제작 관련")]
    public AudioClip select_Piece_SFX;
    public AudioClip piece_Insert_SFX;
    public AudioClip puzzle_Done_SFX;
    public AudioClip puzzle_Failed_SFX;

    [Space]

    [Header("책")]
    public AudioClip open_Book_SFX;
    public AudioClip page_Next_SFX;

    [Space]

    [Header("------------------------")]

    [Header("몬스터")]

    [Space]
    [Header("슬라임")]
    public AudioClip slime_Dead_SFX;

    [Space]
    [Header("고블린")]
    public AudioClip gobline_Attack_SFX;
    public AudioClip gobline_Dead_SFX;
    
    [Space]
    [Header("코볼트")]
    public AudioClip cobolt_Attack_SFX;
    public AudioClip cobolt_Floor_Attack_SFX;
    public AudioClip cobolt_Dead_SFX;   




}
