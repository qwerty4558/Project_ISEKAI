using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Complet_Item_delay : MonoBehaviour
{
    public AudioSource PuzSFX;
    public void delay()
    {
        StartCoroutine(img_delay());
    }
    IEnumerator img_delay()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponent<DOTweenAnimation>().DOPlayBackwards();
        StopAllCoroutines();
    }

    public void Sound_Delay()
    {
        StartCoroutine(SFX_delay());
    }
    IEnumerator SFX_delay()
    {
        AudioSource audio = GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.1f);
        audio.Play();
        StopAllCoroutines();
    }
}
 
