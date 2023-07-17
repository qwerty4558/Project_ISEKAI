using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline_Play : MonoBehaviour
{
    public PlayableDirector timeline;
   public void Dellay_Play()
    {
        StartCoroutine(dellayplay());
    }
    IEnumerator dellayplay()
    {
        yield return new WaitForSeconds(5f);
        timeline.Play();
        StopAllCoroutines();
    }
}
