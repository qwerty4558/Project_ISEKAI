using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIAnimationTerm : MonoBehaviour
{
    [SerializeField] UISpriteAnimation uiAnimation_1;
    [SerializeField] UISpriteAnimation uiAnimation_2;
    [SerializeField] float waitSecond = 0.5f;

    public void StartEffect()
    {
        StartCoroutine(PlayanimationTerm());
    }

    IEnumerator PlayanimationTerm()
    {
        uiAnimation_1.StartEffact();
        yield return new WaitForSeconds(waitSecond);
        uiAnimation_2.StartEffact();
        yield return new WaitForSeconds(waitSecond);
    }
}
