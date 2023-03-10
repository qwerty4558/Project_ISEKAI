using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class FrameWork : MonoBehaviour
{
    [Header("Framework Fader")]
    public Canvas faderCanvas;
    public Image faderImage;

    Coroutine _fadeRoutine;

    public void StartFadeRoutine(float aFrom, float aTo, float duration)
    {
        Debug.Assert(_fadeRoutine == null, "이미 실행중인 FadeRoutine 이 존재한다. 로직에러.");

        _fadeRoutine = StartCoroutine(FadeRoutine(aFrom, aTo, duration));
    }

    public IEnumerator FadeRoutine(float aFrom, float aTo, float duration)
    {
        float accTime = 0f;

        while (accTime < duration)
        {
            float a = Mathf.Lerp(aFrom, aTo, accTime / duration);
            faderImage.color = new Color(0f, 0f, 0f, a);

            //  * 경우에 따라 unscaled delta time 으로 변경 될 여지가 있다. (또는 두개 다 있던가)
            accTime += Time.deltaTime;
            yield return null;
        }

        _fadeRoutine = null;
    }
}
