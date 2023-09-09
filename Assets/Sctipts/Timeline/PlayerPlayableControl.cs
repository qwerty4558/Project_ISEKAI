using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class PlayerPlayableControl : MonoBehaviour
{
    public bool DisablePlayerOnStart = true;
    public bool DisablePlayerOnEnabled = true;
    public bool EnablePlayerOnComplete = true;
    public bool EnablePlayerOnDisabled = true;
    public bool SkipEnabled = true;
    PlayableDirector playDir;

    public void OnEnable()
    {
        playDir = GetComponent<PlayableDirector>();
        playDir.played += Disableplayer;
        playDir.stopped += EnablePlayer;

        if (DisablePlayerOnEnabled)
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

        playDir.played -= Disableplayer;
        playDir.stopped -= EnablePlayer;
    }

    public void Disableplayer(PlayableDirector obj)
    {
        if (SkipEnabled)
        {
            var skip = FindObjectOfType<SkipTimeline>();
            if (skip != null) skip.ToggleSkip(true);
        }

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
        if (SkipEnabled)
        {
            var skip = FindObjectOfType<SkipTimeline>();
            if (skip != null) skip.ToggleSkip(false);
        }

        if (EnablePlayerOnComplete != true) return;

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ControlEnabled = true;
        }
    }

    public void SkipCutscene()
    {
        if (!SkipEnabled) return;
        playDir.time = playDir.duration;
        playDir.Evaluate();
        playDir.Stop();
    }
}
