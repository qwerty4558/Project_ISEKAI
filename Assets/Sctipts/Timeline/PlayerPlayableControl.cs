using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayerPlayableControl : MonoBehaviour
{
    public bool DisablePlayerOnStart = true;
    public bool EnablePlayerOnComplete = true;
}
