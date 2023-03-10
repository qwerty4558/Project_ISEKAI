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
        Debug.Assert(_fadeRoutine == null, "�̹� �������� FadeRoutine �� �����Ѵ�. ��������.");

        _fadeRoutine = StartCoroutine(FadeRoutine(aFrom, aTo, duration));
    }

    public IEnumerator FadeRoutine(float aFrom, float aTo, float duration)
    {
        float accTime = 0f;

        while (accTime < duration)
        {
            float a = Mathf.Lerp(aFrom, aTo, accTime / duration);
            faderImage.color = new Color(0f, 0f, 0f, a);

            //  * ��쿡 ���� unscaled delta time ���� ���� �� ������ �ִ�. (�Ǵ� �ΰ� �� �ִ���)
            accTime += Time.deltaTime;
            yield return null;
        }

        _fadeRoutine = null;
    }
}
