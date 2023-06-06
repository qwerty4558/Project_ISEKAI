using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Complet_Item_delay : MonoBehaviour
{
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
}
 
