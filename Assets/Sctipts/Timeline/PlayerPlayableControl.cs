using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayerPlayableControl : MonoBehaviour
{
    public bool DisablePlayerOnStart = true;
    public bool DisablePlayerOnEnabled = true;
    public bool EnablePlayerOnComplete = true;
    public bool EnablePlayerOnDisabled = true;


    PlayableDirector playDir;

    private void Awake()
    {
        playDir = GetComponent<PlayableDirector>();
        playDir.played += Disableplayer;
        playDir.stopped += EnablePlayer;
    }

    public void OnEnable()
    {
        if(DisablePlayerOnEnabled)
        {
            var player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ControlEnabled = false;
            }
        }
    }

    public void OnDisable()
    {
        if (EnablePlayerOnDisabled)
        {
            var player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.ControlEnabled = true;
            }
        }
    }

    public void Disableplayer(PlayableDirector obj)
    {
        if (DisablePlayerOnStart != true) return;
        if (obj.time != 0) return;

        var player = FindObjectOfType<PlayerController>();
        if(player != null)
        {
            player.ControlEnabled = false;
        }
    }

    public void EnablePlayer(PlayableDirector obj)
    {
        if (EnablePlayerOnComplete != true) return;

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ControlEnabled = true;
        }
    }
}
