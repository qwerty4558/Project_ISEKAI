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
    public AudioSource cutScenesound;

    [Space]
    [Header("SFX ����� Ŭ��")]
    [Header("�÷��̾�")]
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

    [Header("�ΰ���")]
    public AudioClip swap_Tool_SFX;
    public AudioClip ui_Popup_SFX;
    public AudioClip ui_Close_SFX;
    public AudioClip click_SFX;

    [Space]

    [Header("���� ���� ����")]
    public AudioClip select_Piece_SFX;
    public AudioClip piece_Insert_SFX;
    public AudioClip puzzle_Done_SFX;
    public AudioClip puzzle_Failed_SFX;

    [Space]

    [Header("å")]
    public AudioClip open_Book_SFX;
    public AudioClip page_Next_SFX;

    [Space]

    [Header("------------------------")]

    [Header("����")]

    [Space]
    [Header("������")]
    public AudioClip slime_Dead_SFX;

    [Space]
    [Header("���")]
    public AudioClip gobline_Attack_SFX;
    public AudioClip gobline_Dead_SFX;
    
    [Space]
    [Header("�ں�Ʈ")]
    public AudioClip cobolt_Attack_SFX;
    public AudioClip cobolt_Floor_Attack_SFX;
    public AudioClip cobolt_Dead_SFX;   




}
